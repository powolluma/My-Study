using System;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace ElectronicsStoreApp
{
    // Задание 3.1 и 3.2
    // Классы сущности
    // Разметка классов  и свойств
    [Table(Name = "Categories")]
    public class Category
    {
        private EntitySet<Product> _Products = new EntitySet<Product>();

        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }

        [Column]
        public string Name { get; set; }

        [Association(
            Name = "Category_Product",
            Storage = "_Products",
            OtherKey = "CategoryId")]
        public EntitySet<Product> Products
        {
            get { return _Products; }
            set { _Products.Assign(value); }
        }
    }

    [Table(Name = "Products")]
    public class Product
    {
        private EntityRef<Category> _Category;
        private EntitySet<OrderItem> _OrderItems =
            new EntitySet<OrderItem>();

        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }

        [Column]
        public string Name { get; set; }

        [Column]
        public int CategoryId { get; set; }

        [Column]
        public decimal Price { get; set; }

        [Column]
        public int Stock { get; set; }

        [Association(
            Name = "Category_Product",
            Storage = "_Category",
            ThisKey = "CategoryId",
            OtherKey = "Id",
            IsForeignKey = true)]
        public Category Category
        {
            get { return _Category.Entity; }
            set { _Category.Entity = value; }
        }

        [Association(
            Name = "Product_OrderItem",
            Storage = "_OrderItems",
            OtherKey = "ProductId")]
        public EntitySet<OrderItem> OrderItems
        {
            get { return _OrderItems; }
            set { _OrderItems.Assign(value); }
        }
    }

    [Table(Name = "Orders")]
    public class Order
    {
        private EntitySet<OrderItem> _OrderItems =
            new EntitySet<OrderItem>();

        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }

        [Column]
        public int CustomerId { get; set; }

        [Column]
        public DateTime OrderDate { get; set; }

        [Column]
        public decimal TotalAmount { get; set; }

        [Association(
            Name = "Order_OrderItem",
            Storage = "_OrderItems",
            OtherKey = "OrderId")]
        public EntitySet<OrderItem> OrderItems
        {
            get { return _OrderItems; }
            set { _OrderItems.Assign(value); }
        }
    }

    [Table(Name = "OrderItems")]
    public class OrderItem
    {
        private EntityRef<Order> _Order;
        private EntityRef<Product> _Product;

        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }

        [Column]
        public int OrderId { get; set; }

        [Column]
        public int ProductId { get; set; }

        [Column]
        public int Quantity { get; set; }

        [Association(
            Name = "Order_OrderItem",
            Storage = "_Order",
            ThisKey = "OrderId",
            OtherKey = "Id",
            IsForeignKey = true)]
        public Order Order
        {
            get { return _Order.Entity; }
            set { _Order.Entity = value; }
        }

        [Association(
            Name = "Product_OrderItem",
            Storage = "_Product",
            ThisKey = "ProductId",
            OtherKey = "Id",
            IsForeignKey = true)]
        public Product Product
        {
            get { return _Product.Entity; }
            set { _Product.Entity = value; }
        }
    }
}
