using System;
using System.Net;

namespace DLMSoft.MiniPAC.HttpService {
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    class RequestHandlerAttribute : Attribute {
        public string Method { get; private set; }
        public string Path { get; private set; }

        public RequestHandlerAttribute(string method, string path)
        {
            Method = method;
            Path = path;
        }
    }

    abstract class HttpRequestHandler {
        public HttpListenerRequest Request { get; private set; }
        public HttpListenerResponse Response { get; private set; }

        public void Handle(HttpListenerContext context)
        {
            Request = context.Request;
            Response = context.Response;

            HandleRequest();
        }

        public abstract void HandleRequest();
    }
}
