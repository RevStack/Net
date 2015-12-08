using System;
using System.Net;

namespace RevStack.Net
{
    public class HttpResult
    {
        public string Html { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string Error { get; set; }
    }
}
