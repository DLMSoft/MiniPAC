using Microsoft.Win32;
using DLMSoft.MiniPAC.Configuration;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;

namespace DLMSoft.MiniPAC {
    public partial class TrayIcon : Component {
        #region Constructor
        public TrayIcon()
        {
            InitializeComponent();

            try {
                if (Config.AutoStart) {
                    InstallAutoStart();
                }
                else {
                    UninstallAutoStart();
                }

                Program.UpdateSystemPAC();
                mnuUpdateProxy.Checked = Config.SetProxy;

                mnuCopyUrl.Click += HandleCopyUrlClick;
                mnuAutoStart.Click += HandleAutoStartClick;
                mnuUpdateProxy.Click += HandleUpdateProxyClick;
                mnuSettings.Click += HandleSettingsClick;
                mnuUserRules.Click += HandleUserRulesClick;
                mnuExit.Click += HandleExitClick;
            }
            catch (Exception ex) {
                Debug.WriteLine(ex);
                LogSystem.DumpError(ex);
            }
        }

        #endregion

        #region Event Handlers
        private void HandleCopyUrlClick(object sender, EventArgs e)
        {
            var randomStr = Utility.GenerateRandomString(16);
            var url = $"http://localhost:{Config.HttpPort}/pac?{randomStr}";

            Clipboard.SetText(url);

            MessageBox.Show(Resource.MESSAGE_ALERT_URL_COPIED, Resource.PRODUCT_NAME, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void HandleAutoStartClick(object sender, EventArgs e)
        {
            if (!mnuAutoStart.Checked) {
                InstallAutoStart();
                return;
            }
            UninstallAutoStart();
        }

        private void HandleUpdateProxyClick(object sender, EventArgs e)
        {
            Config.SetProxy = !mnuUpdateProxy.Checked;
            Config.Save();
            try {
                Program.UpdateSystemPAC();
            }
            catch (Exception ex) {
                Debug.WriteLine(ex);
                var msg = string.Format(Resource.MSG_WARN_ERROR_WHEN, Resource.TIMING_SET_PROXY, ex.Message);
                MessageBox.Show(msg, Resource.PRODUCT_NAME, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            mnuUpdateProxy.Checked = !mnuUpdateProxy.Checked;
        }

        private void HandleSettingsClick(object sender, EventArgs e)
        {
            if (settingsWindow_ != null && settingsWindow_.Created) {
                settingsWindow_.BringToFront();
                return;
            }
            settingsWindow_ = new FrmSettings();
            settingsWindow_.Show();
        }

        private void HandleUserRulesClick(object sender, EventArgs e)
        {
            if (userRuleWindow_ != null && userRuleWindow_.Created) {
                userRuleWindow_.BringToFront();
                return;
            }
            userRuleWindow_ = new FrmUserRule();
            userRuleWindow_.Show();
        }

        private void HandleExitClick(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show(
                Resource.MESSAGE_CONFIRM_EXIT,
                Resource.PRODUCT_NAME,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );
            if (confirmResult != DialogResult.Yes) return;
            Program.Exit();
        }
        #endregion

        #region Method : InstallAutoStart
        void InstallAutoStart()
        {
            if (TaskSchedulerManager.IsTaskExists()) {
                mnuAutoStart.Checked = true;
                return;
            }

            Config.AutoStart = true;
            Config.Save();

            var startInfo = new ProcessStartInfo {
                Verb = "runas",
                FileName = Application.ExecutablePath,
                Arguments = Program.START_ARG_INSTALL
            };
            try {
                Process.Start(startInfo).WaitForExit();
            }
            catch (Exception ex) {
                Debug.WriteLine(ex);
                var msg = string.Format(Resource.MSG_WARN_ERROR_WHEN, Resource.TIMING_SET_AUTO_START, ex.Message);
                MessageBox.Show(msg, Resource.PRODUCT_NAME, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            mnuAutoStart.Checked = true;
        }
        #endregion

        #region Method : UninstallAutoStart
        void UninstallAutoStart()
        {
            if (!TaskSchedulerManager.IsTaskExists()) {
                mnuAutoStart.Checked = false;
                return;
            }

            Config.AutoStart = false;
            Config.Save();

            var startInfo = new ProcessStartInfo {
                Verb = "runas",
                FileName = Application.ExecutablePath,
                Arguments = Program.START_ARG_UNINSTALL
            };
            try {
                Process.Start(startInfo).WaitForExit();
            }
            catch (Exception ex) {
                Debug.WriteLine(ex);
                var msg = string.Format(Resource.MSG_WARN_ERROR_WHEN, Resource.TIMING_SET_AUTO_START, ex.Message);
                MessageBox.Show(msg, Resource.PRODUCT_NAME, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            mnuAutoStart.Checked = false;
        }
        #endregion

        FrmUserRule userRuleWindow_;
        FrmSettings settingsWindow_;
    }
}
