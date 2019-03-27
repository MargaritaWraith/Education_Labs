using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Education.DAL.Context;
using Education.Entityes.EF;
using Education.WEB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
                if (db_group == null)
                    return NotFound();
                db_group.Name = group.Name;
            }

            _DB.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult RemoveGroup(int Id)
        {
            var db_group = _DB.StudentGroups.FirstOrDefault(g => g.Id == Id);
            if (db_group == null)
                return NotFound();
            _DB.StudentGroups.Remove(db_group);
            _DB.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult EditStudent(int? StudId, int GroupId)
        {
            Student stud;
            if (StudId == null)
            {
                stud = new Student();
                var group = _DB.StudentGroups.FirstOrDefault(g => g.Id == GroupId);

                if (group == null)
                    return NotFound();
                stud.Group = group;
            }
            else
            {
                stud = _DB.Students.Include(s => s.Group).FirstOrDefault(g => g.Id == StudId);
                if (stud == null)
                    return NotFound();
            }

            return View(new EditStudentViewModel { Student = stud, Groups = new SelectList(_DB.StudentGroups.ToArray()) });
        }

        [HttpPost]
        public IActionResult EditStudent(EditStudentViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var db_student = _DB.Students.Include(s => s.Group).FirstOrDefault(s => s.Id == model.Student.Id);
            if (db_student == null)
                return NotFound();
            var db_group = _DB.StudentGroups.Include(g => g.Students).FirstOrDefault(g => g.Id == model.Group.Id);
            if (db_group == null)
                return NotFound();

            db_student.Name = model.Student.Name;
            db_student.Surname = model.Student.Surname;
            db_student.Patronymic = model.Student.Patronymic;
            if (db_student.Group.Id != model.Group.Id)
            {
                db_student.Group.Students.Remove(db_student);
                db_group.Students.Add(db_student);
            }

            _DB.SaveChanges();
            return RedirectToAction("GroupStudents", new { db_student.Group.Id });
        }

        public IActionResult RemoveStudent(int Id)
        {
            Student db_student = _DB.Students.Include(s => s.Group).FirstOrDefault(s => s.Id == Id);
            if (db_student == null)
                return NotFound();
            _DB.Students.Remove(db_student);
            _DB.SaveChanges();
            return RedirectToAction("GroupStudents", new { db_student.Group.Id });

        }
    }
}