using Entities;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace upp.Entities
{
    public class Article : BaseEntity
    {
        public string Name { get; set; }
        public int CreatorId { get; set; }
        public User? Creator { get; set; }
        public int StatusTypeId { get; set; }
        public StatusType? StatusType { get; set; }
        public ICollection<ArticleBlock>? ArticleBlocks { get; set; } = new List<ArticleBlock>();
    }
}
