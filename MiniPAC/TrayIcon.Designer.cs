
namespace DLMSoft.MiniPAC {
    partial class TrayIcon {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrayIcon));
            this.mnuNotify = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuCopyUrl = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuAutoStart = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuUpdateProxy = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuUserRules = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.icoNotify = new System.Windows.Forms.NotifyIcon(this.components);
            this.mnuNotify.SuspendLayout();
            // 
            // mnuNotify
            // 
            resources.ApplyResources(this.mnuNotify, "mnuNotify");
            this.mnuNotify.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuCopyUrl,
            this.toolStripSeparator1,
            this.mnuAutoStart,
            this.mnuUpdateProxy,
            this.toolStripSeparator2,
            this.mnuSettings,
            this.mnuUserRules,
            this.toolStripSeparator3,
            this.mnuExit});
            this.mnuNotify.Name = "contextMenuStrip1";
            // 
            // mnuCopyUrl
            // 
            resources.ApplyResources(this.mnuCopyUrl, "mnuCopyUrl");
            this.mnuCopyUrl.Name = "mnuCopyUrl";
            // 
            // toolStripSeparator1
            // 
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            // 
            // mnuAutoStart
            // 
            resources.ApplyResources(this.mnuAutoStart, "mnuAutoStart");
            this.mnuAutoStart.Name = "mnuAutoStart";
            // 
            // mnuUpdateProxy
            // 
            resources.ApplyResources(this.mnuUpdateProxy, "mnuUpdateProxy");
            this.mnuUpdateProxy.Name = "mnuUpdateProxy";
            // 
            // toolStripSeparator2
            // 
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            // 
            // mnuSettings
            // 
            resources.ApplyResources(this.mnuSettings, "mnuSettings");
            this.mnuSettings.Name = "mnuSettings";
            // 
            // mnuUserRules
            // 
            resources.ApplyResources(this.mnuUserRules, "mnuUserRules");
            this.mnuUserRules.Name = "mnuUserRules";
            // 
            // toolStripSeparator3
            // 
            resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            // 
            // mnuExit
            // 
            resources.ApplyResources(this.mnuExit, "mnuExit");
            this.mnuExit.Name = "mnuExit";
            // 
            // icoNotify
            // 
            resources.ApplyResources(this.icoNotify, "icoNotify");
            this.icoNotify.ContextMenuStrip = this.mnuNotify;
            this.mnuNotify.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip mnuNotify;
        private System.Windows.Forms.ToolStripMenuItem mnuUpdateProxy;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mnuSettings;
        private System.Windows.Forms.ToolStripMenuItem mnuUserRules;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem mnuExit;
        private System.Windows.Forms.NotifyIcon icoNotify;
        private System.Windows.Forms.ToolStripMenuItem mnuAutoStart;
        private System.Windows.Forms.ToolStripMenuItem mnuCopyUrl;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    }
}
