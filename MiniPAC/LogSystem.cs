using System;
using System.IO;
using System.Text;

namespace DLMSoft.MiniPAC {
    static class LogSystem {
        public static void DumpError(Exception ex)
        {
            if (!Directory.Exists("logs")) {
                Directory.CreateDirectory("logs");
            }
            var dumpFileName = $"logs/{DateTime.Now.ToString("yyyy-MM-dd")}.log";
            var sb = new StringBuilder();
            sb.AppendLine("================================================================");
            sb.AppendLine(DateTime.Now.ToString("HH:mm:ss"));
            sb.AppendLine("----------------------------------------------------------------");
            sb.AppendLine(ex.ToString());
            sb.AppendLine();
            File.AppendAllText(dumpFileName, sb.ToString(), Encoding.UTF8);
        }
    }
}
