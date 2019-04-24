using System;
using System.Collections.Generic;
using Education.Entityes.EF.BaseEntityes;

namespace Education.Entityes.EF
{
    public class Lesson : BaseEntity
    {
        public DateTime Date { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public LessonType Type { get; set; }
        public virtual ICollection<Participation> Participations { get; set; }
        public virtual Lector Lector { get; set; }
        public virtual Course Course { get; set; }
    }
}