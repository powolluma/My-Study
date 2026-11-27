using System;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class Form1 : Form
    {
        TextBox txtIP = new TextBox();
        TextBox txtPort = new TextBox();
        TextBox txtLog = new TextBox();
        TextBox txtMessage = new TextBox();

        ComboBox cmbMode = new ComboBox();

        Button btnConnect = new Button();
        Button btnSend = new Button();
        Button btnBye = new Button();

        ChatClient client = new ChatClient();

        bool clientComputer = false;

        public Form1()
        {
            InitializeComponent();

            this.Text = "Клиент";
            this.Width = 850;
            this.Height = 580;


            Label lblIP = new Label();
            lblIP.Text = "IP:";
            lblIP.Left = 20;
            lblIP.Top = 22;
            lblIP.Width = 30;

            txtIP.Text = "127.0.0.1";
            txtIP.Left = 55;
            txtIP.Top = 18;
            txtIP.Width = 120;


            Label lblPort = new Label();
            lblPort.Text = "Порт:";
            lblPort.Left = 190;
            lblPort.Top = 22;
            lblPort.Width = 45;

            txtPort.Text = "5000";
            txtPort.Left = 240;
            txtPort.Top = 18;
            txtPort.Width = 70;


            Label lblMode = new Label();
            lblMode.Text = "Режим:";
            lblMode.Left = 330;
            lblMode.Top = 22;
            lblMode.Width = 55;

            cmbMode.Left = 390;
            cmbMode.Top = 18;
            cmbMode.Width = 230;

            cmbMode.DropDownStyle = ComboBoxStyle.DropDownList;

            cmbMode.Items.AddRange(new object[]
            {
                "Человек — человек",
                "Человек — компьютер",
                "Компьютер — человек",
                "Компьютер — компьютер"
            });

            cmbMode.SelectedIndex = 0;


            btnConnect.Text = "Подсоединиться";
            btnConnect.Left = 650;
            btnConnect.Top = 15;
            btnConnect.Width = 150;
            btnConnect.Click += BtnConnect_Click;


            txtLog.Left = 20;
            txtLog.Top = 65;
            txtLog.Width = 780;
            txtLog.Height = 390;
            txtLog.Multiline = true;
            txtLog.ReadOnly = true;
            txtLog.ScrollBars = ScrollBars.Vertical;


            txtMessage.Left = 20;
            txtMessage.Top = 475;
            txtMessage.Width = 520;


            btnSend.Text = "Отправить";
            btnSend.Left = 550;
            btnSend.Top = 472;
            btnSend.Width = 110;
            btnSend.Enabled = false;
            btnSend.Click += BtnSend_Click;


            btnBye.Text = "Bye";
            btnBye.Left = 680;
            btnBye.Top = 472;
            btnBye.Width = 100;
            btnBye.Enabled = false;
            btnBye.Click += BtnBye_Click;


            this.Controls.Add(lblIP);
            this.Controls.Add(txtIP);
            this.Controls.Add(lblPort);
            this.Controls.Add(txtPort);
            this.Controls.Add(lblMode);
            this.Controls.Add(cmbMode);
            this.Controls.Add(btnConnect);
            this.Controls.Add(txtLog);
            this.Controls.Add(txtMessage);
            this.Controls.Add(btnSend);
            this.Controls.Add(btnBye);


            client.MessageReceived += Client_MessageReceived;
            client.Disconnected += Client_Disconnected;

            this.FormClosing += (s, e) => client.Close();
        }


        private async void BtnConnect_Click(object sender, EventArgs e)
        {
            string ip = txtIP.Text.Trim();

            if (!IPAddress.TryParse(ip, out _))
            {
                Log("Неверный IP-адрес!");
                return;
            }

            if (!int.TryParse(txtPort.Text, out int port) ||
                port < 1 || port > 65535)
            {
                Log("Неверный номер порта!");
                return;
            }

            string[] modes = { "HH", "HC", "CH", "CC" };

            string mode = modes[cmbMode.SelectedIndex];

            clientComputer = mode == "CH" || mode == "CC";

            try
            {
                await client.ConnectAsync(ip, port, mode);

                Log("Успешное подключение к серверу.");

                UpdateConnection(true);


                if (clientComputer)
                {
                    await SendMessage(PhraseGenerator.GetRandom());
                }
            }
            catch (Exception ex)
            {
                Log("Ошибка подключения: " + ex.Message);
            }
        }


        private async void Client_MessageReceived(string message)
        {
            Log("Сервер: " + message);

            if (message.Equals("Bye",
                StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            if (clientComputer)
            {
                await Task.Delay(700);

                string answer = PhraseGenerator.GetRandom();

                await SendMessage(answer);
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
                await client.SendAsync(message);

                Log("Клиент: " + message);
            }
            catch (Exception ex)
            {
                Log("Ошибка отправки: " + ex.Message);
            }
        }

        private void Client_Disconnected()
        {
            Log("Соединение закрыто.");

            UpdateConnection(false);
        }

        private void UpdateConnection(bool connected)
        {
            RunOnForm(() =>
            {
                btnConnect.Enabled = !connected;

                txtIP.Enabled = !connected;
                txtPort.Enabled = !connected;
                cmbMode.Enabled = !connected;

                btnSend.Enabled = connected && !clientComputer;
                txtMessage.Enabled = connected && !clientComputer;

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
