using System.Drawing;

namespace Task8;

public class MainForm : Form
{
    private readonly NumericUpDown limit = new() { Minimum = 1, Maximum = 100000, Value = 10000 };
    private readonly ListBox results = new();

    public MainForm()
    {
        Text = "Задача 8";
        ClientSize = new Size(360, 330);

        var button = new Button { Text = "Найти числа", AutoSize = true };
        button.Click += (_, _) =>
        {
            results.Items.Clear();

            for (int n = 2; n <= (int)limit.Value; n++)
            {
                if (IsPerfectNumber(n))
                {
                    results.Items.Add(n);
                }
            }
        };

        var layout = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(12),
            FlowDirection = FlowDirection.TopDown,
            WrapContents = false
        };

        layout.Controls.Add(new Label { Text = "Предел поиска:", AutoSize = true });
        layout.Controls.Add(limit);
        layout.Controls.Add(button);

        results.Width = 310;
        results.Height = 220;
        layout.Controls.Add(results);

        Controls.Add(layout);
    }

    private static bool IsPerfectNumber(int n)
    {
        if (n < 2) return false;
        int sum = 1;
        for (int i = 2; i <= n / i; i++)
        {
            if (n % i == 0)
            {
                sum += i;

                if (i != n / i)
                {
                    sum += n / i;
                }
            }
        }
        return sum == n;
    }
}
