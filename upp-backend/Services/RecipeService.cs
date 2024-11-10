using AutoMapper;
using Entities;
using Microsoft.EntityFrameworkCore;
using upp.Dtos.Article;
using upp.Dtos.Recipe;
using upp.Dtos.Training;
using upp.Entities;
using upp.Mapper;
using upp.Migrations;

namespace upp.Services
{
    public class RecipeService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public RecipeService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> CreateRecipe(RecipeDto dto, CancellationToken token)
        {
            if (dto.Id != 0)
                throw new Exception("training Id is not null!");


            var recipe = _mapper.Map<Recipe>(dto);

            //foreach(var block in dto.Blocks)
            //{
            //    _context.Blocks.Add(_mapper.Map<Block>(block));
            //    _context.ArticleBlocks.Add(new ArticleBlock() { ArticleId = article.Id, BlockId = block.Id }); 
            //}
            _context.Recipes.Add(recipe);
            await _context.SaveChangesAsync(token);
            return recipe.Id;
        }

        public async Task<PaginatedList<RecipeDto>> GetRecipes(FindRecipesDto dto, CancellationToken token)
        {
            IQueryable<Recipe> query = _context.Recipes
                .Include(p => p.RecipeBlocks)
                .ThenInclude(p => p.Block)
                .Include(p => p.Creator)
                .ThenInclude(c => c.Info);

            if (dto.SearchName != "")
            {
                query = query.Where(p => p.Name.ToUpper().Contains(dto.SearchName.ToUpper()));
            }

            if (dto.CreatorId != 0)
            {
                query = query.Where(p => dto.CreatorId == p.CreatorId);
            }

            query = query.OrderBy(p => p.Name);

            return await query.ToPaginateListAsync<Recipe, RecipeDto>(_mapper, dto.Page, dto.Size, token);
        }


        public async Task<int> EditRecipe(RecipeDto dto, CancellationToken token)
        {
            if (dto.Id == 0)
                throw new Exception("recipe Id is null!");

            var recipe = _context.Recipes.FirstOrDefault(x => x.Id == dto.Id);

            if (recipe == null)
                throw new Exception("recipe Id is null!");

            recipe = _mapper.Map(dto, recipe);

            _context.Recipes.Update(recipe);

            await _context.SaveChangesAsync(token);

            return recipe.Id;
        }

        public async Task<RecipeDto> GetRecipe(int id, CancellationToken token)
        {
            var recipe = await _context.Recipes.FirstOrDefaultAsync(x => x.Id == id, token);

            if (recipe == null)
            {
                throw new Exception("recipe is null");
            }

            return _mapper.Map<Recipe, RecipeDto>(recipe);
        }

        public async Task Delete(int id, CancellationToken token)
        {
            var recipe = await _context.Recipes
                .FirstOrDefaultAsync(g => g.Id == id, token);

            if (recipe == null) throw new Exception("training is null");

            recipe.IsDeleted = true;
            _context.Recipes.Update(recipe);

            await _context.SaveChangesAsync(token);
        }
    }
}
