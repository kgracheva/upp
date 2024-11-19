using Entities;

namespace upp.Dtos.User
{
    public class LkUserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int Weight { get; set; }
        public WorkType WorkType { get; set; }
        public int DesiredWeight { get; set; }
        public int CaloriesByDay { get; set; }
        public int Height { get; set; }
    }
}
