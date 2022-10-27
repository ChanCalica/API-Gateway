using API_Gateway.Models.Request;
using API_Gateway.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Gateway.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TableStorageController : ControllerBase
    {
        private readonly ITableStorageService _storageService;
        public TableStorageController(ITableStorageService storageService)
        {
            _storageService = storageService ?? throw new ArgumentNullException(nameof(storageService));
        }

        [HttpGet]
        [ActionName(nameof(GetAsync))]
        public async Task<IActionResult> GetAsync([FromQuery] string PartitionKey, string RowKey)
        {
            return Ok(await _storageService.GetEntityAsync(PartitionKey, RowKey));
        }
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] MemberEntityRequest entity)
        {
            entity.PartitionKey = entity.PartitionKey;

            string Id = Guid.NewGuid().ToString();
            entity.RowKey = Id;

            var createdEntity = await _storageService.UpsertEntityAsync(entity);
            return CreatedAtAction(nameof(GetAsync), createdEntity);
        }
        [HttpPut]
        public async Task<IActionResult> PutAsync([FromBody] MemberEntityRequest entity)
        {
            entity.PartitionKey = entity.PartitionKey;
            entity.RowKey = entity.RowKey;
            await _storageService.UpsertEntityAsync(entity);
            return NoContent();
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync([FromQuery] string PartitionKey, string RowKey)
        {
            await _storageService.DeleteEntityAsync(PartitionKey, RowKey);
            return NoContent();
        }
    }
}
