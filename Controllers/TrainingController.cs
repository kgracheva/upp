using Microsoft.AspNetCore.Mvc;
using upp.Dtos.Article;
using upp.Dtos.Training;
using upp.Mapper;
using upp.Services;

namespace upp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingController : ControllerBase
    {
        private readonly TrainingService _trainingService;
        public TrainingController(TrainingService trainingService)
        {
            _trainingService = trainingService;
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateTraining([FromBody] TrainingDto dto, CancellationToken token)
        {
            return Ok(await _trainingService.CreateTraining(dto, token));
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedList<TrainingDto>>> GetArticles([FromQuery] FindTrainingsDto dto, CancellationToken token)
        {
            return Ok(await _trainingService.GetTrainings(dto, token));
        }

        [HttpPut]
        public async Task<ActionResult<int>> EditTraining(TrainingDto dto, CancellationToken token)
        {
            return Ok(await _trainingService.EditTraining(dto, token));
        }


        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<TrainingDto>> GetTraining(int id, CancellationToken token)
        {
            return Ok(await _trainingService.GetTraining(id, token));
        }

        [HttpPatch]
        [Route("{id}")]
        public async Task<ActionResult> DeleteCalendar(int id, CancellationToken token)
        {
            await _trainingService.Delete(id, token);
            return NoContent();
        }
    }
}
