﻿namespace upp.Dtos
{
    public class AddUserDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; } = "";
        public string Lastname { get; set; } = "";
        public int RoleId { get; set; }
    }
}
