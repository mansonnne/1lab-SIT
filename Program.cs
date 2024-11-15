using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        TcpListener server = new TcpListener(IPAddress.Any, 5000);
        server.Start();
        Console.WriteLine("Сервер запущен и ожидает подключения");

        while (true)
        {
            TcpClient client = server.AcceptTcpClient();
            Console.WriteLine("Клиент подключен.");

            NetworkStream stream = client.GetStream();
            StreamReader reader = new StreamReader(stream, Encoding.UTF8);
            StreamWriter writer = new StreamWriter(stream, Encoding.UTF8) { AutoFlush = true };

            while (true)
            {
                string message = reader.ReadLine();
                if (message != null)
                {
                    Console.WriteLine($"Получено сообщение: {message}");

                    if (message.ToLower() == "hello")
                    {
                        writer.WriteLine("Hello Client");
                    }
                    else if (message.ToLower() == "exit")
                    {
                        writer.WriteLine("Goodbye");
                        break;
                    }
                    else
                    {
                        writer.WriteLine($"Вы отправили: {message}");
                    }
                }
            }
            client.Close();
            Console.WriteLine("Клиент отключен.");
        }
    }
}
