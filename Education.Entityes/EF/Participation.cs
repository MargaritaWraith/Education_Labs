using System.ComponentModel.DataAnnotations;
using Education.Entityes.EF.BaseEntityes;

namespace Education.Entityes.EF
{
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