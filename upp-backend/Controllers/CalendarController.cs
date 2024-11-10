using Microsoft.AspNetCore.Mvc;
using Services;
using upp.Dtos.Calendar;
using upp.Dtos.Product;
using upp.Dtos.User;
using upp.Mapper;
using upp.Services;

namespace upp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalendarController : ControllerBase
    {
        private readonly CalendarService _calendarService;
        public CalendarController(CalendarService calendarService)
        {
            _calendarService = calendarService;
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateCalendar([FromBody] CalendarDto dto, CancellationToken token)
        {
            return Ok(await _calendarService.CreateCalendar(dto, token));
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedList<ProductDto>>> GetCalendars([FromQuery] FindCalendarsDto dto, CancellationToken token)
        {
            return Ok(await _calendarService.GetCalendars(dto, token));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<CalendarDto>> GetCalendar(int id, CancellationToken token)
        {
            return Ok(await _calendarService.GetCalendar(id, token));
        }

        [HttpPut]
        public async Task<ActionResult<int>> EditCalendar([FromBody] CalendarDto dto, CancellationToken token)
        {
            return Ok(await _calendarService.EditCalendar(dto, token));
        }

        [HttpPatch]
        [Route("{id}")]
        public async Task<ActionResult> DeleteCalendar(int id, CancellationToken token)
        {
            await _calendarService.Delete(id, token);
            return NoContent();
        }

        [HttpPost]
        [Route("count-day")]
        public async Task<ActionResult<DateCaloriesDto>> CountDayCalories([FromBody] CaloriesDto dto, CancellationToken token)
        {
            return Ok(await _calendarService.CountDayCalories(dto, token));
        }
    }
}
