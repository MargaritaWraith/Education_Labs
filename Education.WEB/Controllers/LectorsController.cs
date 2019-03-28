using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Education.DAL.Context;
using Microsoft.AspNetCore.Mvc;

namespace Education.WEB.Controllers
{
    public class LectorsController : Controller
    {
        private readonly EducationDB _DB;

        public LectorsController(EducationDB db)
        {
            _DB = db;
        }

        public IActionResult Index()
        {
            var lectors = _DB.Lectors.ToArray();

            return View(lectors);
        }
    }
}