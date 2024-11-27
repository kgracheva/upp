using AutoMapper;
using upp.Dtos.Calendar;
using upp.Dtos.Product;
using upp.Entities;

namespace upp.Mapper
{
    public class CalendarMapper : Profile
    {
        public CalendarMapper()
        {
            CreateMap<Calendar, CalendarDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(x => x.Product.Name));
            CreateMap<CalendarDto, Calendar>()
                .ForMember(dest => dest.Created, opt => opt.Ignore());
        }
    }
}
