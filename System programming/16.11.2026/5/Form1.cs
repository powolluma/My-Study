using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task5;

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

    CancellationTokenSource? cts;
    ManualResetEventSlim? pauseEvent;

    public Form1()
    {
        InitializeComponent();

        Text = "Задание 5";
        Width = 600;
        Height = 450;

        source.Width = 400;
        destination.Width = 400;

        threads.Minimum = 1;
        threads.Maximum = 32;
        threads.Value = 4;

        progress.Width = 400;

        Button chooseSource = new() { Text = "Выбрать папку" };
        Button chooseDestination = new() { Text = "Куда копировать" };

        FlowLayoutPanel panel = new();
        panel.Dock = DockStyle.Fill;
        panel.FlowDirection = FlowDirection.TopDown;

        panel.Controls.Add(new Label { Text = "Исходная папка:" });
        panel.Controls.Add(source);
        panel.Controls.Add(chooseSource);

        panel.Controls.Add(new Label { Text = "Папка назначения:" });
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
            FolderBrowserDialog dialog = new();

            if (dialog.ShowDialog() == DialogResult.OK)
                source.Text = dialog.SelectedPath;
        };

        chooseDestination.Click += (s, e) =>
        {
            FolderBrowserDialog dialog = new();

            if (dialog.ShowDialog() == DialogResult.OK)
                destination.Text = dialog.SelectedPath;
        };

        start.Click += async (s, e) => await CopyFolder();
        pauseButton.Click += (s, e) => PauseCopy();
        stop.Click += (s, e) => StopCopy();
    }

    async Task CopyFolder()
    {
        if (!Directory.Exists(source.Text))
        {
            MessageBox.Show("Выберите исходную папку");
            return;
        }

        if (string.IsNullOrWhiteSpace(destination.Text))
        {
            MessageBox.Show("Выберите папку назначения");
            return;
        }

        string src = Path.GetFullPath(source.Text)
            .TrimEnd(Path.DirectorySeparatorChar);

        string dest = Path.GetFullPath(destination.Text)
            .TrimEnd(Path.DirectorySeparatorChar);

        if (dest.Equals(src, StringComparison.OrdinalIgnoreCase) ||
            dest.StartsWith(src + Path.DirectorySeparatorChar,
                StringComparison.OrdinalIgnoreCase))
        {
            MessageBox.Show("Папка назначения не должна быть внутри исходной");
            return;
        }

        CancellationTokenSource tokenSource = new();
        ManualResetEventSlim pause = new(true);

        cts = tokenSource;
        pauseEvent = pause;

        try
        {
            string[] files = Directory.GetFiles(
                src, "*", SearchOption.AllDirectories);

            string[] folders = Directory.GetDirectories(
                src, "*", SearchOption.AllDirectories);

            long totalSize = 0;
            long copied = 0;

            foreach (string file in files)
                totalSize += new FileInfo(file).Length;

            Directory.CreateDirectory(dest);

            foreach (string folder in folders)
            {
                string relative = Path.GetRelativePath(src, folder);
                Directory.CreateDirectory(Path.Combine(dest, relative));
            }

            progress.Value = 0;
            status.Text = "Копирование...";

            start.Enabled = false;
            pauseButton.Enabled = true;
            stop.Enabled = true;

            int count = (int)threads.Value;

            Progress<long> reporter = new(value =>
            {
                if (totalSize > 0)
                    progress.Value = Math.Min(100,
                        (int)(value * 100.0 / totalSize));
            });

            await Task.Run(() =>
            {
                Parallel.ForEach(files,
                    new ParallelOptions
                    {
                        MaxDegreeOfParallelism = count,
                        CancellationToken = tokenSource.Token
                    },
                    file =>
                    {
                        string relative = Path.GetRelativePath(src, file);
                        string newFile = Path.Combine(dest, relative);

                        string? dir = Path.GetDirectoryName(newFile);

                        if (dir != null)
                            Directory.CreateDirectory(dir);

                        using FileStream input = new(
                            file, FileMode.Open, FileAccess.Read);

                        using FileStream output = new(
                            newFile, FileMode.Create, FileAccess.Write);

                        byte[] buffer = new byte[65536];
                        int read;

                        while (true)
                        {
                            pause.Wait(tokenSource.Token);

                            read = input.Read(buffer, 0, buffer.Length);

                            if (read == 0) break;

                            output.Write(buffer, 0, read);

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