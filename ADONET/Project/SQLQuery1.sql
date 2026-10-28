-- =====================================================
-- ELECTRONICS STORE
-- Альтернативный набор тестовых данных № 2
-- =====================================================

-- 1. Создание базы данных
IF DB_ID(N'ElectronicsStore') IS NULL
BEGIN
    CREATE DATABASE ElectronicsStore;
END;
GO

USE ElectronicsStore;
GO

SET NOCOUNT ON;
GO

-- =====================================================
-- 2. СОЗДАНИЕ ТАБЛИЦ
-- =====================================================

IF OBJECT_ID(N'dbo.Categories', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Categories
    (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Name NVARCHAR(100) NOT NULL UNIQUE
    );
END;
GO

IF OBJECT_ID(N'dbo.Products', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Products
    (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Name NVARCHAR(150) NOT NULL,
        CategoryId INT NOT NULL,
        Price DECIMAL(10,2) NOT NULL CHECK (Price >= 0),
        Stock INT NOT NULL CHECK (Stock >= 0),

        CONSTRAINT FK_Products_Categories
            FOREIGN KEY (CategoryId)
            REFERENCES dbo.Categories(Id)
    );
END;
GO

IF OBJECT_ID(N'dbo.Customers', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Customers
    (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        FullName NVARCHAR(150) NOT NULL,
        Email NVARCHAR(254) NOT NULL UNIQUE,
        RegistrationDate DATETIME NOT NULL
    );
END;
GO

IF OBJECT_ID(N'dbo.Orders', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Orders
    (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        CustomerId INT NOT NULL,
        OrderDate DATETIME NOT NULL,
        TotalAmount DECIMAL(12,2) NOT NULL
            CHECK (TotalAmount >= 0),

        CONSTRAINT FK_Orders_Customers
            FOREIGN KEY (CustomerId)
            REFERENCES dbo.Customers(Id)
    );
END;
GO

IF OBJECT_ID(N'dbo.OrderItems', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.OrderItems
    (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        OrderId INT NOT NULL,
        ProductId INT NOT NULL,
        Quantity INT NOT NULL CHECK (Quantity > 0),

        CONSTRAINT FK_OrderItems_Orders
            FOREIGN KEY (OrderId)
            REFERENCES dbo.Orders(Id),

        CONSTRAINT FK_OrderItems_Products
            FOREIGN KEY (ProductId)
            REFERENCES dbo.Products(Id)
    );
END;
GO

-- =====================================================
-- 3. ОЧИСТКА ПРЕДЫДУЩИХ ДАННЫХ
-- =====================================================

DELETE FROM dbo.OrderItems;
DELETE FROM dbo.Orders;
DELETE FROM dbo.Products;
DELETE FROM dbo.Customers;
DELETE FROM dbo.Categories;
GO

DBCC CHECKIDENT ('dbo.OrderItems', RESEED, 0);
DBCC CHECKIDENT ('dbo.Orders', RESEED, 0);
DBCC CHECKIDENT ('dbo.Products', RESEED, 0);
DBCC CHECKIDENT ('dbo.Customers', RESEED, 0);
DBCC CHECKIDENT ('dbo.Categories', RESEED, 0);
GO

-- =====================================================
-- 4. КАТЕГОРИИ
-- =====================================================

INSERT INTO dbo.Categories (Name)
VALUES
    (N'Настольные компьютеры'),
    (N'Мобильные телефоны'),
    (N'Мониторы и экраны'),
    (N'Устройства ввода'),
    (N'Наушники и гарнитуры'),
    (N'Твердотельные накопители'),
    (N'Роутеры и адаптеры'),
    (N'Оргтехника');
GO

-- =====================================================
-- 5. ТОВАРЫ
-- =====================================================

INSERT INTO dbo.Products (Name, CategoryId, Price, Stock)
VALUES
    (N'Системный блок MSI MAG', 1, 1499.99, 7),
    (N'Ноутбук ASUS VivoBook', 1, 829.99, 12),
    (N'Смартфон OnePlus Nord 4', 2, 479.99, 14),
    (N'Motorola Edge 50 Neo', 2, 399.99, 19),
    (N'Монитор Samsung Odyssey G5', 3, 329.99, 9),
    (N'Клавиатура Keychron C2', 4, 89.99, 22),
    (N'Мышь SteelSeries Rival 3', 4, 49.99, 30),
    (N'Наушники Sony WF-C700N', 5, 99.99, 16),
    (N'SSD Crucial P3 Plus 1 ТБ', 6, 74.99, 27),
    (N'Роутер TP-Link Archer AX55', 7, 109.99, 10);
GO

-- =====================================================
-- 6. ПОКУПАТЕЛИ
-- Даты регистрации рассчитываются относительно сегодня
-- =====================================================

INSERT INTO dbo.Customers (FullName, Email, RegistrationDate)
VALUES
    (N'Артём Никитин',
     N'artem.nikitin@example.com',
     DATEADD(MONTH, -20, GETDATE())),

    (N'Виктория Крылова',
     N'viktoria.krylova@example.com',
     DATEADD(MONTH, -15, GETDATE())),

    (N'Роман Титов',
     N'roman.titov@example.com',
     DATEADD(MONTH, -10, GETDATE())),

    (N'Алёна Макарова',
     N'alena.makarova@example.com',
     DATEADD(MONTH, -8, GETDATE())),

    (N'Глеб Воробьёв',
     N'gleb.vorobyev@example.com',
     DATEADD(MONTH, -6, GETDATE())),

    (N'Ксения Фомина',
     N'kseniya.fomina@example.com',
     DATEADD(MONTH, -5, GETDATE())),

    (N'Михаил Гусев',
     N'mikhail.gusev@example.com',
     DATEADD(MONTH, -2, GETDATE())),

    (N'Полина Захарова',
     N'polina.zakharova@example.com',
     DATEADD(DAY, -20, GETDATE())),

    (N'Клиент без заказов',
     N'client.without.orders@example.com',
     DATEADD(MONTH, -18, GETDATE()));
GO

-- =====================================================
-- 7. ЗАКАЗЫ
-- Первые 7 заказов созданы за последние 30 дней
-- =====================================================

INSERT INTO dbo.Orders (CustomerId, OrderDate, TotalAmount)
VALUES
    (1, DATEADD(DAY, -2, GETDATE()), 649.91),
    (2, DATEADD(DAY, -4, GETDATE()), 1159.98),
    (3, DATEADD(DAY, -7, GETDATE()), 1059.97),
    (4, DATEADD(DAY, -10, GETDATE()), 404.95),
    (5, DATEADD(DAY, -14, GETDATE()), 1499.99),
    (6, DATEADD(DAY, -18, GETDATE()), 2719.94),
    (7, DATEADD(DAY, -25, GETDATE()), 199.98),

    (1, DATEADD(DAY, -60, GETDATE()), 879.98),
    (2, DATEADD(DAY, -90, GETDATE()), 619.97),
    (3, DATEADD(DAY, -120, GETDATE()), 1499.99),
    (4, DATEADD(DAY, -45, GETDATE()), 239.97),
    (5, DATEADD(DAY, -38, GETDATE()), 329.99);
GO

-- =====================================================
-- 8. ПОЗИЦИИ ЗАКАЗОВ
-- =====================================================

INSERT INTO dbo.OrderItems (OrderId, ProductId, Quantity)
VALUES
    -- Заказ 1
    (1, 6, 5),
    (1, 7, 4),

    -- Заказ 2
    (2, 2, 1),
    (2, 5, 1),

    -- Заказ 3
    (3, 3, 2),
    (3, 8, 1),

    -- Заказ 4
    (4, 6, 2),
    (4, 9, 3),

    -- Заказ 5
    (5, 1, 1),

    -- Заказ 6
    (6, 3, 4),
    (6, 4, 2),

    -- Заказ 7
    (7, 8, 2),

    -- Старые заказы
    (8, 2, 1),
    (8, 7, 1),

    (9, 4, 1),
    (9, 10, 2),

    (10, 1, 1),

    (11, 9, 2),
    (11, 6, 1),

    (12, 5, 1);
GO

-- =====================================================
-- 9. ПРОВЕРКА КОЛИЧЕСТВА ЗАПИСЕЙ
-- =====================================================

SELECT N'Categories' AS TableName, COUNT(*) AS RecordCount
FROM dbo.Categories

UNION ALL

SELECT N'Products', COUNT(*)
FROM dbo.Products

UNION ALL

SELECT N'Customers', COUNT(*)
FROM dbo.Customers

UNION ALL

SELECT N'Orders', COUNT(*)
FROM dbo.Orders

UNION ALL

SELECT N'OrderItems', COUNT(*)
FROM dbo.OrderItems;
GO

-- =====================================================
-- 10. ПРОВЕРКА ТОП-3 ТОВАРОВ ЗА ПОСЛЕДНИЕ 30 ДНЕЙ
-- =====================================================

SELECT TOP 3
    p.Name AS ProductName,
    SUM(oi.Quantity) AS QuantitySold
FROM dbo.OrderItems AS oi
INNER JOIN dbo.Orders AS o
    ON oi.OrderId = o.Id
INNER JOIN dbo.Products AS p
    ON oi.ProductId = p.Id
WHERE o.OrderDate >= DATEADD(DAY, -30, GETDATE())
  AND o.OrderDate <= GETDATE()
GROUP BY p.Id, p.Name
ORDER BY SUM(oi.Quantity) DESC;
GO

-- =====================================================
-- 11. ПРОВЕРКА НЕАКТИВНЫХ ПОКУПАТЕЛЕЙ
-- =====================================================

SELECT
    c.FullName,
    c.Email,
    c.RegistrationDate
FROM dbo.Customers AS c
LEFT JOIN dbo.Orders AS o
    ON c.Id = o.CustomerId
WHERE c.RegistrationDate < DATEADD(MONTH, -3, GETDATE())
  AND o.Id IS NULL;
GO