using API_Gateway.Models.Request;
using API_Gateway.Models.Response;
using API_Gateway.Services.Interface;
using API_Gateway.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Net;

namespace API_Gateway.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly ILoyaltyRobinsonsService _loyaltyService;
        public MemberController(ILoyaltyRobinsonsService loyaltyService)
        {
            _loyaltyService = loyaltyService;
        }

        [HttpGet("{mobile}/{query}/{queryValue}")]
        public async Task<ActionResult> GetMemberInfo(string mobile, string query, string queryValue)
        {
            var request = new MemberRequest()
            {
                mobileNumber = mobile,
                Query = query,
                QueryValue = queryValue
            };

            var member = new MemberInfoResponse();

            member = await _loyaltyService.GetMemberInfo(request);

            return Ok(member);
        }

        [HttpGet("{mobile}/filter")]
        public async Task<ActionResult> CheckMemberInfo(string mobile, [FromQuery] string? dob, string? lname, string? email, string? fname)
        {
            DateTime birthDate = DateTime.MinValue;

            if (!string.IsNullOrEmpty(dob))
            {
                DateTime.TryParse(dob, out birthDate);

                MemberValidator.ValidateBirthdate(dob);
            }

            Request.Headers.TryGetValue("Tier-Id", out var tierValue);

            var request = new CheckMemberRequest()
            {
                MobileNumber = mobile,
                BirthDate = birthDate,
                LastName = lname,
                Email = email,
                FirstName = fname
            };

            if (!string.IsNullOrEmpty(tierValue))
            {
                request.Tier = tierValue;
            }

            var result = await _loyaltyService.CheckMemberVersionThree(request);

            JsonSerializerSettings settings = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore,
                Formatting = Formatting.Indented
            };

            string response = JsonConvert.SerializeObject(result, settings);

            return Ok(response);
        }

        [HttpGet("filter")]
        public async Task<ActionResult> CheckMemberInfoUsingFilter([FromQuery] string? email, string? lname, string? dob)
        {
            DateTime birthDate = DateTime.MinValue;

            if (!string.IsNullOrEmpty(dob))
            {
                DateTime.TryParse(dob, out birthDate);
            }

            var request = new CheckMemberRequest()
            {
                Email = email,
                LastName = lname,
                BirthDate = birthDate
            };

            var result = await _loyaltyService.CheckMemberUsingQuery(request);

            JsonSerializerSettings settings = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore,
                Formatting = Formatting.Indented
            };

            string response = JsonConvert.SerializeObject(result, settings);

            return Ok(response);
        }

        [HttpGet("{mobile}")]
        public async Task<ActionResult> CheckMemberInfoWithoutFilter(string mobile)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore,
                Formatting = Formatting.Indented
            };

            Request.Headers.TryGetValue("Tier-Id", out var tierValue);

            if (mobile.Length == 16)
            {

                var result = await _loyaltyService.GetMemberInfoUsingCardNumber(mobile, tierValue);

                string response = JsonConvert.SerializeObject(result, settings);

                return Ok(response);
            }
            else
            {
                var request = new CheckMemberRequest()
                {
                    MobileNumber = mobile
                };

                if (!string.IsNullOrEmpty(tierValue))
                {
                    request.Tier = tierValue;
                }

                var result = await _loyaltyService.CheckMemberVersionThree(request);

                string response = JsonConvert.SerializeObject(result, settings);

                return Ok(response);
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateDemographics([FromBody] DemographicRequest request)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore,
                Formatting = Formatting.Indented
            };

            var result = await _loyaltyService.CreateDemographics(request);

            string response = JsonConvert.SerializeObject(result, settings);

            return Created("member/create", response);
        }

        [HttpPatch("{cardNumber}")]
        public async Task<ActionResult> UpdateDemographics(string cardNumber, [FromBody] DemographicRequest request)
        {
            Request.Headers.TryGetValue("Tier-Id", out var tierValue);

            await _loyaltyService.UpdateDemographics(request, cardNumber, tierValue);

            return Ok();
        }

        [HttpGet("{cardNumber}/points")]
        public async Task<ActionResult> GetPoints(string cardNumber)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore,
                Formatting = Formatting.Indented
            };

            var result = await _loyaltyService.GetPointsUsingCardNumber(cardNumber);

            string response = JsonConvert.SerializeObject(result, settings);

            return Ok(response);

        }

        [HttpPatch("{cardId}/points")]
        public async Task<ActionResult> AdjustPoints(string cardId, [FromBody] AdjustPointsRequest request)
        {
            request.CardId = cardId;

            Request.Headers.TryGetValue("Return-Balance", out var balanceValue);

            string balance = balanceValue;

            if (!string.IsNullOrEmpty(balance) && balance.ToLower().Equals("true"))
            {
                var result = await _loyaltyService.GetPointsUsingCardNumber(cardId);

                var prevPoints = result.Balance;

                await _loyaltyService.AdjustPoints(request);

                var newResult = await _loyaltyService.GetPointsUsingCardNumber(cardId);

                var newPoints = newResult.Balance;
                var newLastUpdated = newResult.LastUpdated;

                var response = new NewAdjustPointsResponse()
                {
                    PreviousBalance = prevPoints,
                    NewBalance = newPoints,
                    LastUpdated = newLastUpdated
                };

                return Ok(response);
            }
            else
            {
                await _loyaltyService.AdjustPoints(request);
                return Ok();
            }
        }

        [HttpGet("{cardId}/status")]
        public async Task<ActionResult> CardValidate(string cardId)
        {
            var result = await _loyaltyService.CardValidate(cardId);

            return Ok(result);
        }


    }
}
