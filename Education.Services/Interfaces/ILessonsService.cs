using System;
using System.Collections.Generic;
using System.Text;
using Education.Entityes.EF;

namespace Education.Services.Interfaces
{
    public interface ILessonsService
    {
        IEnumerable<Lesson> GetLessons(int LectorId, int CourseId, LessonType Type);
        Lesson CreateLesson(DateTime Date, LessonType Type, int LectorId, string Subject = null, string Description = null); 
    }
}
