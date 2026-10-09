using System.Drawing;

namespace Task9;

public class MainForm : Form
{
    private readonly NumericUpDown size = new() { Minimum = 1, Maximum = 1000, Value = 10 };
    private readonly NumericUpDown min = new() { Minimum = -100000, Maximum = 100000, Value = 0 };
    private readonly NumericUpDown max = new() { Minimum = -100000, Maximum = 100000, Value = 100 };
    private readonly ListBox results = new();

    public MainForm()
    {
        Text = "Задача 9";
        ClientSize = new Size(380, 370);

        var button = new Button { Text = "Сгенерировать", AutoSize = true };
        button.Click += (_, _) =>
        {
            if (min.Value > max.Value)
            {
                MessageBox.Show("Минимум должен быть меньше или равен максимуму");
                return;
            }

            results.Items.Clear();

            int[] values = GetRandomArray(
                (int)size.Value,
                (int)min.Value,
                (int)max.Value);

            foreach (int value in values)
            {
                results.Items.Add(value);
            }
        };
        var layout = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(12),
            FlowDirection = FlowDirection.TopDown,
            WrapContents = false
        };

        layout.Controls.Add(new Label { Text = "Размер массива:", AutoSize = true });
        layout.Controls.Add(size);
        layout.Controls.Add(new Label { Text = "Минимум:", AutoSize = true });
        layout.Controls.Add(min);
        layout.Controls.Add(new Label { Text = "Максимум:", AutoSize = true });
        layout.Controls.Add(max);
        layout.Controls.Add(button);

        results.Width = 320;
        results.Height = 180;
        layout.Controls.Add(results);

        Controls.Add(layout);
    }

    private static int[] GetRandomArray(int size, int min, int max)
    {
        var random = new Random();
        var values = new int[size];

        for (int i = 0; i < size; i++)
        {
            values[i] = random.Next(min, (int)Math.Min((long)max + 1, int.MaxValue));
        }

        return values;
    }
}
