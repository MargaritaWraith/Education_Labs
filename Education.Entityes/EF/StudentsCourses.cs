﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Education.Entityes.EF
{
    [Table("StudentsCourses")]
    public class StudentsCourses
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public virtual Student Student { get; set; }
        public virtual Course Course { get; set; }
    }
}