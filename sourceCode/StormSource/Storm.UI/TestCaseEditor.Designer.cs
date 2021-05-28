namespace Storm.UI
{
    partial class TestCaseEditor
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnEditRaw = new System.Windows.Forms.Button();
            this.btnXmlView = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.bgSyntaxHighlighter = new System.ComponentModel.BackgroundWorker();
            this.gradientPanel1 = new Ascend.Windows.Forms.GradientPanel();
            this.rtbRaw = new Storm.UI.CustomEditorRTB();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1.SuspendLayout();
            this.gradientPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.Controls.Add(this.btnEditRaw, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnXmlView, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnSave, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(386, 26);
            this.tableLayoutPanel1.TabIndex = 0;
            this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
            // 
            // btnEditRaw
            // 
            this.btnEditRaw.FlatAppearance.BorderSize = 0;
            this.btnEditRaw.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditRaw.Image = global::Storm.UI.StormResource.text_block16;
            this.btnEditRaw.Location = new System.Drawing.Point(337, 4);
            this.btnEditRaw.Name = "btnEditRaw";
            this.btnEditRaw.Size = new System.Drawing.Size(19, 18);
            this.btnEditRaw.TabIndex = 3;
            this.toolTip1.SetToolTip(this.btnEditRaw, "Edit");
            this.btnEditRaw.UseVisualStyleBackColor = true;
            this.btnEditRaw.Click += new System.EventHandler(this.btnEditRaw_Click);
            // 
            // btnXmlView
            // 
            this.btnXmlView.FlatAppearance.BorderSize = 0;
            this.btnXmlView.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXmlView.Image = global::Storm.UI.StormResource.xhtml;
            this.btnXmlView.Location = new System.Drawing.Point(311, 4);
            this.btnXmlView.Name = "btnXmlView";
            this.btnXmlView.Size = new System.Drawing.Size(19, 18);
            this.btnXmlView.TabIndex = 4;
            this.toolTip1.SetToolTip(this.btnXmlView, "Xml View");
            this.btnXmlView.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Image = global::Storm.UI.StormResource.disk;
            this.btnSave.Location = new System.Drawing.Point(363, 4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(19, 18);
            this.btnSave.TabIndex = 5;
            this.toolTip1.SetToolTip(this.btnSave, "Save");
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "label1";
            // 
            // webBrowser1
            // 
            this.webBrowser1.Location = new System.Drawing.Point(42, 223);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(141, 96);
            this.webBrowser1.TabIndex = 2;
            // 
            // bgSyntaxHighlighter
            // 
            this.bgSyntaxHighlighter.WorkerReportsProgress = true;
            this.bgSyntaxHighlighter.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgSyntaxHighlighter_DoWork);
            // 
            // gradientPanel1
            // 
            this.gradientPanel1.Controls.Add(this.tableLayoutPanel1);
            this.gradientPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.gradientPanel1.GradientLowColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.gradientPanel1.Location = new System.Drawing.Point(0, 0);
            this.gradientPanel1.Name = "gradientPanel1";
            this.gradientPanel1.Size = new System.Drawing.Size(386, 29);
            this.gradientPanel1.TabIndex = 3;
            // 
            // rtbRaw
            // 
            this.rtbRaw.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbRaw.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbRaw.Location = new System.Drawing.Point(32, 49);
            this.rtbRaw.Name = "rtbRaw";
            this.rtbRaw.Size = new System.Drawing.Size(328, 146);
            this.rtbRaw.TabIndex = 1;
            this.rtbRaw.Text = "";
            this.rtbRaw.WordWrap = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.webBrowser1);
            this.panel1.Controls.Add(this.rtbRaw);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 29);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(386, 366);
            this.panel1.TabIndex = 4;
            // 
            // TestCaseEditor
            // 
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.gradientPanel1);
            this.Name = "TestCaseEditor";
            this.Size = new System.Drawing.Size(386, 395);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.gradientPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Button btnEditRaw;
        public System.Windows.Forms.Button btnXmlView;
        public System.Windows.Forms.Button btnSave;
        public System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        public System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolTip toolTip1;
        public CustomEditorRTB rtbRaw;
        public System.Windows.Forms.WebBrowser webBrowser1;
        private System.ComponentModel.BackgroundWorker bgSyntaxHighlighter;
        public Ascend.Windows.Forms.GradientPanel gradientPanel1;
        private System.Windows.Forms.Panel panel1;
    }
}
