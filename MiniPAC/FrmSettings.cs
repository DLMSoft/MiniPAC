using DLMSoft.MiniPAC.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DLMSoft.MiniPAC {
    public partial class FrmSettings : Form {
        public FrmSettings()
        {
            InitializeComponent();
            Font = SystemFonts.CaptionFont;
        }

        protected override void OnLoad(EventArgs e)
        {
            Config.Load();

            nudHttpPort.Value = Config.HttpPort;
            sltProxyType.SelectedItem = Config.ProxyType;
            txtProxyHost.Text = Config.ProxyHost;
            nudProxyPort.Value = Config.ProxyPort;
        }

        private void HandleCloseClick(object sender, EventArgs e)
        {
            Close();
        }

        private void HandleOKClick(object sender, EventArgs e)
        {
            Config.HttpPort = (ushort)nudHttpPort.Value;
            Config.ProxyType = sltProxyType.SelectedItem as string;
            Config.ProxyHost = txtProxyHost.Text;
            Config.ProxyPort = (ushort)nudProxyPort.Value;
            Config.Save();
            Program.UpdateSystemPAC(true);

            Close();
        }
    }
}
