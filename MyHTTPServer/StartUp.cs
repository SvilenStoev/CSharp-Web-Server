
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
            .MapGet("/", new TextResponse("Hello from my Server!"))
            .MapGet("/Cats", new TextResponse("<h1>Hello from the cats!</h1>", "text/html; charset=UTF-8"))
            .MapGet("/Svilen", new TextResponse("<h2>Hello from Svilen</h2>", "text/html; charset=UTF-8"))
            .MapGet("/Trainings", new TextResponse("<h3>Hello from Trainings!</h3>", "text/html; charset=UTF-8")))
            .Start();
    }
}
