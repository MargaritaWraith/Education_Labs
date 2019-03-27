using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Education.DAL.Context;
using Education.Entityes.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Education.WEB.Controllers
{
    public class StudentsController : Controller
    {
        private readonly EducationDB _DB;

        public StudentsController(EducationDB db)
        {
            _DB = db;
        }

        public IActionResult Index()
        {
            var groups = _DB.StudentGroups.ToArray();

            return View(groups);
        }

        public IActionResult GroupStudents(int Id)
        {
            var group = _DB.StudentGroups.Include(g => g.Students).FirstOrDefault(g => g.Id == Id);
            if (group == null)
                return NotFound();

            return View(group);
        }

        public IActionResult EditGroup(int? Id)
        {
            StudentGroup group;
            if (Id == null)
                @group = new StudentGroup();
            else
            {
                group = _DB.StudentGroups.FirstOrDefault(g => g.Id == Id);
                if (group == null)
                    return NotFound();
            }

            return View(group);
        }

        [HttpPost]
        public IActionResult EditGroup(StudentGroup group)
        {
            if (!ModelState.IsValid)
            {
                return View(group);
            }

            if (group.Id == 0) _DB.StudentGroups.Add(group);
            else
            {
                var db_group = _DB.StudentGroups.FirstOrDefault(g => g.Id == group.Id);
                if (db_group==null)
                    return NotFound();
                db_group.Name = group.Name;
            }

            _DB.SaveChanges();
            return RedirectToAction("Index");
        }
    }

}