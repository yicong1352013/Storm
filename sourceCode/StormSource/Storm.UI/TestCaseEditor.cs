//Copyright (c) 2008, Erik Araojo
//All rights reserved.
//
//Redistribution and use in source and binary forms, with or without modification, are permitted provided that the 
//following conditions are met:
//
//* Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.
//* Redistributions in binary form must reproduce the above copyright notice, this list of conditions and 
//the following disclaimer in the documentation and/or other materials provided with the distribution.
//* Neither the name of Erik Araojo nor the names of its contributors may be used to endorse or 
//promote products derived from this software without specific prior written permission.
//
//THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR 
//IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND 
//FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE 
//LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT 
//NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS 
//INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT 
//(INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF 
//THE POSSIBILITY OF SUCH DAMAGE. 

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Storm.UI
{
    public partial class TestCaseEditor : UserControl
    {
        private string targetFile;
        private string browserText;
        private bool colorSoap;
        private ViewType viewType;

        public ViewType ViewType
        {
            get { return viewType; }
        }
        
        public Color BackGroundColor
        {
            get { return gradientPanel1.GradientLowColor; }
            set { gradientPanel1.GradientLowColor = value; }
        }


        public bool ColorSoap
        {
            get { return colorSoap; }
            set { colorSoap = value; }
        }

        public string BrowserText
        {
            get { return browserText; }
            set { browserText = value; }
        }
       
        public Color RawTextBackColor
        {
            get { return this.rtbRaw.BackColor; }
            set { this.rtbRaw.BackColor = value; }
        }

        public string RawText
        {
            get { return this.rtbRaw.Text; }
            set { this.rtbRaw.Text = value; }
        }

        public string TargetFile
        {
            get { return targetFile; }
            set { targetFile = value; }
        }

        public string Label
        {
            set { this.label1.Text = value; }
        }
        public string SaveToolTip
        {
            set { this.toolTip1.SetToolTip(this.btnSave, value);  }
        }


        public TestCaseEditor()
        {
            InitializeComponent();
            //this.webBrowser1.SendToBack();
        }

        public void ShowBrowser(string docText)
        {
            this.BrowserText = docText;
            this.ShowBrowser();
        }
        public void ShowBrowser()
        {
            //this.webBrowser1.Location = this.rtbRaw.Location;
            //this.webBrowser1.Size = this.rtbRaw.Size;
            this.webBrowser1.DocumentText = this.BrowserText;
            //this.Anchor = this.rtbRaw.Anchor;
            this.webBrowser1.Dock = DockStyle.Fill;
            this.webBrowser1.BringToFront();
            this.rtbRaw.SendToBack();
            this.viewType = ViewType.Xml;
        }

        private void ShowRawEdit(bool colorSoap)
        {
            this.rtbRaw.EnablePaint = true;

            if (colorSoap)
            {
                this.rtbRaw.ColorAllText();
                //this.bgSyntaxHighlighter.WorkerReportsProgress = true;
                //if (this._txtWasUpdated)
                //    this.RunSyntaxHighlighter(this.rtbRaw.Text);
            }
        }

        private void RunSyntaxHighlighter(string fragment)
        {
            //this.rtbRaw.Text = "";
            //if (!bgSyntaxHighlighter.IsBusy)
            //    bgSyntaxHighlighter.RunWorkerAsync(fragment);
        }

        public void RefreshView()
        {
            switch (this.viewType)
            {
                case ViewType.Raw : this.ShowEdit(); break;
                case ViewType.Xml: this.ShowBrowser(); break;
                default: break;
            }
        }
        public void ShowEdit()
        {
            this.webBrowser1.SendToBack();
            this.rtbRaw.BringToFront();
            this.rtbRaw.Dock = DockStyle.Fill;
            this.rtbRaw.Text = this.RawText;
            this.ShowRawEdit(this.ColorSoap);
            this.viewType = ViewType.Raw;
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bgSyntaxHighlighter_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        //private void btnXmlView_Click(object sender, EventArgs e)
        //{
        //    this.ShowBrowser();
        //}

        private void btnEditRaw_Click(object sender, EventArgs e)
        {
            this.ShowEdit();
        }
    }
}
