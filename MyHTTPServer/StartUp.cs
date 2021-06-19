
using System.Net.Http;
using System.Threading.Tasks;
using MyWebServer.Server;
using MyWebServer.Server.Responses;

namespace MyHttpServer
{
    public class StartUp
    {
        public static async Task Main(string[] args)
        => await new HttpServer(routes => routes
            .MapGet("/", new TextResponse("Hello from Svilen!"))
            .MapGet("/Cats", new TextResponse("<h1>Hello from the cats!</h1>", "text/html; charset=UTF-8")))
            .Start();
    }
}
