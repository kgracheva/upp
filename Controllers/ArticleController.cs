using Microsoft.AspNetCore.Mvc;
using upp.Dtos.Article;
using upp.Dtos.Calendar;
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
    }
   }
