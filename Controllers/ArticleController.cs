using Microsoft.AspNetCore.Mvc;
using upp.Dtos.Article;
using upp.Dtos.Calendar;
using upp.Mapper;
using upp.Services;

namespace upp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly ArticleService _articleService;
        public ArticleController(ArticleService articleService)
        {
            _articleService = articleService;
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateArticle([FromBody] ArticleDto dto, CancellationToken token)
        {
            return Ok(await _articleService.CreateArticle(dto, token));
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedList<ArticleDto>>> GetArticles([FromQuery] FindAtriclesDto dto, CancellationToken token)
        {
            return Ok(await _articleService.GetArticles(dto, token));
        }

        [HttpPut]
        public async Task<ActionResult<int>> EditArticle(ArticleDto dto, CancellationToken token)
        {
            return Ok(await _articleService.EditArticle(dto, token));
        }


        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<ArticleDto>> GetArticle(int id, CancellationToken token)
        {
            return Ok(await _articleService.GetArticle(id, token));
        }

        [HttpPatch]
        [Route("{id}")]
        public async Task<ActionResult> DeleteArticle(int id, CancellationToken token)
        {
            await _articleService.Delete(id, token);
            return NoContent();
        }
    }
}
