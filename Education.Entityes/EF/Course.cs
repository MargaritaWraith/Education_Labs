using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Education.Entityes.EF.BaseEntityes;

namespace Education.Entityes.EF
{
    [Table("EducationCourses")]
    public class Course : NamedEntity
    {
        public ICollection<LabWork> LaboratoryWorks { get; set; }
        public ICollection<LectorsCourses> Lecturers { get; set; }
        public ICollection<StudentsCourses> Students { get; set; }
    }
}
