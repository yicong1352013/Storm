namespace Storm.UI
{
    partial class TestEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TestEditor));
            this.gpToolBar = new Ascend.Windows.Forms.GradientPanel();
            this.btnSave = new System.Windows.Forms.Button();
            this.gradientLine2 = new Ascend.Windows.Forms.GradientLine();
            this.btnXmlView = new System.Windows.Forms.Button();
            this.btnEditRaw = new System.Windows.Forms.Button();
            this.btnQuickEdit = new System.Windows.Forms.Button();
            this.gradientLine1 = new Ascend.Windows.Forms.GradientLine();
            this.btnGo = new System.Windows.Forms.Button();
            this.spcEdit = new System.Windows.Forms.SplitContainer();
            this.tvQuickEditInput = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.spcRaw = new System.Windows.Forms.SplitContainer();
            this.rtbRaw = new Storm.UI.CustomEditorRTB();
            this.wbXmlView = new System.Windows.Forms.WebBrowser();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.bgSyntaxHighlighter = new System.ComponentModel.BackgroundWorker();
            this.colorTimer = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.gpToolBar.SuspendLayout();
            this.spcEdit.Panel1.SuspendLayout();
            this.spcEdit.Panel2.SuspendLayout();
            this.spcEdit.SuspendLayout();
            this.spcRaw.Panel1.SuspendLayout();
            this.spcRaw.Panel2.SuspendLayout();
            this.spcRaw.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gpToolBar
            // 
            this.gpToolBar.Border = new Ascend.Border(1);
            this.gpToolBar.Controls.Add(this.btnSave);
            this.gpToolBar.Controls.Add(this.gradientLine2);
            this.gpToolBar.Controls.Add(this.btnXmlView);
            this.gpToolBar.Controls.Add(this.btnEditRaw);
            this.gpToolBar.Controls.Add(this.btnQuickEdit);
            this.gpToolBar.Controls.Add(this.gradientLine1);
            this.gpToolBar.Controls.Add(this.btnGo);
            this.gpToolBar.Dock = System.Windows.Forms.DockStyle.Left;
            this.gpToolBar.GradientLowColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.gpToolBar.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.gpToolBar.Location = new System.Drawing.Point(3, 3);
            this.gpToolBar.Name = "gpToolBar";
            this.gpToolBar.RenderMode = Ascend.Windows.Forms.RenderMode.Glass;
            this.gpToolBar.Size = new System.Drawing.Size(28, 271);
            this.gpToolBar.TabIndex = 0;
            // 
            // btnSave
            // 
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Image = global::Storm.UI.StormResource.disk;
            this.btnSave.Location = new System.Drawing.Point(3, 143);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(21, 22);
            this.btnSave.TabIndex = 4;
            this.toolTip1.SetToolTip(this.btnSave, "Save");
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // gradientLine2
            // 
            this.gradientLine2.GradientHighColor = System.Drawing.SystemColors.ScrollBar;
            this.gradientLine2.GradientLowColor = System.Drawing.SystemColors.ControlDarkDark;
            this.gradientLine2.Location = new System.Drawing.Point(5, 135);
            this.gradientLine2.Name = "gradientLine2";
            this.gradientLine2.Size = new System.Drawing.Size(40, 2);
            this.gradientLine2.TabIndex = 1;
            // 
            // btnXmlView
            // 
            this.btnXmlView.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXmlView.Image = global::Storm.UI.StormResource.xhtml;
            this.btnXmlView.Location = new System.Drawing.Point(3, 106);
            this.btnXmlView.Name = "btnXmlView";
            this.btnXmlView.Size = new System.Drawing.Size(21, 20);
            this.btnXmlView.TabIndex = 3;
            this.toolTip1.SetToolTip(this.btnXmlView, "XML view");
            this.btnXmlView.UseVisualStyleBackColor = true;
            this.btnXmlView.Click += new System.EventHandler(this.btnXmlView_Click);
            // 
            // btnEditRaw
            // 
            this.btnEditRaw.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditRaw.Image = global::Storm.UI.StormResource.text_block16;
            this.btnEditRaw.Location = new System.Drawing.Point(3, 77);
            this.btnEditRaw.Name = "btnEditRaw";
            this.btnEditRaw.Size = new System.Drawing.Size(21, 23);
            this.btnEditRaw.TabIndex = 2;
            this.toolTip1.SetToolTip(this.btnEditRaw, "Raw View");
            this.btnEditRaw.UseVisualStyleBackColor = true;
            this.btnEditRaw.Click += new System.EventHandler(this.btnEditRaw_Click);
            // 
            // btnQuickEdit
            // 
            this.btnQuickEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuickEdit.Image = global::Storm.UI.StormResource.lightning;
            this.btnQuickEdit.Location = new System.Drawing.Point(3, 49);
            this.btnQuickEdit.Name = "btnQuickEdit";
            this.btnQuickEdit.Size = new System.Drawing.Size(21, 20);
            this.btnQuickEdit.TabIndex = 1;
            this.toolTip1.SetToolTip(this.btnQuickEdit, "Tree View");
            this.btnQuickEdit.UseVisualStyleBackColor = true;
            this.btnQuickEdit.Click += new System.EventHandler(this.btnQuickEdit_Click);
            // 
            // gradientLine1
            // 
            this.gradientLine1.GradientHighColor = System.Drawing.SystemColors.ScrollBar;
            this.gradientLine1.GradientLowColor = System.Drawing.SystemColors.ControlDarkDark;
            this.gradientLine1.Location = new System.Drawing.Point(4, 42);
            this.gradientLine1.Name = "gradientLine1";
            this.gradientLine1.Size = new System.Drawing.Size(40, 2);
            this.gradientLine1.TabIndex = 0;
            // 
            // btnGo
            // 
            this.btnGo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGo.Image = global::Storm.UI.StormResource.arrow_right;
            this.btnGo.Location = new System.Drawing.Point(3, 12);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(21, 20);
            this.btnGo.TabIndex = 0;
            this.toolTip1.SetToolTip(this.btnGo, "Send");
            this.btnGo.UseVisualStyleBackColor = true;
            // 
            // spcEdit
            // 
            this.spcEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spcEdit.Location = new System.Drawing.Point(37, 3);
            this.spcEdit.Name = "spcEdit";
            // 
            // spcEdit.Panel1
            // 
            this.spcEdit.Panel1.Controls.Add(this.tvQuickEditInput);
            this.spcEdit.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.spcEdit_Panel1_Paint);
            // 
            // spcEdit.Panel2
            // 
            this.spcEdit.Panel2.Controls.Add(this.spcRaw);
            this.spcEdit.Size = new System.Drawing.Size(442, 271);
            this.spcEdit.SplitterDistance = 120;
            this.spcEdit.TabIndex = 0;
            // 
            // tvQuickEditInput
            // 
            this.tvQuickEditInput.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tvQuickEditInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvQuickEditInput.FullRowSelect = true;
            this.tvQuickEditInput.ImageIndex = 0;
            this.tvQuickEditInput.ImageList = this.imageList1;
            this.tvQuickEditInput.Location = new System.Drawing.Point(0, 0);
            this.tvQuickEditInput.Name = "tvQuickEditInput";
            this.tvQuickEditInput.SelectedImageIndex = 2;
            this.tvQuickEditInput.Size = new System.Drawing.Size(120, 271);
            this.tvQuickEditInput.TabIndex = 0;
            this.tvQuickEditInput.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "bullet_green.png");
            this.imageList1.Images.SetKeyName(1, "bullet_blank.png");
            this.imageList1.Images.SetKeyName(2, "bullet_go.png");
            // 
            // spcRaw
            // 
            this.spcRaw.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spcRaw.Location = new System.Drawing.Point(0, 0);
            this.spcRaw.Name = "spcRaw";
            // 
            // spcRaw.Panel1
            // 
            this.spcRaw.Panel1.Controls.Add(this.rtbRaw);
            // 
            // spcRaw.Panel2
            // 
            this.spcRaw.Panel2.Controls.Add(this.wbXmlView);
            this.spcRaw.Size = new System.Drawing.Size(318, 271);
            this.spcRaw.SplitterDistance = 133;
            this.spcRaw.TabIndex = 0;
            // 
            // rtbRaw
            // 
            this.rtbRaw.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbRaw.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbRaw.Location = new System.Drawing.Point(0, 0);
            this.rtbRaw.Name = "rtbRaw";
            this.rtbRaw.Size = new System.Drawing.Size(133, 271);
            this.rtbRaw.TabIndex = 1;
            this.rtbRaw.Text = "";
            this.rtbRaw.WordWrap = false;
            // 
            // wbXmlView
            // 
            this.wbXmlView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wbXmlView.Location = new System.Drawing.Point(0, 0);
            this.wbXmlView.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbXmlView.Name = "wbXmlView";
            this.wbXmlView.Size = new System.Drawing.Size(181, 271);
            this.wbXmlView.TabIndex = 0;
            // 
            // bgSyntaxHighlighter
            // 
            this.bgSyntaxHighlighter.WorkerReportsProgress = true;
            this.bgSyntaxHighlighter.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgSyntaxHighlighter_DoWork);
            // 
            // colorTimer
            // 
            this.colorTimer.Interval = 300;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.SystemColors.Window;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.spcEdit, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.gpToolBar, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(482, 277);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // TestEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "TestEditor";
            this.Size = new System.Drawing.Size(482, 277);
            this.gpToolBar.ResumeLayout(false);
            this.spcEdit.Panel1.ResumeLayout(false);
            this.spcEdit.Panel2.ResumeLayout(false);
            this.spcEdit.ResumeLayout(false);
            this.spcRaw.Panel1.ResumeLayout(false);
            this.spcRaw.Panel2.ResumeLayout(false);
            this.spcRaw.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Ascend.Windows.Forms.GradientLine gradientLine1;
        private System.Windows.Forms.ToolTip toolTip1;
        private Ascend.Windows.Forms.GradientLine gradientLine2;
        public System.Windows.Forms.Button btnGo;
        public System.Windows.Forms.Button btnQuickEdit;
        public System.Windows.Forms.Button btnXmlView;
        public System.Windows.Forms.Button btnEditRaw;
        public System.Windows.Forms.Button btnSave;
        public Ascend.Windows.Forms.GradientPanel gpToolBar;
        public System.Windows.Forms.SplitContainer spcEdit;
        public System.Windows.Forms.TreeView tvQuickEditInput;
        public System.Windows.Forms.SplitContainer spcRaw;
        public System.Windows.Forms.WebBrowser wbXmlView;
        private System.Windows.Forms.ImageList imageList1;
        private System.ComponentModel.BackgroundWorker bgSyntaxHighlighter;
        private System.Windows.Forms.Timer colorTimer;
        public CustomEditorRTB rtbRaw;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}
