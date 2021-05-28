namespace Storm.UI
{
    partial class TestEditorResponseControl
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
            this.rtbRawRespHead = new System.Windows.Forms.RichTextBox();
            this.testEditor1 = new Storm.UI.TestEditor();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // rtbRawRespHead
            // 
            this.rtbRawRespHead.BackColor = System.Drawing.SystemColors.Info;
            this.rtbRawRespHead.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbRawRespHead.DetectUrls = false;
            this.rtbRawRespHead.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbRawRespHead.Location = new System.Drawing.Point(0, 0);
            this.rtbRawRespHead.Name = "rtbRawRespHead";
            this.rtbRawRespHead.ReadOnly = true;
            this.rtbRawRespHead.Size = new System.Drawing.Size(395, 58);
            this.rtbRawRespHead.TabIndex = 7;
            this.rtbRawRespHead.Text = "";
            // 
            // testEditor1
            // 
            this.testEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.testEditor1.Location = new System.Drawing.Point(0, 0);
            this.testEditor1.Name = "testEditor1";
            this.testEditor1.Size = new System.Drawing.Size(395, 177);
            this.testEditor1.TabIndex = 8;
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.rtbRawRespHead);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.testEditor1);
            this.splitContainer1.Size = new System.Drawing.Size(397, 243);
            this.splitContainer1.SplitterDistance = 60;
            this.splitContainer1.TabIndex = 9;
            // 
            // TestEditorResponseControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "TestEditorResponseControl";
            this.Size = new System.Drawing.Size(397, 243);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.RichTextBox rtbRawRespHead;
        private System.Windows.Forms.SplitContainer splitContainer1;
        public TestEditor testEditor1;
    }
}
