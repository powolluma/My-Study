using System.Drawing;

namespace Task2;

public class MainForm : Form
{
    private readonly TextBox number = new();
    private readonly Label result = new() { AutoSize = true };

    public MainForm()
    {
        Text = "Задача 2";
        ClientSize = new Size(360, 170);

        var button = new Button { Text = "Преобразовать", AutoSize = true };

        button.Click += (_, _) =>
        {
            if (int.TryParse(number.Text, out int n))
            {
                result.Text = $"Двоичное представление: {ConvertToBinary(n)}";
            }
            else
            {
                MessageBox.Show("Введите целое число");
            }
        };

        var layout = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(12),
            FlowDirection = FlowDirection.TopDown,
            WrapContents = false
        };

        layout.Controls.Add(new Label { Text = "Целое число:", AutoSize = true });
        number.Width = 220;
        layout.Controls.Add(number);
        layout.Controls.Add(button);
        layout.Controls.Add(result);

        Controls.Add(layout);
    }

    private static string ConvertToBinary(int n)
    {
        return Convert.ToString(n, 2);
    }
}
