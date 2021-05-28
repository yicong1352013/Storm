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
    public partial class TestEditorResponseControl : UserControl
    {
        public TestEditorResponseControl()
        {
            InitializeComponent();
            this.testEditor1.btnGo.Visible = false;
        }

        public ViewType CurrentView
        {
            get { return this.testEditor1.CurrentView; }
        }
        public string RawRespHeader
        {
            set { this.rtbRawRespHead.Text = value; }
        }
        public string RawText
        {
            get { return this.testEditor1.RawText; }
            set { this.testEditor1.RawText = value; }
        }
        public string XmlViewText
        {
            set { this.testEditor1.XmlViewText = value; }
        }
        public TreeNode QuickEditNode
        {
            set
            {
                this.testEditor1.QuickEditNode = value;
            }
        }

        public void ShowRawEdit()
        {
            this.testEditor1.ShowRawEdit();
        }
        public void ShowXmlView()
        {
            this.testEditor1.ShowXmlView();
        }
        public void ShowQuickEdit()
        {
            this.testEditor1.ShowQuickEdit();
        }
    }
}
