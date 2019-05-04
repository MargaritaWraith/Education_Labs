using System;
using System.Linq;
using System.Threading.Tasks;
using Education.Entityes.EF;

namespace Education.Services.Interfaces
{
    public interface ILessonsService : IDataService<Lesson>
    {
        IQueryable<Lesson> Get(int LectorId, int CourseId, LessonType Type);

        Task<Lesson> CreateLessonAsync(DateTime Date, LessonType Type, int LectorId, string Subject = null, string Description = null);
    }
}
