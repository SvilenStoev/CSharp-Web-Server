using MyWebServer.Server.Common;
using MyWebServer.Server.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebServer.Server.Responses
{
    public class TextResponse : HttpResponse
    {
        public TextResponse(string content, string contentType)
            : base(HttpStatusCode.OK)
        {
            Guard.AgainstNull(content);

            var contentLength = Encoding.UTF8.GetByteCount(content).ToString();

            this.Headers.Add("Content-Length", contentLength);
            this.Headers.Add("Content-Type", $"{contentType}");

            this.Content = content;
        }

        public TextResponse(string text)
            : this(text, "text/plain; charset=UTF-8")
        {
        }
    }
}
