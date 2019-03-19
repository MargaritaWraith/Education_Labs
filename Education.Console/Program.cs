using System;
using System.Data.SqlClient;
using Education.DAL.Context;
using Education.DAL.Initial;
using Education.Entityes.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;

namespace Education.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //var tst = "scalar:SELECT * FROM STUDENT";
            //var prefix = tst.Substring(0, tst.IndexOf(':'));
            //var c = tst.Substring(prefix.Length + 1);

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

                using (var connection = new SqlConnection(config.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();
                    while (true)
                    {
                        Console.Write("Enter command >");
                        var cmd = Console.ReadLine();

                        if (cmd?.Equals("exit", StringComparison.OrdinalIgnoreCase) == true) break;

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



                        SqlCommand command = new SqlCommand(cmd_body, connection);
                        switch (cmd_prefix)
                        {
                            case "scalar":
                                {
                                    var result = command.ExecuteScalar();
                                    Console.WriteLine(result);
                                }
                                break;

                            case "nonquery":
                                {
                                    var result = command.ExecuteNonQuery();
                                    Console.WriteLine(result);
                                }
                                break;


                            case "reader":
                            case "vector":
                            default:
                                {
                                    var reader = command.ExecuteReader();
                                    var schema = reader.GetColumnSchema();
                                    foreach (var db_column in schema)
                                    {
                                        Console.Write(db_column.ColumnName + "   ");
                                    }

                                    Console.WriteLine();

                                    while (reader.Read())
                                    {
                                        for (int i = 0; i < schema.Count; i++)
                                        {
                                            Console.Write(reader[i]);
                                            Console.Write("| ");
                                        }
                                        Console.WriteLine();
                                    }


                                }
                                break;
                        }




                    }

                    ;
                }
            }

            //Console.ReadLine();
        }
    }
}
