using upp.Entities;

namespace upp.Dtos.User
{
    public class FindUserDto
    {
        public int Page { get; set; } = 1;
        public int Size { get; set; } = 10;
    }
}
