namespace Storm.UI
{
    partial class FormQuickEdit
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        public System.ComponentModel.IContainer components = null;

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
            this.gradientPanel1 = new Ascend.Windows.Forms.GradientPanel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lblLoadFile = new System.Windows.Forms.Label();
            this.btnLoadFile = new System.Windows.Forms.Button();
            this.lblPoly = new System.Windows.Forms.Label();
            this.cbPolymorphTypes = new System.Windows.Forms.ComboBox();
            this.pgInput = new System.Windows.Forms.PropertyGrid();
            this.label1 = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.gradientPanel1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gradientPanel1
            // 
            this.gradientPanel1.Border = new Ascend.Border(1);
            this.gradientPanel1.Controls.Add(this.splitContainer1);
            this.gradientPanel1.Controls.Add(this.label1);
            this.gradientPanel1.CornerRadius = new Ascend.CornerRadius(6);
            this.gradientPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gradientPanel1.GradientLowColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.gradientPanel1.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.gradientPanel1.Location = new System.Drawing.Point(0, 0);
            this.gradientPanel1.Name = "gradientPanel1";
            this.gradientPanel1.Size = new System.Drawing.Size(367, 213);
            this.gradientPanel1.TabIndex = 0;
            this.gradientPanel1.Click += new System.EventHandler(this.gradientPanel1_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(12, 27);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lblLoadFile);
            this.splitContainer1.Panel1.Controls.Add(this.btnLoadFile);
            this.splitContainer1.Panel1.Controls.Add(this.lblPoly);
            this.splitContainer1.Panel1.Controls.Add(this.cbPolymorphTypes);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.pgInput);
            this.splitContainer1.Size = new System.Drawing.Size(343, 152);
            this.splitContainer1.SplitterDistance = 25;
            this.splitContainer1.TabIndex = 4;
            // 
            // lblLoadFile
            // 
            this.lblLoadFile.AutoSize = true;
            this.lblLoadFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLoadFile.Location = new System.Drawing.Point(2, 5);
            this.lblLoadFile.Name = "lblLoadFile";
            this.lblLoadFile.Size = new System.Drawing.Size(35, 13);
            this.lblLoadFile.TabIndex = 5;
            this.lblLoadFile.Text = "label2";
            // 
            // btnLoadFile
            // 
            this.btnLoadFile.BackColor = System.Drawing.SystemColors.Window;
            this.btnLoadFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoadFile.Location = new System.Drawing.Point(39, 0);
            this.btnLoadFile.Name = "btnLoadFile";
            this.btnLoadFile.Size = new System.Drawing.Size(78, 23);
            this.btnLoadFile.TabIndex = 4;
            this.btnLoadFile.Text = "Load a file...";
            this.btnLoadFile.UseVisualStyleBackColor = false;
            // 
            // lblPoly
            // 
            this.lblPoly.AutoSize = true;
            this.lblPoly.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblPoly.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPoly.Location = new System.Drawing.Point(115, 0);
            this.lblPoly.Name = "lblPoly";
            this.lblPoly.Size = new System.Drawing.Size(92, 13);
            this.lblPoly.TabIndex = 3;
            this.lblPoly.Text = "Select a new type";
            // 
            // cbPolymorphTypes
            // 
            this.cbPolymorphTypes.Dock = System.Windows.Forms.DockStyle.Right;
            this.cbPolymorphTypes.FormattingEnabled = true;
            this.cbPolymorphTypes.Location = new System.Drawing.Point(207, 0);
            this.cbPolymorphTypes.Name = "cbPolymorphTypes";
            this.cbPolymorphTypes.Size = new System.Drawing.Size(136, 21);
            this.cbPolymorphTypes.TabIndex = 2;
            // 
            // pgInput
            // 
            this.pgInput.BackColor = System.Drawing.SystemColors.Info;
            this.pgInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgInput.HelpVisible = false;
            this.pgInput.Location = new System.Drawing.Point(0, 0);
            this.pgInput.Name = "pgInput";
            this.pgInput.Size = new System.Drawing.Size(343, 123);
            this.pgInput.TabIndex = 0;
            this.pgInput.ToolbarVisible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Edit the data...";
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOk.BackColor = System.Drawing.SystemColors.Window;
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.FlatAppearance.BorderSize = 0;
            this.btnOk.Image = global::Storm.UI.StormResource.accept;
            this.btnOk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOk.Location = new System.Drawing.Point(12, 183);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(48, 25);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "OK";
            this.btnOk.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnOk.UseVisualStyleBackColor = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.SystemColors.Window;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.Image = global::Storm.UI.StormResource.cancel;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(290, 183);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(65, 25);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancel.UseVisualStyleBackColor = false;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "All files(*.*)|*.*";
            // 
            // FormQuickEdit
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(367, 213);
            this.ControlBox = false;
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.gradientPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "FormQuickEdit";
            this.Opacity = 0.9;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.gradientPanel1.ResumeLayout(false);
            this.gradientPanel1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.PropertyGrid pgInput;
        public System.Windows.Forms.Button btnOk;
        public System.Windows.Forms.Button btnCancel;
        public Ascend.Windows.Forms.GradientPanel gradientPanel1;
        public System.Windows.Forms.SplitContainer splitContainer1;
        public System.Windows.Forms.ComboBox cbPolymorphTypes;
        public System.Windows.Forms.Label lblPoly;
        public System.Windows.Forms.Button btnLoadFile;
        public System.Windows.Forms.OpenFileDialog openFileDialog1;
        public System.Windows.Forms.Label lblLoadFile;
    }
}