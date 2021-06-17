﻿using System;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
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

            while (true)
            {
                var connection = await serverListener.AcceptTcpClientAsync();

                var networkStream = connection.GetStream();

                var responseBody = "<h1>Здрасти!</h1>";
                var contentLength = Encoding.UTF8.GetByteCount(responseBody);

                var response = @$"HTTP/1.1 200 OK    
Content-Length: {contentLength}    
Content-Type: text/html; charset=UTF-8

{responseBody}";

                byte[] responseByte = Encoding.UTF8.GetBytes(response);

                await networkStream.WriteAsync(responseByte);

                connection.Close();
            }
        }
    }
}