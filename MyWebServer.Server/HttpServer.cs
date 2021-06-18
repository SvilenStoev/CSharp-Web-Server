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

        public HttpServer(string ipAddress, int port)
        {
            this.ipAddress = IPAddress.Parse(ipAddress);
            this.port = port;
            this.listener = new TcpListener(this.ipAddress, this.port);
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
                        connection.Close();
                    }

                    requestBuilder.Append(Encoding.UTF8.GetString(buffer, 0, bytesRead));

                } while (networkStream.DataAvailable);

                Console.WriteLine(requestBuilder);

                var responseBody = "<h1>Hey from my server!</h1>";
                var contentLength = Encoding.UTF8.GetByteCount(responseBody);

                var response = @$"HTTP/1.1 200 OK    
Server: My Web Server
Date: {DateTime.UtcNow.ToString("r")}
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
