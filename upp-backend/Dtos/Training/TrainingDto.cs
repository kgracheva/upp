using upp.Dtos.Article;
using upp.Entities;

namespace upp.Dtos.Training
{
    public class TrainingDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int StatusTypeId { get; set; }
        public int CreatorId { get; set; }
        public string VideoRef { get; set; }
        public bool IsDeleted { get; set; }
        public List<BlockDto> Blocks { get; set; } = new List<BlockDto>();
    }
}
