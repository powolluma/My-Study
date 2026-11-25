using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        // Сокет
        Socket client = new Socket(
            AddressFamily.InterNetwork,
            SocketType.Stream,
            ProtocolType.Tcp
        );

        IPEndPoint point = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5000);

        client.Connect(point);

        string message = "Привет, сервер!";
        byte[] data = Encoding.UTF8.GetBytes(message);

        client.Send(data);

        // ответ от сервера
        byte[] buffer = new byte[1024];
        int count = client.Receive(buffer);

        string answer = Encoding.UTF8.GetString(buffer, 0, count);

        // адрес сервера
        IPEndPoint serverPoint = (IPEndPoint)client.RemoteEndPoint;

        Console.WriteLine(
            $"В {DateTime.Now:HH:mm} от {serverPoint.Address} получена строка: {answer}"
        );

        client.Close();
    }
}
