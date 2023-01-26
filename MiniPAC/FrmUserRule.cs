using DLMSoft.MiniPAC.Configuration;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace DLMSoft.MiniPAC {
    public partial class FrmUserRule : Form {
        public FrmUserRule()
        {
            InitializeComponent();
            Font = SystemFonts.CaptionFont;
        }

        void BindData()
        {
            foreach (var i in Config.UserRules) {
                lstRules.Items.Add(i);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            BindData();
        }

        private void HandleCloseClick(object sender, System.EventArgs e)
        {
            Close();
        }

        private void HandleAddClick(object sender, System.EventArgs e)
        {
            using (var dlg = new DlgInputUserRule()) {
                if (dlg.ShowDialog() != DialogResult.OK) return;

                lstRules.Items.Add(dlg.Rule);
            }
        }

        private void HandleRulesSelect(object sender, EventArgs e)
        {
            if (lstRules.SelectedIndex != -1) {
                tbtnRemove.Enabled = true;
            }
            else {
                tbtnRemove.Enabled = false;
            }
        }

        private void HandleRemoveClick(object sender, EventArgs e)
        {
            var rule = lstRules.SelectedItem as string;
            var message = string.Format(Resource.MESSAGE_CONFIRM_USER_RULE_REMOVE, rule);
            if (MessageBox.Show(message, Resource.PRODUCT_NAME,
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            lstRules.Items.RemoveAt(lstRules.SelectedIndex);
        }

        private void HandleSaveClick(object sender, EventArgs e)
        {
            Config.UserRules.Clear();
            
            foreach (var i in lstRules.Items) {
                Config.UserRules.Add(i as string);
            }

            Config.Save();
            Program.UpdateSystemPAC(true);

            Close();
        }
    }
}
