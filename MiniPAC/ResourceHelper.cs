using System.Reflection;
using System.IO;

namespace DLMSoft.MiniPAC {
    static class ResourceHelper {
        const string RES_NAME_PAC = "DLMSoft.MiniPAC.pac.js";

        public static void UnpackPACFile()
        {
            var asm = Assembly.GetExecutingAssembly();
            var pacPath = Path.Combine(Path.GetDirectoryName(asm.Location), "pac");
            if (File.Exists(pacPath)) return;
            using (var stream = asm.GetManifestResourceStream(RES_NAME_PAC)) {
                using (var file = File.Open(pacPath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read)) {
                    var buffer = new byte[1024];

                    while (true) {
                        var len = stream.Read(buffer, 0, 1024);
                        if (len == 0) break;
                        file.Write(buffer, 0, len);
                    }
                }
            }
        }
    }
}
