using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    public class ChatServer
    {
        private Socket listener;
        private Socket client;

        private NetworkStream stream;
        private StreamReader reader;
        private StreamWriter writer;

        private SemaphoreSlim sendLock = new SemaphoreSlim(1, 1);

        private bool running = false;

        public event Action<string> ClientConnected;
        public event Action<string> MessageReceived;
        public event Action ClientDisconnected;
        public event Action<string> ErrorOccurred;

        // Запуск сервера
        public void Start(int port)
        {
            listener = new Socket(
                AddressFamily.InterNetwork,
                SocketType.Stream,
                ProtocolType.Tcp
            );

            listener.Bind(new IPEndPoint(IPAddress.Any, port));
            listener.Listen(10);

            running = true;

            // Начинаем ожидать клиентов
            _ = AcceptClientsAsync();
        }

        // Ожидание подключения
        private async Task AcceptClientsAsync()
        {
            while (running)
            {
                bool connected = false;

                try
                {
                    client = await listener.AcceptAsync();

                    stream = new NetworkStream(client, true);

                    reader = new StreamReader(stream, Encoding.UTF8);
                    writer = new StreamWriter(stream, new UTF8Encoding(false))
                    {
                        AutoFlush = true
                    };

                    // Получаем режим работы от клиента
                    string mode = await reader.ReadLineAsync();

                    if (mode == null)
                        continue;

                    connected = true;

                    ClientConnected?.Invoke(mode);

                    // Получаем сообщения
                    string message;

                    while (running &&
                           (message = await reader.ReadLineAsync()) != null)
                    {
                        MessageReceived?.Invoke(message);

                        if (message.Equals("Bye",
                            StringComparison.OrdinalIgnoreCase))
                        {
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (running)
                        ErrorOccurred?.Invoke(ex.Message);
                }
                finally
                {
                    CloseClient();

                    if (connected)
                        ClientDisconnected?.Invoke();
                }
            }
        }

        // Отправка сообщения
        public async Task SendAsync(string message)
        {
            await sendLock.WaitAsync();

            try
            {
                if (writer == null)
                    throw new Exception("Клиент не подключен");

                await writer.WriteLineAsync(message);
            }
            finally
            {
                sendLock.Release();
            }
        }

        // Закрытие подключения
        private void CloseClient()
        {
            try { stream?.Dispose(); } catch { }
            try { client?.Close(); } catch { }

            stream = null;
            reader = null;
            writer = null;
            client = null;
        }

        // Остановка сервера
        public void Stop()
        {
            running = false;

            try { listener?.Close(); } catch { }

            CloseClient();
        }
    }
}
