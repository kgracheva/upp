using Entities;
using upp.Entities;

namespace upp.Dtos.Calendar
{
    public class CalendarDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int MealTypeId { get; set; }
        public int ProductCount { get; set; }
        public DateTime Created { get; set; }
    }
}
