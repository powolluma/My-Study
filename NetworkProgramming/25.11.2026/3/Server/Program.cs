using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        // сокет
        Socket server = new Socket(
            AddressFamily.InterNetwork,
            SocketType.Stream,
            ProtocolType.Tcp
        );

        // порт
        IPEndPoint point = new IPEndPoint(IPAddress.Any, 5000);

        server.Bind(point);
        server.Listen(10);

        Console.WriteLine("Сервер запущен. Ожидание клиента...");

        while (true)
        {
            // подключение
            Socket client = await server.AcceptAsync();

            // сообщение от клиента
            byte[] buffer = new byte[1024];
            int count = await client.ReceiveAsync(buffer, SocketFlags.None);
            string message = Encoding.UTF8.GetString(buffer, 0, count);
            IPEndPoint clientPoint = (IPEndPoint)client.RemoteEndPoint;
            Console.WriteLine(
                $"В {DateTime.Now:HH:mm} от {clientPoint.Address} получена строка: {message}"
            );

            // ответ клиенту
            string answer = "Привет, клиент!";
            byte[] data = Encoding.UTF8.GetBytes(answer);
            await client.SendAsync(data, SocketFlags.None);

            client.Close();
        }
    }
}
