namespace upp.Dtos.User
{
    public class AddUserDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; } = "";
        public string Lastname { get; set; } = "";
    }
}
