using AutoMapper;
using Entities;
using upp.Dtos.Article;
using upp.Dtos.User;
using upp.Entities;

namespace upp.Mapper
{
    public class AdditionalInfoMapper : Profile
    {
        public AdditionalInfoMapper()
        {
            CreateMap<AdditionalInfo, SpecialInfoDto>();
        }
    }
}
