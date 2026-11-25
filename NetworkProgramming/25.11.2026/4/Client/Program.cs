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
        Console.InputEncoding = Encoding.UTF8;

        // запрос пользователю
        Console.WriteLine("Что запросить у сервера?");
        Console.WriteLine("Введите: дата или время");
        string request = Console.ReadLine().ToLower();

        // запрос
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

        // подключение к серверу
        IPEndPoint point = new IPEndPoint(
            IPAddress.Parse("127.0.0.1"), 5000
        );
        await client.ConnectAsync(point);

        // запрос
        byte[] data = Encoding.UTF8.GetBytes(request);
        await client.SendAsync(data, SocketFlags.None);

        // ответ
        byte[] buffer = new byte[1024];
        int count = await client.ReceiveAsync(buffer, SocketFlags.None);
        string answer = Encoding.UTF8.GetString(buffer, 0, count);
        Console.WriteLine("Ответ сервера: " + answer);

        client.Close();
    }
}
