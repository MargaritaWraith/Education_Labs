using System;
using Education.DAL.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Education.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            var db_config = new DbContextOptionsBuilder<EducationDB>();
            db_config.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            using (var db = new EducationDB(db_config.Options))
            {
                db.Database.EnsureCreated();

            }

            //Console.ReadLine();
        }
    }
}
