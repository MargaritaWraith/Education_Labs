using System;
using Education.DAL.Context;
using Education.DAL.Initial;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Education.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()         // создание объекта конфигурации приложения
                .SetBasePath(Environment.CurrentDirectory)  // указание ему рабочей директории, откуда читать файлы
                .AddJsonFile("appsettings.json")            // указание, что мы будем использовать указанный файл в формате json
                .Build();                                   // построение конфигурации

            var db_config = new DbContextOptionsBuilder<EducationDB>();                 // создание строителя конфигурации БД
            db_config.UseSqlServer(config.GetConnectionString("DefaultConnection"));    // указание, что мы хотим использовать sql сервер, указываем строку подключения по имени из конфигурационного файла
            using (var db = new EducationDB(db_config.Options))                         // создание контекста БД
            {
                db.Database.EnsureCreated();            // проверяем, что БД существует (иначе создаём новую БД)

                db.Initialize();

            }

            //Console.ReadLine();
        }
    }
}
