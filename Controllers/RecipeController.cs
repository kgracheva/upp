using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using upp.Dtos.Article;
using upp.Dtos.Recipe;
using upp.Mapper;
using upp.Services;

namespace upp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly RecipeService _recipeService;
        public RecipeController(RecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateRecipe([FromBody] RecipeDto dto, CancellationToken token)
        {
            return Ok(await _recipeService.CreateRecipe(dto, token));
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedList<RecipeDto>>> GetRecipes([FromQuery] FindRecipesDto dto, CancellationToken token)
        {
            return Ok(await _recipeService.GetRecipes(dto, token));
        }

        [HttpPut]
        public async Task<ActionResult<int>> EditRecipe(RecipeDto dto, CancellationToken token)
        {
            return Ok(await _recipeService.EditRecipe(dto, token));
        }


        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<ArticleDto>> GetRecipe(int id, CancellationToken token)
        {
            return Ok(await _recipeService.GetRecipe(id, token));
        }

        [HttpPatch]
        [Route("{id}")]
        public async Task<ActionResult> DeleteRecipe(int id, CancellationToken token)
        {
            await _recipeService.Delete(id, token);
            return NoContent();
        }
    }
}
