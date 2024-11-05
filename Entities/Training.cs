using Entities;

namespace upp.Entities
{
    public class Training : BaseEntity
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public int StatusTypeId { get; set; }
        public StatusType? StatusType { get; set; }
        public int CreatorId { get; set; }
        public User? Creator { get; set; }
        public string VideoRef { get; set; }
    }
}
