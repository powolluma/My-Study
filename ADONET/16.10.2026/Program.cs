using System;
using System.Configuration;
using System.Data; 
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            //Task1();
            //Task2();
            //Task3();
            //Task4();
            //Task5();
            //Task6();
            Task7();
        }

        // Вспомогательный метод для вывода
        static void PrintTable(DataTable table, string title = "")
        {
            if (!string.IsNullOrEmpty(title))
            {
                Console.WriteLine($"\n{title}");
            }
            foreach (DataColumn col in table.Columns)
                Console.Write(col.ColumnName.PadRight(15));
            Console.WriteLine();
            Console.WriteLine(new string('-', table.Columns.Count * 15));
            foreach (DataRow row in table.Rows)
            {
                foreach (DataColumn col in table.Columns)
                {
                    object val = row[col];
                    string s = val == DBNull.Value ? "NULL" : val.ToString();
                    Console.Write(s.PadRight(15));
                }
                Console.WriteLine();
            }
        }

        // Задание 1
        static void Task1()
        {
            // Создание DataTable
            DataTable table = new DataTable("Authors");

            // Добавление 3 колонок
            table.Columns.Add("Id", typeof(int));
            table.Columns.Add("FirstName", typeof(string));
            table.Columns.Add("LastName", typeof(string));

            // Добавление 4 строк
            DataRow row1 = table.NewRow();
            row1["Id"] = 1;
            row1["FirstName"] = "Isaac";
            row1["LastName"] = "Asimov";
            table.Rows.Add(row1);

            DataRow row2 = table.NewRow();
            row2["Id"] = 2;
            row2["FirstName"] = "Ray";
            row2["LastName"] = "Bradbury";
            table.Rows.Add(row2);

            DataRow row3 = table.NewRow();
            row3["Id"] = 3;
            row3["FirstName"] = "Clifford";
            row3["LastName"] = "Simak";
            table.Rows.Add(row3);

            DataRow row4 = table.NewRow();
            row4["Id"] = 4;
            row4["FirstName"] = "Roger";
            row4["LastName"] = "Zelazny";
            table.Rows.Add(row4);

            // Вывод 
            PrintTable(table, "Authors");

            // Демонстрация чтения значения
            if (table.Rows.Count > 0)
            {
                DataRow firstRow = table.Rows[0];
                Console.WriteLine($"Чтение через индекс row[0]: {firstRow[0]}");
                Console.WriteLine($"Чтение через имя колонки row[\"FirstName\"]: {firstRow["FirstName"]}");
            }
        }

        // Задание 2
        static void Task2()
        {
            // Cтрока подключения 
            string connectionString = ConfigurationManager.ConnectionStrings["MyConnString"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // Создание SqlDataAdapter с запросом 
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Authors", conn);

                // Создание DataSet 
                DataSet ds = new DataSet();

                // Заполнение DataSet Authors
                da.Fill(ds, "Authors");

                // Вывод БД 
                PrintTable(ds.Tables["Authors"], "Authors из БД");

                // Подключение второго запроса и загрузка двух таблиц в DataSet
                da.SelectCommand.CommandText = "SELECT * FROM Books";
                da.Fill(ds, "Books");

                // Вывод из БД таблица книг 
                PrintTable(ds.Tables["Books"], "Books из БД");
            }
        }

        // Задание 3
        static void Task3()
        {
            // Строка подключения
            string connectionString = ConfigurationManager.ConnectionStrings["MyConnString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // Создание адаптера
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Books", conn);

                // Автогенерация команд
                SqlCommandBuilder cmdBuilder = new SqlCommandBuilder(da);

                // Создание и заполнение DataSet
                DataSet ds = new DataSet();
                da.Fill(ds, "Books");
                DataTable booksTable = ds.Tables["Books"];

                // Проверка на наличие строк 
                if (booksTable.Rows.Count == 0)
                {
                    Console.WriteLine("\nНет 1данных в таблице Books");
                    return;
                }

                // Вывод до
                PrintTable(booksTable, "Books до локального изменения");

                // Смена цены книги на 999
                booksTable.Rows[0]["Price"] = 999;

                // Вывод после 
                PrintTable(booksTable, "Books после локального изменения");

                // Синхронизация изменений
                da.Update(ds, "Books");

                // Вывод в консоль текстов сгенерированных команд
                Console.WriteLine("\nТексты автоматически сгенерированных команд");
                Console.WriteLine("INSERT: " + cmdBuilder.GetInsertCommand().CommandText);
                Console.WriteLine("UPDATE: " + cmdBuilder.GetUpdateCommand().CommandText);
                Console.WriteLine("DELETE: " + cmdBuilder.GetDeleteCommand().CommandText);

                // Проблемный эксперимент
                Console.WriteLine("\nПроблемный эксперимент");
                // Замена адаптера
                SqlDataAdapter daBad = new SqlDataAdapter("SELECT Title, Price, Pages FROM Books", conn);
                // Замена команд
                SqlCommandBuilder cmdBuilderBad = new SqlCommandBuilder(daBad);
                // Замена DataSet
                DataSet dsBad = new DataSet();
                // Заполнение данными
                daBad.Fill(dsBad, "BooksBad");

                // Проверка на наличие строк в таблице
                if (dsBad.Tables["BooksBad"].Rows.Count > 0)
                {
                    // Замена данными локально
                    dsBad.Tables["BooksBad"].Rows[0]["Price"] = 1200;
                    try
                    {
                        // Синхронизация
                        daBad.Update(dsBad, "BooksBad");
                    }
                    catch (InvalidOperationException ex)
                    {
                        // Ошибка
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }

        // Задание 4
        static void Task4()
        {
            // Строка подключения
            string connectionString = ConfigurationManager.ConnectionStrings["MyConnString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // Создание адаптера
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Books", conn);
                // Создание и заполнение DataSet
                DataSet ds = new DataSet();
                da.Fill(ds, "Books");

                DataTable booksTable = ds.Tables["Books"];

                // Проверка на наличии двух книг
                if (booksTable.Rows.Count < 2)
                {
                    Console.WriteLine("\n[Task 4] В базе данных должно быть не менее 2 книг для выполнения теста.");
                    return;
                }

                // Замена цены книги 
                SqlCommand updateCmd = new SqlCommand("UPDATE Books SET Price = @pPrice WHERE Id = @pId", conn);
                // Создание параметра price
                updateCmd.Parameters.Add(new SqlParameter("@pPrice", SqlDbType.Int));
                // Параметр хранения текущей версии
                updateCmd.Parameters["@pPrice"].SourceVersion = DataRowVersion.Current;
                // Параметр хранения цены в колонке цен
                updateCmd.Parameters["@pPrice"].SourceColumn = "Price";

                // Создание параметра id
                updateCmd.Parameters.Add(new SqlParameter("@pId", SqlDbType.Int));
                // Параметр хранения оригинальной версии
                updateCmd.Parameters["@pId"].SourceVersion = DataRowVersion.Original;
                // Параметр хранения id в колонке id
                updateCmd.Parameters["@pId"].SourceColumn = "Id";

                // Назначаем созданные команды адаптеру
                da.UpdateCommand = updateCmd;

                // Замена цены книг локально
                booksTable.Rows[0]["Price"] = 450;
                booksTable.Rows[1]["Price"] = 620;

                Console.WriteLine("\nЛокальные изменения внесены");

                // Синхронизация
                da.Update(ds, "Books");
            }
        }

        // Задание 5
        static void Task5()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyConnString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Books", conn);
                SqlCommandBuilder cb = new SqlCommandBuilder(da); 
                DataSet ds = new DataSet();
                da.Fill(ds, "Books");

                DataTable books = ds.Tables["Books"];
                if (books.Rows.Count < 2)
                {
                    Console.WriteLine("\nНедостаточно строк в Books");
                    return;
                }

                // Удаление первой строки локально
                books.Rows[0].Delete();

                // Изменение цены второй строки локально
                books.Rows[1]["Price"] = 777;

                // Добавление новой строки локально
                DataRow newRow = books.NewRow();
                newRow["AuthorId"] = 1;
                newRow["Title"] = "New Book";
                newRow["Price"] = 500;
                newRow["Pages"] = 100;
                books.Rows.Add(newRow);

                // Вывод всех строк локальной таблицы 
                Console.WriteLine("\nТекущие состояния строк ");
                foreach (DataRow row in books.Rows)
                {
                    if (row.RowState == DataRowState.Deleted)
                    {
                        continue;
                    }
                    Console.WriteLine($"{row["Title"],-20} -> {row.RowState}");
                }

                // Синхронизация в правильном порядке
                da.Update(books.Select(null, null, DataViewRowState.Deleted));
                da.Update(books.Select(null, null, DataViewRowState.ModifiedCurrent));
                da.Update(books.Select(null, null, DataViewRowState.Added));

                // Перечитывание данных
                DataSet dsVerify = new DataSet();
                da.Fill(dsVerify, "Books");
                PrintTable(dsVerify.Tables["Books"], "Books после синхронизации");
            }
        }

        static void Task6()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyConnString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // Запрос
                string sql = "SELECT * FROM Authors; SELECT * FROM Books;";
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);

                DataSet dsDefault = new DataSet();
                da.Fill(dsDefault);
                
                Console.WriteLine("\nИмена таблиц по умолчанию");
                foreach (DataTable t in dsDefault.Tables)
                {
                    Console.WriteLine($"Имя таблицы в DataSet: {t.TableName}");
                }

                SqlDataAdapter daWithMaps = new SqlDataAdapter(sql, conn);

                // Маппинг Authors 
                DataTableMapping dtm = daWithMaps.TableMappings.Add("Table", "Authors");

                dtm.ColumnMappings.Add("Id", "Num");
                dtm.ColumnMappings.Add("FirstName", "Name");
                dtm.ColumnMappings.Add("LastName", "Surname");

                // Маппинг для Books 
                daWithMaps.TableMappings.Add("Table1", "Books");

                // Теперь можно спокойно заполнять DataSet
                DataSet dsMapped = new DataSet();
                daWithMaps.Fill(dsMapped);

            }
        }
        // Задание 7
        static void Task7()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyConnString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // Создание адаптера 
                SqlDataAdapter da = new SqlDataAdapter("SELECT Picture FROM Pictures WHERE Id = @id", conn);
                da.SelectCommand.Parameters.Add("@id", SqlDbType.Int).Value = 1;

                DataSet ds = new DataSet();
                da.Fill(ds);

                // Извлекаем массив байтов из строки DataTable
                byte[] bytes = (byte[])ds.Tables[0].Rows[0]["Picture"];

                // Записываем массива байтов 
                System.IO.File.WriteAllBytes("out.jpg", bytes);
            }


        }
    }
}
