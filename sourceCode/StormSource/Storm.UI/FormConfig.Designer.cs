namespace Storm.UI
{
    partial class FormConfig
    {

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.gradientPanel1 = new Ascend.Windows.Forms.GradientPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tabControl1 = new TabControl.TabControl();
            this.tabcServiceConfig = new TabControl.TabControlItem();
            this.cbUseProxy = new System.Windows.Forms.CheckBox();
            this.gbProxy = new System.Windows.Forms.GroupBox();
            this.cbProxyUseDefaultCredentials = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbProxyServer = new System.Windows.Forms.TextBox();
            this.tblPanel = new System.Windows.Forms.TableLayoutPanel();
            this.lblDomain = new System.Windows.Forms.Label();
            this.lblUsername = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.tbProxyUsername = new System.Windows.Forms.TextBox();
            this.tbDomain = new System.Windows.Forms.TextBox();
            this.maskTbPassword = new System.Windows.Forms.MaskedTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tbWSSoapCallTimeout = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbWSUseDefaultCredentials = new System.Windows.Forms.CheckBox();
            this.tabcUI = new TabControl.TabControlItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnAddFaveWs = new System.Windows.Forms.Button();
            this.tbAddFaveWs = new System.Windows.Forms.TextBox();
            this.chkListBxTop5Ws = new System.Windows.Forms.CheckedListBox();
            this.cbShowDetailedExc = new System.Windows.Forms.CheckBox();
            this.cbColorRawSoap = new System.Windows.Forms.CheckBox();
            this.gradientPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabControl1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabcServiceConfig.SuspendLayout();
            this.gbProxy.SuspendLayout();
            this.tblPanel.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabcUI.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gradientPanel1
            // 
            this.gradientPanel1.Border = new Ascend.Border(1);
            this.gradientPanel1.Controls.Add(this.btnOk);
            this.gradientPanel1.Controls.Add(this.tabControl1);
            this.gradientPanel1.Controls.Add(this.label3);
            this.gradientPanel1.CornerRadius = new Ascend.CornerRadius(3);
            this.gradientPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gradientPanel1.GradientLowColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.gradientPanel1.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.gradientPanel1.Location = new System.Drawing.Point(0, 0);
            this.gradientPanel1.Name = "gradientPanel1";
            this.gradientPanel1.Size = new System.Drawing.Size(400, 408);
            this.gradientPanel1.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(132, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 16);
            this.label3.TabIndex = 5;
            this.label3.Text = "Configuration";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.SystemColors.Window;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.Image = global::Storm.UI.StormResource.cancel;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(323, 372);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(65, 25);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancel.UseVisualStyleBackColor = false;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOk.BackColor = System.Drawing.SystemColors.Window;
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.FlatAppearance.BorderSize = 0;
            this.btnOk.Image = global::Storm.UI.StormResource.accept;
            this.btnOk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOk.Location = new System.Drawing.Point(12, 372);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(48, 25);
            this.btnOk.TabIndex = 5;
            this.btnOk.Text = "OK";
            this.btnOk.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnOk.UseVisualStyleBackColor = false;
            // 
            // toolTip1
            // 
            this.toolTip1.AutoPopDelay = 8000;
            this.toolTip1.InitialDelay = 300;
            this.toolTip1.ReshowDelay = 100;
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            // 
            // tabControl1
            // 
            this.tabControl1.AlwaysShowClose = false;
            this.tabControl1.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.tabControl1.Items.AddRange(new TabControl.TabControlItem[] {
            this.tabcServiceConfig,
            this.tabcUI});
            this.tabControl1.Location = new System.Drawing.Point(3, 26);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedItem = this.tabcUI;
            this.tabControl1.ShowBorder = false;
            this.tabControl1.ShowToolTipOnTitle = false;
            this.tabControl1.Size = new System.Drawing.Size(397, 340);
            this.tabControl1.TabIndex = 6;
            // 
            // tabcServiceConfig
            // 
            this.tabcServiceConfig.CanClose = false;
            this.tabcServiceConfig.Controls.Add(this.cbUseProxy);
            this.tabcServiceConfig.Controls.Add(this.gbProxy);
            this.tabcServiceConfig.Controls.Add(this.groupBox2);
            this.tabcServiceConfig.ID = null;
            this.tabcServiceConfig.IsDrawn = true;
            this.tabcServiceConfig.Name = "tabcServiceConfig";
            this.tabcServiceConfig.TabIndex = 0;
            this.tabcServiceConfig.TabType = TabControl.TabType.QuickTestTab;
            this.tabcServiceConfig.Title = "Service";
            this.tabcServiceConfig.ToolTipText = "This is the global web configuration settings";
            // 
            // cbUseProxy
            // 
            this.cbUseProxy.AutoSize = true;
            this.cbUseProxy.Location = new System.Drawing.Point(9, 10);
            this.cbUseProxy.Name = "cbUseProxy";
            this.cbUseProxy.Size = new System.Drawing.Size(75, 17);
            this.cbUseProxy.TabIndex = 1;
            this.cbUseProxy.Text = "Use proxy";
            this.cbUseProxy.UseVisualStyleBackColor = true;
            // 
            // gbProxy
            // 
            this.gbProxy.Controls.Add(this.cbProxyUseDefaultCredentials);
            this.gbProxy.Controls.Add(this.label1);
            this.gbProxy.Controls.Add(this.tbProxyServer);
            this.gbProxy.Controls.Add(this.tblPanel);
            this.gbProxy.Controls.Add(this.label5);
            this.gbProxy.Enabled = false;
            this.gbProxy.Location = new System.Drawing.Point(9, 33);
            this.gbProxy.Name = "gbProxy";
            this.gbProxy.Size = new System.Drawing.Size(376, 191);
            this.gbProxy.TabIndex = 0;
            this.gbProxy.TabStop = false;
            this.gbProxy.Text = "Proxy";
            // 
            // cbProxyUseDefaultCredentials
            // 
            this.cbProxyUseDefaultCredentials.AutoSize = true;
            this.cbProxyUseDefaultCredentials.Checked = true;
            this.cbProxyUseDefaultCredentials.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbProxyUseDefaultCredentials.Location = new System.Drawing.Point(6, 129);
            this.cbProxyUseDefaultCredentials.Name = "cbProxyUseDefaultCredentials";
            this.cbProxyUseDefaultCredentials.Size = new System.Drawing.Size(139, 17);
            this.cbProxyUseDefaultCredentials.TabIndex = 5;
            this.cbProxyUseDefaultCredentials.Text = "Use Default Credentials";
            this.cbProxyUseDefaultCredentials.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 105);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Or...";
            // 
            // tbProxyServer
            // 
            this.tbProxyServer.Location = new System.Drawing.Point(54, 153);
            this.tbProxyServer.Name = "tbProxyServer";
            this.tbProxyServer.Size = new System.Drawing.Size(209, 21);
            this.tbProxyServer.TabIndex = 7;
            // 
            // tblPanel
            // 
            this.tblPanel.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset;
            this.tblPanel.ColumnCount = 2;
            this.tblPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40.05525F));
            this.tblPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 59.94475F));
            this.tblPanel.Controls.Add(this.lblDomain, 0, 2);
            this.tblPanel.Controls.Add(this.lblUsername, 0, 0);
            this.tblPanel.Controls.Add(this.lblPassword, 0, 1);
            this.tblPanel.Controls.Add(this.tbProxyUsername, 1, 0);
            this.tblPanel.Controls.Add(this.tbDomain, 1, 2);
            this.tblPanel.Controls.Add(this.maskTbPassword, 1, 1);
            this.tblPanel.Enabled = false;
            this.tblPanel.Location = new System.Drawing.Point(6, 19);
            this.tblPanel.Name = "tblPanel";
            this.tblPanel.RowCount = 3;
            this.tblPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 52F));
            this.tblPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 48F));
            this.tblPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tblPanel.Size = new System.Drawing.Size(364, 80);
            this.tblPanel.TabIndex = 0;
            // 
            // lblDomain
            // 
            this.lblDomain.AutoSize = true;
            this.lblDomain.Location = new System.Drawing.Point(5, 53);
            this.lblDomain.Name = "lblDomain";
            this.lblDomain.Size = new System.Drawing.Size(42, 13);
            this.lblDomain.TabIndex = 5;
            this.lblDomain.Text = "Domain";
            // 
            // lblUsername
            // 
            this.lblUsername.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblUsername.AutoSize = true;
            this.lblUsername.Location = new System.Drawing.Point(5, 2);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(137, 24);
            this.lblUsername.TabIndex = 0;
            this.lblUsername.Text = "Username";
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(5, 28);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(53, 13);
            this.lblPassword.TabIndex = 1;
            this.lblPassword.Text = "Password";
            // 
            // tbProxyUsername
            // 
            this.tbProxyUsername.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbProxyUsername.Location = new System.Drawing.Point(150, 5);
            this.tbProxyUsername.Name = "tbProxyUsername";
            this.tbProxyUsername.Size = new System.Drawing.Size(209, 21);
            this.tbProxyUsername.TabIndex = 2;
            // 
            // tbDomain
            // 
            this.tbDomain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbDomain.Location = new System.Drawing.Point(150, 56);
            this.tbDomain.Name = "tbDomain";
            this.tbDomain.Size = new System.Drawing.Size(209, 21);
            this.tbDomain.TabIndex = 4;
            // 
            // maskTbPassword
            // 
            this.maskTbPassword.Dock = System.Windows.Forms.DockStyle.Fill;
            this.maskTbPassword.Location = new System.Drawing.Point(150, 31);
            this.maskTbPassword.Name = "maskTbPassword";
            this.maskTbPassword.PasswordChar = '*';
            this.maskTbPassword.Size = new System.Drawing.Size(209, 21);
            this.maskTbPassword.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 156);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(39, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Server";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tbWSSoapCallTimeout);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.cbWSUseDefaultCredentials);
            this.groupBox2.Location = new System.Drawing.Point(9, 230);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(376, 90);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Web Services";
            // 
            // tbWSSoapCallTimeout
            // 
            this.tbWSSoapCallTimeout.Location = new System.Drawing.Point(188, 39);
            this.tbWSSoapCallTimeout.Name = "tbWSSoapCallTimeout";
            this.tbWSSoapCallTimeout.Size = new System.Drawing.Size(117, 21);
            this.tbWSSoapCallTimeout.TabIndex = 9;
            this.tbWSSoapCallTimeout.Text = "100";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(185, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(122, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Soap call timeout (sec) :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(361, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Note: You can override these settings at the webservice configuration level";
            // 
            // cbWSUseDefaultCredentials
            // 
            this.cbWSUseDefaultCredentials.AutoSize = true;
            this.cbWSUseDefaultCredentials.Checked = true;
            this.cbWSUseDefaultCredentials.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbWSUseDefaultCredentials.Location = new System.Drawing.Point(6, 42);
            this.cbWSUseDefaultCredentials.Name = "cbWSUseDefaultCredentials";
            this.cbWSUseDefaultCredentials.Size = new System.Drawing.Size(139, 17);
            this.cbWSUseDefaultCredentials.TabIndex = 4;
            this.cbWSUseDefaultCredentials.Text = "Use Default Credentials";
            this.cbWSUseDefaultCredentials.UseVisualStyleBackColor = true;
            // 
            // tabcUI
            // 
            this.tabcUI.CanClose = false;
            this.tabcUI.Controls.Add(this.groupBox1);
            this.tabcUI.Controls.Add(this.cbShowDetailedExc);
            this.tabcUI.Controls.Add(this.cbColorRawSoap);
            this.tabcUI.ID = null;
            this.tabcUI.IsDrawn = true;
            this.tabcUI.Name = "tabcUI";
            this.tabcUI.Selected = true;
            this.tabcUI.TabIndex = 1;
            this.tabcUI.TabType = TabControl.TabType.QuickTestTab;
            this.tabcUI.Title = "User Interface";
            this.tabcUI.ToolTipText = "Configuration of UI elements";
            this.tabcUI.Changed += new System.EventHandler(this.tabcUI_Changed);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnAddFaveWs);
            this.groupBox1.Controls.Add(this.tbAddFaveWs);
            this.groupBox1.Controls.Add(this.chkListBxTop5Ws);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.groupBox1.Location = new System.Drawing.Point(9, 87);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(376, 141);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "My Favorite Services";
            // 
            // btnAddFaveWs
            // 
            this.btnAddFaveWs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddFaveWs.Image = global::Storm.UI.StormResource.add;
            this.btnAddFaveWs.Location = new System.Drawing.Point(341, 113);
            this.btnAddFaveWs.Name = "btnAddFaveWs";
            this.btnAddFaveWs.Size = new System.Drawing.Size(29, 23);
            this.btnAddFaveWs.TabIndex = 5;
            this.toolTip1.SetToolTip(this.btnAddFaveWs, "Add a web service endpoint.");
            this.btnAddFaveWs.UseVisualStyleBackColor = true;
            // 
            // tbAddFaveWs
            // 
            this.tbAddFaveWs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbAddFaveWs.Location = new System.Drawing.Point(6, 114);
            this.tbAddFaveWs.Name = "tbAddFaveWs";
            this.tbAddFaveWs.Size = new System.Drawing.Size(329, 21);
            this.tbAddFaveWs.TabIndex = 4;
            this.toolTip1.SetToolTip(this.tbAddFaveWs, "Please enter a valid service endpoint");
            this.tbAddFaveWs.TextChanged += new System.EventHandler(this.tbAddTop5Ws_TextChanged);
            // 
            // chkListBxTop5Ws
            // 
            this.chkListBxTop5Ws.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.chkListBxTop5Ws.CheckOnClick = true;
            this.chkListBxTop5Ws.FormattingEnabled = true;
            this.chkListBxTop5Ws.Items.AddRange(new object[] {
            "http://localhost:2493/TestWS/Service.asmx",
            "https://adwords.google.com/api/adwords/v12/AdService?wsdl"});
            this.chkListBxTop5Ws.Location = new System.Drawing.Point(6, 19);
            this.chkListBxTop5Ws.Name = "chkListBxTop5Ws";
            this.chkListBxTop5Ws.Size = new System.Drawing.Size(364, 84);
            this.chkListBxTop5Ws.TabIndex = 3;
            this.toolTip1.SetToolTip(this.chkListBxTop5Ws, "Only checked service endpoints will be saved.");
            // 
            // cbShowDetailedExc
            // 
            this.cbShowDetailedExc.AutoSize = true;
            this.cbShowDetailedExc.Checked = true;
            this.cbShowDetailedExc.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbShowDetailedExc.Location = new System.Drawing.Point(9, 55);
            this.cbShowDetailedExc.Name = "cbShowDetailedExc";
            this.cbShowDetailedExc.Size = new System.Drawing.Size(143, 17);
            this.cbShowDetailedExc.TabIndex = 2;
            this.cbShowDetailedExc.Text = "Show detailed exception";
            this.cbShowDetailedExc.UseVisualStyleBackColor = true;
            // 
            // cbColorRawSoap
            // 
            this.cbColorRawSoap.AutoSize = true;
            this.cbColorRawSoap.Checked = true;
            this.cbColorRawSoap.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbColorRawSoap.Location = new System.Drawing.Point(9, 26);
            this.cbColorRawSoap.Name = "cbColorRawSoap";
            this.cbColorRawSoap.Size = new System.Drawing.Size(148, 17);
            this.cbColorRawSoap.TabIndex = 0;
            this.cbColorRawSoap.Text = "Color raw soap messages";
            this.cbColorRawSoap.UseVisualStyleBackColor = true;
            // 
            // FormConfig
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(400, 408);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.gradientPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "FormConfig";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "  ";
            this.gradientPanel1.ResumeLayout(false);
            this.gradientPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabControl1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabcServiceConfig.ResumeLayout(false);
            this.tabcServiceConfig.PerformLayout();
            this.gbProxy.ResumeLayout(false);
            this.gbProxy.PerformLayout();
            this.tblPanel.ResumeLayout(false);
            this.tblPanel.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabcUI.ResumeLayout(false);
            this.tabcUI.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.GroupBox gbProxy;
        public System.Windows.Forms.Label lblDomain;
        public System.Windows.Forms.Label lblUsername;
        public System.Windows.Forms.Label lblPassword;
        public System.Windows.Forms.TextBox tbProxyUsername;
        public System.Windows.Forms.TextBox tbDomain;
        public System.Windows.Forms.MaskedTextBox maskTbPassword;
        public System.Windows.Forms.GroupBox groupBox2;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.CheckBox cbProxyUseDefaultCredentials;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.Label label4;
        public System.Windows.Forms.CheckBox cbWSUseDefaultCredentials;
        public System.Windows.Forms.TextBox tbWSSoapCallTimeout;
        public System.Windows.Forms.Label label5;
        public System.Windows.Forms.TextBox tbProxyServer;
        public System.Windows.Forms.CheckBox cbUseProxy;
        public System.Windows.Forms.TableLayoutPanel tblPanel;
        public Ascend.Windows.Forms.GradientPanel gradientPanel1;
        public System.Windows.Forms.Button btnCancel;
        public System.Windows.Forms.Button btnOk;
        private TabControl.TabControl tabControl1;
        private TabControl.TabControlItem tabcServiceConfig;
        private TabControl.TabControlItem tabcUI;
        public System.Windows.Forms.CheckBox cbColorRawSoap;
        public System.Windows.Forms.CheckBox cbShowDetailedExc;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.ComponentModel.IContainer components;
        public System.Windows.Forms.Button btnAddFaveWs;
        public System.Windows.Forms.TextBox tbAddFaveWs;
        public System.Windows.Forms.CheckedListBox chkListBxTop5Ws;
    }
}