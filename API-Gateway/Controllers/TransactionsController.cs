using API_Gateway.Models.Request;
using API_Gateway.Models.Response;
using API_Gateway.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Gateway.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ILoyaltyRobinsonsService _loyaltyService;
        public TransactionsController(ILoyaltyRobinsonsService loyaltyService)
        {
            _loyaltyService = loyaltyService;
        }

        [HttpGet("member/{cardId}/filter")]
        public async Task<ActionResult> GetMemberFilteredTransactions(string cardId, [FromQuery] string? start, string? end, string? limit)
        {
            var request = new TransactionsRequest()
            {
                StartDate = start,
                EndDate = end,
                MaxRows = limit
            };

            var response = await _loyaltyService.GetMemberFilteredTransactions(request, cardId);

            return Ok(response);
        }

        [HttpGet("member/{cardId}")]
        public async Task<ActionResult> GetMemberFilteredTransactionsWithoutFilter(string cardId)
        {
            string dateToday = DateTime.Now.ToString("yyyy-MM-dd");

            var request = new TransactionsRequest()
            {
                StartDate = dateToday,
                EndDate = dateToday
                
            };

            var response = await _loyaltyService.GetMemberFilteredTransactions(request, cardId);

            return Ok(response);
        }

        [HttpGet("member/{cardId}/detailed/filter")]
        public async Task<ActionResult> GetMemberAccountDetailedTransactions(string cardId, [FromQuery] int accountId, string? start, string? end, int limit = 0)
        {
            AccountDetailedRequest request = new AccountDetailedRequest();

            request.FilteredAccountDetailedTransactions.Member.ClubCardId = cardId;
            request.FilteredAccountDetailedTransactions.Member.Account.Id = accountId;
            request.FilteredAccountDetailedTransactions.SearchCriteria.TransactionDates.StartDate = start;
            request.FilteredAccountDetailedTransactions.SearchCriteria.TransactionDates.EndDate = end;

            var response = await _loyaltyService.GetMemberAccountTransactions(request, limit);

            return Ok(response);
        }

        [HttpGet("member/{cardId}/detailed")]
        public async Task<ActionResult> GetMemberAccountDetailedTransactionsWithoutFilter(string cardId, [FromQuery] int accountId, string? start, string? end, int limit)
        {
            string dateToday = DateTime.Now.ToString("yyyy-MM-dd");

            AccountDetailedRequest request = new AccountDetailedRequest();

            request.FilteredAccountDetailedTransactions.Member.ClubCardId = cardId;
            request.FilteredAccountDetailedTransactions.Member.Account.Id = accountId;
            request.FilteredAccountDetailedTransactions.SearchCriteria.TransactionDates.StartDate = dateToday;
            request.FilteredAccountDetailedTransactions.SearchCriteria.TransactionDates.EndDate = dateToday;

            var response = await _loyaltyService.GetMemberAccountTransactions(request, limit);

            return Ok(response);
        }

        [HttpPatch("{retailId}")]
        public async Task<ActionResult> BatchPoints(string retailId, [FromBody] BatchPointsRequest request)
        {
            Request.Query.TryGetValue("cid", out var cidValue);

            await _loyaltyService.BatchPoints(request, retailId);

            var response = new AcceptedResponse()
            {
                StatusId = cidValue
            };

            return Accepted(response);
        }

    }

}
