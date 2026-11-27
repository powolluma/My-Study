using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server
{
    public partial class Form1 : Form
    {
        TextBox txtPort = new TextBox();
        TextBox txtLog = new TextBox();
        TextBox txtMessage = new TextBox();

        Button btnStart = new Button();
        Button btnSend = new Button();
        Button btnBye = new Button();

        ChatServer server = new ChatServer();

        bool serverComputer = false;

        public Form1()
        {
            InitializeComponent();

            this.Text = "Сервер";
            this.Width = 720;
            this.Height = 550;

            Label lblPort = new Label();
            lblPort.Text = "Порт:";
            lblPort.Left = 20;
            lblPort.Top = 22;
            lblPort.Width = 50;

            txtPort.Text = "5000";
            txtPort.Left = 75;
            txtPort.Top = 18;
            txtPort.Width = 80;

            btnStart.Text = "Запустить сервер";
            btnStart.Left = 175;
            btnStart.Top = 15;
            btnStart.Width = 160;
            btnStart.Click += BtnStart_Click;

            txtLog.Left = 20;
            txtLog.Top = 65;
            txtLog.Width = 660;
            txtLog.Height = 350;
            txtLog.Multiline = true;
            txtLog.ReadOnly = true;
            txtLog.ScrollBars = ScrollBars.Vertical;

            txtMessage.Left = 20;
            txtMessage.Top = 435;
            txtMessage.Width = 440;

            btnSend.Text = "Отправить";
            btnSend.Left = 470;
            btnSend.Top = 432;
            btnSend.Width = 100;
            btnSend.Enabled = false;
            btnSend.Click += BtnSend_Click;

            btnBye.Text = "Bye";
            btnBye.Left = 585;
            btnBye.Top = 432;
            btnBye.Width = 90;
            btnBye.Enabled = false;
            btnBye.Click += BtnBye_Click;

            this.Controls.Add(lblPort);
            this.Controls.Add(txtPort);
            this.Controls.Add(btnStart);
            this.Controls.Add(txtLog);
            this.Controls.Add(txtMessage);
            this.Controls.Add(btnSend);
            this.Controls.Add(btnBye);

            server.ClientConnected += Server_ClientConnected;
            server.MessageReceived += Server_MessageReceived;
            server.ClientDisconnected += Server_ClientDisconnected;
            server.ErrorOccurred += Server_ErrorOccurred;

            this.FormClosing += (s, e) => server.Stop();
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtPort.Text, out int port) ||
                port < 1 || port > 65535)
            {
                Log("Неверный номер порта!");
                return;
            }

            try
            {
                server.Start(port);

                Log("Сервер запущен на порту " + port);
                Log("Ожидание клиента...");

                btnStart.Enabled = false;
                txtPort.Enabled = false;
            }
            catch (Exception ex)
            {
                Log("Ошибка: " + ex.Message);
            }
        }

        private void Server_ClientConnected(string mode)
        {
            serverComputer = mode == "HC" || mode == "CC";

            Log("Клиент подключился. Режим: " + mode);

            UpdateConnection(true);
        }

        private async void Server_MessageReceived(string message)
        {
            Log("Клиент: " + message);

            if (message.Equals("Bye",
                StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            if (serverComputer)
            {
                await Task.Delay(700);

                string answer = PhraseGenerator.GetRandom();

                try
                {
                    await server.SendAsync(answer);
                    Log("Сервер: " + answer);
                }
                catch (Exception ex)
                {
                    Log("Ошибка отправки: " + ex.Message);
                }
            }
        }

        private async void BtnSend_Click(object sender, EventArgs e)
        {
            string message = txtMessage.Text.Trim();

            if (message == "")
                return;

            txtMessage.Clear();

            await SendMessage(message);
        }

        private async void BtnBye_Click(object sender, EventArgs e)
        {
            await SendMessage("Bye");
        }

        private async Task SendMessage(string message)
        {
            try
            {
                await server.SendAsync(message);

                Log("Сервер: " + message);
            }
            catch (Exception ex)
            {
                Log("Ошибка: " + ex.Message);
            }
        }

        private void Server_ClientDisconnected()
        {
            Log("Клиент отключился.");

            UpdateConnection(false);
        }

        private void Server_ErrorOccurred(string error)
        {
            Log("Ошибка: " + error);
        }

        private void UpdateConnection(bool connected)
        {
            RunOnForm(() =>
            {
                btnSend.Enabled = connected && !serverComputer;
                txtMessage.Enabled = connected && !serverComputer;
                btnBye.Enabled = connected;
            });
        }

        private void Log(string message)
        {
            RunOnForm(() =>
            {
                txtLog.AppendText(message + Environment.NewLine);
            });
        }

        private void RunOnForm(Action action)
        {
            if (IsDisposed || !IsHandleCreated)
                return;

            if (InvokeRequired)
            {
                try { BeginInvoke(action); } catch { }
            }
            else
            {
                action();
            }
        }
    }
}
