using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Education.DAL.Context;
using Education.Entityes.EF;
using Education.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Education.Services.Implementation
{
    public class SQLLessonsService: ILessonsService
    {
        private EducationDB _DB;
        public SQLLessonsService(EducationDB db) => _DB = db;
        
        public IEnumerable<Lesson> GetLessons(int LectorId, int CourseId, LessonType Type)
        {
            return _DB.Lessons
                .Include(L=>L.Lector)
                .Include(L=>L.Course)
                .Where(Lesson => 
                    Lesson.Lector.Id == LectorId 
                    && Lesson.Course.Id == CourseId 
                    && Lesson.Type == Type);
        }

        public Lesson CreateLesson(DateTime Date, LessonType Type, int LectorId, string Subject = null, string Description = null)
        {
            return new Lesson
            {
                Type = Type,
                Date = Date,
                Lector = _DB.Lectors.First(l=>l.Id == LectorId),
                Subject = Subject,
                Description = Description
            };
            
        }
    }
}
