using System;
using System.Configuration;
using System.Data.Common;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data;
using Dapper;
using System.Linq;

namespace ElectronicsStoreApp
{
    public class DatabaseHelper
    {
        private readonly DbProviderFactory _factory;
        private readonly string _connectionString;

        // Задание 1.1 чтение строки подключения 
        public DatabaseHelper()
        {
            ConnectionStringSettings settings =
                ConfigurationManager.ConnectionStrings[
                    "ElectronicsStoreConnection"];

            if (settings == null)
            {
                throw new InvalidOperationException(
                    "Строка подключения не найдена в App.config");
            }

            _connectionString = settings.ConnectionString;
            _factory = DbProviderFactories.GetFactory(
                "System.Data.SqlClient");
        }
        // Задание 1.2
        public DbConnection CreateConnection()
        {
            DbConnection connection = _factory.CreateConnection();

            if (connection == null)
            {
                throw new InvalidOperationException(
                    "Не удалось создать объект подключения.");
            }

            connection.ConnectionString = _connectionString;

            return connection;
        }
        // Задание 1.2
        // Создание команды DbCommand через фабрику провайдера
        public DbCommand CreateCommand(DbConnection connection)
        {
            if (connection == null)
            {
                throw new ArgumentNullException(nameof(connection));
            }

            DbCommand command = _factory.CreateCommand();

            if (command == null)
            {
                throw new InvalidOperationException(
                    "Не удалось создать объект команды");
            }

            command.Connection = connection;

            return command;
        }

        // Задание 1.3
        // Синхронный метод добавления нового товара
        public int AddProduct(string name, int categoryId, decimal price, int stock)
        {
            DbConnection connection = null;
            DbCommand command = null;

            try
            {
                connection = CreateConnection();
                connection.Open();
                command = CreateCommand(connection);
                
                command.CommandText = @"
                INSERT INTO Products (Name, CategoryId, Price, Stock)
                VALUES (@Name, @CategoryId, @Price, @Stock)";

                DbParameter nameParam = command.CreateParameter();
                nameParam.ParameterName = "@Name";
                nameParam.Value = name;
                command.Parameters.Add(nameParam);

                DbParameter categoryParam = command.CreateParameter();
                categoryParam.ParameterName = "@CategoryId";
                categoryParam.Value = categoryId;
                command.Parameters.Add(categoryParam);

                DbParameter priceParam = command.CreateParameter();
                priceParam.ParameterName = "@Price";
                priceParam.Value = price;
                command.Parameters.Add(priceParam);

                DbParameter stockParam = command.CreateParameter();
                stockParam.ParameterName = "@Stock";
                stockParam.Value = stock;
                command.Parameters.Add(stockParam);

                return command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                    "Ошибка при добавлении товара: " + ex.Message, ex);
            }
            finally
            {
                if (command != null)
                    command.Dispose();

                if (connection != null)
                    connection.Dispose();
            }
        }

        // Задание 1.4
        // Вывод списка всех товаров
        public List<string> GetProductsWithCategories()
        {
            DbConnection connection = null;
            DbCommand command = null;
            DbDataReader reader = null;

            List<string> products = new List<string>();

            try
            {
                connection = CreateConnection();
                connection.Open();

                command = CreateCommand(connection);

                command.CommandText = @"
                SELECT c.Name, p.Name, p.Price, p.Stock
                FROM Products p
                INNER JOIN Categories c ON p.CategoryId = c.Id
                ORDER BY c.Name, p.Name";

                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string category = reader.GetString(0);
                    string name = reader.GetString(1);
                    decimal price = reader.GetDecimal(2);
                    int stock = reader.GetInt32(3);

                    products.Add(
                        $"[{category}] {name} - {price} " +
                        $"(Остаток: {stock} шт)");
                }

                return products;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                    "Ошибка при получении списка товаров: " + ex.Message, ex);
            }
            finally
            {
                if (reader != null)
                    reader.Dispose();

                if (command != null)
                    command.Dispose();

                if (connection != null)
                    connection.Dispose();
            }
        }

        public string GetProductName(int productId)
        {
            DbConnection connection = null;
            DbCommand command = null;

            try
            {
                connection = CreateConnection();
                connection.Open();
                command = CreateCommand(connection);
                command.CommandText = "SELECT Name FROM Products WHERE Id = @ProductId";

                DbParameter parameter = command.CreateParameter();
                parameter.ParameterName = "@ProductId";
                parameter.Value = productId;
                command.Parameters.Add(parameter);

                object result = command.ExecuteScalar();
                if (result == null || result == DBNull.Value)
                    return null;

                return Convert.ToString(result);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                    "Ошибка при получении названия товара: " + ex.Message, ex);
            }
            finally
            {
                if (command != null)
                    command.Dispose();
                if (connection != null)
                    connection.Dispose();
            }
        }

        // Задание 1.5
        // Асинхронный метод возврата остатка конкретного товара
        public async Task<int> GetStockAsync(int productId)
        {
            DbConnection connection = null;
            DbCommand command = null;

            try
            {
                connection = CreateConnection();
                await connection.OpenAsync();

                command = CreateCommand(connection);

                
                command.CommandText = 
                    "SELECT Stock " +
                    "FROM Products " +
                    "WHERE Id = @ProductId";

                DbParameter parameter = command.CreateParameter();
                parameter.ParameterName = "@ProductId";
                parameter.Value = productId;
                command.Parameters.Add(parameter);

                object result = await command.ExecuteScalarAsync();

                if (result == null || result == DBNull.Value)
                {
                    return 0;
                }

                return Convert.ToInt32(result);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                    "Ошибка при получении остатка товара: " + ex.Message, ex);
            }
            finally
            {
                if (command != null)
                    command.Dispose();

                if (connection != null)
                    connection.Dispose();
            }
        }
        // Задание 2.1
        // Метод загрузки таблицы Products
        public DataTable GetProductsTable()
        {
            DbConnection connection = null;
            DbCommand command = null;
            DbDataAdapter adapter = null;

            try
            {
                connection = CreateConnection();
                command = CreateCommand(connection);

                command.CommandText = @"
                    SELECT Id, Name, CategoryId, Price, Stock
                    FROM Products
                    ORDER BY Id";

                adapter = _factory.CreateDataAdapter();

                if (adapter == null)
                {
                    throw new InvalidOperationException(
                        "Не удалось создать DbDataAdapter");
                }

                adapter.SelectCommand = command;

                DataTable table = new DataTable("Products");

                connection.Open();
                adapter.Fill(table);

                return table;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                    "Ошибка при загрузке таблицы Products: " + ex.Message, ex);
            }
            finally
            {
                if (adapter != null)
                    adapter.Dispose();

                if (command != null)
                    command.Dispose();

                if (connection != null)
                    connection.Dispose();
            }
        }
        // Задание 2.2
        // Метод логики изменения цен
        public void IncreasePricesByCategory(DataTable table, int categoryId)
        {
            if (table == null)
            {
                throw new ArgumentNullException(nameof(table));
            }

            foreach (DataRow row in table.Rows)
            {
                if (row.RowState == DataRowState.Deleted)
                    continue;

                if (Convert.ToInt32(row["CategoryId"]) == categoryId)
                {
                    decimal price = Convert.ToDecimal(row["Price"]);

                    row["Price"] = Math.Round(
                        price * 1.10m,
                        2,
                        MidpointRounding.AwayFromZero);
                }
            }
        }
        // Задание 2.3
        // Добавление новой строки с товаром
        public void AddProductToTable(DataTable table, string name, int categoryId, decimal price, int stock)
        {
            if (table == null)
                throw new ArgumentNullException(nameof(table));

            DataRow newRow = table.NewRow();
            int minId = 0;

            foreach (DataRow row in table.Rows)
            {
                if (row.RowState == DataRowState.Deleted)
                    continue;

                int id = Convert.ToInt32(row["Id"]);

                if (id < minId)
                    minId = id;
            }

            newRow["Id"] = minId - 1;
            newRow["Name"] = name;
            newRow["CategoryId"] = categoryId;
            newRow["Price"] = price;
            newRow["Stock"] = stock;

            table.Rows.Add(newRow);
        }


        // Задание 2.4
        // Метод синхронизации изменений в бд
        public int SaveProductTableChanges(DataTable table)
        {
            if (table == null)
                throw new ArgumentNullException(nameof(table));

            DbConnection connection = null;
            DbCommand insertCommand = null;
            DbCommand updateCommand = null;
            DbCommand deleteCommand = null;
            DbDataAdapter adapter = null;

            try
            {
                connection = CreateConnection();

                insertCommand = CreateCommand(connection);
                updateCommand = CreateCommand(connection);
                deleteCommand = CreateCommand(connection);

                adapter = _factory.CreateDataAdapter();

                if (adapter == null)
                {
                    throw new InvalidOperationException(
                        "Не удалось создать DbDataAdapter");
                }


                insertCommand.CommandText = @"
                    INSERT INTO Products (Name, CategoryId, Price, Stock)
                    VALUES (@Name, @CategoryId, @Price, @Stock);

                    SELECT Id, Name, CategoryId, Price, Stock
                    FROM Products
                    WHERE Id = SCOPE_IDENTITY();";

                DbParameter insertName = insertCommand.CreateParameter();
                insertName.ParameterName = "@Name";
                insertName.SourceColumn = "Name";
                insertName.SourceVersion = DataRowVersion.Current;
                insertCommand.Parameters.Add(insertName);

                DbParameter insertCategory = insertCommand.CreateParameter();
                insertCategory.ParameterName = "@CategoryId";
                insertCategory.SourceColumn = "CategoryId";
                insertCategory.SourceVersion = DataRowVersion.Current;
                insertCommand.Parameters.Add(insertCategory);

                DbParameter insertPrice = insertCommand.CreateParameter();
                insertPrice.ParameterName = "@Price";
                insertPrice.SourceColumn = "Price";
                insertPrice.SourceVersion = DataRowVersion.Current;
                insertCommand.Parameters.Add(insertPrice);

                DbParameter insertStock = insertCommand.CreateParameter();
                insertStock.ParameterName = "@Stock";
                insertStock.SourceColumn = "Stock";
                insertStock.SourceVersion = DataRowVersion.Current;
                insertCommand.Parameters.Add(insertStock);
                insertCommand.UpdatedRowSource =
                    UpdateRowSource.FirstReturnedRecord;
                updateCommand.CommandText = @"
                    UPDATE Products
                    SET Name = @Name, CategoryId = @CategoryId, Price = @Price, Stock = @Stock
                    WHERE Id = @Id";

                DbParameter updateName = updateCommand.CreateParameter();
                updateName.ParameterName = "@Name";
                updateName.SourceColumn = "Name";
                updateName.SourceVersion = DataRowVersion.Current;
                updateCommand.Parameters.Add(updateName);

                DbParameter updateCategory = updateCommand.CreateParameter();
                updateCategory.ParameterName = "@CategoryId";
                updateCategory.SourceColumn = "CategoryId";
                updateCategory.SourceVersion = DataRowVersion.Current;
                updateCommand.Parameters.Add(updateCategory);

                DbParameter updatePrice = updateCommand.CreateParameter();
                updatePrice.ParameterName = "@Price";
                updatePrice.SourceColumn = "Price";
                updatePrice.SourceVersion = DataRowVersion.Current;
                updateCommand.Parameters.Add(updatePrice);

                DbParameter updateStock = updateCommand.CreateParameter();
                updateStock.ParameterName = "@Stock";
                updateStock.SourceColumn = "Stock";
                updateStock.SourceVersion = DataRowVersion.Current;
                updateCommand.Parameters.Add(updateStock);

                DbParameter updateId = updateCommand.CreateParameter();
                updateId.ParameterName = "@Id";
                updateId.SourceColumn = "Id";
                updateId.SourceVersion = DataRowVersion.Original;
                updateCommand.Parameters.Add(updateId);
                deleteCommand.CommandText =
                    "DELETE FROM Products WHERE Id = @Id";

                DbParameter deleteId = deleteCommand.CreateParameter();
                deleteId.ParameterName = "@Id";
                deleteId.SourceColumn = "Id";
                deleteId.SourceVersion = DataRowVersion.Original;

                deleteCommand.Parameters.Add(deleteId);
                adapter.InsertCommand = insertCommand;
                adapter.UpdateCommand = updateCommand;
                adapter.DeleteCommand = deleteCommand;

                connection.Open();
                return adapter.Update(table);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                    "Ошибка при сохранении изменений в Products: "
                    + ex.Message, ex);
            }
            finally
            {
                if (adapter != null)
                    adapter.Dispose();

                if (insertCommand != null)
                    insertCommand.Dispose();

                if (updateCommand != null)
                    updateCommand.Dispose();

                if (connection != null)
                    connection.Dispose();

                if (deleteCommand != null)
                    deleteCommand.Dispose();
            }
        }

        // Задание 2.5
        // Удаление строки из таблицы
        public bool DeleteProductFromTable(DataTable table, int productId)
        {
            if (table == null)
                throw new ArgumentNullException(nameof(table));

            foreach (DataRow row in table.Rows)
            {
                if (row.RowState != DataRowState.Deleted &&
                    Convert.ToInt32(row["Id"]) == productId)
                {
                    row.Delete();
                    return true;
                }
            }
            return false;
        }

        // Задание 5.3 и 5.4
        // Метод для выполнения запроса (клиенты за 3 месяца ничего не заказали)
        public List<SleepingCustomerDto> GetSleepingCustomers()
        {
            DbConnection connection = null;

            try
            {
                connection = CreateConnection();
                connection.Open();

                string sqlQuery = @"
                    SELECT
                        c.FullName,
                        c.Email,
                        c.RegistrationDate
                    FROM Customers c
                    LEFT JOIN Orders o ON c.Id = o.CustomerId
                    WHERE c.RegistrationDate < @CutoffDate
                      AND o.Id IS NULL
                    ORDER BY c.RegistrationDate";

                DateTime cutoffDate = DateTime.Now.AddMonths(-3);

                List<SleepingCustomerDto> customers =
                    connection.Query<SleepingCustomerDto>(
                        sqlQuery,
                        new { CutoffDate = cutoffDate }
                    ).ToList();

                return customers;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                    "Ошибка при получении списка неактивных клиентов: "
                    + ex.Message, ex);
            }
            finally
            {
                if (connection != null)
                    connection.Dispose();
            }
        }
    }
}