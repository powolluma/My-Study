using System;
using System.Threading;
using System.Windows.Forms;

namespace Task1;

public partial class Form1 : Form
{
    Thread t1, t2, t3;
    bool running = false;

    ComboBox p1 = new();
    ComboBox p2 = new();
    ComboBox p3 = new();
    RichTextBox output = new();

    public Form1()
    {
        InitializeComponent();

        Text = "Задание 1";
        Width = 600;
        Height = 500;

        p1.Items.AddRange(new object[] { "Lowest", "Normal", "Highest" });
        p2.Items.AddRange(new object[] { "Lowest", "Normal", "Highest" });
        p3.Items.AddRange(new object[] { "Lowest", "Normal", "Highest" });

        p1.SelectedIndex = 1;
        p2.SelectedIndex = 1;
        p3.SelectedIndex = 1;

        Button start = new() { Text = "Старт" };
        Button stop = new() { Text = "Стоп" };

        output.Width = 500;
        output.Height = 250;

        FlowLayoutPanel panel = new();
        panel.Dock = DockStyle.Fill;
        panel.FlowDirection = FlowDirection.TopDown;
        panel.Controls.Add(new Label { Text = "Приоритет чисел:" });
        panel.Controls.Add(p1);
        panel.Controls.Add(new Label { Text = "Приоритет букв:" });
        panel.Controls.Add(p2);
        panel.Controls.Add(new Label { Text = "Приоритет символов:" });
        panel.Controls.Add(p3);
        panel.Controls.Add(start);
        panel.Controls.Add(stop);
        panel.Controls.Add(output);

        Controls.Add(panel);

        start.Click += (s, e) => StartThreads();
        stop.Click += (s, e) => running = false;
    }

    void StartThreads()
    {
        if (running) return;

        running = true;

        t1 = new Thread(() =>
        {
            int i = 0;

            while (running)
            {
                Print("Число: " + i++);
                Thread.Sleep(300);
            }
        });

        t2 = new Thread(() =>
        {
            char c = 'A';

            while (running)
            {
                Print("Буква: " + c);

                c++;
                if (c > 'Z') c = 'A';

                Thread.Sleep(300);
            }
        });

        t3 = new Thread(() =>
        {
            string symbols = "!@#$%^&*()";
            int i = 0;

            while (running)
            {
                Print("Символ: " + symbols[i % symbols.Length]);
                i++;

                Thread.Sleep(300);
            }
        });

        t1.Priority = GetPriority(p1);
        t2.Priority = GetPriority(p2);
        t3.Priority = GetPriority(p3);

        t1.IsBackground = true;
        t2.IsBackground = true;
        t3.IsBackground = true;

        t1.Start();
        t2.Start();
        t3.Start();
    }

    ThreadPriority GetPriority(ComboBox combo)
    {
        if (combo.SelectedIndex == 0)
            return ThreadPriority.Lowest;

        if (combo.SelectedIndex == 2)
            return ThreadPriority.Highest;

        return ThreadPriority.Normal;
    }

    void Print(string text)
    {
        if (IsDisposed || !IsHandleCreated) return;

        try
        {
            BeginInvoke(new Action(() =>
            {
                output.AppendText(text + Environment.NewLine);
            }));
        }
        catch { }
    }
}