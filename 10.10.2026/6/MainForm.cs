using System.Drawing;

namespace Task6;

public class MainForm : Form
{
    private readonly TextBox text = new();
    private readonly Label result = new() { AutoSize = true };

    public MainForm()
    {
        Text = "Задача 6";
        ClientSize = new Size(360, 160);

        var button = new Button { Text = "Посчитать", AutoSize = true };
        button.Click += (_, _) =>
        {
            result.Text = $"Количество гласных: {CountVowels(text.Text)}";
        };

        var layout = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(12),
            FlowDirection = FlowDirection.TopDown,
            WrapContents = false
        };

        layout.Controls.Add(new Label { Text = "Текст:", AutoSize = true });
        text.Width = 300;
        layout.Controls.Add(text);
        layout.Controls.Add(button);
        layout.Controls.Add(result);

        Controls.Add(layout);
    }

    private static int CountVowels(string text)
    {
        const string vowels = "аеёиоуыэюяaeiouy";
        return text.Count(character => vowels.Contains(char.ToLowerInvariant(character)));
    }
}
