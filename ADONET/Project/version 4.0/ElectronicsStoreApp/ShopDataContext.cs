using System.Data.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ElectronicsStoreApp
{
    // Задание 3.3
    // Класс наследник
    public class ShopDataContext : DataContext
    {
        public ShopDataContext(string connectionString) : base(connectionString)
        {
        }

        public Table<Category> Categories
        {
            get { return GetTable<Category>(); }
        }

        public Table<Product> Products
        {
            get { return GetTable<Product>(); }
        }

        public Table<Order> Orders
        {
            get { return GetTable<Order>(); }
        }

        public Table<OrderItem> OrderItems
        {
            get { return GetTable<OrderItem>(); }
        }

        // Задание 3.4
        // Запрос топ 3 продаваемых товаров
        public List<string> GetTop3BestSellingProductsLast30Days()
        {
            DateTime endDate = DateTime.Now;
            DateTime startDate = endDate.AddDays(-30);

            var query =
                from item in OrderItems
                join order in Orders
                    on item.OrderId equals order.Id
                join product in Products
                    on item.ProductId equals product.Id
                where order.OrderDate >= startDate
                      && order.OrderDate <= endDate
                group item by product.Name into productGroup

                let totalSold = productGroup.Sum(item => item.Quantity)

                orderby totalSold descending
                select new
                {
                    ProductName = productGroup.Key,
                    QuantitySold = totalSold
                };

            // Задание 3.5
            // Вывод результата 
            return query
                .Take(3)
                .AsEnumerable()
                .Select(item =>
                    $"{item.ProductName} - Продано {item.QuantitySold} шт")
                .ToList();
        }
    }
}
