using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebServer.Server.Http
{
    public class HttpRequest
    {
        private const string NewLine = "\r\n";

        public HttpMethod httpMethod { get; private set; }

        public string Path { get; private set; }

        public Dictionary<string, string> Query { get; private set; }

        public HttpHeaderCollection Headers { get; private set; }

        public string Body { get; private set; }

        public static HttpRequest Parse(string requestText)
        {
            var lines = requestText.Split(NewLine);

            var startLine = lines[0].Split(" ");

            var method = ParseHttpMethod(startLine[0]);

            var url = startLine[1];

            var (path, query) = ParseUrl(url);

            var headerLines = lines.Skip(1).ToArray();

            var headers = ParseHttpHeaders(headerLines);

            var bodyLines = lines.Skip(headers.Count + 2).ToArray();

            var body = string.Join(NewLine, bodyLines);

            return new HttpRequest
            {
                httpMethod = method,
                Path = path,
                Query = query,
                Headers = headers,
                Body = body
            };
        }

        private static HttpHeaderCollection ParseHttpHeaders(string[] headerLines)
        {
            var headerCollection = new HttpHeaderCollection();

            foreach (var headerLine in headerLines)
            {
                if (headerLine == string.Empty)
                {
                    break;
                }

                var headerParts = headerLine.Split(":", 2).ToArray();

                if (headerParts.Length != 2)
                {
                    throw new InvalidOperationException("Request is not valid.");
                }

                var headerName = headerParts[0];
                var headerValue = headerParts[1].Trim();

                headerCollection.Add(headerName, headerValue);
            }

            return headerCollection;
        }

        private static HttpMethod ParseHttpMethod(string method)
        {
            return method.ToUpper() switch
            {
                "GET" => HttpMethod.Get,
                "POST" => HttpMethod.Post,
                "PUT" => HttpMethod.Put,
                "DELETE" => HttpMethod.Delete,
                _ => throw new InvalidOperationException($"Method '{method}' is not supported!"),
            };
        }

        private static (string, Dictionary<string, string>) ParseUrl(string url)
        {
            var urlParts = url.Split('?', 2);
            var path = urlParts[0];

            var query = new Dictionary<string, string>();

            if (urlParts.Length > 1)
            {
                query = ParseQuery(urlParts[1]);
            }

            return (path, query);
        }

        // /Svilen? FirstName=Svilen & LastName=Stoev

        private static Dictionary<string, string> ParseQuery(string queryString) => queryString
                .Split('&')
                .Select(qsp => qsp.Split('='))
                .Where(part => part.Length == 2)
                .ToDictionary(part => part[0], part => part[1]);
    }
}
