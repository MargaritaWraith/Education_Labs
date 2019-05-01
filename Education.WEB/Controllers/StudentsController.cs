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

        public StudentsController(EducationDB db) => _DB = db;

        public IActionResult Index() => View();

        public IActionResult GetStudentsGroups() => View(_DB.StudentGroups.ToArray());

        public IActionResult GetLaboratoryWorks() => View();

        public IActionResult GetEducationLiterature() => View();

        public IActionResult GroupStudents(int Id)
        {
            var group = _DB.StudentGroups.Include(g => g.Students).FirstOrDefault(g => g.Id == Id);
            if (group is null)
                return NotFound();

            return View(group);
        }

        public IActionResult EditGroup(int? GroupId)
        {
            StudentGroup group;
            if (GroupId is null)
                group = new StudentGroup();
            else
            {
                group = _DB.StudentGroups.FirstOrDefault(g => g.Id == GroupId);
                if (group is null)
                    return NotFound();
            }

            return View(group);
        }

        [HttpPost]
        public IActionResult EditGroup(StudentGroup group)
        {
            if (!ModelState.IsValid)
                return View(group);

            if (group.Id == 0)
                _DB.StudentGroups.Add(group);
            else
            {
                var db_group = _DB.StudentGroups.FirstOrDefault(g => g.Id == group.Id);
                if (db_group is null)
                    return NotFound();
                db_group.Name = group.Name;
            }

            _DB.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult RemoveGroup(int GroupId)
        {
            var db_group = _DB.StudentGroups.FirstOrDefault(g => g.Id == GroupId);
            if (db_group is null)
                return NotFound();
            _DB.StudentGroups.Remove(db_group);
            _DB.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult EditStudent(int? StudId, int GroupId)
        {
            Student stud;
            if (StudId is null)
            {
                stud = new Student();
                var group = _DB.StudentGroups.FirstOrDefault(g => g.Id == GroupId);

                if (group is null)
                    return NotFound();
                stud.Group = group;
            }
            else
            {
                stud = _DB.Students.FirstOrDefault(g => g.Id == StudId);
                if (stud is null)
                    return NotFound();
            }

            ViewBag.Groups = _DB.StudentGroups.ToArray();

            return View(stud);
        }

        [HttpPost]
        public IActionResult EditStudent(Student Student, int GroupId)
        {
            if (!ModelState.IsValid)
                return View(Student);

            if (Student.Id == 0)
            {
                var new_group = _DB.StudentGroups.Include(s => s.Students).FirstOrDefault(g => g.Id == GroupId);
                if (new_group is null)
                    return NotFound();

                new_group.Students.Add(Student);
                _DB.Students.Add(Student);
            }

            else
            {
                var db_student = _DB.Students.Include(s => s.Group).FirstOrDefault(s => s.Id == Student.Id);
                if (db_student is null)
                    return NotFound();

                if (db_student.Group.Id != GroupId)
                {
                    var new_group = _DB.StudentGroups.Include(s => s.Students).FirstOrDefault(g => g.Id == GroupId);
                    if (new_group is null)
                        return NotFound();
                    db_student.Group.Students.Remove(db_student);
                    db_student.Group = new_group;
                    //new_group.Students.Add(db_student);
                }

                db_student.Name = Student.Name;
                db_student.Surname = Student.Surname;
                db_student.Patronymic = Student.Patronymic;
            }
            
            _DB.SaveChanges();
            return RedirectToAction("GroupStudents", new { Id = GroupId });
        }

        public IActionResult RemoveStudent(int Id)
        {
            var db_student = _DB.Students.Include(s => s.Group).FirstOrDefault(s => s.Id == Id);
            if (db_student is null)
                return NotFound();
            _DB.Students.Remove(db_student);
            _DB.SaveChanges();
            return RedirectToAction("GroupStudents", new { db_student.Group.Id });

        }
    }
}