using System.Drawing;

namespace Task5;

public class MainForm : Form
{
    private readonly TextBox source = new();
    private readonly TextBox oldChar = new();
    private readonly TextBox newChar = new();
    private readonly Label result = new() { AutoSize = true };

    public MainForm()
    {
        Text = "Задача 5";
        ClientSize = new Size(360, 250);

        var button = new Button { Text = "Заменить", AutoSize = true };
        button.Click += (_, _) =>
        {
            if (oldChar.Text.Length != 1 || newChar.Text.Length != 1)
            {
                MessageBox.Show("Введите по одному символу в оба поля");
                return;
            }

            result.Text = ReplaceChar(source.Text, oldChar.Text[0], newChar.Text[0]);
        };
        var layout = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(12),
            FlowDirection = FlowDirection.TopDown,
            WrapContents = false
        };

        AddField(layout, "Строка:", source, 260);
        AddField(layout, "Старый символ:", oldChar, 80);
        AddField(layout, "Новый символ:", newChar, 80);
        layout.Controls.Add(button);
        layout.Controls.Add(result);

        Controls.Add(layout);
    }

    private static void AddField(
        FlowLayoutPanel panel,
        string caption,
        TextBox box,
        int width)
    {
        panel.Controls.Add(new Label { Text = caption, AutoSize = true });
        box.Width = width;
        panel.Controls.Add(box);
    }

    private static string ReplaceChar(string str, char oldChar, char newChar)
    {
        return str.Replace(oldChar, newChar);
    }
}
