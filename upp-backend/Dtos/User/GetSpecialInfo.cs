using Entities;

namespace upp.Dtos.User
{
    public class GetSpecialInfo
    {
        public string Name { get; set; } = "";
        public string Lastname { get; set; } = "";
        public int? Weight { get; set; } //каждый раз - текущий вес пользователя!!
        public WorkType? WorkType { get; set; }
        public int? DesiredWeight { get; set; }
        public int? Height { get; set; }
        public string Sex { get; set; } = "";
        public DateTime BirthDay { get; set; }
    }
}
