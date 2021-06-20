namespace MyWebServer.Server.Responses
{
    public class HtmlResponse : ContentResponse
    {
        public HtmlResponse(string content)
            : base(content, "text/html; charset=UTF-8")
        {
        }
    }
}
