using System;
using System.Diagnostics;
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

            try {
                HandleRequest();
            }
            catch (Exception ex) {
                Debug.WriteLine(ex);
                LogSystem.DumpError(ex);

                Response.StatusCode = 500;
                Response.WriteText("500 Internal Server Error");
                Response.Close();
            }
        }

        public abstract void HandleRequest();
    }
}
