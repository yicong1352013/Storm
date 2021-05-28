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
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using TabControl;

namespace Storm.UI
{
    public partial class FormMain : Form
    {
       
        public TestEditor ActiveTestEditorReq
        {
            get{
                TabControlItem ti = this.tabcMainTests.SelectedItem;
                SplitContainer sc = ti.Controls[0] as SplitContainer;
                if (sc == null)
                   return null;
                else  
                    return sc.Panel1.Controls[0] as TestEditor;
            }
        }

        public TestEditorResponseControl ActiveTestEditorResp
        {
            get{
                TabControlItem ti = this.tabcMainTests.SelectedItem;
                SplitContainer sc = ti.Controls[0] as SplitContainer;
                if (sc == null)
                   return null;
                else
                   return sc.Panel2.Controls[0] as TestEditorResponseControl;
            }
        }


        public FormMain()
        {
            InitializeComponent();
            //toolStrip1.Renderer = new ToolStripProfessionalRenderer(new CustomProfessionalColors());
            this.tsAddMenuItem1.Image = this.tsbButtonAdd.Image;
            this.tabcMainTests.SelectedItem = this.tabcQuickTest;
            this.tsRemoveMenuItem.Image = this.tsBtnRemove.Image;
            this.showQuickTestToolStripMenuItem.Enabled = false;
            this.spcTestRun.SplitterDistance =this.spcTestRun.Width / 2;
            this.newWindowToolStripMenuItem.Image = this.Icon.ToBitmap();

            //this.tabcMainTests.TabControlItemSelectionChanged +=new TabControlItemChangedHandler(tabcMainTests_TabControlItemSelectionChanged);
            //this.tabcMainTests.TabControlItemClosing +=new TabControlItemClosingHandler(tabcMainTests_TabControlItemClosing);
            
        }

     
        public TabControlItem FindTabControlByID(string id)
        {
            
            foreach (TabControlItem ti in this.tabcMainTests.Items)
            {
                if (ti.ID == id)
                    return ti;
                else
                    continue;

            }

            return null;
        }
        private void LoadImages()
        {
         //  this.btnGo.Image = global::Storm.UI.StormResource.arrow_right;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            TestEditor te = this.ActiveTestEditorReq;
        }

        private void gradientClock_Click(object sender, EventArgs e)
        {

        }

        private void tvWebServices_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        
        private void logToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            string msg = this.logToolStripMenuItem.Text;
            if (msg.Contains("Hide"))
            {
                this.logToolStripMenuItem.Text = msg.Replace("Hide", "Show");
                this.scRight_OfscMain.Panel2Collapsed = true; //Hide the logging window at the bottom-right
            }
            else
            {
                this.logToolStripMenuItem.Text = msg.Replace("Show", "Hide");
                this.scRight_OfscMain.Panel2Collapsed = false;
            }
        }

        private void openInNewTabToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void ctxMenuStripWebMethod_Opening(object sender, CancelEventArgs e)
        {

        }

      
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void tabcMainTests_TabControlItemClosing(TabControlItemClosingEventArgs e)
        {
            if (e.Item == tabcWsdl)
            {
                e.Item.Visible = false;
                this.tabcMainTests.SelectedItem = this.tabcQuickTest;
                e.Cancel = true;
            }
        }

        private void showQuickTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (showQuickTestToolStripMenuItem.Text.StartsWith("Show"))
            {
                tabcQuickTest.Visible = true;
                showQuickTestToolStripMenuItem.Text = showQuickTestToolStripMenuItem.Text.Replace("Show", "Hide");
            }
            else
            {
                
                tabcQuickTest.Visible = false;
                showQuickTestToolStripMenuItem.Text = showQuickTestToolStripMenuItem.Text.Replace("Hide", "Show");
            }
            tabcMainTests.Invalidate(true);
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.rtbLog.Clear();
        }

        private void hideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            logToolStripMenuItem_Click(null, null);
        }

        private void scRight_OfscMain_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void gradientCaptionWebCfg_Click(object sender, EventArgs e)
        {

        }

       
       
       
       

       

       
    }
}