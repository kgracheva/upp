using AutoMapper;
using upp.Dtos.Article;
using upp.Dtos.Training;
using upp.Entities;

namespace upp.Mapper
{
    public class TrainingMapper : Profile
    {
        public TrainingMapper()
        {
            CreateMap<Training, TrainingDto>()
                .ForMember(dest => dest.Blocks, opt => opt.MapFrom(x => x.TrainingBlocks.Select(ab => ab.Block)));


            CreateMap<TrainingDto, Training>()
                .ForMember(dest => dest.Created, opt => opt.Ignore())
                .ForMember(dest => dest.TrainingBlocks, opt => opt.MapFrom(x => x.Blocks));
        }
    }
}
