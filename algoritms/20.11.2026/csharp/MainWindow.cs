using System;
using System.Globalization;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Media;

namespace BmiCalculator;

public sealed class MainWindow : Window
{
    private readonly TextBox heightInput;
    private readonly TextBox weightInput;
    private readonly TextBlock resultLabel;

    public MainWindow()
    {
        Title = "Калькулятор ИМТ";
        Width = 400;
        Height = 360;
        MinWidth = 360;
        MinHeight = 320;
        WindowStartupLocation = WindowStartupLocation.CenterScreen;

        var layout = new StackPanel
        {
            Width = 320,
            Spacing = 12,
            Margin = new Thickness(24),
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center
        };

        layout.Children.Add(new TextBlock
        {
            Text = "Калькулятор индекса массы тела",
            FontSize = 20,
            FontWeight = FontWeight.Bold,
            TextWrapping = TextWrapping.Wrap
        });

        layout.Children.Add(new TextBlock { Text = "Рост (м):" });
        heightInput = new TextBox { Watermark = "Например, 1.75" };
        layout.Children.Add(heightInput);

        layout.Children.Add(new TextBlock { Text = "Вес (кг):" });
        weightInput = new TextBox { Watermark = "Например, 70" };
        layout.Children.Add(weightInput);

        var buttons = new StackPanel
        {
            Orientation = Orientation.Horizontal,
            Spacing = 10
        };

        var calculateButton = new Button
        {
            Content = "Рассчитать",
            MinWidth = 120
        };
        var clearButton = new Button
        {
            Content = "Очистить",
            MinWidth = 100
        };

        // Подключаем события Click к методам-обработчикам.
        calculateButton.Click += Calculate_Click;
        clearButton.Click += Clear_Click;

        buttons.Children.Add(calculateButton);
        buttons.Children.Add(clearButton);
        layout.Children.Add(buttons);

        resultLabel = new TextBlock
        {
            Text = "Результат появится здесь",
            TextWrapping = TextWrapping.Wrap,
            Margin = new Thickness(0, 8, 0, 0)
        };
        layout.Children.Add(resultLabel);

        Content = layout;
    }

    private void Calculate_Click(object? sender, RoutedEventArgs e)
    {
        try
        {
            string heightText = (heightInput.Text ?? "").Trim().Replace(',', '.');
            string weightText = (weightInput.Text ?? "").Trim().Replace(',', '.');

            double height = double.Parse(heightText, CultureInfo.InvariantCulture);
            double weight = double.Parse(weightText, CultureInfo.InvariantCulture);

            if (!double.IsFinite(height) || !double.IsFinite(weight))
            {
                resultLabel.Text = "Ошибка: введите обычные числовые значения.";
                return;
            }

            if (height <= 0 || weight <= 0)
            {
                resultLabel.Text = "Ошибка: рост и вес должны быть больше нуля.";
                return;
            }

            double bmi = weight / (height * height);
            string interpretation;

            if (bmi < 18.5)
            {
                interpretation = "Недостаточный вес";
            }
            else if (bmi < 25)
            {
                interpretation = "Норма";
            }
            else if (bmi < 30)
            {
                interpretation = "Избыточный вес";
            }
            else
            {
                interpretation = "Ожирение";
            }

            resultLabel.Text = string.Format(
                CultureInfo.InvariantCulture,
                "ИМТ: {0:F2}\n{1}",
                bmi,
                interpretation
            );
        }
        catch (FormatException)
        {
            resultLabel.Text = "Ошибка: введите числа в поля роста и веса.";
        }
        catch (OverflowException)
        {
            resultLabel.Text = "Ошибка: слишком большое значение.";
        }
    }

    private void Clear_Click(object? sender, RoutedEventArgs e)
    {
        heightInput.Clear();
        weightInput.Clear();
        resultLabel.Text = "Результат появится здесь";
        heightInput.Focus();
    }
}
