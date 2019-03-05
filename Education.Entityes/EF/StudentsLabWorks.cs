using System.ComponentModel.DataAnnotations.Schema;

namespace Education.Entityes.EF
{
    [Table("StudentsLabWorks")]
    public class StudentsLabWorks
    {
        public int StudentId { get; set; }
        public int LabWorkId { get; set; }
        public virtual Student Student { get; set; }
        public virtual LabWork LabWorks { get; set; }

        public int Rating { get; set; }
    }
}