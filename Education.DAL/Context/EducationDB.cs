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
        public DbSet<StudentGroup> StudentGroups { get; set; }

        public EducationDB(DbContextOptions<EducationDB> options) 
            : base(options)
        {
            ChangeTracker.LazyLoadingEnabled = true;
        }

        protected override void OnModelCreating(ModelBuilder model)
        {
            base.OnModelCreating(model);
            model.Entity<LectorsCourses>().HasKey(e => new { e.LectorId, e.CourseId });
            model.Entity<StudentsCourses>().HasKey(e => new { e.StudentId, e.CourseId });
            model.Entity<StudentsLabWorks>().HasKey(e => new { e.StudentId, e.LabWorkId });
        }
    }
}
