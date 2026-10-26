using Dapper;
using Microsoft.Data.SqlClient; 
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;


namespace DapperExample
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
    }

    class Program
    {
        // Строка подключения. В реальном проекте храните её в конфигурации (appsettings.json)
        static string connectionString = "Server=localhost;Database=TestDB;Integrated Security=True;TrustServerCertificate=True;";
        static void Main(string[] args)
        {
            // 1. ВСТАВКА (INSERT)
            var newUser = new User
            {
                Name = "Иван Петров",
                Email = "ivan@example.com"
           ,
                Age = 30
            };
            string insertSql = "INSERT INTO Users (Name, Email, Age) VALUES (@Name, @Email, @Age)";

            using (IDbConnection db = new SqlConnection(connectionString))
            {
                int rowsAffected = db.Execute(insertSql, newUser);
                Console.WriteLine($"Вставлено строк: {rowsAffected}");
            }
            // 2. ЧТЕНИЕ (SELECT)
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                // Получаем всех пользователей старше 25 лет
                var users = db.Query<User>("SELECT * FROM Users WHERE Age > @Age", new { Age = 25 }).ToList();
                foreach (var user in users)
                {
                    Console.WriteLine($"ID: {user.Id}, Имя: {user.Name}, Email: {users.Email}, Возраст: {user.Age}");
                }
            }
            // 3. ОБНОВЛЕНИЕ (UPDATE)
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var updateSql = "UPDATE Users SET Age = @Age WHERE Name = @Name";
                db.Execute(updateSql, new { Age = 31, Name = "Иван Петров" });
            }
            // 4. УДАЛЕНИЕ (DELETE)
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var deleteSql = "DELETE FROM Users WHERE Name = @Name";
                db.Execute(deleteSql, new { Name = "Иван Петров" });
            }
        }
    }
}