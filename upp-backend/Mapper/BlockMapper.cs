using AutoMapper;
using upp.Dtos.Article;
using upp.Entities;

namespace upp.Mapper
{
    public class BlockMapper : Profile
    {
        public BlockMapper()
        {
            CreateMap<Block, BlockDto>();
            CreateMap<BlockDto, Block>()
                .ForMember(dest => dest.Created, opt => opt.Ignore());

            CreateMap<BlockDto, ArticleBlock>()
                .ForMember(dest => dest.Block, opt => opt.MapFrom(x => x));

            CreateMap<BlockDto, TrainingBlock>()
                .ForMember(dest => dest.Block, opt => opt.MapFrom(x => x));
            
            CreateMap<BlockDto, RecipeBlock>()
                .ForMember(dest => dest.Block, opt => opt.MapFrom(x => x));
        }
    }
}
