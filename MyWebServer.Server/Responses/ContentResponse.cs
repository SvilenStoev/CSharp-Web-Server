using System.Text;
using MyWebServer.Server.Http;
using MyWebServer.Server.Common;

namespace MyWebServer.Server.Responses
{
    public class ContentResponse : HttpResponse
    {
        public ContentResponse(string content, string contentType)
          : base(HttpStatusCode.OK)
        {
            Guard.AgainstNull(content);

            var contentLength = Encoding.UTF8.GetByteCount(content).ToString();

            this.Headers.Add("Content-Length", contentLength);
            this.Headers.Add("Content-Type", contentType);

            this.Content = content;
        }
    }
}
