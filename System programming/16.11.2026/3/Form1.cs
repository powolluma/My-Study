using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task3;

public partial class Form1 : Form
{
    TextBox source = new();
    TextBox destination = new();

    NumericUpDown threads = new();
    ProgressBar progress = new();
    Label status = new();

    Button start = new() { Text = "Старт" };
    Button pauseButton = new() { Text = "Пауза", Enabled = false };
    Button stop = new() { Text = "Стоп", Enabled = false };

    CancellationTokenSource cts;
    ManualResetEventSlim pauseEvent;

    public Form1()
    {
        InitializeComponent();

        Text = "Задание 3";
        Width = 600;
        Height = 450;

        source.Width = 400;
        destination.Width = 400;

        threads.Minimum = 1;
        threads.Maximum = 32;
        threads.Value = 4;

        progress.Width = 400;

        Button chooseSource = new() { Text = "Выбрать файл" };
        Button chooseDestination = new() { Text = "Куда копировать" };

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
        panel.Controls.Add(pauseButton);
        panel.Controls.Add(stop);

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
        pauseButton.Click += (s, e) => PauseCopy();
        stop.Click += (s, e) => StopCopy();
    }

    async Task CopyFile()
    {
        if (!File.Exists(source.Text))
        {
            MessageBox.Show("Выберите исходный файл");
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

        long length = new FileInfo(source.Text).Length;
        long copied = 0;

        int count = (int)threads.Value;
        int blockSize = 1024 * 1024;
        int blocks = (int)Math.Ceiling((double)length / blockSize);

        CancellationTokenSource tokenSource =
            new CancellationTokenSource();

        ManualResetEventSlim pause =
            new ManualResetEventSlim(true);

        cts = tokenSource;
        pauseEvent = pause;

        try
        {
            string dir = Path.GetDirectoryName(destination.Text);

            if (!string.IsNullOrEmpty(dir))
                Directory.CreateDirectory(dir);

            using (FileStream fs = new(destination.Text, FileMode.Create))
            {
                fs.SetLength(length);
            }

            progress.Value = 0;
            status.Text = "Копирование";

            start.Enabled = false;
            pauseButton.Enabled = true;
            stop.Enabled = true;

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
                        MaxDegreeOfParallelism = count,
                        CancellationToken = tokenSource.Token
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
                            pause.Wait(tokenSource.Token);

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
        catch (OperationCanceledException)
        {
            status.Text = "Копирование остановлено";
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
        finally
        {
            pause.Set();
            pause.Dispose();
            tokenSource.Dispose();

            cts = null;
            pauseEvent = null;

            start.Enabled = true;
            pauseButton.Enabled = false;
            stop.Enabled = false;
            pauseButton.Text = "Пауза";
        }
    }

    void PauseCopy()
    {
        if (pauseEvent == null) return;

        if (pauseEvent.IsSet)
        {
            pauseEvent.Reset();
            pauseButton.Text = "Продолжить";
            status.Text = "Пауза";
        }
        else
        {
            pauseEvent.Set();
            pauseButton.Text = "Пауза";
            status.Text = "Копирование";
        }
    }

    void StopCopy()
    {
        cts?.Cancel();
        pauseEvent?.Set();
        status.Text = "Остановка";
    }
}