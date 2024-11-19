using Entities;

namespace upp.Entities
{
    public class Calendar : BaseEntity
    {
        public int UserId { get; set; }
        public User? User { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        public int ProductCount { get; set; }
        public int MealTypeId { get; set; }
        public MealType? MealType { get; set; }
        public int Weight { get; set; }
    }
}
