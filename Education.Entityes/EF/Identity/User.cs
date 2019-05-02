using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace Education.Entityes.EF.Identity
{
    public class User : IdentityUser
    {
        public const string Admin = "Admin";
        public const string AdminDefaultPassword = "1147";

        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronimic { get; set; }
    }
}
