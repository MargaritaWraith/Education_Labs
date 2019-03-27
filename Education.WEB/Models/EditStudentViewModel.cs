using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Education.Entityes.EF;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Education.WEB.Models
{
    public class EditStudentViewModel
    {
        public Student Student { get; set; }
        public SelectList Groups { get; set; }

        public StudentGroup Group => (StudentGroup) Groups.SelectedValue;
    }
}
