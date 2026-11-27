using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server
{
    public partial class Form1 : Form
    {
        Button btnStart;
        TextBox txtLog;

        public Form1()
        {
            InitializeComponent();

            // окно
            this.Text = "Сервер";
            this.Width = 600;
            this.Height = 400;

            // кнопка
            btnStart = new Button();
            btnStart.Text = "Запустить сервер";
            btnStart.Width = 150;
            btnStart.Height = 40;
            btnStart.Left = 20;
            btnStart.Top = 20;
            btnStart.Click += btnStart_Click;

            // поле для сообщений
            txtLog = new TextBox();
            txtLog.Multiline = true;
            txtLog.ReadOnly = true;
            txtLog.ScrollBars = ScrollBars.Vertical;
            txtLog.Left = 20;
            txtLog.Top = 80;
            txtLog.Width = 530;
            txtLog.Height = 250;

            // элементы на форму
            this.Controls.Add(btnStart);
            this.Controls.Add(txtLog);
        }

        private async void btnStart_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = false;

            txtLog.AppendText("Сервер запущен. Ожидание клиента...\r\n");

            try
            {
                var result = await Task.Run(() =>
                {
                    Socket server = new Socket(
                        AddressFamily.InterNetwork,
                        SocketType.Stream,
                        ProtocolType.Tcp
                    );

                    server.Bind(new IPEndPoint(IPAddress.Any, 5000));
                    server.Listen(10);

                    Socket client = server.Accept();

                    byte[] buffer = new byte[1024];
                    int count = client.Receive(buffer);

                    string message = Encoding.UTF8.GetString(buffer, 0, count);

                    IPEndPoint clientPoint =
                        (IPEndPoint)client.RemoteEndPoint;

                    string ip = clientPoint.Address.ToString();

                    string answer = "Привет, клиент!";
                    byte[] data = Encoding.UTF8.GetBytes(answer);

                    client.Send(data);

                    client.Close();
                    server.Close();

                    return (message, ip);
                });

                txtLog.AppendText(
                    $"В {DateTime.Now:HH:mm} от {result.ip} получена строка: {result.message}\r\n"
                );

                txtLog.AppendText("Отправлен ответ: Привет, клиент!\r\n");
            }
            catch (Exception ex)
            {
                txtLog.AppendText("Ошибка: " + ex.Message + "\r\n");
            }

            btnStart.Enabled = true;
        }
    }
}
