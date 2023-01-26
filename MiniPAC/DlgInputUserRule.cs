using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DLMSoft.MiniPAC {
    public partial class DlgInputUserRule : Form {
        public string Rule
        {
            get => txtRuleContent.Text;
        }

        public DlgInputUserRule()
        {
            InitializeComponent();
            Font = SystemFonts.CaptionFont;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
