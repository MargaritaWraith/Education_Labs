using System.Threading.Tasks;
using Education.DAL.Context;
using Education.Entityes.EF;
using Education.WEB.Infrastructure.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Education.WEB.Controllers
{
    [Authorize(Policy = nameof(LectorsPolicy))]
    public class LectorsController : Controller
    {
        private readonly EducationDB _db;

        public LectorsController(EducationDB db) => _db = db;

        public async Task<IActionResult> Index() => View(await _db.Lectors.ToArrayAsync());

        //[AllowAnonymous]
        public async Task<IActionResult> GetLectorCourses(int LectorId)
        {
            if (LectorId <= 0)
                return BadRequest();

            var lector = await _db.Lectors
               .Include(l => l.Courses)
               .ThenInclude(c => c.Course)
               .FirstOrDefaultAsync(l => l.Id == LectorId);

            if (lector is null)
                return NotFound();

            return View(lector);
        }

        public async Task<IActionResult> EditLector(int? LectorId)
        {
            Lector lector;
            if (LectorId is null)
                lector = new Lector();
            else
            {
                lector = await _db.Lectors.FirstOrDefaultAsync(l => l.Id == LectorId);
                if (lector is null)
                    return NotFound();
            }

            return View(lector);
        }

        [HttpPost]
        public async Task<IActionResult> EditLector(Lector Lector)
        {
            if (!ModelState.IsValid)
                return View(Lector);

            if (Lector.Id == 0)
                await _db.Lectors.AddAsync(Lector);
            else
            {
                var db_lector = await _db.Lectors.FirstOrDefaultAsync(l => l.Id == Lector.Id);
                if (db_lector is null)
                    return NotFound();

                db_lector.Surname = Lector.Surname;
                db_lector.Name = Lector.Name;
                db_lector.Patronymic = Lector.Patronymic;
            }

            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> RemoveLector(int Id)
        {
            var db_lector = await _db.Lectors.FirstOrDefaultAsync(l => l.Id == Id);
            if (db_lector is null)
                return NotFound();

            return View(db_lector);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveLector(Lector Lector)
        {
            var db_lector = await _db.Lectors.FirstOrDefaultAsync(l => l.Id == Lector.Id);
            if (db_lector is null) return NotFound();

            _db.Lectors.Remove(db_lector);
            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}