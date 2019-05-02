using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace Education.Entityes.EF.Identity
{
    public class Role : IdentityRole
    {
        public const string Admin = "Admin";
        public const string User = "User";
        public const string Lector = "Lector";
        public const string Student = "Student";
    }
}
