using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Education.DAL.Context;
using Microsoft.AspNetCore.Mvc;

namespace Education.WEB.Controllers
{
    public class CoursesController : Controller
    {
        private readonly EducationDB _DB;
        public CoursesController(EducationDB db)
        {
            _DB = db;
        }

        public IActionResult Index()
        {
            var courses = _DB.Courses.ToArray();

            return View(courses);
        }
    }
}