using AutoMapper;
using Entities;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

using System.Linq;
using System.Reflection;
using upp.Dtos.Calendar;
using upp.Dtos.Product;
using upp.Dtos.User;
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
            IQueryable<Calendar> query = _context.Calendars.Include(x => x.Product);

            if (dto.UserId != 0)
            {
                query = query.Where(p => dto.UserId == p.UserId);
            }

            if(dto.Date != null)
            {
                query = query.Where(x => dto.Date.Value.Date == x.Created.Value.Date);
            }

            if(dto.MealTypeId != 0) {
                query = query.Where(x => x.MealTypeId == dto.MealTypeId);
            }

            return await query.ToPaginateListAsync<Entities.Calendar, CalendarDto>(_mapper, dto.Page, dto.Size, token);
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
                .Include(x => x.User)
                .ThenInclude(x => x.Info)
                .Include(x => x.Product)
                .Include(x => x.MealType)
                .Where(x => x.UserId == dto.UserId && dto.Date.Value.Date == x.Created.Value.Date)
                .ToListAsync(token);

            if (calendar == null) throw new Exception("Calendar is null");

            var dc = new DateCaloriesDto();

            dc.AllCaloriesByDay = (int)_context.AdditionalInfo.FirstOrDefault(x => x.Id == dto.UserId).CaloriesCountByDay;

            try
            {
                foreach (var c in calendar)
                {
                    dc.ProteinsCount += c.Product.ProteinsCount * (c.ProductCount / 100);
                    dc.FatsCount += c.Product.FatsCount * (c.ProductCount / 100);
                    dc.CarbsCount += c.Product.CarbsCount * (c.ProductCount / 100);
                    dc.CaloriesCount += c.Product.CaloriesCount * (c.ProductCount / 100);

                    if (c.MealTypeId == 1)
                    {
                        dc.BreakfastCount += c.Product.CaloriesCount * (c.ProductCount / 100);
                    }

                    if (c.MealTypeId == 2)
                    {
                        dc.LunchCount += c.Product.CaloriesCount * (c.ProductCount / 100);
                    }

                    if (c.MealTypeId == 3)
                    {
                        dc.DinnerCount += c.Product.CaloriesCount * (c.ProductCount / 100);
                    }

                    if (c.MealTypeId == 4)
                    {
                        dc.SnackCount += c.Product.CaloriesCount * (c.ProductCount / 100);
                    }

                }
            }
            catch(Exception ex)
            {

            }
            return dc;
        }

        public async Task CreateSpecialUserData(SpecialInfoDto dto, CancellationToken token)
        {
            var info = _context.AdditionalInfo.FirstOrDefault(x => x.Id == dto.Id);

            if (info == null) throw new Exception("Info is null");

            var age = DateTime.Today.Year - info.BirthDay.Year; 

            if (dto.Sex == "Мужской")
                info.CaloriesCountByDay = Convert.ToInt32((10 * dto.Weight + 6.25 * dto.Height - 5 * age + 5) * 1.55);
            else
                info.CaloriesCountByDay = Convert.ToInt32((10 * dto.Weight + 6.25 * dto.Height - 5 * age - 161) * 1.55);

            info.Weight = dto.Weight;
            info.WorkType = dto.WorkType;
            info.DesiredWeight = dto.DesiredWeight;
            info.Height = dto.Height;
            info.Sex = dto.Sex;

            _context.AdditionalInfo.Update(info);
            await _context.SaveChangesAsync(token);
        }

        public async Task<SpecialInfoDto> GetSpecialInfo(int userId, CancellationToken token)
        {
            var info = _context.AdditionalInfo.FirstOrDefault(x => x.Id == userId);

            if (info == null) throw new Exception("Info is null");

            return _mapper.Map<AdditionalInfo, SpecialInfoDto>(info);
        }
    }
}
