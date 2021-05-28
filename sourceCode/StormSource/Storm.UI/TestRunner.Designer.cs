namespace Storm.UI
{
    partial class TestRunner
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TestRunner));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tvTests = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tabTestInfo = new System.Windows.Forms.TabControl();
            this.tabPageRqResp = new System.Windows.Forms.TabPage();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tabPageLog = new System.Windows.Forms.TabPage();
            this.rtbLogTestRunner = new System.Windows.Forms.RichTextBox();
            this.tabPageError = new System.Windows.Forms.TabPage();
            this.rtbErrorsTestRunner = new System.Windows.Forms.RichTextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbNotes = new System.Windows.Forms.CheckBox();
            this.lnkLabelViewErr = new System.Windows.Forms.LinkLabel();
            this.lblCurenttest = new System.Windows.Forms.Label();
            this.btnRunOrStop = new System.Windows.Forms.Button();
            this.testCaseEditorNotes = new Storm.UI.TestCaseEditor();
            this.testCaseEditorRequest = new Storm.UI.TestCaseEditor();
            this.testCaseEditorExpectedResp = new Storm.UI.TestCaseEditor();
            this.testCaseEditorActualResp = new Storm.UI.TestCaseEditor();
            this.progBar = new Storm.UI.SmoothProgressbar();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabTestInfo.SuspendLayout();
            this.tabPageRqResp.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tabPageLog.SuspendLayout();
            this.tabPageError.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tvTests);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer4);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Size = new System.Drawing.Size(788, 390);
            this.splitContainer1.SplitterDistance = 153;
            this.splitContainer1.TabIndex = 0;
            // 
            // tvTests
            // 
            this.tvTests.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvTests.ImageIndex = 0;
            this.tvTests.ImageList = this.imageList1;
            this.tvTests.Location = new System.Drawing.Point(0, 0);
            this.tvTests.Name = "tvTests";
            this.tvTests.SelectedImageIndex = 0;
            this.tvTests.Size = new System.Drawing.Size(153, 390);
            this.tvTests.TabIndex = 0;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "bullet_green.png");
            this.imageList1.Images.SetKeyName(1, "bullet_orange.png");
            this.imageList1.Images.SetKeyName(2, "bullet_red.png");
            // 
            // splitContainer4
            // 
            this.splitContainer4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer4.Location = new System.Drawing.Point(3, 105);
            this.splitContainer4.Name = "splitContainer4";
            this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer4.Panel1.Controls.Add(this.testCaseEditorNotes);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer4.Size = new System.Drawing.Size(627, 284);
            this.splitContainer4.SplitterDistance = 60;
            this.splitContainer4.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.tabTestInfo);
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(617, 213);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Test Info";
            // 
            // tabTestInfo
            // 
            this.tabTestInfo.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabTestInfo.Controls.Add(this.tabPageRqResp);
            this.tabTestInfo.Controls.Add(this.tabPageLog);
            this.tabTestInfo.Controls.Add(this.tabPageError);
            this.tabTestInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabTestInfo.Location = new System.Drawing.Point(3, 16);
            this.tabTestInfo.Name = "tabTestInfo";
            this.tabTestInfo.SelectedIndex = 0;
            this.tabTestInfo.Size = new System.Drawing.Size(611, 194);
            this.tabTestInfo.TabIndex = 0;
            // 
            // tabPageRqResp
            // 
            this.tabPageRqResp.Controls.Add(this.splitContainer2);
            this.tabPageRqResp.Location = new System.Drawing.Point(4, 4);
            this.tabPageRqResp.Name = "tabPageRqResp";
            this.tabPageRqResp.Size = new System.Drawing.Size(603, 168);
            this.tabPageRqResp.TabIndex = 2;
            this.tabPageRqResp.Text = "Request/Response";
            this.tabPageRqResp.UseVisualStyleBackColor = true;
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.tableLayoutPanel3);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer2.Size = new System.Drawing.Size(603, 168);
            this.splitContainer2.SplitterDistance = 208;
            this.splitContainer2.TabIndex = 0;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.testCaseEditorRequest, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 390F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(206, 166);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.tableLayoutPanel1);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.tableLayoutPanel2);
            this.splitContainer3.Size = new System.Drawing.Size(389, 166);
            this.splitContainer3.SplitterDistance = 75;
            this.splitContainer3.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.SystemColors.Window;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.testCaseEditorExpectedResp, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 179F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(389, 75);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.SystemColors.Window;
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.testCaseEditorActualResp, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 207F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(389, 87);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // tabPageLog
            // 
            this.tabPageLog.Controls.Add(this.rtbLogTestRunner);
            this.tabPageLog.Location = new System.Drawing.Point(4, 4);
            this.tabPageLog.Name = "tabPageLog";
            this.tabPageLog.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageLog.Size = new System.Drawing.Size(609, 171);
            this.tabPageLog.TabIndex = 0;
            this.tabPageLog.Text = "Log";
            this.tabPageLog.UseVisualStyleBackColor = true;
            // 
            // rtbLogTestRunner
            // 
            this.rtbLogTestRunner.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbLogTestRunner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbLogTestRunner.Location = new System.Drawing.Point(3, 3);
            this.rtbLogTestRunner.Name = "rtbLogTestRunner";
            this.rtbLogTestRunner.Size = new System.Drawing.Size(603, 165);
            this.rtbLogTestRunner.TabIndex = 0;
            this.rtbLogTestRunner.Text = "";
            // 
            // tabPageError
            // 
            this.tabPageError.Controls.Add(this.rtbErrorsTestRunner);
            this.tabPageError.Location = new System.Drawing.Point(4, 4);
            this.tabPageError.Name = "tabPageError";
            this.tabPageError.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageError.Size = new System.Drawing.Size(609, 171);
            this.tabPageError.TabIndex = 1;
            this.tabPageError.Text = "Errors";
            this.tabPageError.UseVisualStyleBackColor = true;
            // 
            // rtbErrorsTestRunner
            // 
            this.rtbErrorsTestRunner.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbErrorsTestRunner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbErrorsTestRunner.ForeColor = System.Drawing.Color.DarkRed;
            this.rtbErrorsTestRunner.Location = new System.Drawing.Point(3, 3);
            this.rtbErrorsTestRunner.Name = "rtbErrorsTestRunner";
            this.rtbErrorsTestRunner.Size = new System.Drawing.Size(603, 165);
            this.rtbErrorsTestRunner.TabIndex = 0;
            this.rtbErrorsTestRunner.Text = "";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.cbNotes);
            this.groupBox2.Controls.Add(this.lnkLabelViewErr);
            this.groupBox2.Controls.Add(this.lblCurenttest);
            this.groupBox2.Controls.Add(this.btnRunOrStop);
            this.groupBox2.Controls.Add(this.progBar);
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(625, 96);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Execution";
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // cbNotes
            // 
            this.cbNotes.AutoSize = true;
            this.cbNotes.Location = new System.Drawing.Point(183, 70);
            this.cbNotes.Name = "cbNotes";
            this.cbNotes.Size = new System.Drawing.Size(54, 17);
            this.cbNotes.TabIndex = 4;
            this.cbNotes.Text = "Notes";
            this.cbNotes.UseVisualStyleBackColor = true;
            this.cbNotes.CheckedChanged += new System.EventHandler(this.cbNotes_CheckedChanged);
            // 
            // lnkLabelViewErr
            // 
            this.lnkLabelViewErr.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lnkLabelViewErr.AutoSize = true;
            this.lnkLabelViewErr.Location = new System.Drawing.Point(454, 71);
            this.lnkLabelViewErr.Name = "lnkLabelViewErr";
            this.lnkLabelViewErr.Size = new System.Drawing.Size(94, 13);
            this.lnkLabelViewErr.TabIndex = 3;
            this.lnkLabelViewErr.TabStop = true;
            this.lnkLabelViewErr.Text = "View Comparison?";
            this.lnkLabelViewErr.Visible = false;
            // 
            // lblCurenttest
            // 
            this.lblCurenttest.AutoSize = true;
            this.lblCurenttest.Location = new System.Drawing.Point(6, 71);
            this.lblCurenttest.Name = "lblCurenttest";
            this.lblCurenttest.Size = new System.Drawing.Size(171, 13);
            this.lblCurenttest.TabIndex = 2;
            this.lblCurenttest.Text = "Executing Test : {Test case name}";
            // 
            // btnRunOrStop
            // 
            this.btnRunOrStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRunOrStop.Location = new System.Drawing.Point(554, 19);
            this.btnRunOrStop.Name = "btnRunOrStop";
            this.btnRunOrStop.Size = new System.Drawing.Size(65, 37);
            this.btnRunOrStop.TabIndex = 1;
            this.btnRunOrStop.Text = "Run";
            this.btnRunOrStop.UseVisualStyleBackColor = true;
            // 
            // testCaseEditorNotes
            // 
            this.testCaseEditorNotes.BackGroundColor = System.Drawing.SystemColors.Info;
            this.testCaseEditorNotes.BrowserText = null;
            this.testCaseEditorNotes.ColorSoap = false;
            this.testCaseEditorNotes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.testCaseEditorNotes.Location = new System.Drawing.Point(0, 0);
            this.testCaseEditorNotes.Name = "testCaseEditorNotes";
            this.testCaseEditorNotes.RawText = "";
            this.testCaseEditorNotes.RawTextBackColor = System.Drawing.SystemColors.Window;
            this.testCaseEditorNotes.Size = new System.Drawing.Size(623, 56);
            this.testCaseEditorNotes.TabIndex = 0;
            this.testCaseEditorNotes.TargetFile = null;
            // 
            // testCaseEditorRequest
            // 
            this.testCaseEditorRequest.BackGroundColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.testCaseEditorRequest.BrowserText = null;
            this.testCaseEditorRequest.ColorSoap = false;
            this.testCaseEditorRequest.Dock = System.Windows.Forms.DockStyle.Fill;
            this.testCaseEditorRequest.Location = new System.Drawing.Point(3, 3);
            this.testCaseEditorRequest.Name = "testCaseEditorRequest";
            this.testCaseEditorRequest.RawText = "";
            this.testCaseEditorRequest.RawTextBackColor = System.Drawing.SystemColors.Window;
            this.testCaseEditorRequest.Size = new System.Drawing.Size(200, 384);
            this.testCaseEditorRequest.TabIndex = 1;
            this.testCaseEditorRequest.TargetFile = null;
            // 
            // testCaseEditorExpectedResp
            // 
            this.testCaseEditorExpectedResp.BackGroundColor = System.Drawing.Color.DarkSeaGreen;
            this.testCaseEditorExpectedResp.BrowserText = null;
            this.testCaseEditorExpectedResp.ColorSoap = false;
            this.testCaseEditorExpectedResp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.testCaseEditorExpectedResp.Location = new System.Drawing.Point(3, 3);
            this.testCaseEditorExpectedResp.Name = "testCaseEditorExpectedResp";
            this.testCaseEditorExpectedResp.RawText = "";
            this.testCaseEditorExpectedResp.RawTextBackColor = System.Drawing.SystemColors.Window;
            this.testCaseEditorExpectedResp.Size = new System.Drawing.Size(383, 173);
            this.testCaseEditorExpectedResp.TabIndex = 2;
            this.testCaseEditorExpectedResp.TargetFile = null;
            // 
            // testCaseEditorActualResp
            // 
            this.testCaseEditorActualResp.BackGroundColor = System.Drawing.SystemColors.MenuBar;
            this.testCaseEditorActualResp.BrowserText = null;
            this.testCaseEditorActualResp.ColorSoap = false;
            this.testCaseEditorActualResp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.testCaseEditorActualResp.Location = new System.Drawing.Point(3, 3);
            this.testCaseEditorActualResp.Name = "testCaseEditorActualResp";
            this.testCaseEditorActualResp.RawText = "";
            this.testCaseEditorActualResp.RawTextBackColor = System.Drawing.SystemColors.Window;
            this.testCaseEditorActualResp.Size = new System.Drawing.Size(383, 201);
            this.testCaseEditorActualResp.TabIndex = 3;
            this.testCaseEditorActualResp.TargetFile = null;
            this.testCaseEditorActualResp.Load += new System.EventHandler(this.testCaseEditor2_Load);
            // 
            // progBar
            // 
            this.progBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.progBar.Location = new System.Drawing.Point(6, 19);
            this.progBar.Maximum = 100;
            this.progBar.Minimum = 0;
            this.progBar.Name = "progBar";
            this.progBar.ProgressBarColor = System.Drawing.Color.Blue;
            this.progBar.Size = new System.Drawing.Size(542, 37);
            this.progBar.TabIndex = 0;
            this.progBar.Text = "smoothProgressbar1";
            this.progBar.Value = 0;
            // 
            // TestRunner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "TestRunner";
            this.Size = new System.Drawing.Size(788, 390);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            this.splitContainer4.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tabTestInfo.ResumeLayout(false);
            this.tabPageRqResp.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tabPageLog.ResumeLayout(false);
            this.tabPageError.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        public System.Windows.Forms.TreeView tvTests;
        private System.Windows.Forms.GroupBox groupBox2;
        public System.Windows.Forms.Label lblCurenttest;
        public System.Windows.Forms.Button btnRunOrStop;
        private System.Windows.Forms.GroupBox groupBox1;
        public SmoothProgressbar progBar;
        public System.Windows.Forms.TabControl tabTestInfo;
        public System.Windows.Forms.TabPage tabPageRqResp;
        public System.Windows.Forms.TabPage tabPageLog;
        public System.Windows.Forms.TabPage tabPageError;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        public System.Windows.Forms.RichTextBox rtbLogTestRunner;
        public System.Windows.Forms.RichTextBox rtbErrorsTestRunner;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        public TestCaseEditor testCaseEditorActualResp;
        public TestCaseEditor testCaseEditorExpectedResp;
        public TestCaseEditor testCaseEditorRequest;
        private System.Windows.Forms.SplitContainer splitContainer4;
        public System.Windows.Forms.LinkLabel lnkLabelViewErr;
        public System.Windows.Forms.CheckBox cbNotes;
        public TestCaseEditor testCaseEditorNotes;
    }
}
