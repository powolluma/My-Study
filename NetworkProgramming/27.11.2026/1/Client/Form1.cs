using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class Form1 : Form
    {
        Button btnConnect;
        TextBox txtLog;

        public Form1()
        {
            InitializeComponent();

            // окно
            this.Text = "Клиент";
            this.Width = 600;
            this.Height = 400;

            // кнопка
            btnConnect = new Button();
            btnConnect.Text = "Подключиться";
            btnConnect.Width = 150;
            btnConnect.Height = 40;
            btnConnect.Left = 20;
            btnConnect.Top = 20;
            btnConnect.Click += btnConnect_Click;

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
            this.Controls.Add(btnConnect);
            this.Controls.Add(txtLog);
        }

        private async void btnConnect_Click(object sender, EventArgs e)
        {
            btnConnect.Enabled = false;

            try
            {
                var result = await Task.Run(() =>
                {
                    Socket client = new Socket(
                        AddressFamily.InterNetwork,
                        SocketType.Stream,
                        ProtocolType.Tcp
                    );

                    IPEndPoint point = new IPEndPoint(
                        IPAddress.Parse("127.0.0.1"), 5000
                    );

                    client.Connect(point);

                    string message = "Привет, сервер!";
                    byte[] data = Encoding.UTF8.GetBytes(message);

                    client.Send(data);

                    byte[] buffer = new byte[1024];
                    int count = client.Receive(buffer);

                    string answer = Encoding.UTF8.GetString(buffer, 0, count);

                    IPEndPoint serverPoint =
                        (IPEndPoint)client.RemoteEndPoint;

                    string ip = serverPoint.Address.ToString();

                    client.Close();

                    return (answer, ip);
                });

                txtLog.AppendText(
                    $"В {DateTime.Now:HH:mm} от {result.ip} получена строка: {result.answer}\r\n"
                );
            }
            catch (Exception ex)
            {
                txtLog.AppendText("Ошибка: " + ex.Message + "\r\n");
            }

            btnConnect.Enabled = true;
        }
    }
}
