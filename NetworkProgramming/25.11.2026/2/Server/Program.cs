using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        // сокет
        Socket server = new Socket(
            AddressFamily.InterNetwork,
            SocketType.Stream,
            ProtocolType.Tcp
        );

        IPEndPoint point = new IPEndPoint(IPAddress.Any, 5000);

        server.Bind(point);
        server.Listen(10);

        Console.WriteLine("Сервер запущен. Ожидание запроса...");

        while (true)
        {
            // подключение клиента
            Socket client = server.Accept();

            // запрос
            byte[] buffer = new byte[1024];
            int count = client.Receive(buffer);
            string request = Encoding.UTF8.GetString(buffer, 0, count);
            Console.WriteLine("Получен запрос: " + request);

            // ответ
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

            // ответ клиенту
            byte[] data = Encoding.UTF8.GetBytes(answer);
            client.Send(data);
            Console.WriteLine("Отправлен ответ: " + answer);

            client.Close();
        }
    }
}
