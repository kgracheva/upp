﻿using Entities;

namespace upp.Dtos.User
{
    public class AuthUserDto
    {
        public string Key { get; set; }
        public int UserId { get; set; }
        public List<Role> Roles { get; set; }
    }
}
