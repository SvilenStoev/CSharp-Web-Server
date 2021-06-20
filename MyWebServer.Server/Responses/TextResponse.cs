using MyWebServer.Server.Common;
using MyWebServer.Server.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
