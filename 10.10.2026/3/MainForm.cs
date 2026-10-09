using System.Drawing;

namespace Task3;

public class MainForm : Form
{
    private readonly TextBox numbers = new();
    private readonly ListBox results = new();

    public MainForm()
    {
        Text = "Задача 3";
        ClientSize = new Size(380, 300);

        var button = new Button { Text = "Сортировать", AutoSize = true };
        button.Click += (_, _) =>
        {
            var parts = numbers.Text.Split(
                (char[]?)null,
                StringSplitOptions.RemoveEmptyEntries);

            if (!parts.All(part => int.TryParse(part, out _)))
            {
                MessageBox.Show("Введите целые числа через пробел");
                return;
            }

            int[] values = parts.Select(int.Parse).ToArray();
            int[] original = (int[])values.Clone();
            SortArray(values);

            results.Items.Clear();
            results.Items.Add($"Исходный: {string.Join(" ", original)}");
            results.Items.Add($"Отсортированный: {string.Join(" ", values)}");
        };
        var layout = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(12),
            FlowDirection = FlowDirection.TopDown,
            WrapContents = false
        };

        layout.Controls.Add(new Label { Text = "Числа через пробел:", AutoSize = true });
        numbers.Width = 330;
        layout.Controls.Add(numbers);
        layout.Controls.Add(button);

        results.Width = 330;
        results.Height = 190;
        layout.Controls.Add(results);

        Controls.Add(layout);
    }

    private static void SortArray(int[] arr)
    {
        Array.Sort(arr);
    }
}
