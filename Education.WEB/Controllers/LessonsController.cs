using Education.Entityes.EF;
using Education.Entityes.EF.Identity;
using Education.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Education.WEB.Controllers
{
    [Authorize(Roles = Role.Admin)]
    [Authorize(Roles = Role.Lector)]
    public class LessonsController : Controller
    {
        private readonly ILessonsService _LessonsService;

        public LessonsController(ILessonsService LessonsService) => _LessonsService = LessonsService;

        public IActionResult Index(int LectorID, int CourseID, LessonType Type = LessonType.Lecture) => 
            View(_LessonsService.Get(LectorID, CourseID, Type));
    }
}