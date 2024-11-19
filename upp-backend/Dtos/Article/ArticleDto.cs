using upp.Entities;

namespace upp.Dtos.Article
{
    public class ArticleDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CreatorId { get; set; }
        public string CreatorName { get; set; } = "";
        public int StatusTypeId { get; set; }
        public List<BlockDto> Blocks { get; set; } = new List<BlockDto>();
    }
}
