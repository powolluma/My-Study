using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        // Создание сокета
        Socket server = new Socket(
            AddressFamily.InterNetwork,
            SocketType.Stream,
            ProtocolType.Tcp
        );

        IPEndPoint point = new IPEndPoint(IPAddress.Any, 5000);

        // Запуск сервера
        server.Bind(point);
        server.Listen(10);

        Console.WriteLine("Сервер запущен. Ожидание клиента...");

        // Подключение клиента
        Socket client = server.Accept();

        // Сообщение от клиента
        byte[] buffer = new byte[1024];
        int count = client.Receive(buffer);

        string message = Encoding.UTF8.GetString(buffer, 0, count);

        // IP адрес клиента
        IPEndPoint clientPoint = (IPEndPoint)client.RemoteEndPoint;

        Console.WriteLine(
            $"В {DateTime.Now:HH:mm} от {clientPoint.Address} получена строка: {message}"
        );

        // Ответ клиенту
        string answer = "Привет, клиент!";
        byte[] data = Encoding.UTF8.GetBytes(answer);

        client.Send(data);

        // Закрытие
        client.Close();
        server.Close();
    }
}
