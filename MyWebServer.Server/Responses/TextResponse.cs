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
        public TextResponse(string text) 
            : base(HttpStatusCode.OK)
        {
            Guard.AgainstNull(text);

            var contentLength = Encoding.UTF8.GetByteCount(text).ToString();

            this.Headers.Add("Content-Length", contentLength);
            this.Headers.Add("Content-Type", "text/plain; charset=UTF-8");
        }
    }
}
