using DLMSoft.MiniPAC.Configuration;
using System.IO;
using System.Text;
using System.Linq;

namespace DLMSoft.MiniPAC.HttpService.Handlers {
    [RequestHandler("GET", "/pac")]
    class PACHandler : HttpRequestHandler {
        const string REPLACE_KEY_PROXY = "{$PROXY}";
        const string USER_RULES_BEGIN = "var userRules = [";
        const string USER_RULES_END = "];";

        public override void HandleRequest()
        {
            if (string.IsNullOrEmpty(Config.ProxyType) || string.IsNullOrEmpty(Config.ProxyHost) || Config.ProxyPort == 0) {
                Response.StatusCode = 500;
                Response.WriteText("// Proxy not setted.\r\nfunction FindProxyForURL(url, host) { return \"DIRECT;\"; }");
                return;
            }

            var proxyStr = $"{Config.ProxyType} {Config.ProxyHost}:{Config.ProxyPort}";

            using (var reader = File.OpenText("pac")) {
                var sb = new StringBuilder();

                while (!reader.EndOfStream) {
                    var line = reader.ReadLine();
                    line = line.Replace(REPLACE_KEY_PROXY, proxyStr);
                    if (line.StartsWith(USER_RULES_BEGIN)) {
                        sb.AppendLine(USER_RULES_BEGIN);
                        var rules = from r in Config.UserRules select "\t\"" + r + '"';
                        sb.AppendLine(string.Join(",\r\n", rules.ToArray()));
                        sb.AppendLine(USER_RULES_END);
                        continue;
                    }
                    sb.AppendLine(line);
                }

                Response.StatusCode = 200;
                Response.WriteText(sb.ToString());
            }
        }
    }
}
