using System.Drawing;

namespace Task10;

public class MainForm : Form
{
    private readonly TextBox sum = new();
    private readonly TextBox rate = new();
    private readonly TextBox years = new();
    private readonly Label result = new() { AutoSize = true };

    public MainForm()
    {
        Text = "Задача 10";
        ClientSize = new Size(360, 250);

        var button = new Button { Text = "Рассчитать", AutoSize = true };
        button.Click += (_, _) =>
        {
            if (!double.TryParse(sum.Text, out double amount) || amount < 0 ||
                !double.TryParse(rate.Text, out double percent) || percent < 0)
            {
                MessageBox.Show("Введите корректную сумму и ставку");
                return;
            }

            int term = 1;
            if (years.Text.Trim().Length > 0 &&
                (!int.TryParse(years.Text, out term) || term < 0))
            {
                MessageBox.Show("Введите целое число лет");
                return;
            }

            result.Text = $"Итоговая сумма: {CalculateDeposit(amount, percent, term):0.00}";
        };
        var layout = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(12),
            FlowDirection = FlowDirection.TopDown,
            WrapContents = false
        };

        AddField(layout, "Сумма:", sum);
        AddField(layout, "Ставка (%):", rate);
        AddField(layout, "Лет:", years);
        layout.Controls.Add(button);
        layout.Controls.Add(result);

        Controls.Add(layout);
    }

    private static void AddField(FlowLayoutPanel panel, string caption, TextBox box)
    {
        panel.Controls.Add(new Label { Text = caption, AutoSize = true });
        box.Width = 220;
        panel.Controls.Add(box);
    }

    private static double CalculateDeposit(double sum, double rate, int years = 1)
    {
        return sum * Math.Pow(1 + rate / 100, years);
    }
}
