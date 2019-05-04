using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Education.DAL.Context;
using Education.Entityes.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Education.DAL.Initial
{
    public static class EducationContextInitializer
    {
        public static void Initialize(this EducationDB db)
        {
            if (!db.StudentGroups.Any())
            {
                for (var i = 1; i <= 6; i++)
                    db.StudentGroups.AddRange(
                        new StudentGroup { Name = $"04-{i}07" },
                        new StudentGroup { Name = $"04-{i}06" },
                        new StudentGroup { Name = $"04-{i}08" },
                        new StudentGroup { Name = $"04-{i}15" },
                        new StudentGroup { Name = $"04-{i}01" },
                        new StudentGroup { Name = $"04-{i}03" }
                    );
                db.SaveChanges();
            }

            if (!db.Students.Any())
            {
                var groups = db.StudentGroups.ToArray();
                var rnd = new Random();
                using (var reader = File.OpenText("names2.txt"))
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        if (line is null) continue;
                        var components = line.Split(' ');
                        if (components.Length < 2) continue;
                        db.Students.Add(new Student
                        {
                            Surname = components[0],
                            Name = components[1],
                            Group = rnd.Next(groups)
                        });
                    }

                db.SaveChanges();
            }

            if (!db.Courses.Any())
            {
                db.Courses.AddRange(
                    new Course { Name = "РПУ" },
                    new Course { Name = "АФУ" },
                    new Course { Name = "Электродинамика" },
                    new Course { Name = "Введение в специальность" },
                    new Course { Name = "ЭМС" },
                    new Course { Name = "Проектирование ФАР и АФАР" }
                );
                db.SaveChanges();

                var students = db.Students.ToArray();
                var courses = db.Courses.ToArray();
                var rnd = new Random();
                for (var i = 0; i < students.Length; i++)
                {
                    var stud = rnd.Next(students);
                    if (stud.Courses is null)
                        stud.Courses = new List<StudentsCourses>();
                    var crs = rnd.Next(courses);
                    if (stud.Courses.Any(c => c.Course == crs))
                        continue;
                    stud.Courses.Add(new StudentsCourses { Course = crs, Student = stud });
                }
                db.SaveChanges();
            }

            if (!db.Lectors.Any())
            {
                void AddLector(string name, string surname, string patronymic, params string[] courses)
                {
                    if (db.Lectors.Any(l => l.Name == name && l.Surname == surname)) return;
                    var lector = new Lector
                    {
                        Name = name,
                        Surname = surname,
                        Patronymic = patronymic,
                        Courses = new List<LectorsCourses>()
                    };

                    foreach (var course in courses)
                    {
                        var db_course = db.Courses.FirstOrDefault(c => c.Name == course);
                        if (db_course is null)
                        {
                            db_course = new Course { Name = course };
                            db.Courses.Add(db_course);
                        }
                        var lector_course = new LectorsCourses
                        {
                            Lector = lector,
                            Course = db_course
                        };
                        lector.Courses.Add(lector_course);
                    }

                    db.Lectors.Add(lector);

                }

                AddLector("Андрей", "Щербачёв", "Юрьевич", "РПУ");
                AddLector("Олег", "Терёхин", "Васильевич", "ЭМС", "АФУ");
                AddLector("Антон", "Васин", "Александрович", "ЭМС", "РПУ");
                AddLector("Павел", "Шмачилин", "Александрович", "АФУ", "РПУ", "Введение в специальность");
                AddLector("Александр", "Гринёв", "Юрьевич", "Электродинамика");
                AddLector("Леонид", "Пономарёв", "Иванович", "АФУ", "ЭМС");
                AddLector("Дмитрий", "Воскресенский", "Иванович", "АФУ", "Проектирование ФАР и АФАР");
                AddLector("Елена", "Добычина", "Михайловна", "РПУ");
                AddLector("Елена", "Овчинникова", "Викторовна", "АФУ", "Сверхширокополосные системы");

                db.SaveChanges();
            }

            if (!db.LabWorks.Any())
            {
                db.LabWorks.AddRange(
                    new LabWork { Name = "Длинная линия", Course = db.Courses.First(g => g.Name == "АФУ") },
                    new LabWork { Name = "Симметричный вибратор", Course = db.Courses.First(g => g.Name == "АФУ") },
                    new LabWork { Name = "Умножитель частоты", Course = db.Courses.First(g => g.Name == "РПУ") },
                    new LabWork { Name = "Автогенератор", Course = db.Courses.First(g => g.Name == "РПУ") },
                    new LabWork { Name = "Падение плоской волны на границу раздела двух сред", Course = db.Courses.First(g => g.Name == "Электродинамика") },
                    new LabWork { Name = "Зеркальная антенна", Course = db.Courses.First(g => g.Name == "АФУ") }
               );
                db.SaveChanges();
            }

            if (!db.Students.Any(s => s.LabWorks.Count > 0))
            {
                var students = db.Students.ToArray();
                var labs = db.LabWorks.ToArray();
                var rnd = new Random();
                for (var i = 0; i < students.Length * 5; i++)
                {
                    var stud = rnd.Next(students);
                    if (stud.LabWorks is null) stud.LabWorks = new List<StudentsLabWorks>();
                    var lbw = rnd.Next(labs);
                    if (stud.Courses != null && stud.Courses.Any(c => c.Course == lbw.Course) && !stud.LabWorks.Any(lw => lw.LabWorks == lbw))
                    {
                        stud.LabWorks.Add(new StudentsLabWorks
                        {
                            Rating = rnd.Next(1, 6),
                            LabWorks = lbw,
                            Student = stud
                        });
                    }

                }
                db.SaveChanges();
            }

            const string test_student_group_name = "TestGroup";
            var test_group = db.StudentGroups.Include(g => g.Students).FirstOrDefault(g => g.Name == test_student_group_name);
            if (test_group is null)
            {
                test_group = new StudentGroup { Name = test_student_group_name, Students = new List<Student>(20) };
                db.StudentGroups.Add(test_group);
                db.SaveChanges();
            }

            if (test_group.Students.Count == 0)
            {
                for (var i = 1; i <= 20; i++)
                {
                    var student = new Student
                    {
                        Surname = $"Surname {i:00}",
                        Name = $"Student {i:00}",
                        Group = test_group
                    };
                    db.Students.Add(student);
                }
                db.SaveChanges();
            }
        }
    }
}
