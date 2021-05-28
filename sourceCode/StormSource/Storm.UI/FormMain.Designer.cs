namespace Storm.UI
{
    partial class FormMain
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
        public void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Web Services");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.scMain = new System.Windows.Forms.SplitContainer();
            this.scLeft_OfscMain = new System.Windows.Forms.SplitContainer();
            this.tvWebServices = new System.Windows.Forms.TreeView();
            this.imgList_All = new System.Windows.Forms.ImageList(this.components);
            this.gradientClock = new Ascend.Windows.Forms.GradientCaption();
            this.tabConfigs = new System.Windows.Forms.TabControl();
            this.tabPageReqConfig = new System.Windows.Forms.TabPage();
            this.pgWebServiceProp = new System.Windows.Forms.PropertyGrid();
            this.tabPageMiscConfig = new System.Windows.Forms.TabPage();
            this.pgAppCOnfig = new System.Windows.Forms.PropertyGrid();
            this.gradientCaptionWebCfg = new Ascend.Windows.Forms.GradientCaption();
            this.scRight_OfscMain = new System.Windows.Forms.SplitContainer();
            this.gradientLine1 = new Ascend.Windows.Forms.GradientLine();
            this.rtbLog = new System.Windows.Forms.RichTextBox();
            this.ctxMenuStripLog = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hideToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.tsLabelDoneOrProcessing = new System.Windows.Forms.ToolStripStatusLabel();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbButtonAdd = new System.Windows.Forms.ToolStripButton();
            this.tsBtnRemove = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbSave = new System.Windows.Forms.ToolStripButton();
            this.tsbOpen = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsBtnConfig = new System.Windows.Forms.ToolStripButton();
            this.tsBtnColorScheme = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.helpToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.newWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsAddMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsRemoveMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsViewMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showQuickTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMenuItemCfg = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMenuItemPlugin = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMenuItemFSharp = new System.Windows.Forms.ToolStripMenuItem();
            this.clocktimer1 = new System.Windows.Forms.Timer(this.components);
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.ctxMenuStripWebService = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ctxMenuItemRunTests = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxMenuItemViewWsdl = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxMenuItemRemove = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxMenuStripWebMethod = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ctxMenuItemOpenInTab = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxMenuItemRunTest = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxMenuStripTestCase = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ctxMenuItemRun = new System.Windows.Forms.ToolStripMenuItem();
            this.tsShowConfigSubMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.tsAboutFSSubMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.tabcMainTests = new TabControl.TabControl();
            this.tabcQuickTest = new TabControl.TabControlItem();
            this.spcTestRun = new System.Windows.Forms.SplitContainer();
            this.testEditorReq = new Storm.UI.TestEditor();
            this.testEditorResponseControl1 = new Storm.UI.TestEditorResponseControl();
            this.tabcWsdl = new TabControl.TabControlItem();
            this.wbWsdl = new System.Windows.Forms.WebBrowser();
            this.tabControlItem2 = new TabControl.TabControlItem();
            this.testRunner1 = new Storm.UI.TestRunner();
            this.tabControlItem1 = new TabControl.TabControlItem();
            this.scMain.Panel1.SuspendLayout();
            this.scMain.Panel2.SuspendLayout();
            this.scMain.SuspendLayout();
            this.scLeft_OfscMain.Panel1.SuspendLayout();
            this.scLeft_OfscMain.Panel2.SuspendLayout();
            this.scLeft_OfscMain.SuspendLayout();
            this.tabConfigs.SuspendLayout();
            this.tabPageReqConfig.SuspendLayout();
            this.tabPageMiscConfig.SuspendLayout();
            this.scRight_OfscMain.Panel1.SuspendLayout();
            this.scRight_OfscMain.Panel2.SuspendLayout();
            this.scRight_OfscMain.SuspendLayout();
            this.ctxMenuStripLog.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.menuStrip2.SuspendLayout();
            this.ctxMenuStripWebService.SuspendLayout();
            this.ctxMenuStripWebMethod.SuspendLayout();
            this.ctxMenuStripTestCase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabcMainTests)).BeginInit();
            this.tabcMainTests.SuspendLayout();
            this.tabcQuickTest.SuspendLayout();
            this.spcTestRun.Panel1.SuspendLayout();
            this.spcTestRun.Panel2.SuspendLayout();
            this.spcTestRun.SuspendLayout();
            this.tabcWsdl.SuspendLayout();
            this.tabControlItem2.SuspendLayout();
            this.SuspendLayout();
            // 
            // scMain
            // 
            this.scMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.scMain.BackColor = System.Drawing.SystemColors.Control;
            this.scMain.Location = new System.Drawing.Point(0, 67);
            this.scMain.Name = "scMain";
            // 
            // scMain.Panel1
            // 
            this.scMain.Panel1.Controls.Add(this.scLeft_OfscMain);
            // 
            // scMain.Panel2
            // 
            this.scMain.Panel2.Controls.Add(this.scRight_OfscMain);
            this.scMain.Size = new System.Drawing.Size(783, 384);
            this.scMain.SplitterDistance = 185;
            this.scMain.TabIndex = 1;
            // 
            // scLeft_OfscMain
            // 
            this.scLeft_OfscMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.scLeft_OfscMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scLeft_OfscMain.Location = new System.Drawing.Point(0, 0);
            this.scLeft_OfscMain.Name = "scLeft_OfscMain";
            this.scLeft_OfscMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scLeft_OfscMain.Panel1
            // 
            this.scLeft_OfscMain.Panel1.Controls.Add(this.tvWebServices);
            this.scLeft_OfscMain.Panel1.Controls.Add(this.gradientClock);
            // 
            // scLeft_OfscMain.Panel2
            // 
            this.scLeft_OfscMain.Panel2.Controls.Add(this.tabConfigs);
            this.scLeft_OfscMain.Panel2.Controls.Add(this.gradientCaptionWebCfg);
            this.scLeft_OfscMain.Size = new System.Drawing.Size(185, 384);
            this.scLeft_OfscMain.SplitterDistance = 229;
            this.scLeft_OfscMain.TabIndex = 0;
            // 
            // tvWebServices
            // 
            this.tvWebServices.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tvWebServices.ImageIndex = 1;
            this.tvWebServices.ImageList = this.imgList_All;
            this.tvWebServices.Location = new System.Drawing.Point(-2, 23);
            this.tvWebServices.Name = "tvWebServices";
            treeNode1.Name = "webServicesNode";
            treeNode1.Text = "Web Services";
            this.tvWebServices.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this.tvWebServices.SelectedImageIndex = 1;
            this.tvWebServices.ShowNodeToolTips = true;
            this.tvWebServices.Size = new System.Drawing.Size(186, 204);
            this.tvWebServices.TabIndex = 0;
            this.tvWebServices.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvWebServices_AfterSelect);
            // 
            // imgList_All
            // 
            this.imgList_All.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgList_All.ImageStream")));
            this.imgList_All.TransparentColor = System.Drawing.Color.Transparent;
            this.imgList_All.Images.SetKeyName(0, "");
            this.imgList_All.Images.SetKeyName(1, "");
            this.imgList_All.Images.SetKeyName(2, "");
            this.imgList_All.Images.SetKeyName(3, "");
            this.imgList_All.Images.SetKeyName(4, "");
            this.imgList_All.Images.SetKeyName(5, "");
            this.imgList_All.Images.SetKeyName(6, "");
            this.imgList_All.Images.SetKeyName(7, "");
            this.imgList_All.Images.SetKeyName(8, "");
            this.imgList_All.Images.SetKeyName(9, "");
            this.imgList_All.Images.SetKeyName(10, "");
            this.imgList_All.Images.SetKeyName(11, "");
            this.imgList_All.Images.SetKeyName(12, "");
            this.imgList_All.Images.SetKeyName(13, "");
            this.imgList_All.Images.SetKeyName(14, "");
            this.imgList_All.Images.SetKeyName(15, "");
            this.imgList_All.Images.SetKeyName(16, "");
            this.imgList_All.Images.SetKeyName(17, "");
            this.imgList_All.Images.SetKeyName(18, "");
            this.imgList_All.Images.SetKeyName(19, "");
            this.imgList_All.Images.SetKeyName(20, "bullet_go.png");
            // 
            // gradientClock
            // 
            this.gradientClock.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gradientClock.Border = new Ascend.Border(0);
            this.gradientClock.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gradientClock.ForeColor = System.Drawing.SystemColors.ControlText;
            this.gradientClock.GradientHighColor = System.Drawing.SystemColors.Window;
            this.gradientClock.GradientLowColor = System.Drawing.SystemColors.AppWorkspace;
            this.gradientClock.Image = global::Storm.UI.StormResource.clock;
            this.gradientClock.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.gradientClock.Location = new System.Drawing.Point(-1, 0);
            this.gradientClock.Name = "gradientClock";
            this.gradientClock.RenderMode = Ascend.Windows.Forms.RenderMode.Glass;
            this.gradientClock.Size = new System.Drawing.Size(185, 26);
            this.gradientClock.TabIndex = 1;
            this.gradientClock.Click += new System.EventHandler(this.gradientClock_Click);
            // 
            // tabConfigs
            // 
            this.tabConfigs.Controls.Add(this.tabPageReqConfig);
            this.tabConfigs.Controls.Add(this.tabPageMiscConfig);
            this.tabConfigs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabConfigs.Location = new System.Drawing.Point(0, 23);
            this.tabConfigs.Multiline = true;
            this.tabConfigs.Name = "tabConfigs";
            this.tabConfigs.SelectedIndex = 0;
            this.tabConfigs.Size = new System.Drawing.Size(183, 126);
            this.tabConfigs.TabIndex = 4;
            // 
            // tabPageReqConfig
            // 
            this.tabPageReqConfig.Controls.Add(this.pgWebServiceProp);
            this.tabPageReqConfig.Location = new System.Drawing.Point(4, 22);
            this.tabPageReqConfig.Name = "tabPageReqConfig";
            this.tabPageReqConfig.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageReqConfig.Size = new System.Drawing.Size(175, 100);
            this.tabPageReqConfig.TabIndex = 0;
            this.tabPageReqConfig.Text = "Request";
            this.tabPageReqConfig.UseVisualStyleBackColor = true;
            // 
            // pgWebServiceProp
            // 
            this.pgWebServiceProp.BackColor = System.Drawing.SystemColors.Window;
            this.pgWebServiceProp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgWebServiceProp.HelpVisible = false;
            this.pgWebServiceProp.Location = new System.Drawing.Point(3, 3);
            this.pgWebServiceProp.Name = "pgWebServiceProp";
            this.pgWebServiceProp.Size = new System.Drawing.Size(169, 94);
            this.pgWebServiceProp.TabIndex = 0;
            this.pgWebServiceProp.ToolbarVisible = false;
            // 
            // tabPageMiscConfig
            // 
            this.tabPageMiscConfig.Controls.Add(this.pgAppCOnfig);
            this.tabPageMiscConfig.Location = new System.Drawing.Point(4, 22);
            this.tabPageMiscConfig.Name = "tabPageMiscConfig";
            this.tabPageMiscConfig.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMiscConfig.Size = new System.Drawing.Size(175, 100);
            this.tabPageMiscConfig.TabIndex = 1;
            this.tabPageMiscConfig.Text = "Misc";
            this.tabPageMiscConfig.UseVisualStyleBackColor = true;
            // 
            // pgAppCOnfig
            // 
            this.pgAppCOnfig.BackColor = System.Drawing.SystemColors.Window;
            this.pgAppCOnfig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgAppCOnfig.HelpVisible = false;
            this.pgAppCOnfig.Location = new System.Drawing.Point(3, 3);
            this.pgAppCOnfig.Name = "pgAppCOnfig";
            this.pgAppCOnfig.Size = new System.Drawing.Size(169, 94);
            this.pgAppCOnfig.TabIndex = 1;
            this.pgAppCOnfig.ToolbarVisible = false;
            // 
            // gradientCaptionWebCfg
            // 
            this.gradientCaptionWebCfg.Border = new Ascend.Border(0);
            this.gradientCaptionWebCfg.Dock = System.Windows.Forms.DockStyle.Top;
            this.gradientCaptionWebCfg.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gradientCaptionWebCfg.ForeColor = System.Drawing.SystemColors.ControlText;
            this.gradientCaptionWebCfg.GradientHighColor = System.Drawing.SystemColors.Window;
            this.gradientCaptionWebCfg.GradientLowColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.gradientCaptionWebCfg.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.gradientCaptionWebCfg.Location = new System.Drawing.Point(0, 0);
            this.gradientCaptionWebCfg.Name = "gradientCaptionWebCfg";
            this.gradientCaptionWebCfg.Size = new System.Drawing.Size(183, 23);
            this.gradientCaptionWebCfg.TabIndex = 2;
            this.gradientCaptionWebCfg.Text = "Web Method Configuration";
            this.gradientCaptionWebCfg.Click += new System.EventHandler(this.gradientCaptionWebCfg_Click);
            // 
            // scRight_OfscMain
            // 
            this.scRight_OfscMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scRight_OfscMain.Location = new System.Drawing.Point(0, 0);
            this.scRight_OfscMain.Name = "scRight_OfscMain";
            this.scRight_OfscMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scRight_OfscMain.Panel1
            // 
            this.scRight_OfscMain.Panel1.Controls.Add(this.tabcMainTests);
            this.scRight_OfscMain.Panel1.Controls.Add(this.gradientLine1);
            this.scRight_OfscMain.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.scRight_OfscMain_Panel1_Paint);
            // 
            // scRight_OfscMain.Panel2
            // 
            this.scRight_OfscMain.Panel2.Controls.Add(this.rtbLog);
            this.scRight_OfscMain.Size = new System.Drawing.Size(594, 384);
            this.scRight_OfscMain.SplitterDistance = 302;
            this.scRight_OfscMain.SplitterWidth = 3;
            this.scRight_OfscMain.TabIndex = 1;
            // 
            // gradientLine1
            // 
            this.gradientLine1.Dock = System.Windows.Forms.DockStyle.Top;
            this.gradientLine1.GradientLowColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.gradientLine1.Location = new System.Drawing.Point(0, 0);
            this.gradientLine1.Name = "gradientLine1";
            this.gradientLine1.Size = new System.Drawing.Size(594, 4);
            this.gradientLine1.TabIndex = 3;
            // 
            // rtbLog
            // 
            this.rtbLog.BackColor = System.Drawing.Color.GhostWhite;
            this.rtbLog.ContextMenuStrip = this.ctxMenuStripLog;
            this.rtbLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbLog.Location = new System.Drawing.Point(0, 0);
            this.rtbLog.Name = "rtbLog";
            this.rtbLog.Size = new System.Drawing.Size(594, 79);
            this.rtbLog.TabIndex = 0;
            this.rtbLog.Text = "";
            // 
            // ctxMenuStripLog
            // 
            this.ctxMenuStripLog.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearToolStripMenuItem,
            this.hideToolStripMenuItem});
            this.ctxMenuStripLog.Name = "ctxMenuStripLog";
            this.ctxMenuStripLog.Size = new System.Drawing.Size(102, 48);
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(101, 22);
            this.clearToolStripMenuItem.Text = "Clear";
            this.clearToolStripMenuItem.Click += new System.EventHandler(this.clearToolStripMenuItem_Click);
            // 
            // hideToolStripMenuItem
            // 
            this.hideToolStripMenuItem.Name = "hideToolStripMenuItem";
            this.hideToolStripMenuItem.Size = new System.Drawing.Size(101, 22);
            this.hideToolStripMenuItem.Text = "Hide";
            this.hideToolStripMenuItem.Click += new System.EventHandler(this.hideToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel3,
            this.tsLabelDoneOrProcessing,
            this.tsProgressBar1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 448);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(783, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(155, 17);
            this.toolStripStatusLabel1.Text = "© erik.araojo@hotmail.com";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(473, 17);
            this.toolStripStatusLabel3.Spring = true;
            // 
            // tsProgressBar1
            // 
            this.tsProgressBar1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsProgressBar1.Name = "tsProgressBar1";
            this.tsProgressBar1.Size = new System.Drawing.Size(100, 16);
            // 
            // tsLabelDoneOrProcessing
            // 
            this.tsLabelDoneOrProcessing.Name = "tsLabelDoneOrProcessing";
            this.tsLabelDoneOrProcessing.Size = new System.Drawing.Size(38, 17);
            this.tsLabelDoneOrProcessing.Text = "Done.";
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbButtonAdd,
            this.tsBtnRemove,
            this.toolStripSeparator2,
            this.tsbSave,
            this.tsbOpen,
            this.toolStripSeparator3,
            this.tsBtnConfig,
            this.tsBtnColorScheme,
            this.toolStripSeparator8,
            this.helpToolStripButton});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(783, 38);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbButtonAdd
            // 
            this.tsbButtonAdd.Image = global::Storm.UI.StormResource.add;
            this.tsbButtonAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbButtonAdd.Name = "tsbButtonAdd";
            this.tsbButtonAdd.Size = new System.Drawing.Size(33, 35);
            this.tsbButtonAdd.Text = "Add";
            this.tsbButtonAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbButtonAdd.ToolTipText = "Add a web service";
            // 
            // tsBtnRemove
            // 
            this.tsBtnRemove.Image = global::Storm.UI.StormResource.delete;
            this.tsBtnRemove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnRemove.Name = "tsBtnRemove";
            this.tsBtnRemove.Size = new System.Drawing.Size(54, 35);
            this.tsBtnRemove.Text = "Remove";
            this.tsBtnRemove.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsBtnRemove.ToolTipText = "Remove web service";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 38);
            // 
            // tsbSave
            // 
            this.tsbSave.Image = global::Storm.UI.StormResource.disk;
            this.tsbSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSave.Name = "tsbSave";
            this.tsbSave.Size = new System.Drawing.Size(35, 35);
            this.tsbSave.Text = "Save";
            this.tsbSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.tsbSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbSave.ToolTipText = "Save as storm project";
            // 
            // tsbOpen
            // 
            this.tsbOpen.Image = global::Storm.UI.StormResource.folder;
            this.tsbOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbOpen.Name = "tsbOpen";
            this.tsbOpen.Size = new System.Drawing.Size(40, 35);
            this.tsbOpen.Text = "Open";
            this.tsbOpen.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbOpen.ToolTipText = "Open storm project";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 38);
            // 
            // tsBtnConfig
            // 
            this.tsBtnConfig.Image = global::Storm.UI.StormResource.wrench;
            this.tsBtnConfig.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnConfig.Name = "tsBtnConfig";
            this.tsBtnConfig.Size = new System.Drawing.Size(47, 35);
            this.tsBtnConfig.Text = "Config";
            this.tsBtnConfig.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsBtnConfig.ToolTipText = "Global Configuration";
            // 
            // tsBtnColorScheme
            // 
            this.tsBtnColorScheme.Image = global::Storm.UI.StormResource.colorize22;
            this.tsBtnColorScheme.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnColorScheme.Name = "tsBtnColorScheme";
            this.tsBtnColorScheme.Size = new System.Drawing.Size(45, 35);
            this.tsBtnColorScheme.Text = "Colors";
            this.tsBtnColorScheme.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsBtnColorScheme.ToolTipText = "Modify Colors";
            this.tsBtnColorScheme.Visible = false;
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 38);
            // 
            // helpToolStripButton
            // 
            this.helpToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("helpToolStripButton.Image")));
            this.helpToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.helpToolStripButton.Name = "helpToolStripButton";
            this.helpToolStripButton.Size = new System.Drawing.Size(44, 35);
            this.helpToolStripButton.Text = "About";
            this.helpToolStripButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // menuStrip2
            // 
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem1,
            this.tsViewMenuItem,
            this.tsMenuItemCfg,
            this.tsMenuItemPlugin,
            this.tsMenuItemFSharp});
            this.menuStrip2.Location = new System.Drawing.Point(0, 0);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Size = new System.Drawing.Size(783, 24);
            this.menuStrip2.TabIndex = 3;
            this.menuStrip2.Text = "menuStrip2";
            // 
            // fileToolStripMenuItem1
            // 
            this.fileToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newWindowToolStripMenuItem,
            this.tsAddMenuItem1,
            this.tsRemoveMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem1.Name = "fileToolStripMenuItem1";
            this.fileToolStripMenuItem1.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem1.Text = "&File";
            // 
            // newWindowToolStripMenuItem
            // 
            this.newWindowToolStripMenuItem.Name = "newWindowToolStripMenuItem";
            this.newWindowToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.newWindowToolStripMenuItem.Text = "New Window";
            // 
            // tsAddMenuItem1
            // 
            this.tsAddMenuItem1.Name = "tsAddMenuItem1";
            this.tsAddMenuItem1.Size = new System.Drawing.Size(145, 22);
            this.tsAddMenuItem1.Text = "Add";
            // 
            // tsRemoveMenuItem
            // 
            this.tsRemoveMenuItem.Name = "tsRemoveMenuItem";
            this.tsRemoveMenuItem.Size = new System.Drawing.Size(145, 22);
            this.tsRemoveMenuItem.Text = "Remove";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // tsViewMenuItem
            // 
            this.tsViewMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.logToolStripMenuItem,
            this.showQuickTestToolStripMenuItem});
            this.tsViewMenuItem.Name = "tsViewMenuItem";
            this.tsViewMenuItem.Size = new System.Drawing.Size(44, 20);
            this.tsViewMenuItem.Text = "&View";
            // 
            // logToolStripMenuItem
            // 
            this.logToolStripMenuItem.Name = "logToolStripMenuItem";
            this.logToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.logToolStripMenuItem.Text = "Hide logging window";
            this.logToolStripMenuItem.Click += new System.EventHandler(this.logToolStripMenuItem_Click);
            // 
            // showQuickTestToolStripMenuItem
            // 
            this.showQuickTestToolStripMenuItem.Name = "showQuickTestToolStripMenuItem";
            this.showQuickTestToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.showQuickTestToolStripMenuItem.Text = "Hide Quick Test";
            this.showQuickTestToolStripMenuItem.Visible = false;
            this.showQuickTestToolStripMenuItem.Click += new System.EventHandler(this.showQuickTestToolStripMenuItem_Click);
            // 
            // tsMenuItemCfg
            // 
            this.tsMenuItemCfg.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsShowConfigSubMenu});
            this.tsMenuItemCfg.Name = "tsMenuItemCfg";
            this.tsMenuItemCfg.Size = new System.Drawing.Size(93, 20);
            this.tsMenuItemCfg.Text = "&Configuration";
            // 
            // tsMenuItemPlugin
            // 
            this.tsMenuItemPlugin.Name = "tsMenuItemPlugin";
            this.tsMenuItemPlugin.Size = new System.Drawing.Size(53, 20);
            this.tsMenuItemPlugin.Text = "&Plugin";
            this.tsMenuItemPlugin.Visible = false;
            // 
            // tsMenuItemFSharp
            // 
            this.tsMenuItemFSharp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsAboutFSSubMenu});
            this.tsMenuItemFSharp.Name = "tsMenuItemFSharp";
            this.tsMenuItemFSharp.Size = new System.Drawing.Size(32, 20);
            this.tsMenuItemFSharp.Text = "F#";
            // 
            // clocktimer1
            // 
            this.clocktimer1.Enabled = true;
            this.clocktimer1.Interval = 1000;
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.DefaultExt = "xml";
            this.saveFileDialog.Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*";
            this.saveFileDialog.RestoreDirectory = true;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "storm project(*.stormproj)|*.stormproj";
            this.openFileDialog1.RestoreDirectory = true;
            // 
            // ctxMenuStripWebService
            // 
            this.ctxMenuStripWebService.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctxMenuItemRunTests,
            this.ctxMenuItemViewWsdl,
            this.ctxMenuItemRemove});
            this.ctxMenuStripWebService.Name = "contextMenuStrip1";
            this.ctxMenuStripWebService.Size = new System.Drawing.Size(143, 70);
            // 
            // ctxMenuItemRunTests
            // 
            this.ctxMenuItemRunTests.Enabled = false;
            this.ctxMenuItemRunTests.Name = "ctxMenuItemRunTests";
            this.ctxMenuItemRunTests.Size = new System.Drawing.Size(142, 22);
            this.ctxMenuItemRunTests.Text = "Run All Tests";
            this.ctxMenuItemRunTests.Visible = false;
            this.ctxMenuItemRunTests.Click += new System.EventHandler(this.openInNewTabToolStripMenuItem_Click);
            // 
            // ctxMenuItemViewWsdl
            // 
            this.ctxMenuItemViewWsdl.Name = "ctxMenuItemViewWsdl";
            this.ctxMenuItemViewWsdl.Size = new System.Drawing.Size(142, 22);
            this.ctxMenuItemViewWsdl.Text = "View Wsdl";
            // 
            // ctxMenuItemRemove
            // 
            this.ctxMenuItemRemove.Name = "ctxMenuItemRemove";
            this.ctxMenuItemRemove.Size = new System.Drawing.Size(142, 22);
            this.ctxMenuItemRemove.Text = "Remove";
            // 
            // ctxMenuStripWebMethod
            // 
            this.ctxMenuStripWebMethod.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctxMenuItemOpenInTab,
            this.ctxMenuItemRunTest});
            this.ctxMenuStripWebMethod.Name = "ctxMenuStripWebMethod";
            this.ctxMenuStripWebMethod.Size = new System.Drawing.Size(162, 48);
            this.ctxMenuStripWebMethod.Opening += new System.ComponentModel.CancelEventHandler(this.ctxMenuStripWebMethod_Opening);
            // 
            // ctxMenuItemOpenInTab
            // 
            this.ctxMenuItemOpenInTab.Name = "ctxMenuItemOpenInTab";
            this.ctxMenuItemOpenInTab.Size = new System.Drawing.Size(161, 22);
            this.ctxMenuItemOpenInTab.Text = "Open in new tab";
            // 
            // ctxMenuItemRunTest
            // 
            this.ctxMenuItemRunTest.Enabled = false;
            this.ctxMenuItemRunTest.Name = "ctxMenuItemRunTest";
            this.ctxMenuItemRunTest.Size = new System.Drawing.Size(161, 22);
            this.ctxMenuItemRunTest.Text = "Run Tests";
            this.ctxMenuItemRunTest.Visible = false;
            // 
            // ctxMenuStripTestCase
            // 
            this.ctxMenuStripTestCase.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctxMenuItemRun});
            this.ctxMenuStripTestCase.Name = "ctxMenuStripTestCase";
            this.ctxMenuStripTestCase.Size = new System.Drawing.Size(121, 26);
            // 
            // ctxMenuItemRun
            // 
            this.ctxMenuItemRun.Name = "ctxMenuItemRun";
            this.ctxMenuItemRun.Size = new System.Drawing.Size(120, 22);
            this.ctxMenuItemRun.Text = "Run/Edit";
            // 
            // tsShowConfigSubMenu
            // 
            this.tsShowConfigSubMenu.Name = "tsShowConfigSubMenu";
            this.tsShowConfigSubMenu.Size = new System.Drawing.Size(103, 22);
            this.tsShowConfigSubMenu.Text = "Show";
            // 
            // tsAboutFSSubMenu
            // 
            this.tsAboutFSSubMenu.Name = "tsAboutFSSubMenu";
            this.tsAboutFSSubMenu.Size = new System.Drawing.Size(152, 22);
            this.tsAboutFSSubMenu.Text = "About";
            // 
            // tabcMainTests
            // 
            this.tabcMainTests.AlwaysShowClose = false;
            this.tabcMainTests.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabcMainTests.Enabled = false;
            this.tabcMainTests.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.tabcMainTests.Items.AddRange(new TabControl.TabControlItem[] {
            this.tabcQuickTest,
            this.tabcWsdl});
            this.tabcMainTests.Location = new System.Drawing.Point(0, 4);
            this.tabcMainTests.Name = "tabcMainTests";
            this.tabcMainTests.SelectedItem = this.tabcQuickTest;
            this.tabcMainTests.ShowBorder = false;
            this.tabcMainTests.ShowToolTipOnTitle = false;
            this.tabcMainTests.Size = new System.Drawing.Size(594, 298);
            this.tabcMainTests.TabIndex = 4;
            // 
            // tabcQuickTest
            // 
            this.tabcQuickTest.CanClose = false;
            this.tabcQuickTest.Controls.Add(this.spcTestRun);
            this.tabcQuickTest.ID = null;
            this.tabcQuickTest.IsDrawn = true;
            this.tabcQuickTest.Name = "tabcQuickTest";
            this.tabcQuickTest.Selected = true;
            this.tabcQuickTest.TabIndex = 0;
            this.tabcQuickTest.TabType = TabControl.TabType.QuickTestTab;
            this.tabcQuickTest.Title = "Quick Test";
            this.tabcQuickTest.ToolTipText = "The contents of this tab gets updated whenever the selectionhas changed";
            // 
            // spcTestRun
            // 
            this.spcTestRun.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.spcTestRun.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spcTestRun.Location = new System.Drawing.Point(0, 0);
            this.spcTestRun.Name = "spcTestRun";
            // 
            // spcTestRun.Panel1
            // 
            this.spcTestRun.Panel1.Controls.Add(this.testEditorReq);
            // 
            // spcTestRun.Panel2
            // 
            this.spcTestRun.Panel2.Controls.Add(this.testEditorResponseControl1);
            this.spcTestRun.Size = new System.Drawing.Size(594, 278);
            this.spcTestRun.SplitterDistance = 288;
            this.spcTestRun.TabIndex = 1;
            // 
            // testEditorReq
            // 
            this.testEditorReq.ColorRawSoap = true;
            this.testEditorReq.Dock = System.Windows.Forms.DockStyle.Fill;
            this.testEditorReq.Location = new System.Drawing.Point(0, 0);
            this.testEditorReq.Name = "testEditorReq";
            this.testEditorReq.RawText = "";
            this.testEditorReq.Size = new System.Drawing.Size(286, 276);
            this.testEditorReq.TabIndex = 0;
            // 
            // testEditorResponseControl1
            // 
            this.testEditorResponseControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.testEditorResponseControl1.Location = new System.Drawing.Point(0, 0);
            this.testEditorResponseControl1.Name = "testEditorResponseControl1";
            this.testEditorResponseControl1.RawText = "";
            this.testEditorResponseControl1.Size = new System.Drawing.Size(300, 276);
            this.testEditorResponseControl1.TabIndex = 0;
            // 
            // tabcWsdl
            // 
            this.tabcWsdl.Controls.Add(this.wbWsdl);
            this.tabcWsdl.ID = null;
            this.tabcWsdl.IsDrawn = true;
            this.tabcWsdl.Name = "tabcWsdl";
            this.tabcWsdl.TabIndex = 1;
            this.tabcWsdl.TabType = TabControl.TabType.QuickTestTab;
            this.tabcWsdl.Title = "Wsdl";
            this.tabcWsdl.ToolTipText = "Web Service Description ";
            this.tabcWsdl.Visible = false;
            // 
            // wbWsdl
            // 
            this.wbWsdl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wbWsdl.Location = new System.Drawing.Point(0, 0);
            this.wbWsdl.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbWsdl.Name = "wbWsdl";
            this.wbWsdl.Size = new System.Drawing.Size(200, 100);
            this.wbWsdl.TabIndex = 1;
            // 
            // tabControlItem2
            // 
            this.tabControlItem2.Controls.Add(this.testRunner1);
            this.tabControlItem2.ID = null;
            this.tabControlItem2.IsDrawn = true;
            this.tabControlItem2.Name = "tabControlItem2";
            this.tabControlItem2.TabIndex = 2;
            this.tabControlItem2.TabType = TabControl.TabType.QuickTestTab;
            this.tabControlItem2.Title = "TabControl Page 3";
            this.tabControlItem2.ToolTipText = "";
            // 
            // testRunner1
            // 
            this.testRunner1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.testRunner1.Location = new System.Drawing.Point(0, 0);
            this.testRunner1.Name = "testRunner1";
            this.testRunner1.Size = new System.Drawing.Size(200, 100);
            this.testRunner1.TabIndex = 0;
            // 
            // tabControlItem1
            // 
            this.tabControlItem1.ID = null;
            this.tabControlItem1.IsDrawn = true;
            this.tabControlItem1.Name = "tabControlItem1";
            this.tabControlItem1.TabIndex = 2;
            this.tabControlItem1.TabType = TabControl.TabType.QuickTestTab;
            this.tabControlItem1.Title = "TabControl Page 3";
            this.tabControlItem1.ToolTipText = "";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(783, 470);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip2);
            this.Controls.Add(this.scMain);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SoapBits in F#!";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.scMain.Panel1.ResumeLayout(false);
            this.scMain.Panel2.ResumeLayout(false);
            this.scMain.ResumeLayout(false);
            this.scLeft_OfscMain.Panel1.ResumeLayout(false);
            this.scLeft_OfscMain.Panel2.ResumeLayout(false);
            this.scLeft_OfscMain.ResumeLayout(false);
            this.tabConfigs.ResumeLayout(false);
            this.tabPageReqConfig.ResumeLayout(false);
            this.tabPageMiscConfig.ResumeLayout(false);
            this.scRight_OfscMain.Panel1.ResumeLayout(false);
            this.scRight_OfscMain.Panel2.ResumeLayout(false);
            this.scRight_OfscMain.ResumeLayout(false);
            this.ctxMenuStripLog.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            this.ctxMenuStripWebService.ResumeLayout(false);
            this.ctxMenuStripWebMethod.ResumeLayout(false);
            this.ctxMenuStripTestCase.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabcMainTests)).EndInit();
            this.tabcMainTests.ResumeLayout(false);
            this.tabcQuickTest.ResumeLayout(false);
            this.spcTestRun.Panel1.ResumeLayout(false);
            this.spcTestRun.Panel2.ResumeLayout(false);
            this.spcTestRun.ResumeLayout(false);
            this.tabcWsdl.ResumeLayout(false);
            this.tabControlItem2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.SplitContainer scMain;
        public System.Windows.Forms.SplitContainer scLeft_OfscMain;
        public System.Windows.Forms.SplitContainer scRight_OfscMain;
        public System.Windows.Forms.StatusStrip statusStrip1;
        public System.Windows.Forms.ToolStripProgressBar tsProgressBar1;
        public System.Windows.Forms.TreeView tvWebServices;
        public System.Windows.Forms.RichTextBox rtbLog;
        public System.Windows.Forms.ImageList imgList_All;
        public System.ComponentModel.BackgroundWorker backgroundWorker1;
        public System.Windows.Forms.ToolStrip toolStrip1;
        public System.Windows.Forms.ToolStripButton tsbButtonAdd;
        public System.Windows.Forms.ToolStripButton tsBtnRemove;
        public System.Windows.Forms.ToolStripStatusLabel tsLabelDoneOrProcessing;
        public System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        public System.Windows.Forms.ToolStripButton tsBtnConfig;
        public System.Windows.Forms.MenuStrip menuStrip2;
        public System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem1;
        public System.Windows.Forms.Timer clocktimer1;
        public System.Windows.Forms.ToolStripButton tsBtnColorScheme;
        public System.Windows.Forms.ToolStripMenuItem tsAddMenuItem1;
        public System.Windows.Forms.ToolStripMenuItem tsRemoveMenuItem;
        public System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem tsMenuItemCfg;
        public System.Windows.Forms.ToolStripMenuItem tsMenuItemPlugin;
        public System.Windows.Forms.SaveFileDialog saveFileDialog;
        public System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        public System.Windows.Forms.ToolStripButton helpToolStripButton;
        private System.ComponentModel.IContainer components;
        public TestEditor testEditorReq;
        public System.Windows.Forms.SplitContainer spcTestRun;
        public TestEditorResponseControl testEditorResponseControl1;
        public System.Windows.Forms.ToolStripButton tsbSave;
        public System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        public System.Windows.Forms.ToolStripButton tsbOpen;
        public System.Windows.Forms.OpenFileDialog openFileDialog1;
        public System.Windows.Forms.ToolStripMenuItem tsViewMenuItem;
        public System.Windows.Forms.ToolStripMenuItem logToolStripMenuItem;
        public TabControl.TabControlItem tabcQuickTest;
        public TabControl.TabControlItem tabcWsdl;
        public TabControl.TabControl tabcMainTests;
        public System.Windows.Forms.ContextMenuStrip ctxMenuStripWebService;
        public System.Windows.Forms.ToolStripMenuItem ctxMenuItemRunTests;
        public System.Windows.Forms.ToolStripMenuItem ctxMenuItemViewWsdl;
        public System.Windows.Forms.ToolStripMenuItem ctxMenuItemRemove;
        public System.Windows.Forms.ToolStripMenuItem ctxMenuItemOpenInTab;
        public System.Windows.Forms.ContextMenuStrip ctxMenuStripWebMethod;
        public System.Windows.Forms.WebBrowser wbWsdl;
        public TabControl.TabControlItem tabControlItem1;
        public System.Windows.Forms.ToolStripMenuItem tsMenuItemFSharp;
        public System.Windows.Forms.ToolStripMenuItem ctxMenuItemRunTest;
        public System.Windows.Forms.ContextMenuStrip ctxMenuStripTestCase;
        public System.Windows.Forms.ToolStripMenuItem ctxMenuItemRun;
        public System.Windows.Forms.ToolStripMenuItem showQuickTestToolStripMenuItem;
        public Ascend.Windows.Forms.GradientLine gradientLine1;
        public Ascend.Windows.Forms.GradientCaption gradientClock;
        public Ascend.Windows.Forms.GradientCaption gradientCaptionWebCfg;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ContextMenuStrip ctxMenuStripLog;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hideToolStripMenuItem;
        public System.Windows.Forms.TabControl tabConfigs;
        public System.Windows.Forms.TabPage tabPageMiscConfig;
        public System.Windows.Forms.PropertyGrid pgAppCOnfig;
        public System.Windows.Forms.TabPage tabPageReqConfig;
        public System.Windows.Forms.PropertyGrid pgWebServiceProp;
        private TabControl.TabControlItem tabControlItem2;
        private TestRunner testRunner1;
        public System.Windows.Forms.ToolStripMenuItem newWindowToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem tsShowConfigSubMenu;
        public System.Windows.Forms.ToolStripMenuItem tsAboutFSSubMenu;
    }
}