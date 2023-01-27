using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

using Microsoft.Win32;

using DLMSoft.MiniPAC.Configuration;
using DLMSoft.MiniPAC.HttpService;
using System.IO;

namespace DLMSoft.MiniPAC {
    static class Program {
        #region Constants
        const string MUTEX_NAME = "MiniPAC.Exists";

        public const string START_ARG_INSTALL = "--install";
        public const string START_ARG_UNINSTALL = "--uninstall";
        public const string START_ARG_SET_PROXY = "--set";
        public const string START_ARG_UNSET_PROXY = "--unset";

        public const string REG_KEY_SET_PROXY_ITEM = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Internet Settings";
        public const string REG_KEY_SET_PROXY_VALUE = "AutoConfigURL";
        #endregion

        #region Method : SetSystemPAC
        static void SetSystemPAC(string url)
        {
            var key = Registry.CurrentUser.OpenSubKey(REG_KEY_SET_PROXY_ITEM, true);
            key.SetValue(REG_KEY_SET_PROXY_VALUE, url);
        }
        #endregion

        #region Method : UnsetSystemPAC
        static void UnsetSystemPAC()
        {
            var key = Registry.CurrentUser.OpenSubKey(REG_KEY_SET_PROXY_ITEM, true);
            key.DeleteValue(REG_KEY_SET_PROXY_VALUE);
        }
        #endregion

        #region Method : InstallAutoStart
        static void InstallAutoStart()
        {
            TaskSchedulerManager.CreateTask();
        }
        #endregion

        #region Method : UninstallAutoStart
        static void UninstallAutoStart()
        {
            TaskSchedulerManager.DeleteTask();
        }
        #endregion

        #region Method : UpdateSystemPAC
        public static void UpdateSystemPAC(bool force = false)
        {
            var key = Registry.CurrentUser.OpenSubKey(REG_KEY_SET_PROXY_ITEM, true);
            var url = key.GetValue(REG_KEY_SET_PROXY_VALUE) as string;
            var pacSetted = url != null && url.StartsWith($"http://localhost:{Config.HttpPort}/pac?");
            ProcessStartInfo startInfo;
            if (Config.SetProxy) {
                if (!force && pacSetted) return;
                var randomStr = Utility.GenerateRandomString(16);
                startInfo = new ProcessStartInfo {
                    Verb = "runas",
                    FileName = Application.ExecutablePath,
                    Arguments = $"{START_ARG_SET_PROXY} \"http://localhost:{Config.HttpPort}/pac?{randomStr}\""
                };
                try {
                    Process.Start(startInfo).WaitForExit();
                }
                catch {
                    throw;
                }
                return;
            }

            startInfo = new ProcessStartInfo {
                Verb = "runas",
                FileName = Application.ExecutablePath,
                Arguments = START_ARG_UNSET_PROXY
            };
            try {
                Process.Start(startInfo).WaitForExit();
            }
            catch {
                throw;
            }
        }
        #endregion

        #region Method : DoMainLoop
        static void DoMainLoop()
        {
            running_ = true;

            while (running_) {
                Application.DoEvents();
            }
        }
        #endregion

        #region Method : Exit
        public static void Exit()
        {
            running_ = false;
        }
        #endregion

        [STAThread]
        static void Main(string[] args)
        {
            ResourceHelper.UnpackPACFile();

            if (args.Length > 0) {
                switch (args[0]) {
                    case START_ARG_SET_PROXY:
                        if (args.Length < 2) { return; }
                        SetSystemPAC(args[1]);
                        return;
                    case START_ARG_UNSET_PROXY:
                        UnsetSystemPAC();
                        return;
                    case START_ARG_INSTALL:
                        InstallAutoStart();
                        return;
                    case START_ARG_UNINSTALL:
                        UninstallAutoStart();
                        return;
                }
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using (var mutex = new Mutex(true, MUTEX_NAME, out var mutexCreated)) {
                if (!mutexCreated) {
                    MessageBox.Show(
                        Resource.MESSAGE_WARN_INSTANCE_EXISTS,
                        Resource.PRODUCT_NAME,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation
                    );
                    return;
                }

                Config.Load();

                HttpServer.Instance.RegisterHandler<HttpService.Handlers.PACHandler>();

                try {
                    using (var trayIcon = new TrayIcon()) {
                        HttpServer.Instance.Start(Config.HttpPort);

                        var proxyKey = Registry.CurrentUser.OpenSubKey(REG_KEY_SET_PROXY_ITEM, true);
                        if (Config.SetProxy && proxyKey.GetValue(REG_KEY_SET_PROXY_VALUE) == null) {
                            var randomStr = Utility.GenerateRandomString(16);
                            var startInfo = new ProcessStartInfo {
                                Verb = "runas",
                                FileName = Application.ExecutablePath,
                                Arguments = $"{START_ARG_SET_PROXY} \"http://localhost:{Config.HttpPort}/pac?{randomStr}\""
                            };
                        }

                        DoMainLoop();
                        HttpServer.Instance.Stop();
                    }
                }
                catch (Exception ex) {
                    Debug.WriteLine(ex);
                    LogSystem.DumpError(ex);
                }
            }
        }

        #region Fields
        static bool running_;
        #endregion
    }
}
