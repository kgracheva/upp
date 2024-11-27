using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using upp.Dtos.Article;
using upp.Dtos.Request;
using upp.Mapper;
using upp.Services;

namespace upp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly RequestService _requestService;
        public RequestController(RequestService requestService)
        {
            _requestService = requestService;
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateRequest([FromBody] CreateRequestDto dto, CancellationToken token)
        {
            return Ok(await _requestService.CreateRequest(dto, token));
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedList<RequestDto>>> GetRequests([FromQuery] FindRequestsDto dto, CancellationToken token)
        {
            return Ok(await _requestService.GetRequests(dto, token));
        }

        [HttpPut]
        public async Task<ActionResult<int>> EditRequest(RequestDto dto, CancellationToken token)
        {
            return Ok(await _requestService.EditRequest(dto, token));
        }


        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<CreateRequestDto>> GetRequest(int id, CancellationToken token)
        {
            return Ok(await _requestService.GetRequest(id, token));
        }

        [HttpPatch]
        [Route("{id}")]
        public async Task<ActionResult> DeleteRequest(int id, CancellationToken token)
        {
            await _requestService.Delete(id, token);
            return NoContent();
        }

        [HttpPut]
        [Route("status")]
        public async Task<ActionResult> ChangeRequestStatus(ChangeRequestDto dto, CancellationToken token)
        {
            await _requestService.ChangeRequestStatus(dto, token);
            return NoContent();
        }

        [HttpGet]
        [Route("info/{id}")]
        public async Task<ActionResult<RequestDto>> GetRequestInfo(int id, CancellationToken token)
        {
            return Ok(await _requestService.GetRequestInfo(id, token));
        }
    }
}
