using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace DLMSoft.MiniPAC.HttpService {
    static class HttpHelper {
        const int BUFFER_SIZE = 1024;

        public static void WriteText(this HttpListenerResponse response, string text, Encoding encoding = null)
        {
            try {
                if (response == null) throw new NullReferenceException();
                if (string.IsNullOrEmpty(text)) return;
                if (encoding == null) {
                    encoding = Encoding.UTF8;
                }

                var textBytes = encoding.GetBytes(text);

                using (var output = response.OutputStream) {
                    var buffer = new byte[BUFFER_SIZE];

                    using (var ms = new MemoryStream(textBytes)) {
                        while (true) {
                            var len = ms.Read(buffer, 0, BUFFER_SIZE);
                            if (len == 0) break;
                            output.Write(buffer, 0, len);
                        }
                    }
                }
            }
            catch (Exception ex) {
                Debug.WriteLine(ex);
                LogSystem.DumpError(ex);
            }
        }
    }
}
