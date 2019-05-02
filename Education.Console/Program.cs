using System;
using System.Data.SqlClient;
using Education.DAL.Context;
using Education.DAL.Initial;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Education.ConsoleTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()         // создание объекта конфигурации приложения
                .SetBasePath(Environment.CurrentDirectory)  // указание ему рабочей директории, откуда читать файлы
                .AddJsonFile("appsettings.json")            // указание, что мы будем использовать указанный файл в формате json
                .Build();                                   // построение конфигурации

            // создание строителя конфигурации БД
            var db_config = new DbContextOptionsBuilder<EducationDB>();
            // указание, что мы хотим использовать sql сервер, указываем строку подключения по имени из конфигурационного файла
            db_config.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            // создание контекста БД
            using (var db = new EducationDB(db_config.Options))
            {
                db.Database.EnsureCreated();            // проверяем, что БД существует (иначе создаём новую БД)

                db.Initialize();

                using (var connection = new SqlConnection(config.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();
                    while (true)
                    {
                        Console.Write("Enter command >>");
                        var cmd = Console.ReadLine();

                        if (cmd?.Equals("exit", StringComparison.OrdinalIgnoreCase) == true) break;
                        if(cmd is null) continue;

                        if (cmd.Equals("help"))
                        {
                            PrintHelp();
                            continue;
                        }

                        if (cmd.Equals("clear") || cmd.Equals("cls"))
                        {
                            Console.Clear();
                            continue;
                        }

                        var d_index = cmd.IndexOf(':');
                        string cmd_prefix;
                        string cmd_body;

                        if (d_index >= 0)
                        {
                            cmd_prefix = cmd.Substring(0, cmd.IndexOf(':'));
                            cmd_body = cmd.Substring(cmd_prefix.Length + 1);
                        }
                        else
                        {
                            cmd_prefix = null;
                            cmd_body = cmd;
                        }

                        try
                        {
                            var command = new SqlCommand(cmd_body, connection);
                            switch (cmd_prefix)
                            {
                                case "scalar":
                                    {
                                        var result = command.ExecuteScalar();
                                        Console.WriteLine(result);
                                    }
                                    break;

                                case "nonquery":
                                case "nq":
                                    {
                                        var result = command.ExecuteNonQuery();
                                        Console.WriteLine(result);
                                    }
                                    break;

                                case "reader":
                                case "vector":
                                default:
                                    {
                                        using (var reader = command.ExecuteReader())
                                        {
                                            var schema = reader.GetColumnSchema();
                                            foreach (var db_column in schema)
                                                Console.Write($"{db_column.ColumnName}   ");

                                            Console.WriteLine();

                                            while (reader.Read())
                                            {
                                                for (var i = 0; i < schema.Count; i++)
                                                    Console.Write($"{reader[i]}| ");

                                                Console.WriteLine();
                                            }
                                            Console.WriteLine();
                                        }
                                    }
                                    break;
                            }
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("При выполнении команды {0} произошла ошибка:", cmd_body);
                            Console.WriteLine(error.GetType().Name);
                            Console.WriteLine(error.Message);
                            Console.WriteLine();
                        }
                    }
                }
            }
        }

        private static void PrintHelp()
        {
            Console.WriteLine(@"Для выхода из программы введите команду ""exit""

Для очистки консоли используйте команду ""clear"" либо ""cls""

Для выполнения команд требуется конкретизировать тип выполняемой команды: скалярный, векторный, процедурный


Для выполнения скалярных запросов (возвращающих одиночный объект результата) к команде требуется добавить префикс ""scalar""
    scalar:SELECT COUNT(*) FROM [dbo].[Students]

Для выполнения процедурных запросов к команде требуется добавить префикс ""nonquery"" либо сокращённо ""nq""
    nonquery:DELETE FROM [dbo].[Students] WHERE Id=5
    nq:CREATE TABLE [dbo].[Vozrast] ( [id] INT NOT NULL, [name] NVARCHAR (MAX) NOT NULL, [age] INT NOT NULL, PRIMARY KEY CLUSTERED ([id] ASC))
    nq:DROP TABLE Vozrast

Для выполнения векторного запроса к команде требуется добавить префикс ""vector"", либо ""reader"", либо просто ввести команду
    SELECT * FROM sys.triggers
    vector:SELECT Lectors.Surname, Courses.Name FROM Lectors JOIN LectorsCourses ON Lectors.Id=LectorsCourses.LectorId JOIN Courses ON LectorsCourses.CourseId=Courses.Id ORDER BY Lectors.Surname
    redaer:SELECT * FROM Students
");
        }
    }
}
