using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task8;

public partial class Form1 : Form
{
    TextBox input = new();

    Label vowelsResult = new();
    Label consonantsResult = new();
    Label symbolsResult = new();

    public Form1()
    {
        InitializeComponent();

        Text = "Задание 8";
        Width = 500;
        Height = 400;

        input.Width = 400;
        input.Height = 150;
        input.Multiline = true;

        Button calculate = new()
        {
            Text = "Посчитать"
        };

        FlowLayoutPanel panel = new();
        panel.Dock = DockStyle.Fill;
        panel.FlowDirection = FlowDirection.TopDown;

        panel.Controls.Add(new Label { Text = "Введите текст:" });
        panel.Controls.Add(input);
        panel.Controls.Add(calculate);

        panel.Controls.Add(vowelsResult);
        panel.Controls.Add(consonantsResult);
        panel.Controls.Add(symbolsResult);

        Controls.Add(panel);

        calculate.Click += async (s, e) =>
        {
            string text = input.Text;

            calculate.Enabled = false;

            var result = await Task.Run(() =>
            {
                int vowels = 0;
                int consonants = 0;
                int symbols = 0;

                string vowelLetters =
                    "аеёиоуыэюяАЕЁИОУЫЭЮЯ";

                foreach (char c in text)
                {
                    if (vowelLetters.Contains(c))
                    {
                        vowels++;
                    }
                    else if (char.IsLetter(c))
                    {
                        consonants++;
                    }
                    else
                    {
                        symbols++;
                    }
                }

                return (vowels, consonants, symbols);
            });

            vowelsResult.Text = "Гласные: " + result.vowels;
            consonantsResult.Text = "Согласные: " + result.consonants;
            symbolsResult.Text = "Другие символы: " + result.symbols;

            calculate.Enabled = true;
        };
    }
}