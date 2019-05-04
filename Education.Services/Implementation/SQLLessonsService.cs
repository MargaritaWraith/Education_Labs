using System;
using System.Linq;
using System.Threading.Tasks;
using Education.DAL.Context;
using Education.Entityes.EF;
using Education.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Education.Services.Implementation
{
    public class SQLLessonsService : SQLDataService<Lesson>, ILessonsService
    {
        public SQLLessonsService(EducationDB db) : base(db) { }

        public IQueryable<Lesson> Get(int LectorId, int CourseId, LessonType Type) =>
            Entityes
               .Include(L => L.Lector)
               .Include(L => L.Course)
               .Where(Lesson =>
                    Lesson.Lector.Id == LectorId
                    && Lesson.Course.Id == CourseId
                    && Lesson.Type == Type);

        public async Task<Lesson> CreateLessonAsync(DateTime Date, LessonType Type, int LectorId, string Subject = null, string Description = null)
        {
            var lesson = new Lesson
            {
                Type = Type,
                Date = Date,
                Lector = _db.Lectors.First(l => l.Id == LectorId),
                Subject = Subject,
                Description = Description
            };
            await AddAsync(lesson);

            return lesson;
        }

        public override async Task<Lesson> EditAsync(Lesson lesson)
        {
            var db_lesson = await GetByIdAsync(lesson.Id);
            if (db_lesson is null) return null;

            if (lesson.Course != null)
                db_lesson.Course = lesson.Course;

            db_lesson.Date = lesson.Date;

            if (lesson.Description != null)
                db_lesson.Description = lesson.Description;

            if (lesson.Lector != null)
                db_lesson.Lector = lesson.Lector;

            if (lesson.Participations != null)
                db_lesson.Participations = lesson.Participations;

            if (lesson.Subject != null)
                db_lesson.Subject = lesson.Subject;

            db_lesson.Type = lesson.Type;

            return db_lesson;
        }
    }
}
