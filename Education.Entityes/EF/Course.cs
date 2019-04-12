using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Education.Entityes.EF.BaseEntityes;

namespace Education.Entityes.EF
{
    [Table("Courses")]
    public class Course : NamedEntity
    {
        public ICollection<LabWork> LaboratoryWorks { get; set; }
        public ICollection<LectorsCourses> Lecturers { get; set; }
        public ICollection<StudentsCourses> Students { get; set; }
        public ICollection<Lesson> Lessons { get; set; }
    }

    public enum LessonType { Lecture, Seminar, LabWork }

    public class Lesson : BaseEntity
    {
        public DateTime Date { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public LessonType Type { get; set; }
        public ICollection<Participation> Participations { get; set; }
    }


    public class Participation : BaseEntity
    {
        [Required]
        public virtual Student Student { get; set; }
        [Required]
        public virtual Lesson Lesson { get; set; }

        public double? Order { get; set; }
        public string Description { get; set; }
    }
}
