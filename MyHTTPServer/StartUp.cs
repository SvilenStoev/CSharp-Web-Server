
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
            .MapGet("/Cats", new HtmlResponse("<h1>Hello from the cats!</h1>"))
            .MapGet("/Svilen", new HtmlResponse("<h2>Hello from Svilen</h2>"))
            .MapGet("/Trainings", new HtmlResponse("<h3>Hello from Trainings!</h3>")))
            .Start();
    }
}
