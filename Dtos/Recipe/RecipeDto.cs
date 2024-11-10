using upp.Dtos.Article;
using upp.Entities;

namespace upp.Dtos.Recipe
{
    public class RecipeDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public int ProteinsCount { get; set; }
        public int FatsCount { get; set; }
        public int CarbsCount { get; set; }
        public int CaloriesCount { get; set; }
        public int CreatorId { get; set; }
        public bool IsDeleted { get; set; } = false;
        public int StatusTypeId { get; set; }
        public List<BlockDto>? Blocks { get; set; } = new List<BlockDto>();
    }
}
