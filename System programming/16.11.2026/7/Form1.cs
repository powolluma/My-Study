using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task7;

public partial class Form1 : Form
{
    TextBox number = new();
    TextBox degree = new();
    TextBox result = new();

    public Form1()
    {
        InitializeComponent();

        Text = "Задание 7";
        Width = 450;
        Height = 350;

        Button calculate = new() { Text = "Посчитать" };

        number.Width = 200;
        degree.Width = 200;
        result.Width = 350;

        result.ReadOnly = true;

        FlowLayoutPanel panel = new();
        panel.Dock = DockStyle.Fill;
        panel.FlowDirection = FlowDirection.TopDown;

        panel.Controls.Add(new Label { Text = "Введите число:" });
        panel.Controls.Add(number);

        panel.Controls.Add(new Label { Text = "Введите степень:" });
        panel.Controls.Add(degree);

        panel.Controls.Add(calculate);

        panel.Controls.Add(new Label { Text = "Результат:" });
        panel.Controls.Add(result);

        Controls.Add(panel);

        calculate.Click += async (s, e) =>
        {
            if (!double.TryParse(number.Text, out double n))
            {
                MessageBox.Show("Введите правильное число");
                return;
            }

            if (!double.TryParse(degree.Text, out double d))
            {
                MessageBox.Show("Введите правильную степень");
                return;
            }

            calculate.Enabled = false;

            double value = await Task.Run(() => Math.Pow(n, d));

            result.Text = value.ToString();

            calculate.Enabled = true;
        };
    }
}