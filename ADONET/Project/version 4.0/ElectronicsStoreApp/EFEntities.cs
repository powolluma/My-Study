using System;
using System.Collections.Generic;

namespace ElectronicsStoreApp.EFModels
{
    // Задание 4.2
    // Сущность категорий
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Навигационное свойство
        public virtual ICollection<Product> Products { get; set; } = new HashSet<Product>();
    }

    // Задание 4.2 
    // Сущность товара
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }

        // Навигационное свойство 
        public virtual Category Category { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();
    }

    // Сущность покупателя
    public class Customer
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public DateTime RegistrationDate { get; set; }
        // Навигационное свойство
        public virtual ICollection<Order> Orders { get; set; } = new HashSet<Order>();
    }

    // Сущность заказа
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public virtual Customer Customer { get; set; }
        // Навигационное свойство
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();
    }

    // Сущность позиции заказа
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        // Навигационные свойства
        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}
