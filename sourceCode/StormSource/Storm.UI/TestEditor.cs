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
using System.Text.RegularExpressions;
using System.Xml;
using System.Configuration;

namespace Storm.UI
{
    //public class RTBHelper<T> where T : RichTextBox
    //{
    //    public delegate string GetTextString(T control);
    //    public static string GetText(T control)
    //    {
    //        if (control.InvokeRequired)
    //        {
    //            GetTextString g = new RTBHelper<T>.GetTextString(GetText);
    //            return (string)control.Invoke(g, control);
    //        }
    //        else
    //            return control.Text;
    //    }
    //}
    public partial class TestEditor : UserControl
    {
        static string _defText = "<div style=\"color:gray; font-style:italic; font-size:xxx-small\">No data to display.</div>";
        private bool _txtWasUpdated = false;
        private List<ColoringState> _coloredText = new List<ColoringState>();

        private ViewType _view = ViewType.Tree;
   //     private string _rawTextCopy = "";
        
        private bool colorRawSoap = true;
       // string afterFragment = "";

        

        public TestEditor()
        {
            //this.rtbRaw.VScroll +=new EventHandler(rtbRaw_VScroll);
            InitializeComponent();
            this.wbXmlView.DocumentText = _defText;
            
           // InitSyntaxHighlighter();
            //this.tvQuickEditInput.ImageIndex = -1;
           // this.tvQuickEditInput.SelectedImageIndex = 0;
        }

        public bool ColorRawSoap
        {
            get { return this.colorRawSoap; }
            set { this.colorRawSoap = value; }
        }
        public ViewType CurrentView
        {
            get { return this._view; }
        }
        public string RawText
        {
            get { return this.rtbRaw.Text; }
            set
            {
                //this._txtWasUpdated = value != this.rtbRaw.Text;
                this._txtWasUpdated = true;
                this.rtbRaw.Text = value;
                if (this.CurrentView == ViewType.Raw)
                {
                    this.ShowRawEdit();
                }
                   
                if (value.Contains("<soap:Fault>"))
                    this.ShowXmlView();
            }
        }
        public string XmlViewText
        {
            set {
                if (String.IsNullOrEmpty(value))
                    this.wbXmlView.DocumentText = _defText;
                else
                    this.wbXmlView.DocumentText = value; }
        }
        public TreeNode QuickEditNode
        {
            set
            {
                if (value != null)
                {
                    this.tvQuickEditInput.Nodes.Clear();
                    this.tvQuickEditInput.Nodes.Add(value);
                    this.tvQuickEditInput.ExpandAll();
                }
            }
        }

        //private void InitSyntaxHighlighter()
        //{
        //    this.rtbSyntax.Settings.AddFont("commentFont", "Microsoft Sans Serif", "Gray", this.rtbSyntax.Font.Size.ToString(), "Italic");
        //    this.rtbSyntax.Settings.AddFont("tagFont", "Microsoft Sans Serif", "FireBrick", this.rtbSyntax.Font.Size.ToString(), "Regular");
        //    this.rtbSyntax.Settings.AddFont("attribFont", "Microsoft Sans Serif", "Green", this.rtbSyntax.Font.Size.ToString(), "Regular");
        //    this.rtbSyntax.Settings.AddFont("elemFont", "Microsoft Sans Serif", "Black", this.rtbSyntax.Font.Size.ToString(), "Bold");
        //    this.rtbSyntax.Settings.AddFont("defFont", "Microsoft Sans Serif", "NavyBlue", this.rtbSyntax.Font.Size.ToString(), "Regular");

        //    //this.rtbSyntax.Settings.AddKeyWord("<");
        //    //this.rtbSyntax.Settings.AddKeyWord(">");
        //    //this.rtbSyntax.Settings.AddKeyWord("xmlns:");
        //    //this.rtbSyntax.Settings.AddKeyWord("xs:");

        //    this.rtbSyntax.Settings.LoadComment("<!--", "commentFont");
        //    this.rtbSyntax.Settings.SetKeywordFont("tagFont");


        //    //match attributes
        //    this.rtbSyntax.Settings.AddRegex("(\\s\\S+=\"\\S+\")", "attribFont", "match attributes");
        //    this.rtbSyntax.Settings.AddRegex("(<|>|/>)", "tagFont", "match tags");

        //}

       
        public void Clear()
        {
            this.tvQuickEditInput.Nodes.Clear();
            this.rtbRaw.Text = "";
            this.wbXmlView.DocumentText = _defText;
        }
        //spcEdit
        //   - quickedit P1
        //   - spcRaw P2
        //        - Rawedit rtb P1
        //        - webBrowser p2
        public void ShowQuickEdit()
        {
            this.SuspendLayout();
            this.spcEdit.Panel1Collapsed = false;
            this.spcEdit.Panel2Collapsed = true;
            this.ResumeLayout();
            this._view = ViewType.Tree;

            //this.Invalidate();      
        }

        public void ShowRawEdit(bool colorSoap)
        {
            this.rtbRaw.EnablePaint = true;
            this.spcRaw.Panel1Collapsed = false;
            this.spcRaw.Panel2Collapsed = true;
            this.spcEdit.Panel2Collapsed = false;
            this.spcEdit.Panel1Collapsed = true;
            //this.ActiveControl = this.rtbRaw;

            if (colorSoap)
            {
                //this.bgSyntaxHighlighter.WorkerReportsProgress = true;
                //if (this._txtWasUpdated)
                //    this.RunSyntaxHighlighter(this.rtbRaw.Text);
                this.RunSyntaxHighlighter();
            }
            this._view = ViewType.Raw;
            this._txtWasUpdated = false;
        }
        public void ShowRawEdit()
        {
            this.ShowRawEdit(this.colorRawSoap);
        }
        public void ShowXmlView()
        {
            this.SuspendLayout(); 
            this.spcRaw.Panel1Collapsed = true;
            this.spcRaw.Panel2Collapsed = false;
            
            this.spcEdit.Panel2Collapsed = false;
            this.spcEdit.Panel1Collapsed = true;
            this.ResumeLayout();
            this._view = ViewType.Xml;
    
        }

        
        
        private void RunSyntaxHighlighter()
        {
            this.rtbRaw.ColorAllText();
            //if (!bgSyntaxHighlighter.IsBusy)
            //    bgSyntaxHighlighter.RunWorkerAsync();
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void spcEdit_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnQuickEdit_Click(object sender, EventArgs e)
        {
            this.ShowQuickEdit();
        }

        private void btnEditRaw_Click(object sender, EventArgs e)
        {
            this.ShowRawEdit();
        }

        private void btnXmlView_Click(object sender, EventArgs e)
        {
            this.ShowXmlView();
        }

        private static object lockObj = 1;
        

        //
        // Very simple syntax highlighter
        //
        //private void ColorText(string raw)
        //{
        //    string[] lines = newlineRegex.Split(raw);

        //    foreach (string line in lines)
        //    {
        //        string[] tokens = wordRegex.Split(line);
        //        foreach (string token in tokens)
        //        {
        //            string temp = token.Trim();
        //            if (temp.StartsWith("<") && temp.EndsWith(">"))
        //            {
        //                int start = token.IndexOf("<");
        //                int end = token.IndexOf(">");

        //                string begin = token.Substring(0, start);
        //                string between = token.Substring(start + 1, end - start - 1);

        //                //Regex attrReg = new Regex("(\\s\\S+=\"\\S+\")");
        //                string[] attrTokens = attrRegex.Split(between);

        //                ReportColoringProgress(new ColoringState(origFont, begin, defaultColor,-1,-1));
        //                ReportColoringProgress(new ColoringState(origFont, "<", tagColor, -1, -1));

        //                foreach (string s in attrTokens)
        //                {
        //                    Color curColor;
        //                    if (s.Contains("=\""))
        //                        curColor = attrColor;
        //                    else
        //                        curColor = defaultColor;
        //                    ReportColoringProgress(new ColoringState(origFont, s, curColor, -1, -1));
        //                }
        //                ReportColoringProgress(new ColoringState(origFont, ">", tagColor, -1, -1));

        //            }
        //            else
        //            {
        //                Font f = new Font("Microsoft Sans Serif", origFont.Size, FontStyle.Bold);
        //                ReportColoringProgress(new ColoringState(f, token, elemColor, -1, -1));
        //            }
        //        }
        //        if (!emptyRegex.IsMatch(line))
        //            //if (!Regex.IsMatch(line, @"^\s*$", RegexOptions.Compiled))
        //            ReportColoringProgress(new ColoringState(origFont, Environment.NewLine, defaultColor, -1, -1));
        //    }

        //    // return lcs;
        //}

        private void bgSyntaxHighlighter_DoWork(object sender, DoWorkEventArgs e)
        {

            this.rtbRaw.ColorAllText();
            
        }

        private void ColorText(string allText)
        {
//            ColoringState cs = new ColoringState(

            throw new NotImplementedException();
        }

   
       
    }

    public class ColoringState : ICloneable
    {
        private  Font _currentFont;
        public Font CurrentFont
        {
            get { return this._currentFont; }
        }
        private string _currentText;
        public string CurrentText
        {
            get { return this._currentText; }
            set {this._currentText = value;}
        }

        private Color _currentColor;
        public Color CurrentColor
        {
            get { return this._currentColor; }
        }

        private int _selectionStart;
        public int SelectionStart
        {
            get{return this._selectionStart;}
        }

        private int _selectionLength;
        public int SelectionLength
        {
            get { return this._selectionLength; }
        }
       


        public ColoringState(Font f, string t, Color c, int start, int length)
        {
            this._currentColor = c;
            this._currentFont = f;
            this._currentText = t;
            this._selectionStart = start;
            this._selectionLength = length;
        }

        public object Clone()
        {
            Font f = (Font)this.CurrentFont.Clone();
            Color c = this.CurrentColor;
            StringBuilder sb = new StringBuilder(this.CurrentText);
            ColoringState cs = new ColoringState(f, sb.ToString(), c,0,0);
            return cs;
            
        }

    }
    public enum ViewType
    {
        Tree,
        Raw,
        Xml
    }
}
