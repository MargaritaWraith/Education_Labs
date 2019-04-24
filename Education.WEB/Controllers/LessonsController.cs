using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Education.Entityes.EF;
using Education.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Education.WEB.Controllers
{
    public class LessonsController : Controller
    {
        private readonly ILessonsService _LessonsService;
        public LessonsController(ILessonsService LessonsService) => _LessonsService = LessonsService;

        public IActionResult Index(int LectorID, int CourseID, LessonType Type = LessonType.Lecture)
        {
            var Lessons = _LessonsService.GetLessons(LectorID, CourseID, Type);

            return View(Lessons);
        }
    }
}