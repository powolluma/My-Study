using System;
using System.Text;
using System.Threading;
using System.Windows.Forms;

public class Form1 : Form
{
    NumericUpDown numStart;
    NumericUpDown numEnd;
    NumericUpDown numThreads;
    TextBox output;

    public Form1()
    {
        Text = "Многопоточность";
        Width = 700;
        Height = 500;

        Label label1 = new Label();
        label1.Text = "Начало:";
        label1.Location = new System.Drawing.Point(20, 20);
        Controls.Add(label1);

        numStart = CreateNumber(100, 20, int.MinValue, int.MaxValue, 0);

        Label label2 = new Label();
        label2.Text = "Конец:";
        label2.Location = new System.Drawing.Point(230, 20);
        Controls.Add(label2);

        numEnd = CreateNumber(300, 20, int.MinValue, int.MaxValue, 50);

        Label label3 = new Label();
        label3.Text = "Количество потоков:";
        label3.Location = new System.Drawing.Point(20, 60);
        Controls.Add(label3);

        numThreads = CreateNumber(160, 60, 1, 100, 3);

        Button button = new Button();
        button.Text = "Запустить";
        button.Location = new System.Drawing.Point(300, 60);
        button.Click += StartThreads;
        Controls.Add(button);

        output = new TextBox();
        output.Location = new System.Drawing.Point(20, 110);
        output.Width = 640;
        output.Height = 320;
        output.Multiline = true;
        output.ScrollBars = ScrollBars.Vertical;
        Controls.Add(output);
    }

    private NumericUpDown CreateNumber(
        int x, int y, decimal min, decimal max, decimal value)
    {
        NumericUpDown number = new NumericUpDown();
        number.Location = new System.Drawing.Point(x, y);
        number.Width = 100;
        number.Minimum = min;
        number.Maximum = max;
        number.Value = value;
        Controls.Add(number);

        return number;
    }

    private void StartThreads(object sender, EventArgs e)
    {
        int start = (int)numStart.Value;
        int end = (int)numEnd.Value;
        int count = (int)numThreads.Value;

        if (start > end)
        {
            MessageBox.Show("Начало не может быть больше конца");
            return;
        }

        int total = end - start + 1;
        count = Math.Min(count, total);

        Thread[] threads = new Thread[count];
        string[] results = new string[count];

        for (int i = 0; i < count; i++)
        {
            int index = i;
            int from = start + i * total / count;
            int to = start + (i + 1) * total / count - 1;

            threads[i] = new Thread(() =>
            {
                StringBuilder text = new StringBuilder();

                for (int j = from; j <= to; j++)
                {
                    text.AppendLine(
                        $"Поток {index + 1}: {j}");
                }

                results[index] = text.ToString();
            });

            threads[i].Start();
        }

        foreach (Thread thread in threads)
        {
            thread.Join();
        }

        StringBuilder allResults = new StringBuilder();

        foreach (string result in results)
        {
            allResults.Append(result);
        }

        output.Text = allResults.ToString();
    }
}