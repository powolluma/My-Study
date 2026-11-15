using System;
using System.Numerics;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task6;

public partial class Form1 : Form
{
    TextBox number = new();
    TextBox result = new();

    public Form1()
    {
        InitializeComponent();

        Text = "Задание 6";
        Width = 450;
        Height = 300;

        Button calculate = new() { Text = "Посчитать" };

        number.Width = 200;
        result.Width = 350;

        result.ReadOnly = true;

        FlowLayoutPanel panel = new();
        panel.Dock = DockStyle.Fill;
        panel.FlowDirection = FlowDirection.TopDown;

        panel.Controls.Add(new Label { Text = "Введите число:" });
        panel.Controls.Add(number);
        panel.Controls.Add(calculate);

        panel.Controls.Add(new Label { Text = "Результат:" });
        panel.Controls.Add(result);

        Controls.Add(panel);

        calculate.Click += async (s, e) =>
        {
            if (!int.TryParse(number.Text, out int n) || n < 0 || n > 1000)
            {
                MessageBox.Show("Введите число от 0 до 1000");
                return;
            }

            calculate.Enabled = false;

            BigInteger factorial = await Task.Run(() =>
            {
                BigInteger value = 1;

                for (int i = 1; i <= n; i++)
                    value *= i;

                return value;
            });

            result.Text = factorial.ToString();

            calculate.Enabled = true;
        };
    }
}