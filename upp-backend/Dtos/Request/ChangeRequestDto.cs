using upp.Dtos.Article;
using upp.Dtos.Recipe;
using upp.Dtos.Training;
using upp.Entities;

namespace upp.Dtos.Request
{
    public class ChangeRequestDto
    {
        public int Id { get; set; }
        public int StatusId { get; set; }
        public string Comment { get; set; }
    }
}
