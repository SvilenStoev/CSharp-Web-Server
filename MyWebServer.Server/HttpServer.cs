﻿using MyWebServer.Server.Http;
using MyWebServer.Server.Responses;
using MyWebServer.Server.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MyWebServer.Server
{
    public class HttpServer
    {
        private readonly IPAddress ipAddress;
        private readonly int port;
        private readonly TcpListener listener;

        public HttpServer(string ipAddress, int port, Action<IRoutingTable> routingTable)
        {
            this.ipAddress = IPAddress.Parse(ipAddress);
            this.port = port;
            this.listener = new TcpListener(this.ipAddress, this.port);
        }

        public HttpServer(int port, Action<IRoutingTable> routingTable)
            : this("127.0.0.1", port, routingTable)
        {
        }

        public HttpServer(Action<IRoutingTable> routingTable)
            : this(6969, routingTable)

        {
        }

        public async Task Start()
        {
            this.listener.Start();

            Console.WriteLine($"Server started on port {port}...");
            Console.WriteLine("Waiting for client requests...");

            while (true)
            {
                var connection = await this.listener.AcceptTcpClientAsync();

                var networkStream = connection.GetStream();

                var requestText = await ReadRequest(networkStream);

                Console.WriteLine(requestText);

                var request = HttpRequest.Parse(requestText);

                await WriteResponse(networkStream);

                connection.Close();
            }
        }

        private async Task<string> ReadRequest(NetworkStream networkStream)
        {
            var bufferLenght = 1024;
            var buffer = new byte[bufferLenght];

            var totalBytesRead = 0;

            var requestBuilder = new StringBuilder();

            do
            {
                var bytesRead = await networkStream.ReadAsync(buffer, 0, bufferLenght);

                totalBytesRead += bytesRead;

                if (totalBytesRead >= 10 * 1024)
                {
                    throw new InvalidOperationException("Request is too large (More than 10 MB).");
                }

                requestBuilder.Append(Encoding.UTF8.GetString(buffer, 0, bytesRead));

            } while (networkStream.DataAvailable);

            return requestBuilder.ToString();
        }

        private async Task WriteResponse(NetworkStream networkStream)
        {
            var responseBody = "<h1>Hey from my server!</h1>";
            var contentLength = Encoding.UTF8.GetByteCount(responseBody);

            TextResponse textResponse = new TextResponse(responseBody, "text/html; charset=UTF-8");

//            var response = @$"
//HTTP/1.1 200 OK    
//Server: My Web Server
//Date: {DateTime.UtcNow.ToString("r")}
//Content-Length: {contentLength}    
//Content-Type: text/html; charset=UTF-8

//{responseBody}";

            var response = textResponse.ToString();

            byte[] responseByte = Encoding.UTF8.GetBytes(response);

            await networkStream.WriteAsync(responseByte);
        }
    }
}
