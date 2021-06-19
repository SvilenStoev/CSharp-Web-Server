using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebServer.Server.Http
{
    public abstract class HttpResponse
    {
        public HttpResponse(HttpStatusCode statusCode)
        {
            this.StatusCode = statusCode;

            this.Headers.Add("Server", "My Web Server");
            this.Headers.Add("Date", $"{DateTime.UtcNow:r}");
        }

        public HttpStatusCode StatusCode { get; init; }

        public HttpHeaderCollection Headers { get; } = new HttpHeaderCollection();

        public string Content { get; init; }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"HTTP/1.1 {(int)this.StatusCode} {this.StatusCode}");

            foreach (var header in this.Headers)
            {
                sb.AppendLine(header.ToString());
            }

            if (!string.IsNullOrEmpty(this.Content))
            {
                sb.AppendLine();

                sb.Append(this.Content);
            }

            return sb.ToString().TrimEnd();
        }
    }
}
