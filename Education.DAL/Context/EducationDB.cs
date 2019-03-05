using System;
using System.Collections.Generic;
using System.Text;
using Education.Entityes.EF;
using Microsoft.EntityFrameworkCore;

namespace Education.DAL.Context
{
    public class EducationDB : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Lector> Lectors { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<LabWork> LabWorks { get; set; }
        public EducationDB(DbContextOptions options) : base(options)
        {
        }
    }
}
