using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Education.Entityes.EF.BaseEntityes;

namespace Education.Entityes.EF
{
    [Table("Lectors")]
    public class Lector : Human
    {
        public virtual ICollection<LectorsCourses> Courses { get; set; }
    }
}
