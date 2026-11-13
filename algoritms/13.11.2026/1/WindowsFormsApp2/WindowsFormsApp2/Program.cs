using System;
using System.Drawing;
using System.Windows.Forms;

namespace CalculatorApp
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new CalculatorForm());
        }
    }

    public class CalculatorForm : Form
    {
        TextBox display;
        TableLayoutPanel layout;

        decimal firstNumber = 0;
        string operation = "";
        bool startNewNumber = true;

        public CalculatorForm()
        {
            Text = "Калькулятор";
            Width = 320;
            Height = 430;
            StartPosition = FormStartPosition.CenterScreen;

            layout = new TableLayoutPanel();
            layout.Dock = DockStyle.Fill;
            layout.ColumnCount = 4;
            layout.RowCount = 6;
            layout.Padding = new Padding(5);

            for (int i = 0; i < 4; i++)
                layout.ColumnStyles.Add(
                    new ColumnStyle(SizeType.Percent, 25));

            layout.RowStyles.Add(
                new RowStyle(SizeType.Percent, 18));

            for (int i = 0; i < 5; i++)
                layout.RowStyles.Add(
                    new RowStyle(SizeType.Percent, 16.4f));

            display = new TextBox();
            display.Dock = DockStyle.Fill;
            display.ReadOnly = true;
            display.Font = new Font("Arial", 20);
            display.TextAlign = HorizontalAlignment.Right;

            layout.Controls.Add(display, 0, 0);
            layout.SetColumnSpan(display, 4);

            AddButton("C", 0, 1, Clear);
            AddButton("/", 1, 1, (s, e) => SetOperation("/"));
            AddButton("*", 2, 1, (s, e) => SetOperation("*"));
            AddButton("-", 3, 1, (s, e) => SetOperation("-"));

            AddButton("7", 0, 2, (s, e) => AddDigit("7"));
            AddButton("8", 1, 2, (s, e) => AddDigit("8"));
            AddButton("9", 2, 2, (s, e) => AddDigit("9"));
            AddButton("+", 3, 2, (s, e) => SetOperation("+"));

            AddButton("4", 0, 3, (s, e) => AddDigit("4"));
            AddButton("5", 1, 3, (s, e) => AddDigit("5"));
            AddButton("6", 2, 3, (s, e) => AddDigit("6"));
            AddButton("=", 3, 3, Calculate);

            AddButton("1", 0, 4, (s, e) => AddDigit("1"));
            AddButton("2", 1, 4, (s, e) => AddDigit("2"));
            AddButton("3", 2, 4, (s, e) => AddDigit("3"));

            AddButton("0", 0, 5, (s, e) => AddDigit("0"), 2);

            Controls.Add(layout);
        }

        void AddButton(string text, int col, int row,
            EventHandler handler, int span = 1)
        {
            Button button = new Button();
            button.Text = text;
            button.Dock = DockStyle.Fill;
            button.Font = new Font("Arial", 14);

            button.Click += handler;

            layout.Controls.Add(button, col, row);
            layout.SetColumnSpan(button, span);
        }

        void AddDigit(string digit)
        {
            if (startNewNumber || display.Text == "Error")
            {
                display.Clear();
                startNewNumber = false;
            }

            display.Text += digit;
        }

        void SetOperation(string op)
        {
            if (!decimal.TryParse(display.Text, out firstNumber))
                return;

            operation = op;
            startNewNumber = true;
        }

        void Calculate(object sender, EventArgs e)
        {
            try
            {
                decimal secondNumber =
                    decimal.Parse(display.Text);

                decimal result = 0;

                if (operation == "+")
                    result = firstNumber + secondNumber;
                else if (operation == "-")
                    result = firstNumber - secondNumber;
                else if (operation == "*")
                    result = firstNumber * secondNumber;
                else if (operation == "/")
                {
                    if (secondNumber == 0)
                    {
                        display.Text = "Error";
                        operation = "";
                        return;
                    }

                    result = firstNumber / secondNumber;
                }
                else
                    return;

                display.Text = result.ToString();
                operation = "";
                startNewNumber = true;
            }
            catch
            {
                display.Text = "Error";
            }
        }

        void Clear(object sender, EventArgs e)
        {
            display.Clear();
            firstNumber = 0;
            operation = "";
            startNewNumber = true;
        }
    }
}