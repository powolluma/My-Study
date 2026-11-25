using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

        // Запрос
        Console.WriteLine("Что запросить у сервера?");
        Console.WriteLine("Введите: дата или время");
        string request = Console.ReadLine().ToLower();

        if (request != "дата" && request != "время")
        {
            Console.WriteLine("Неверный запрос!");
            return;
        }

        // сокет
        Socket client = new Socket(
            AddressFamily.InterNetwork,
            SocketType.Stream,
            ProtocolType.Tcp
        );

        // подключение
        IPEndPoint point = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5000);
        client.Connect(point);

        // запрос
        byte[] data = Encoding.UTF8.GetBytes(request);
        client.Send(data);

        // ответ от сервера
        byte[] buffer = new byte[1024];
        int count = client.Receive(buffer);
        string answer = Encoding.UTF8.GetString(buffer, 0, count);

        // вывод данных
        Console.WriteLine("Ответ сервера: " + answer);

        client.Close();
    }
}
