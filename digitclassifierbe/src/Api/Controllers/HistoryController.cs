using Api.Attributes;
using Application.Models.History;
using Application.Services;
using DataAcces.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/history")]
    [ApiController]
    public class HistoryController : ControllerBase
    {
        private readonly IHistoryService _historyService;

        public HistoryController(IHistoryService historyService)
        {
            _historyService = historyService;
        }

        [HttpGet]
        [Authorize]
        public async Task<HistoryResponseFull> GetHistory([FromQuery] HistoryRequest request)
        {
            User user = (User) HttpContext.Items["User"];
            return await _historyService.GetHistoryForUserAsync(user, request);
        }

        [HttpDelete]
        [Authorize]
        public async Task<HistoryResponse> DeleteHistory([FromQuery] string requestId)
        {
            return await _historyService.DeleteHistoryEntry(Guid.Parse(requestId));
        }
    }
}
