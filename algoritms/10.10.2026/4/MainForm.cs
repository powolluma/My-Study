using System.Drawing;

namespace Task4;

public class MainForm : Form
{
    private readonly TextBox radius = new();
    private readonly TextBox pi = new();
    private readonly Label result = new() { AutoSize = true };

    public MainForm()
    {
        Text = "Задача 4";
        ClientSize = new Size(360, 220);

        var button = new Button { Text = "Вычислить", AutoSize = true };
        button.Click += (_, _) =>
        {
            if (!double.TryParse(radius.Text, out double r) || r < 0)
            {
                MessageBox.Show("Введите корректный радиус");
                return;
            }

            double p = 3.14;
            if (pi.Text.Trim().Length > 0 &&
                (!double.TryParse(pi.Text, out p) || p <= 0))
            {
                MessageBox.Show("Введите корректное значение Пи");
                return;
            }

            result.Text = $"Площадь: {CalculateCircle(r, p):0.##}";
        };
        var layout = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(12),
            FlowDirection = FlowDirection.TopDown,
            WrapContents = false
        };

        layout.Controls.Add(new Label { Text = "Радиус:", AutoSize = true });
        radius.Width = 220;
        layout.Controls.Add(radius);
        layout.Controls.Add(new Label { Text = "Значение Пи (необязательно):", AutoSize = true });
        pi.Width = 220;
        layout.Controls.Add(pi);
        layout.Controls.Add(button);
        layout.Controls.Add(result);

        Controls.Add(layout);
    }

    private static double CalculateCircle(double r, double pi = 3.14)
    {
        return pi * r * r;
    }
}
