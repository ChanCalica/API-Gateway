using API_Gateway.Models.Request;
using API_Gateway.Models.Response;
using API_Gateway.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Gateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoyaltyRobinsonsController : ControllerBase
    {
        private readonly ILoyaltyRobinsonsService _loyaltyService;

        public LoyaltyRobinsonsController(ILoyaltyRobinsonsService loyaltyService)
        {
            _loyaltyService = loyaltyService;
        }

        [HttpPost("GetMember")]
        public async Task<ActionResult> GetMember([FromBody] MemberRequest request)
        {
            var member = _loyaltyService.GetMember(request);

            return Ok(member);
        }

        [HttpPost("GetMembers")]
        public async Task<ActionResult> GetMembers([FromBody] MemberRequest request)
        {
            var member = _loyaltyService.GetMembers(request);

            return Ok(member);
        }

        [HttpPost("GetMemberInfo")]
        public async Task<ActionResult> GetMemberInfoPOST([FromBody] MemberRequest request)
        {
            var member = new MemberInfoResponse();

            member = await _loyaltyService.GetMemberInfo(request);

            return Ok(member);
        }

        [HttpGet("GetMemberInfo")]
        public async Task<ActionResult> GetMemberInfo([FromQuery] string mobile, string query, string queryValue)
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
    }
}
