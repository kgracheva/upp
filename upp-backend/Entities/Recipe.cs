using Entities;

namespace upp.Entities
{
    public class Recipe : BaseEntity
    {
        public string Name { get; set; } = "";
        public int ProteinsCount { get; set; }
        public int FatsCount { get; set; }
        public int CarbsCount { get; set; }
        public int CaloriesCount { get; set; }
        public int CreatorId { get; set; }
        public User? Creator { get; set; }
        public bool IsDeleted { get; set; } = false;
        public int StatusTypeId { get; set; }
        public StatusType? StatusType { get; set; }
        public ICollection<RecipeBlock>? RecipeBlocks { get; set; } = new List<RecipeBlock>();
    }
}
