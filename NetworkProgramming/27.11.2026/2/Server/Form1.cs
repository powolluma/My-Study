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

        Socket serverSocket;
        bool serverRunning = false;

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
            btnStart.Width = 170;
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

            // сокет при закрытии окна
            this.FormClosing += Form1_FormClosing;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                serverSocket = new Socket(
                    AddressFamily.InterNetwork,
                    SocketType.Stream,
                    ProtocolType.Tcp
                );

                serverSocket.Bind(
                    new IPEndPoint(IPAddress.Any, 5000)
                );

                serverSocket.Listen(10);

                serverRunning = true;
                btnStart.Enabled = false;

                txtLog.AppendText(
                    "Сервер запущен. Ожидание запросов...\r\n"
                );

                Task.Run(() => WaitForClients());
            }
            catch (Exception ex)
            {
                txtLog.AppendText("Ошибка: " + ex.Message + "\r\n");
                serverSocket?.Close();
            }
        }

        private void WaitForClients()
        {
            while (serverRunning)
            {
                Socket client = null;

                try
                {
                    client = serverSocket.Accept();

                    byte[] buffer = new byte[1024];
                    int count = client.Receive(buffer);

                    if (count == 0)
                        continue;

                    string request = Encoding.UTF8.GetString(
                        buffer, 0, count
                    ).Trim();

                    string answer;

                    if (request == "дата")
                    {
                        answer = DateTime.Now.ToString("dd.MM.yyyy");
                    }
                    else if (request == "время")
                    {
                        answer = DateTime.Now.ToString("HH:mm:ss");
                    }
                    else
                    {
                        answer = "Неверный запрос";
                    }

                    byte[] data = Encoding.UTF8.GetBytes(answer);
                    client.Send(data);

                    string ip = ((IPEndPoint)client.RemoteEndPoint)
                        .Address.ToString();

                    string log =
                        $"От {ip} получен запрос: {request}\r\n" +
                        $"Отправлен ответ: {answer}\r\n";

                    BeginInvoke(new Action(() =>
                    {
                        txtLog.AppendText(log);
                    }));
                }
                catch (SocketException)
                {
                    break;
                }
                catch (ObjectDisposedException)
                {
                    break;
                }
                catch (Exception ex)
                {
                    try
                    {
                        BeginInvoke(new Action(() =>
                        {
                            txtLog.AppendText(
                                "Ошибка: " + ex.Message + "\r\n"
                            );
                        }));
                    }
                    catch
                    {
                        break;
                    }
                }
                finally
                {
                    client?.Close();
                }
            }
        }

        private void Form1_FormClosing(
            object sender,
            FormClosingEventArgs e)
        {
            serverRunning = false;
            serverSocket?.Close();
        }
    }
}
