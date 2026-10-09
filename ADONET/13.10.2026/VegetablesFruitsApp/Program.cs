using System;
using System.Data;
using System.Data.Common;
using System.Configuration;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections.Generic;

namespace VegetablesFruitsApp
{
    class Program
    {
        static string connectionString;
        static string providerName;

        static string currentDatabaseName = "База не выбрана";





        static async Task Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            while (true)
            {
                Console.Clear();

                Console.WriteLine("=== Овощи и фрукты ===");
                Console.WriteLine("Текущая база: " + currentDatabaseName);
                Console.WriteLine();
                Console.WriteLine("1. Выбрать базу данных");
                Console.WriteLine("2. Показать продукты");
                Console.WriteLine("3. Обновить калорийность");
                Console.WriteLine("4. Удалить продукт");
                Console.WriteLine("0. Выход");
                Console.Write("\nВыберите действие: ");

                string choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            SelectDatabase();
                            break;

                        case "2":
                            if (connectionString == null)
                                Console.WriteLine("Сначала выберите базу данных.");
                            else
                                await ShowProductsAsync();
                            break;

                        case "3":
                            if (connectionString == null)
                                Console.WriteLine("Сначала выберите базу данных.");
                            else
                                await UpdateProductAsync();
                            break;

                        case "4":
                            if (connectionString == null)
                                Console.WriteLine("Сначала выберите базу данных.");
                            else
                                await DeleteProductAsync();
                            break;

                        case "0":
                            return;

                        default:
                            Console.WriteLine("Нет такого пункта.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ошибка: " + ex.Message);
                }

                Console.WriteLine("\nНажмите Enter для продолжения...");
                Console.ReadLine();
            }
        }


        static void SelectDatabase()
        {
            Console.WriteLine("\nВыберите базу данных:");
            Console.WriteLine("1. VegetablesFruits");
            Console.WriteLine("2. VegetablesFruits2");
            Console.Write("> ");

            string choice = Console.ReadLine();

            string settingsName;

            if (choice == "1")
                settingsName = "SqlServer1";
            else if (choice == "2")
                settingsName = "SqlServer2";
            else
            {
                Console.WriteLine("Неверный выбор.");
                return;
            }

            ConnectionStringSettings settings =
                ConfigurationManager.ConnectionStrings[settingsName];

            if (settings == null)
            {
                Console.WriteLine("Настройки подключения не найдены.");
                return;
            }

            try
            {
                // Получение фабрики
                DbProviderFactories.GetFactory(settings.ProviderName);

                connectionString = settings.ConnectionString;
                providerName = settings.ProviderName;
                currentDatabaseName = settings.Name;

                Console.WriteLine("База успешно выбрана: " + currentDatabaseName);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка выбора базы: " + ex.Message);
            }
        }








        static async Task ShowProductsAsync()
        {
            DbProviderFactory factory =
                DbProviderFactories.GetFactory(providerName);

            using (DbConnection connection = factory.CreateConnection())
            {
                if (connection == null)
                    throw new Exception("Не удалось создать подключение.");

                connection.ConnectionString = connectionString;

                await connection.OpenAsync();

                using (DbCommand command = factory.CreateCommand())
                {
                    if (command == null)
                        throw new Exception("Не удалось создать команду.");

                    command.Connection = connection;
                    command.CommandText =
                        "SELECT Id, ProductName, Category, Color, Calories " +
                        "FROM Products ORDER BY Id";

                    List<string> rows = new List<string>();
                    int count = 0;

                    // Измерение времени выполнения запроса
                    Stopwatch stopwatch = Stopwatch.StartNew();

                    using (DbDataReader reader =
                        await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            // Сохранение строк
                            rows.Add(string.Format(
                                "{0,-5} | {1,-15} | {2,-10} | {3,-15} | {4}",
                                reader["Id"],
                                reader["ProductName"],
                                reader["Category"],
                                reader["Color"],
                                reader["Calories"]));

                            count++;
                        }
                    }

                    // Остановка секундомера
                    stopwatch.Stop();

                    Console.WriteLine("\nСписок продуктов:\n");
                    Console.WriteLine(
                        "{0,-5} | {1,-15} | {2,-10} | {3,-15} | {4}",
                        "ID", "Название", "Категория", "Цвет", "Калории");

                    Console.WriteLine(new string('-', 70));

                    foreach (string row in rows)
                        Console.WriteLine(row);

                    Console.WriteLine("\nВсего продуктов: " + count);

                    Console.WriteLine(
                        "Время выполнения запроса: {0:F3} сек.",
                        stopwatch.Elapsed.TotalSeconds);
                }
            }
        }



        static async Task UpdateProductAsync()
        {
            Console.Write("\nВведите ID продукта для обновления: ");

            int id;
            if (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Ошибка: ID должен быть числом.");
                return;
            }

            Console.Write("Введите новую калорийность: ");

            int calories;
            if (!int.TryParse(Console.ReadLine(), out calories) || calories < 0)
            {
                Console.WriteLine(
                    "Ошибка: калорийность должна быть неотрицательным числом.");
                return;
            }

            // Получение фабрики
            DbProviderFactory factory =
                DbProviderFactories.GetFactory(providerName);

            using (DbConnection connection = factory.CreateConnection())
            {
                if (connection == null)
                    throw new Exception("Не удалось создать подключение.");

                connection.ConnectionString = connectionString;

                await connection.OpenAsync();

                using (DbCommand command = factory.CreateCommand())
                {
                    if (command == null)
                        throw new Exception("Не удалось создать команду.");

                    command.Connection = connection;

                    command.CommandText =
                        "UPDATE Products SET Calories = @Calories WHERE Id = @Id";

                    // Параметр новой калорийности
                    DbParameter caloriesParameter = command.CreateParameter();
                    caloriesParameter.ParameterName = "@Calories";
                    caloriesParameter.Value = calories;
                    command.Parameters.Add(caloriesParameter);

                    // Параметр ид продукта
                    DbParameter idParameter = command.CreateParameter();
                    idParameter.ParameterName = "@Id";
                    idParameter.Value = id;
                    command.Parameters.Add(idParameter);

                    Stopwatch stopwatch = Stopwatch.StartNew();

                    int rowsAffected = await command.ExecuteNonQueryAsync();

                    stopwatch.Stop();

                    Console.WriteLine(
                        "Время выполнения запроса: {0:F3} сек.",
                        stopwatch.Elapsed.TotalSeconds);

                    if (rowsAffected > 0)
                        Console.WriteLine("Продукт успешно обновлён!");
                    else
                        Console.WriteLine("Продукт с таким ID не найден.");
                }
            }
        }


        static async Task DeleteProductAsync()
        {
            Console.Write("\nВведите ID продукта для удаления: ");

            int id;
            if (!int.TryParse(Console.ReadLine(), out id) || id <= 0)
            {
                Console.WriteLine("Введите корректный положительный ID.");
                return;
            }

            DbProviderFactory factory =
                DbProviderFactories.GetFactory(providerName);

            using (DbConnection connection = factory.CreateConnection())
            {
                if (connection == null)
                    throw new Exception("Не удалось создать подключение.");

                connection.ConnectionString = connectionString;

                await connection.OpenAsync();

                using (DbCommand command = factory.CreateCommand())
                {
                    if (command == null)
                        throw new Exception("Не удалось создать команду.");

                    command.Connection = connection;
                    command.CommandText =
                        "DELETE FROM Products WHERE Id = @Id";

                    DbParameter parameter = command.CreateParameter();
                    parameter.ParameterName = "@Id";
                    parameter.Value = id;
                    command.Parameters.Add(parameter);

                    Stopwatch stopwatch = Stopwatch.StartNew();

                    int rowsAffected = await command.ExecuteNonQueryAsync();

                    stopwatch.Stop();

                    Console.WriteLine(
                        "Время выполнения запроса: {0:F3} сек.",
                        stopwatch.Elapsed.TotalSeconds);

                    if (rowsAffected > 0)
                        Console.WriteLine("Продукт успешно удалён!");
                    else
                        Console.WriteLine("Продукт с таким ID не найден.");
                }
            }
        }

    }
}
