using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Education.Entityes.EF.BaseEntityes;

namespace Education.Entityes.EF
{
    [Table("StudentGroup")]
    public class StudentGroup : NamedEntity
    {
        public virtual ICollection<Student> Students { get; set; }
    }
}
