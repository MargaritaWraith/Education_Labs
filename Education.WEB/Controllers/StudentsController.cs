using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Education.DAL.Context;
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
            var group = _DB.StudentGroups.Include(g => g.Students).First();

            return View(group);
        }
    }
}