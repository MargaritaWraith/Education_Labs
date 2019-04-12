using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Education.Entityes.EF.BaseEntityes;

namespace Education.Entityes.EF
{
    [Table("Students")]
    public class Student: Human
    {
        public virtual StudentGroup Group { get; set; }
        public virtual ICollection<StudentsCourses> Courses { get; set; }
        public virtual ICollection<StudentsLabWorks> LabWorks { get; set; }
        public virtual ICollection<Participation> Partisipations { get; set; }
    }
}
