using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ElectronicsStoreApp
{
    public enum TaskWindowType
    {
        ShowProducts,
        AddProduct,
        CheckStock,
        DataTable,
        TopProducts,
        SleepingCustomers,
        PlaceOrder
    }

    public class TaskWindow : Window
    {
        private readonly TaskWindowType _windowType;
        private readonly DatabaseHelper _databaseHelper;
        private readonly StackPanel _contentPanel;
        private readonly TextBlock _statusTextBlock;

        private DataTable _productsTable;
        private readonly Dictionary<int, int> _orderItemsToBuy = new Dictionary<int, int>();
        private readonly Dictionary<int, string> _orderProductNames = new Dictionary<int, string>();

        private TextBox _productNameTextBox;
        private TextBox _productCategoryIdTextBox;
        private TextBox _productPriceTextBox;
        private TextBox _productStockTextBox;
        private TextBox _stockProductIdTextBox;
        private ListBox _resultsListBox;

        private TextBox _priceIncreaseCategoryTextBox;
        private TextBox _tableProductNameTextBox;
        private TextBox _tableProductCategoryIdTextBox;
        private TextBox _tableProductPriceTextBox;
        private TextBox _tableProductStockTextBox;
        private DataGrid _productTableGrid;

        private TextBox _orderCustomerIdTextBox;
        private TextBox _orderProductIdTextBox;
        private TextBox _orderQuantityTextBox;
        private DataGrid _orderProductCatalogGrid;
        private ListBox _orderItemsListBox;
        private TextBlock _orderResultTextBlock;

        public string WindowTitleForStatus { get; private set; }

        public TaskWindow(TaskWindowType windowType, DatabaseHelper databaseHelper)
        {
            _windowType = windowType;
            _databaseHelper = databaseHelper ?? throw new ArgumentNullException(nameof(databaseHelper));
            WindowTitleForStatus = GetTitle(windowType);

            Title = WindowTitleForStatus;
            Width = windowType == TaskWindowType.DataTable ? 950 :
                (windowType == TaskWindowType.PlaceOrder ? 820 : 620);
            Height = windowType == TaskWindowType.DataTable ? 650 :
                (windowType == TaskWindowType.PlaceOrder ? 680 : 540);
            MinWidth = windowType == TaskWindowType.DataTable ? 800 :
                (windowType == TaskWindowType.PlaceOrder ? 700 : 500);
            MinHeight = 420;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;

            Grid root = new Grid { Margin = new Thickness(12) };
            root.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            root.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

            _contentPanel = new StackPanel();
            ScrollViewer contentScrollViewer = new ScrollViewer
            {
                Content = _contentPanel,
                VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
                HorizontalScrollBarVisibility = ScrollBarVisibility.Auto
            };
            Grid.SetRow(contentScrollViewer, 0);
            root.Children.Add(contentScrollViewer);

            Border footer = new Border
            {
                BorderBrush = System.Windows.Media.Brushes.Gray,
                BorderThickness = new Thickness(0, 1, 0, 0),
                Padding = new Thickness(4, 8, 0, 0),
                Margin = new Thickness(0, 10, 0, 0)
            };
            _statusTextBlock = new TextBlock
            {
                Text = "Текущее окно: " + WindowTitleForStatus,
                TextWrapping = TextWrapping.Wrap
            };
            footer.Child = _statusTextBlock;
            Grid.SetRow(footer, 1);
            root.Children.Add(footer);

            Content = root;
            BuildWindowContent();

            Loaded += TaskWindow_Loaded;
        }

        private static string GetTitle(TaskWindowType type)
        {
            switch (type)
            {
                case TaskWindowType.ShowProducts: return "📋 Список товаров";
                case TaskWindowType.AddProduct: return "➕ Добавление товара";
                case TaskWindowType.CheckStock: return "📦 Проверка остатка";
                case TaskWindowType.DataTable: return "🗂️ Отсоединённый режим";
                case TaskWindowType.TopProducts: return "🏆 Топ-3 продаваемых товара";
                case TaskWindowType.SleepingCustomers: return "😴 Неактивные клиенты";
                case TaskWindowType.PlaceOrder: return "🛒 Оформление заказа";
                default: return "Окно задания";
            }
        }

        private void TaskWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (_windowType == TaskWindowType.ShowProducts)
                LoadProducts();
            else if (_windowType == TaskWindowType.TopProducts)
                LoadTopProducts();
            else if (_windowType == TaskWindowType.SleepingCustomers)
                LoadSleepingCustomers();
            else if (_windowType == TaskWindowType.PlaceOrder)
                LoadOrderProductCatalog();
        }

        private void BuildWindowContent()
        {
            switch (_windowType)
            {
                case TaskWindowType.ShowProducts:
                    BuildShowProductsWindow();
                    break;
                case TaskWindowType.AddProduct:
                    BuildAddProductWindow();
                    break;
                case TaskWindowType.CheckStock:
                    BuildCheckStockWindow();
                    break;
                case TaskWindowType.DataTable:
                    BuildDataTableWindow();
                    break;
                case TaskWindowType.TopProducts:
                    BuildTopProductsWindow();
                    break;
                case TaskWindowType.SleepingCustomers:
                    BuildSleepingCustomersWindow();
                    break;
                case TaskWindowType.PlaceOrder:
                    BuildPlaceOrderWindow();
                    break;
            }
        }

        private TextBox AddField(string label, string initialValue = "")
        {
            TextBlock labelBlock = new TextBlock
            {
                Text = label,
                Margin = new Thickness(0, 6, 0, 2)
            };
            _contentPanel.Children.Add(labelBlock);

            TextBox textBox = new TextBox
            {
                Text = initialValue,
                MinWidth = 180,
                Margin = new Thickness(0, 0, 0, 4),
                Padding = new Thickness(3)
            };
            _contentPanel.Children.Add(textBox);
            return textBox;
        }

        private Button AddButton(Panel panel, string text, RoutedEventHandler handler)
        {
            Button button = new Button
            {
                Content = text,
                Padding = new Thickness(8, 4, 8, 4),
                Margin = new Thickness(0, 4, 8, 4),
                MinWidth = 110,
                HorizontalAlignment = HorizontalAlignment.Left
            };
            button.Click += handler;
            panel.Children.Add(button);
            return button;
        }

        private void SetStatus(string message)
        {
            _statusTextBlock.Text = "Текущее окно: " + WindowTitleForStatus + " · " + message;
        }

        private void ShowError(string message, Exception ex)
        {
            SetStatus("Ошибка");
            MessageBox.Show(message + "\n\n" + ex.Message, "Ошибка",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private static bool TryParsePrice(string text, out decimal price)
        {
            return decimal.TryParse(text, NumberStyles.Number, CultureInfo.CurrentCulture, out price)
                || decimal.TryParse(text, NumberStyles.Number, CultureInfo.InvariantCulture, out price);
        }

        private bool TryReadProductFields(
            TextBox nameBox, TextBox categoryBox, TextBox priceBox, TextBox stockBox,
            out string name, out int categoryId, out decimal price, out int stock)
        {
            name = (nameBox.Text ?? string.Empty).Trim();
            categoryId = 0;
            price = 0m;
            stock = 0;

            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Введи название товара.", "Проверка данных", MessageBoxButton.OK, MessageBoxImage.Information);
                nameBox.Focus();
                return false;
            }
            if (!int.TryParse(categoryBox.Text, out categoryId) || categoryId <= 0)
            {
                MessageBox.Show("ID категории должен быть положительным целым числом.", "Проверка данных", MessageBoxButton.OK, MessageBoxImage.Information);
                categoryBox.Focus();
                return false;
            }
            if (!TryParsePrice(priceBox.Text, out price) || price < 0m)
            {
                MessageBox.Show("Цена должна быть числом не меньше нуля.", "Проверка данных", MessageBoxButton.OK, MessageBoxImage.Information);
                priceBox.Focus();
                return false;
            }
            if (!int.TryParse(stockBox.Text, out stock) || stock < 0)
            {
                MessageBox.Show("Остаток должен быть целым числом не меньше нуля.", "Проверка данных", MessageBoxButton.OK, MessageBoxImage.Information);
                stockBox.Focus();
                return false;
            }
            return true;
        }

        // Задание 1.4
        private void BuildShowProductsWindow()
        {
            AddButton(_contentPanel, "🔄 Обновить список", delegate { LoadProducts(); });
            _resultsListBox = new ListBox { MinHeight = 280, Margin = new Thickness(0, 4, 0, 0) };
            _contentPanel.Children.Add(_resultsListBox);
        }

        private void LoadProducts()
        {
            try
            {
                List<string> products = _databaseHelper.GetProductsWithCategories();
                _resultsListBox.ItemsSource = products;
                SetStatus("список товаров загружен · записей: " + products.Count);
            }
            catch (Exception ex)
            {
                ShowError("Не удалось загрузить список товаров.", ex);
            }
        }

        // Задание 1.3
        private void BuildAddProductWindow()
        {
            _productNameTextBox = AddField("Название товара:");
            _productCategoryIdTextBox = AddField("ID категории:");
            _productPriceTextBox = AddField("Цена:");
            _productStockTextBox = AddField("Количество на складе:", "0");
            AddButton(_contentPanel, "➕ Добавить товар", AddProduct_Click);
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            string name;
            int categoryId;
            decimal price;
            int stock;
            if (!TryReadProductFields(_productNameTextBox, _productCategoryIdTextBox, _productPriceTextBox, _productStockTextBox,
                out name, out categoryId, out price, out stock))
                return;

            try
            {
                int affectedRows = _databaseHelper.AddProduct(name, categoryId, price, stock);
                SetStatus("товар добавлен · затронуто строк: " + affectedRows);
                MessageBox.Show("Товар добавлен в базу данных.", "Готово", MessageBoxButton.OK, MessageBoxImage.Information);
                _productNameTextBox.Clear();
                _productCategoryIdTextBox.Clear();
                _productPriceTextBox.Clear();
                _productStockTextBox.Text = "0";
            }
            catch (Exception ex)
            {
                ShowError("Не удалось добавить товар.", ex);
            }
        }

        // Задание 1.5
        private void BuildCheckStockWindow()
        {
            _stockProductIdTextBox = AddField("ID товара:");
            AddButton(_contentPanel, "📦 Проверить остаток", CheckStock_Click);
            _resultsListBox = new ListBox { MinHeight = 160, Margin = new Thickness(0, 5, 0, 0) };
            _contentPanel.Children.Add(_resultsListBox);
        }

        private async void CheckStock_Click(object sender, RoutedEventArgs e)
        {
            int productId;
            if (!int.TryParse(_stockProductIdTextBox.Text, out productId) || productId <= 0)
            {
                MessageBox.Show("Введи положительный целочисленный ID товара.", "Проверка данных", MessageBoxButton.OK, MessageBoxImage.Information);
                _stockProductIdTextBox.Focus();
                return;
            }

            try
            {
                SetStatus("выполняется асинхронный запрос…");
                string productName = _databaseHelper.GetProductName(productId);
                if (productName == null)
                {
                    _resultsListBox.ItemsSource = new List<string> { "Товар с ID " + productId + " не найден." };
                    SetStatus("товар ID " + productId + " не найден");
                    return;
                }

                int stock = await _databaseHelper.GetStockAsync(productId);
                _resultsListBox.ItemsSource = new List<string>
                {
                    productName + " (ID " + productId + ") — остаток: " + stock + " шт."
                };
                SetStatus("остаток товара получен");
            }
            catch (Exception ex)
            {
                ShowError("Не удалось проверить остаток товара.", ex);
            }
        }

        // Задания 2.1 2.5 
        private void BuildDataTableWindow()
        {

            WrapPanel loadRow = new WrapPanel { Margin = new Thickness(0, 0, 0, 4) };
            AddButton(loadRow, "📥 Загрузить таблицу", LoadProductTable_Click);
            AddButton(loadRow, "💾 Сохранить изменения", SaveProductTable_Click);
            AddButton(loadRow, "🗑️ Удалить выбранную строку", DeleteProductTableRow_Click);
            _contentPanel.Children.Add(loadRow);

            WrapPanel priceRow = new WrapPanel { Margin = new Thickness(0, 0, 0, 4) };
            priceRow.Children.Add(new TextBlock { Text = "ID категории для повышения цены:", VerticalAlignment = VerticalAlignment.Center, Margin = new Thickness(0, 0, 6, 0) });
            _priceIncreaseCategoryTextBox = new TextBox { Width = 70, Margin = new Thickness(0, 0, 8, 0), Padding = new Thickness(3) };
            priceRow.Children.Add(_priceIncreaseCategoryTextBox);
            AddButton(priceRow, "📈 Повысить цены на 10%", IncreasePrices_Click);
            _contentPanel.Children.Add(priceRow);

            _productTableGrid = new DataGrid
            {
                AutoGenerateColumns = true,
                CanUserAddRows = false,
                CanUserDeleteRows = false,
                IsReadOnly = false,
                SelectionMode = DataGridSelectionMode.Single,
                SelectionUnit = DataGridSelectionUnit.FullRow,
                Height = 300,
                Margin = new Thickness(0, 4, 0, 8)
            };
            _productTableGrid.AutoGeneratingColumn += delegate(object sender, DataGridAutoGeneratingColumnEventArgs e)
            {
                if (string.Equals(e.PropertyName, "Id", StringComparison.OrdinalIgnoreCase))
                    e.Column.IsReadOnly = true;
            };
            _contentPanel.Children.Add(_productTableGrid);

            _contentPanel.Children.Add(new TextBlock { Text = "Добавить новую строку в DataTable:", FontWeight = FontWeights.Bold, Margin = new Thickness(0, 3, 0, 2) });
            _tableProductNameTextBox = AddField("Название товара:");
            _tableProductCategoryIdTextBox = AddField("ID категории:");
            _tableProductPriceTextBox = AddField("Цена:");
            _tableProductStockTextBox = AddField("Количество на складе:", "0");
            AddButton(_contentPanel, "➕ Добавить строку в DataTable", AddProductTableRow_Click);
        }

        private DataTable EnsureProductsTableLoaded()
        {
            if (_productsTable == null)
                LoadProductTable();
            if (_productsTable == null)
                throw new InvalidOperationException("Не удалось загрузить DataTable.");
            return _productsTable;
        }

        private void LoadProductTable()
        {
            _productsTable = _databaseHelper.GetProductsTable();
            _productTableGrid.ItemsSource = _productsTable.DefaultView;
        }

        private void LoadProductTable_Click(object sender, RoutedEventArgs e)
        {
            if (_productsTable != null && _productsTable.GetChanges() != null)
            {
                MessageBoxResult answer = MessageBox.Show("В таблице есть несохранённые изменения. Перезагрузка отбросит их. Продолжить?",
                    "Несохранённые изменения", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (answer != MessageBoxResult.Yes)
                    return;
            }
            try
            {
                LoadProductTable();
                SetStatus("DataTable загружена · строк: " + _productsTable.Rows.Count);
            }
            catch (Exception ex)
            {
                ShowError("Не удалось загрузить таблицу Products.", ex);
            }
        }

        private void IncreasePrices_Click(object sender, RoutedEventArgs e)
        {
            int categoryId;
            if (!int.TryParse(_priceIncreaseCategoryTextBox.Text, out categoryId) || categoryId <= 0)
            {
                MessageBox.Show("Введи положительный ID категории.", "Проверка данных", MessageBoxButton.OK, MessageBoxImage.Information);
                _priceIncreaseCategoryTextBox.Focus();
                return;
            }
            try
            {
                DataTable table = EnsureProductsTableLoaded();
                int affected = table.AsEnumerable().Count(row => row.RowState != DataRowState.Deleted && Convert.ToInt32(row["CategoryId"]) == categoryId);
                _databaseHelper.IncreasePricesByCategory(table, categoryId);
                _productTableGrid.Items.Refresh();
                SetStatus("в DataTable повышены цены категории ID " + categoryId + " · строк: " + affected + " · требуется сохранение");
            }
            catch (Exception ex)
            {
                ShowError("Не удалось увеличить цены.", ex);
            }
        }

        private void AddProductTableRow_Click(object sender, RoutedEventArgs e)
        {
            string name;
            int categoryId;
            decimal price;
            int stock;
            if (!TryReadProductFields(_tableProductNameTextBox, _tableProductCategoryIdTextBox,
                _tableProductPriceTextBox, _tableProductStockTextBox, out name, out categoryId, out price, out stock))
                return;

            try
            {
                DataTable table = EnsureProductsTableLoaded();
                _databaseHelper.AddProductToTable(table, name, categoryId, price, stock);
                _productTableGrid.Items.Refresh();
                SetStatus("строка добавлена в DataTable, но ещё не сохранена в БД");
                _tableProductNameTextBox.Clear();
                _tableProductCategoryIdTextBox.Clear();
                _tableProductPriceTextBox.Clear();
                _tableProductStockTextBox.Text = "0";
            }
            catch (Exception ex)
            {
                ShowError("Не удалось добавить строку.", ex);
            }
        }

        private void DeleteProductTableRow_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView selected = _productTableGrid.SelectedItem as DataRowView;
                if (selected == null)
                {
                    MessageBox.Show("Сначала выбери строку в таблице.", "Нет выбранной строки", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                int productId = Convert.ToInt32(selected["Id"]);
                bool deleted = _databaseHelper.DeleteProductFromTable(EnsureProductsTableLoaded(), productId);
                _productTableGrid.Items.Refresh();
                SetStatus(deleted ? "товар ID " + productId + " помечен на удаление · нажми «Сохранить изменения»" : "строка не найдена");
            }
            catch (Exception ex)
            {
                ShowError("Не удалось пометить строку на удаление.", ex);
            }
        }

        private void SaveProductTable_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataTable table = EnsureProductsTableLoaded();
                _productTableGrid.CommitEdit(DataGridEditingUnit.Cell, true);
                _productTableGrid.CommitEdit(DataGridEditingUnit.Row, true);
                if (table.GetChanges() == null)
                {
                    SetStatus("нет изменений для сохранения");
                    return;
                }
                int affectedRows = _databaseHelper.SaveProductTableChanges(table);
                LoadProductTable();
                SetStatus("изменения сохранены в SQL Server · обработано строк: " + affectedRows);
                MessageBox.Show("Изменения DataTable сохранены в SQL Server.", "Готово", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                ShowError("Не удалось сохранить изменения DataTable.", ex);
            }
        }

        // Задания 3.4 3.5 
        private void BuildTopProductsWindow()
        {
            AddButton(_contentPanel, "🔄 Обновить результат", delegate { LoadTopProducts(); });
            _resultsListBox = new ListBox { MinHeight = 260, Margin = new Thickness(0, 4, 0, 0) };
            _contentPanel.Children.Add(_resultsListBox);
        }

        private void LoadTopProducts()
        {
            try
            {
                ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["ElectronicsStoreConnection"];
                if (settings == null)
                    throw new InvalidOperationException("Строка подключения ElectronicsStoreConnection не найдена в App.config.");

                List<string> topProducts;
                using (ShopDataContext context = new ShopDataContext(settings.ConnectionString))
                    topProducts = context.GetTop3BestSellingProductsLast30Days();

                _resultsListBox.ItemsSource = topProducts;
                SetStatus("результат LINQ to SQL получен · товаров: " + topProducts.Count);
            }
            catch (Exception ex)
            {
                ShowError("Не удалось получить топ-3 товаров через LINQ to SQL.", ex);
            }
        }

        // Задания 5.3 5.4 
        private void BuildSleepingCustomersWindow()
        {
            AddButton(_contentPanel, "🔄 Обновить список", delegate { LoadSleepingCustomers(); });
            _resultsListBox = new ListBox { MinHeight = 260, Margin = new Thickness(0, 4, 0, 0) };
            _contentPanel.Children.Add(_resultsListBox);
        }

        private void LoadSleepingCustomers()
        {
            try
            {
                List<SleepingCustomerDto> customers = _databaseHelper.GetSleepingCustomers();
                _resultsListBox.ItemsSource = customers.Select(customer =>
                    customer.FullName + " — " + customer.Email + " — зарегистрирован(а): " +
                    customer.RegistrationDate.ToString("dd.MM.yyyy", CultureInfo.CurrentCulture)).ToList();
                SetStatus("список получен через Dapper · клиентов: " + customers.Count);
            }
            catch (Exception ex)
            {
                ShowError("Не удалось получить список неактивных клиентов.", ex);
            }
        }

        // Задание 4.5 
        private void BuildPlaceOrderWindow()
        {
            _contentPanel.Children.Add(new TextBlock
            {
                Text = "Товары в базе данных",
                FontWeight = FontWeights.Bold,
                Margin = new Thickness(0, 0, 0, 4)
            });
            _orderProductCatalogGrid = new DataGrid
            {
                AutoGenerateColumns = true,
                IsReadOnly = true,
                CanUserAddRows = false,
                CanUserDeleteRows = false,
                SelectionMode = DataGridSelectionMode.Single,
                SelectionUnit = DataGridSelectionUnit.FullRow,
                Height = 175,
                MinWidth = 650,
                Margin = new Thickness(0, 0, 0, 8)
            };
            _contentPanel.Children.Add(_orderProductCatalogGrid);

            _orderCustomerIdTextBox = AddField("ID покупателя:");
            _orderProductIdTextBox = AddField("ID товара:");
            _orderQuantityTextBox = AddField("Количество:", "1");
            _orderProductCatalogGrid.SelectionChanged += delegate(object sender, SelectionChangedEventArgs e)
            {
                DataRowView selected = _orderProductCatalogGrid.SelectedItem as DataRowView;
                if (selected != null)
                    _orderProductIdTextBox.Text = Convert.ToString(selected["Id"]);
            };

            WrapPanel buttons = new WrapPanel();
            AddButton(buttons, "➕ Добавить позицию", AddOrderItem_Click);
            AddButton(buttons, "🧹 Очистить позиции", ClearOrderItems_Click);
            _contentPanel.Children.Add(buttons);

            _orderItemsListBox = new ListBox { MinHeight = 100, Margin = new Thickness(0, 4, 0, 8) };
            _contentPanel.Children.Add(_orderItemsListBox);
            AddButton(_contentPanel, "✅ Оформить заказ", PlaceOrder_Click);
            _orderResultTextBlock = new TextBlock { TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 8, 0, 4) };
            _contentPanel.Children.Add(_orderResultTextBlock);
        }

        private void LoadOrderProductCatalog()
        {
            try
            {
                DataTable products = _databaseHelper.GetProductsTable();
                _orderProductCatalogGrid.ItemsSource = products.DefaultView;
                _orderProductNames.Clear();
                foreach (DataRow row in products.Rows)
                {
                    _orderProductNames[Convert.ToInt32(row["Id"])] = Convert.ToString(row["Name"]);
                }
                SetStatus("список товаров загружен · записей: " + products.Rows.Count);
            }
            catch (Exception ex)
            {
                ShowError("Не удалось загрузить список товаров для оформления заказа.", ex);
            }
        }

        private void AddOrderItem_Click(object sender, RoutedEventArgs e)
        {
            int productId;
            int quantity;
            if (!int.TryParse(_orderProductIdTextBox.Text, out productId) || productId <= 0)
            {
                MessageBox.Show("ID товара должен быть положительным целым числом.", "Проверка данных", MessageBoxButton.OK, MessageBoxImage.Information);
                _orderProductIdTextBox.Focus();
                return;
            }
            if (!int.TryParse(_orderQuantityTextBox.Text, out quantity) || quantity <= 0)
            {
                MessageBox.Show("Количество должно быть целым числом больше нуля.", "Проверка данных", MessageBoxButton.OK, MessageBoxImage.Information);
                _orderQuantityTextBox.Focus();
                return;
            }

            if (!_orderProductNames.ContainsKey(productId))
            {
                MessageBox.Show("Товар с таким ID не найден в таблице. Выбери товар из списка или проверь ID.",
                    "Товар не найден", MessageBoxButton.OK, MessageBoxImage.Information);
                _orderProductIdTextBox.Focus();
                return;
            }

            if (_orderItemsToBuy.ContainsKey(productId))
                _orderItemsToBuy[productId] += quantity;
            else
                _orderItemsToBuy.Add(productId, quantity);

            RefreshOrderItemsList();
            SetStatus("позиция добавлена · товаров в заказе: " + _orderItemsToBuy.Count);
        }

        private void RefreshOrderItemsList()
        {
            _orderItemsListBox.ItemsSource = null;
            _orderItemsListBox.ItemsSource = _orderItemsToBuy
                .OrderBy(item => item.Key)
                .Select(item => _orderProductNames[item.Key] + " (ID " + item.Key + ") — количество: " + item.Value + " шт.")
                .ToList();
        }

        private void ClearOrderItems_Click(object sender, RoutedEventArgs e)
        {
            _orderItemsToBuy.Clear();
            RefreshOrderItemsList();
            _orderResultTextBlock.Text = "Позиции заказа очищены. Заказ ещё не оформлялся.";
            SetStatus("позиции заказа очищены");
        }

        private void PlaceOrder_Click(object sender, RoutedEventArgs e)
        {
            int customerId;
            if (!int.TryParse(_orderCustomerIdTextBox.Text, out customerId) || customerId <= 0)
            {
                MessageBox.Show("ID покупателя должен быть положительным целым числом.", "Проверка данных", MessageBoxButton.OK, MessageBoxImage.Information);
                _orderCustomerIdTextBox.Focus();
                return;
            }
            if (_orderItemsToBuy.Count == 0)
            {
                MessageBox.Show("Добавь хотя бы одну позицию в заказ.", "Пустой заказ", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            try
            {
                using (ShopContext context = new ShopContext())
                    context.PlaceOrder(customerId, new Dictionary<int, int>(_orderItemsToBuy));

                string summary = "Заказ успешно оформлен. Покупатель ID " + customerId +
                    "; позиций: " + _orderItemsToBuy.Count + ". Остатки обновлены в рамках транзакции.";
                _orderResultTextBlock.Text = summary;
                SetStatus("заказ создан через Entity Framework · покупатель ID " + customerId);
                MessageBox.Show(summary, "Заказ оформлен", MessageBoxButton.OK, MessageBoxImage.Information);
                _orderItemsToBuy.Clear();
                RefreshOrderItemsList();
            }
            catch (Exception ex)
            {
                _orderResultTextBlock.Text = "Заказ не оформлен: " + ex.Message;
                ShowError("Ошибка при оформлении заказа. При ошибке операция должна быть отменена транзакцией.", ex);
            }
        }
    }
}
