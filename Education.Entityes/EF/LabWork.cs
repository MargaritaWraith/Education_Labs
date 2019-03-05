using System.ComponentModel.DataAnnotations.Schema;
using Education.Entityes.EF.BaseEntityes;

namespace Education.Entityes.EF
{
    [Table("LaboratoryWorks")]
    public class LabWork : NamedEntity
    {
        public Course Course { get; set; }
    }
}