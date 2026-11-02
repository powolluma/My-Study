using System;
using System.Windows;

namespace ElectronicsStoreApp
{
    public partial class MainWindow : Window
    {
        private readonly DatabaseHelper _databaseHelper;

        public MainWindow()
        {
            InitializeComponent();
            _databaseHelper = new DatabaseHelper();
        }

        private void OpenTaskWindow(TaskWindowType windowType)
        {
            TaskWindow window = new TaskWindow(windowType, _databaseHelper);
            window.Owner = this;
            StatusTextBlock.Text = "Открыто окно: " + window.WindowTitleForStatus;
            window.ShowDialog();
            StatusTextBlock.Text = "Последнее открытое окно: " + window.WindowTitleForStatus;
        }

        private void ShowProducts_Click(object sender, RoutedEventArgs e)
        {
            // Задание 1.4 окно вывода товаров 
            OpenTaskWindow(TaskWindowType.ShowProducts);
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            // Задание 1.3 окно добавления товара
            OpenTaskWindow(TaskWindowType.AddProduct);
        }

        private void CheckStock_Click(object sender, RoutedEventArgs e)
        {
            // Задание 1.5 окно проверки остатка
            OpenTaskWindow(TaskWindowType.CheckStock);
        }

        private void DataTable_Click(object sender, RoutedEventArgs e)
        {
            // Задание 2 окно 
            OpenTaskWindow(TaskWindowType.DataTable);
        }

        private void TopProducts_Click(object sender, RoutedEventArgs e)
        {
            // Задание 3 окно продаваемых товаров
            OpenTaskWindow(TaskWindowType.TopProducts);
        }

        private void PlaceOrder_Click(object sender, RoutedEventArgs e)
        {
            // Задание 4 окно оформления заказа 
            OpenTaskWindow(TaskWindowType.PlaceOrder);
        }

        private void SleepingCustomers_Click(object sender, RoutedEventArgs e)
        {
            // Задание 5 окно спящих клиентов
            OpenTaskWindow(TaskWindowType.SleepingCustomers);
        }
    }
}
