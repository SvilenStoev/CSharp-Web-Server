
using System.Threading.Tasks;
using MyWebServer.Server;

namespace MyHttpServer
{
    public class StartUp
    {
        public static async Task Main(string[] args)
        => await new HttpServer("127.0.0.1", 6969).Start();

    }
}
