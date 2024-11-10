using upp.Dtos.Article;
using upp.Dtos.Recipe;
using upp.Dtos.Training;

namespace upp.Dtos.Request
{
    public class CreateRequestDto
    {
        public ArticleDto? Article { get; set; }
        public RecipeDto? Recipe { get; set; }
        public TrainingDto? Training { get; set; }
    }
}
