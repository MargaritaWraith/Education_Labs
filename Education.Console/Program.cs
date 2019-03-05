using System;
using Education.DAL.Context;
using Microsoft.EntityFrameworkCore;

namespace Education.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            DbContextOptionsBuilder builder = new DbContextOptionsBuilder<EducationDB>();
            builder.UseSqlServer("EducationDB", opt => { });
            using (var db = new Education.DAL.Context.EducationDB((DbContextOptions<EducationDB>) builder.Options))
            {

            }

            Console.ReadLine();
        }
    }
}
