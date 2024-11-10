using AutoMapper;
using upp.Dtos.Article;
using upp.Dtos.Recipe;
using upp.Entities;

namespace upp.Mapper
{
    public class RecipeMapper : Profile
    {
        public RecipeMapper()
        {
            CreateMap<Recipe, RecipeDto>()
                .ForMember(dest => dest.Blocks, opt => opt.MapFrom(x => x.RecipeBlocks.Select(ab => ab.Block)));


            CreateMap<RecipeDto, Recipe>()
                .ForMember(dest => dest.Created, opt => opt.Ignore())
                .ForMember(dest => dest.RecipeBlocks, opt => opt.MapFrom(x => x.Blocks));
        }
    }
}
