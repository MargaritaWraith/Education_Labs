using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Education.DAL.Context;
using Education.Entityes.EF;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Education.WEB.Controllers
{
    [Authorize(Roles = "admin")]
    public class LectorsController : Controller
    {
        private readonly EducationDB _DB;

        public LectorsController(EducationDB db) => _DB = db;

        public IActionResult Index() => View(_DB.Lectors.ToArray());

        //[AllowAnonymous]
        public IActionResult GetLectorCourses(int LectorId)
        {
            if (LectorId <= 0)
                return BadRequest();

            var lector = _DB.Lectors
               .Include(l => l.Courses)
               .Include(l => l.Courses.Select(lc => lc.Course))
               //.Include("Courses.Course")
               .FirstOrDefault(l => l.Id == LectorId);

            if (lector is null)
                return NotFound();

            return View(lector);
        }

        public IActionResult EditLector(int? LectorId)
        {
            Lector lector;
            if (LectorId is null)
                lector = new Lector();
            else
            {
                lector = _DB.Lectors.FirstOrDefault(l => l.Id == LectorId);
                if (lector is null)
                    return NotFound();
            }

            return View(lector);
        }

        [HttpPost]
        public IActionResult EditLector(Lector Lector)
        {
            if (!ModelState.IsValid)
                return View(Lector);

            if (Lector.Id == 0)
                _DB.Lectors.Add(Lector);
            else
            {
                var db_lector = _DB.Lectors.FirstOrDefault(l => l.Id == Lector.Id);
                if (db_lector is null)
                    return NotFound();
                db_lector.Surname = Lector.Surname;
                db_lector.Name = Lector.Name;
                db_lector.Patronymic = Lector.Patronymic;
            }

            _DB.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult RemoveLector(int Id)
        {
            var db_lector = _DB.Lectors.FirstOrDefault(l => l.Id == Id);
            if (db_lector is null) return NotFound();

            return View(db_lector);
        }

        [HttpPost]
        public IActionResult RemoveLector(Lector Lector)
        {
            var db_lector = _DB.Lectors.FirstOrDefault(l => l.Id == Lector.Id);
            if (db_lector is null) return NotFound();

            _DB.Lectors.Remove(db_lector);
            _DB.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}