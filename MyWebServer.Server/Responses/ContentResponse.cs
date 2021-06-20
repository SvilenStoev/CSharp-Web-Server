using MyWebServer.Server.Common;
using MyWebServer.Server.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
