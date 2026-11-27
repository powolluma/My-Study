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
        Button btnDate;
        Button btnTime;
        TextBox txtLog;

        public Form1()
        {
            InitializeComponent();

            this.Text = "Клиент";
            this.Width = 600;
            this.Height = 400;

            btnDate = new Button();
            btnDate.Text = "Получить дату";
            btnDate.Width = 150;
            btnDate.Height = 40;
            btnDate.Left = 20;
            btnDate.Top = 20;
            btnDate.Click += async (sender, e) =>
                await SendRequest("дата");

            btnTime = new Button();
            btnTime.Text = "Получить время";
            btnTime.Width = 150;
            btnTime.Height = 40;
            btnTime.Left = 190;
            btnTime.Top = 20;
            btnTime.Click += async (sender, e) =>
                await SendRequest("время");

            txtLog = new TextBox();
            txtLog.Multiline = true;
            txtLog.ReadOnly = true;
            txtLog.ScrollBars = ScrollBars.Vertical;
            txtLog.Left = 20;
            txtLog.Top = 80;
            txtLog.Width = 530;
            txtLog.Height = 250;

            this.Controls.Add(btnDate);
            this.Controls.Add(btnTime);
            this.Controls.Add(txtLog);
        }

        private async Task SendRequest(string request)
        {
            btnDate.Enabled = false;
            btnTime.Enabled = false;

            try
            {
                var result = await Task.Run(() =>
                {
                    using (Socket client = new Socket(
                        AddressFamily.InterNetwork,
                        SocketType.Stream,
                        ProtocolType.Tcp))
                    {
                        IPEndPoint point = new IPEndPoint(
                            IPAddress.Parse("127.0.0.1"), 5000
                        );

                        client.Connect(point);

                        byte[] data = Encoding.UTF8.GetBytes(request);
                        client.Send(data);

                        byte[] buffer = new byte[1024];
                        int count = client.Receive(buffer);

                        string answer = Encoding.UTF8.GetString(
                            buffer, 0, count
                        );

                        string ip = ((IPEndPoint)client.RemoteEndPoint)
                            .Address.ToString();

                        return (answer, ip);
                    }
                });

                txtLog.AppendText(
                    $"В {DateTime.Now:HH:mm} от {result.ip} получена строка: {result.answer}\r\n"
                );
            }
            catch (Exception ex)
            {
                txtLog.AppendText("Ошибка: " + ex.Message + "\r\n");
            }
            finally
            {
                btnDate.Enabled = true;
                btnTime.Enabled = true;
            }
        }
    }
}
