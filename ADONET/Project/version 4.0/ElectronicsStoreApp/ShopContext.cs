using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using EFCategory = ElectronicsStoreApp.EFModels.Category;
using EFProduct = ElectronicsStoreApp.EFModels.Product;
using EFCustomer = ElectronicsStoreApp.EFModels.Customer;
using EFOrder = ElectronicsStoreApp.EFModels.Order;
using EFOrderItem = ElectronicsStoreApp.EFModels.OrderItem;

namespace ElectronicsStoreApp
{
    // Задание 4.3 
    public class ShopContext : DbContext
    {
        public ShopContext() : base("name=ElectronicsStoreConnection")
        {
        }
        public DbSet<EFCategory> Categories { get; set; }

        public DbSet<EFProduct> Products { get; set; }

        public DbSet<EFCustomer> Customers { get; set; }

        public DbSet<EFOrder> Orders { get; set; }

        public DbSet<EFOrderItem> OrderItems { get; set; }

        // Задание 4.5
        // Метод оформления заказа
        public void PlaceOrder(int customerId, Dictionary<int, int> productsToBuy)
        {
            if (productsToBuy == null || productsToBuy.Count == 0)
                throw new ArgumentException("Список товаров пуст");

            using (var transaction = Database.BeginTransaction())
            {
                try
                {
                    if (!Customers.Any(c => c.Id == customerId))
                        throw new InvalidOperationException(
                            "Покупатель не найден");

                    var validatedItems =
                        new List<KeyValuePair<EFProduct, int>>();

                    decimal totalAmount = 0m;

                    foreach (KeyValuePair<int, int> item in productsToBuy)
                    {
                        int productId = item.Key;
                        int quantity = item.Value;

                        if (quantity <= 0)
                            throw new InvalidOperationException(
                                "Количество товара должно быть больше нуля");

                        EFProduct product =
                            Products.SingleOrDefault(p => p.Id == productId);

                        if (product == null)
                            throw new InvalidOperationException(
                                "Товар с ID " + productId + " не найден");

                        if (product.Stock < quantity)
                            throw new InvalidOperationException(
                                "Недостаточно товара " + product.Name +
                                ". На складе: " + product.Stock +
                                ", требуется: " + quantity + ".");

                        validatedItems.Add(
                            new KeyValuePair<EFProduct, int>(product, quantity));

                        totalAmount += product.Price * quantity;
                    }

                    var order = new EFOrder
                    {
                        CustomerId = customerId,
                        OrderDate = DateTime.Now,
                        TotalAmount = totalAmount
                    };

                    Orders.Add(order);
                    SaveChanges();

                    foreach (KeyValuePair<EFProduct, int> item in validatedItems)
                    {
                        EFProduct product = item.Key;
                        int quantity = item.Value;

                        product.Stock -= quantity;

                        OrderItems.Add(new EFOrderItem
                        {
                            OrderId = order.Id,
                            ProductId = product.Id,
                            Quantity = quantity
                        });
                    }
                    SaveChanges();
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
