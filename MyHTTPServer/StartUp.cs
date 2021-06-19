
using System.Threading.Tasks;
using MyWebServer.Server;

namespace MyHttpServer
{
    public class StartUp
    {
        public static async Task Main(string[] args)
        {
            int test = 1;

            string ipAddress = "127.0.0.1";
            int port = 6969;

            var server = new HttpServer(ipAddress, port);

            await server.Start();
        }
    }
}
