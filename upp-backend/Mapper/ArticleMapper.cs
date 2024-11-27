using AutoMapper;
using upp.Dtos.Article;
using upp.Dtos.Calendar;
using upp.Entities;

namespace upp.Mapper
{
    public class ArticleMapper : Profile
    {
        public ArticleMapper()
        {
            CreateMap<Article, Article>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatorId, opt => opt.Ignore())
                .ForMember(dest => dest.StatusTypeId, opt => opt.Ignore());
                
            CreateMap<Article, ArticleDto>()
                .ForMember(dest => dest.CreatorName, opt => opt.MapFrom(x => x.Creator.Info.Lastname + " " + x.Creator.Info.Name))
                .ForMember(dest => dest.Blocks, opt => opt.MapFrom(x => x.ArticleBlocks.Select(ab => ab.Block)));


            CreateMap<ArticleDto, Article>()
                .ForMember(dest => dest.Created, opt => opt.Ignore())
                .ForMember(dest => dest.ArticleBlocks, opt => opt.MapFrom(x => x.Blocks));
        }
    }
}
