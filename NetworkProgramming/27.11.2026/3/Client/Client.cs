using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Client
{
    public class ChatClient
    {
        private Socket socket;

        private NetworkStream stream;
        private StreamReader reader;
        private StreamWriter writer;

        private SemaphoreSlim sendLock = new SemaphoreSlim(1, 1);

        public event Action<string> MessageReceived;
        public event Action Disconnected;


        public async Task ConnectAsync(
            string ip, int port, string mode)
        {
            Socket newSocket = new Socket(
                AddressFamily.InterNetwork,
                SocketType.Stream,
                ProtocolType.Tcp
            );

            try
            {
                await newSocket.ConnectAsync(
                    new IPEndPoint(IPAddress.Parse(ip), port)
                );

                socket = newSocket;

                stream = new NetworkStream(socket, true);

                reader = new StreamReader(stream, Encoding.UTF8);
                writer = new StreamWriter(
                    stream, new UTF8Encoding(false))
                {
                    AutoFlush = true
                };

                await writer.WriteLineAsync(mode);

                _ = ReceiveMessagesAsync();
            }
            catch
            {
                newSocket.Close();
                Close();
                throw;
            }
        }

        private async Task ReceiveMessagesAsync()
        {
            try
            {
                string message;

                while ((message = await reader.ReadLineAsync()) != null)
                {
                    MessageReceived?.Invoke(message);

                    if (message.Equals("Bye",
                        StringComparison.OrdinalIgnoreCase))
                    {
                        break;
                    }
                }
            }
            catch
            {
            }
            finally
            {
                Close();

                Disconnected?.Invoke();
            }
        }

        public async Task SendAsync(string message)
        {
            await sendLock.WaitAsync();

            try
            {
                if (writer == null)
                    throw new Exception("Нет подключения к серверу");

                await writer.WriteLineAsync(message);
            }
            finally
            {
                sendLock.Release();
            }
        }

        public void Close()
        {
            try { stream?.Dispose(); } catch { }
            try { socket?.Close(); } catch { }

            stream = null;
            reader = null;
            writer = null;
            socket = null;
        }
    }
}
