using AutoMapper;
using Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using upp.Dtos.Calendar;
using upp.Dtos.Product;
using upp.Entities;
using upp.Mapper;

namespace upp.Services
{
    public class CalendarService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CalendarService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> CreateCalendar(CalendarDto dto, CancellationToken token)
        {
            if (dto.Id != 0)
                throw new Exception("calendar Id is not null!");

            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == dto.ProductId, token);
            if (product == null || product.IsDeleted)
                throw new Exception("Product is null");

            var calendar = _mapper.Map<Calendar>(dto);

            _context.Calendars.Add(calendar);
            await _context.SaveChangesAsync(token);

            return calendar.Id;
        }

        public async Task<int> EditCalendar(CalendarDto dto, CancellationToken token)
        {
            if (dto.Id == 0)
                throw new Exception("calendar Id is null!");

            var calendar = _context.Calendars.FirstOrDefault(x => x.Id == dto.Id);

            if (calendar == null)
                throw new Exception("calendar Id is null!");

            calendar = _mapper.Map(dto, calendar);

            _context.Calendars.Update(calendar);
            await _context.SaveChangesAsync(token);

            return calendar.Id;
        }

        public async Task<CalendarDto> GetCalendar(int id, CancellationToken token)
        {
            var calendar = await _context.Calendars.FirstOrDefaultAsync(x => x.Id == id, token);

            if (calendar == null)
            {
                throw new Exception("Calendar is null");
            }

            return _mapper.Map<Calendar, CalendarDto>(calendar);
        }

        public async Task<PaginatedList<CalendarDto>> GetCalendars(FindCalendarsDto dto, CancellationToken token)
        {
            IQueryable<Calendar> query = _context.Calendars;

            if (dto.UserId != 0)
            {
                query = query.Where(p => dto.UserId == p.UserId);
            }

            if(dto.Date != null)
            {
                query = query.Where(x => dto.Date.Value.Date == x.Created.Value.Date);
            }

            return await query.ToPaginateListAsync<Calendar, CalendarDto>(_mapper, dto.Page, dto.Size, token);
        }

        public async Task Delete(int id, CancellationToken token)
        {
            var calendar = await _context.Calendars
                .FirstOrDefaultAsync(g => g.Id == id, token);

            if (calendar == null) throw new Exception("Calendar is null");

            _context.Calendars.Remove(calendar);
            await _context.SaveChangesAsync(token);
        }

        public async Task<DateCaloriesDto> CountDayCalories(CaloriesDto dto, CancellationToken token)
        {
            var calendar = await _context.Calendars
                .Include(x => x.Product)
                .Include(x => x.MealType)
                .Where(x => x.UserId == dto.UserId && dto.Date.Value.Date == x.Created.Value.Date)
                .ToListAsync(token);

            if (calendar == null) throw new Exception("Calendar is null");

            var dc = new DateCaloriesDto();

            foreach(var c in calendar)
            {
                dc.ProteinsCount += c.Product.ProteinsCount * c.ProductCount;
                dc.FatsCount += c.Product.FatsCount * c.ProductCount;
                dc.CarbsCount += c.Product.CarbsCount * c.ProductCount;
                dc.CaloriesCount += c.Product.CaloriesCount * c.ProductCount;
            }

            return dc;
        }
    }
}
