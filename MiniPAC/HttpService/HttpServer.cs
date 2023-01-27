using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Threading;

namespace DLMSoft.MiniPAC.HttpService {
    enum HttpServerStatus {
        Stopped,
        Running,
        Stopping
    }

    class HttpServer {
        #region Singleton instance
        public static HttpServer Instance { get; private set; }
        static HttpServer()
        {
            Instance = new HttpServer();
        }
        #endregion

        #region Properties
        public HttpServerStatus Status { get; private set; }
        public ushort BindPort { get; private set; }
        #endregion

        private HttpServer()
        {
            Status = HttpServerStatus.Stopped;
            handlerTypes_ = new Dictionary<string, Type>();
        }

        void ContextThread_Main(object obj)
        {
            var context = obj as HttpListenerContext;
            try {
                var path = context.Request.Url.LocalPath;
                var method = context.Request.HttpMethod;
                var key = $"{method} {path}";
                if (!handlerTypes_.ContainsKey(key)) {
                    context.Response.StatusCode = 404;
                    context.Response.WriteText("404 Not Found");
                    context.Response.Close();
                    return;
                }
                var handlerType = handlerTypes_[key];
                var handler = Activator.CreateInstance(handlerType) as HttpRequestHandler;
                handler.Handle(context);
                context.Response.Close();
            }
            catch (Exception ex) {
                context.Response.StatusCode = 500;
                context.Response.WriteText("500 Internal Server Error");
                context.Response.Close();
                Debug.WriteLine(ex);
                LogSystem.DumpError(ex);
            }
        }

        void HandleContext(HttpListenerContext context)
        {
            var thread = new Thread(new ParameterizedThreadStart(ContextThread_Main));
            thread.Start(context);
        }

        void ListenThread_Main()
        {
            try {
                listener_.Start();

                while (listener_.IsListening) {
                    try {
                        var context = listener_.GetContext();
                        if (context == null) continue;
                        HandleContext(context);
                    }
                    catch (Exception ex) {
                        if (ex is HttpListenerException && Status == HttpServerStatus.Stopping) {
                            break;
                        }
                        Debug.WriteLine(ex);
                        LogSystem.DumpError(ex);
                    }
                }
            }
            catch (Exception ex) {
                Debug.WriteLine(ex);
                LogSystem.DumpError(ex);
            }
        }

        public void Start(ushort port)
        {
            BindPort = port;
            listener_ = new HttpListener();
            listener_.Prefixes.Add($"http://localhost:{BindPort}/");
            listenThread_ = new Thread(ListenThread_Main);
            listenThread_.Start();
        }

        public void Stop()
        {
            Status = HttpServerStatus.Stopping;
            listener_.Abort();
            listenThread_.Join();
            Status = HttpServerStatus.Stopped;
        }

        public void RegisterHandler<T>() where T : HttpRequestHandler {
            var type = typeof(T);
            var requestHandlerAttrs = type.GetCustomAttributes(typeof(RequestHandlerAttribute), false);
            foreach (RequestHandlerAttribute attr in requestHandlerAttrs) {
                var key = $"{attr.Method.ToUpper()} {attr.Path}";
                if (handlerTypes_.ContainsKey(key)) {
                    Debug.WriteLine($"Handler of \"{key}\" already exists.");
                    continue;
                }

                handlerTypes_.Add(key, type);
            }
        }

        public void ClearHandlers()
        {
            handlerTypes_.Clear();
        }

        Thread listenThread_;
        HttpListener listener_;
        Dictionary<string, Type> handlerTypes_;
    }
}
