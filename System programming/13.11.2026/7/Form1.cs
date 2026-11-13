using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;

public class Form1 : Form
{
    Button button;
    TextBox output;

    public Form1()
    {
        Text = "Расчёт чисел";
        Width = 500;
        Height = 300;

        button = new Button();
        button.Text = "Выполнить расчёт";
        button.Location = new System.Drawing.Point(20, 20);
        button.Click += StartCalculation;
        Controls.Add(button);

        output = new TextBox();
        output.Location = new System.Drawing.Point(20, 70);
        output.Width = 430;
        output.Height = 150;
        output.Multiline = true;
        Controls.Add(output);
    }

    private void StartCalculation(object sender, EventArgs e)
    {
        button.Enabled = false;
        output.Text = "Выполняется расчёт...";

        Thread mainThread = new Thread(Calculate);
        mainThread.Start();
    }

    private void Calculate()
    {
        int[] numbers = new int[10000];
        Random random = new Random();

        for (int i = 0; i < numbers.Length; i++)
        {
            numbers[i] = random.Next(0, 1001);
        }

        int min = 0;
        int max = 0;
        double average = 0;

        Thread minThread = new Thread(() =>
        {
            min = numbers[0];

            foreach (int number in numbers)
            {
                if (number < min)
                    min = number;
            }
        });

        Thread maxThread = new Thread(() =>
        {
            max = numbers[0];

            foreach (int number in numbers)
            {
                if (number > max)
                    max = number;
            }
        });

        Thread averageThread = new Thread(() =>
        {
            long sum = 0;

            foreach (int number in numbers)
            {
                sum += number;
            }

            average = (double)sum / numbers.Length;
        });

        minThread.Start();
        maxThread.Start();
        averageThread.Start();

        minThread.Join();
        maxThread.Join();
        averageThread.Join();

        string result =
            "Минимум: " + min + Environment.NewLine +
            "Максимум: " + max + Environment.NewLine +
            "Среднее: " + average;

        Thread fileThread = new Thread(() =>
        {
            using (StreamWriter writer = new StreamWriter("result.txt"))
            {
                writer.WriteLine("Набор чисел:");

                foreach (int number in numbers)
                {
                    writer.Write(number + " ");
                }

                writer.WriteLine();
                writer.WriteLine(result);
            }
        });

        fileThread.Start();
        fileThread.Join();

        Invoke(new Action(() =>
        {
            output.Text = result + Environment.NewLine +
                          "Данные записаны в result.txt";

            button.Enabled = true;
        }));
    }
}