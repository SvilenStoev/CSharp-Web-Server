namespace MyWebServer.Server.Responses
{
    public class TextResponse : ContentResponse
    {
        public TextResponse(string content)
            : base(content, "text/plain; charset=UTF-8")
        {
        }
    }
}
