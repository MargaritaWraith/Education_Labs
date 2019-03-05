using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Education.Entityes.EF
{
    [Table("LectorsCourses")]
    public class LectorsCourses
    {
        public int LectorId { get; set; }
        public int CourseId { get; set; }
        public virtual Lector Lector { get; set; }
        public virtual Course Course { get; set; }
    }
}
