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
            CreateMap<Article, ArticleDto>()
                .ForMember(dest => dest.Blocks, opt => opt.MapFrom(x => x.ArticleBlocks.Select(ab => ab.Block)));


            CreateMap<ArticleDto, Article>()
                .ForMember(dest => dest.Created, opt => opt.Ignore())
                .ForMember(dest => dest.ArticleBlocks, opt => opt.MapFrom(x => x.Blocks));
        }
    }
}
