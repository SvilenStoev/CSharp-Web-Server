using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebServer.Server.Http
{
    public class HttpHeaderCollection
    {
        private readonly Dictionary<string, HttpHeader> headers;

        public HttpHeaderCollection()
            => this.headers = new Dictionary<string, HttpHeader>();

        public int Count => this.headers.Count();

        public void Add(string name, string value)
        {
            var header = new HttpHeader(name, value);

            this.headers.Add(name, header);
        }
    }
}
