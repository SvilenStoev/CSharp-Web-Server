using System;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace MyHTTPServer
{
    class StartUp
    {
        public static async Task Main(string[] args)
        {
            string ipAddress = "127.0.0.1";

            var address = IPAddress.Parse(ipAddress);
            int port = 6969;

            var serverListener = new TcpListener(address, port);

            serverListener.Start();

            Console.WriteLine($"Server started on port {port}...");
            Console.WriteLine("Waiting for client requests...");

            var connection = await serverListener.AcceptTcpClientAsync();

              



        }
    }
}
