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
        Socket client = new Socket(
            AddressFamily.InterNetwork,
            SocketType.Stream,
            ProtocolType.Tcp
        );

        // подключение
        IPEndPoint point = new IPEndPoint(
            IPAddress.Parse("127.0.0.1"), 5000
        );
        await client.ConnectAsync(point);

        // приветствие серверу
        string message = "Привет, сервер!";
        byte[] data = Encoding.UTF8.GetBytes(message);

        await client.SendAsync(data, SocketFlags.None);

        // ответ от сервера
        byte[] buffer = new byte[1024];
        int count = await client.ReceiveAsync(buffer, SocketFlags.None);
        string answer = Encoding.UTF8.GetString(buffer, 0, count);
        IPEndPoint serverPoint = (IPEndPoint)client.RemoteEndPoint;
        Console.WriteLine(
            $"В {DateTime.Now:HH:mm} от {serverPoint.Address} получена строка: {answer}"
        );

        client.Close();
    }
}
