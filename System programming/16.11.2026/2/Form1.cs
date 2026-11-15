using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task2;

public partial class Form1 : Form
{
    TextBox source = new();
    TextBox destination = new();
    NumericUpDown threads = new();
    ProgressBar progress = new();
    Label status = new();

    public Form1()
    {
        InitializeComponent();

        Text = "Задание 2";
        Width = 600;
        Height = 400;

        Button chooseSource = new() { Text = "Выбрать файл" };
        Button chooseDestination = new() { Text = "Куда копировать" };
        Button start = new() { Text = "Старт" };

        source.Width = 400;
        destination.Width = 400;

        threads.Minimum = 1;
        threads.Maximum = 32;
        threads.Value = 4;

        progress.Width = 400;

        FlowLayoutPanel panel = new();
        panel.Dock = DockStyle.Fill;
        panel.FlowDirection = FlowDirection.TopDown;

        panel.Controls.Add(new Label { Text = "Исходный файл:" });
        panel.Controls.Add(source);
        panel.Controls.Add(chooseSource);

        panel.Controls.Add(new Label { Text = "Файл назначения:" });
        panel.Controls.Add(destination);
        panel.Controls.Add(chooseDestination);

        panel.Controls.Add(new Label { Text = "Количество потоков:" });
        panel.Controls.Add(threads);

        panel.Controls.Add(start);
        panel.Controls.Add(progress);
        panel.Controls.Add(status);

        Controls.Add(panel);

        chooseSource.Click += (s, e) =>
        {
            OpenFileDialog dialog = new();

            if (dialog.ShowDialog() == DialogResult.OK)
                source.Text = dialog.FileName;
        };

        chooseDestination.Click += (s, e) =>
        {
            SaveFileDialog dialog = new();

            if (dialog.ShowDialog() == DialogResult.OK)
                destination.Text = dialog.FileName;
        };

        start.Click += async (s, e) => await CopyFile();
    }

    async Task CopyFile()
    {
        if (!File.Exists(source.Text))
        {
            MessageBox.Show("Выберите файл!");
            return;
        }

        if (string.IsNullOrWhiteSpace(destination.Text))
        {
            MessageBox.Show("Выберите место копирования");
            return;
        }

        if (Path.GetFullPath(source.Text) ==
            Path.GetFullPath(destination.Text))
        {
            MessageBox.Show("Выберите другой файл назначения");
            return;
        }

        try
        {
            long length = new FileInfo(source.Text).Length;
            long copied = 0;

            string dir = Path.GetDirectoryName(destination.Text);

            if (!string.IsNullOrEmpty(dir))
                Directory.CreateDirectory(dir);

            using (FileStream fs = new(destination.Text, FileMode.Create))
            {
                fs.SetLength(length);
            }

            progress.Value = 0;
            status.Text = "Копирование...";

            int count = (int)threads.Value;
            int blockSize = 1024 * 1024;
            int blocks = (int)Math.Ceiling((double)length / blockSize);

            Progress<long> reporter = new(value =>
            {
                if (length > 0)
                    progress.Value = Math.Min(100,
                        (int)(value * 100.0 / length));
            });

            await Task.Run(() =>
            {
                Parallel.For(0, blocks,
                    new ParallelOptions
                    {
                        MaxDegreeOfParallelism = count
                    },
                    i =>
                    {
                        long offset = (long)i * blockSize;
                        int size = (int)Math.Min(blockSize, length - offset);

                        using FileStream input = new(
                            source.Text, FileMode.Open,
                            FileAccess.Read, FileShare.Read);

                        using FileStream output = new(
                            destination.Text, FileMode.Open,
                            FileAccess.Write, FileShare.ReadWrite);

                        input.Seek(offset, SeekOrigin.Begin);
                        output.Seek(offset, SeekOrigin.Begin);

                        byte[] buffer = new byte[65536];
                        int remaining = size;

                        while (remaining > 0)
                        {
                            int read = input.Read(
                                buffer, 0, Math.Min(buffer.Length, remaining));

                            if (read == 0) break;

                            output.Write(buffer, 0, read);

                            remaining -= read;

                            long total = Interlocked.Add(ref copied, read);
                            reporter.Report(total);
                        }
                    });
            });

            progress.Value = 100;
            status.Text = "Копирование завершено";
        }
        catch (Exception ex)
        {
            MessageBox.Show("Ошибка: " + ex.Message);
            status.Text = "Ошибка копирования";
        }
    }
}