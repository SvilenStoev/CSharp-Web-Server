
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
            .MapGet("/Cats", request =>
                {
                    const string nameKey = "Name";

                    var query = request.Query;

                    var catName = query.ContainsKey(nameKey)
                    ? query[nameKey]
                    : "the cats";

                    var result = $"<h1>Hello from {catName}</h1>";

                    return new HtmlResponse(result);
                }
            )
            .MapGet("/Svilen", new HtmlResponse("<h2>Hello from Svilen</h2>"))
            .MapGet("/Trainings", new HtmlResponse("<h3>Hello from Trainings!</h3>")))
            .Start();
    }
}
