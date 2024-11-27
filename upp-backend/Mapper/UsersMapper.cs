using AutoMapper;
using Entities;
using upp.Dtos.Article;
using upp.Dtos.Recipe;
using upp.Dtos.User;
using upp.Entities;

namespace upp.Mapper
{
    public class UsersMapper : Profile
    {
        public UsersMapper()
        {
            CreateMap<User, PhyschologistDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(x => x.Info.Lastname + " " + x.Info.Name));
        }
    }
}
