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
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.XPath;
using System.Windows.Forms;
using Storm.Types.Extensions;


namespace Storm.UI
{
    #region Old code
    //public partial class SyntaxRichTextBox : System.Windows.Forms.RichTextBox
    //{

    //    private SyntaxSettings _settings = new SyntaxSettings();
    //    private static bool _paint = true;

    //    private Color _defaultColor = Color.Black;
    //    /// <summary>
    //    /// The settings.
    //    /// </summary>
    //    public SyntaxSettings Settings
    //    {
    //        get { return _settings; }
    //    }

    //    ///// <summary>
    //    ///// Default OnPaint
    //    ///// </summary>
    //    ///// <param name="pe"></param>
    //    //protected override void OnPaint(PaintEventArgs pe)
    //    //{
    //    //    // TODO: Add custom paint code here

    //    //    // Calling the base class OnPaint
    //    //    base.OnPaint(pe);
    //    //}

    //    public int CurrentLineNumber
    //    {
    //        get
    //        {

    //            return this.GetLineFromCharIndex(this.GetFirstCharIndexOfCurrentLine());
    //        }
    //    }

    //    public void GoToLineNumber(int lineNum)
    //    {
    //        int index = this.GetFirstCharIndexFromLine(lineNum);
    //        if (index <= this.Text.Length - 1 && index > 0)
    //            this.SelectionStart = index;
    //    }

    //    private int PreviousChangePoint
    //    {
    //        get
    //        {
    //            int firstCharIndex = this.GetFirstCharIndexOfCurrentLine();
    //            int lineNum = -1;
    //            string currentLine = GetTextAndLineNumber(this.SelectionStart, out lineNum);
    //            string sub = currentLine.Substring(0, this.SelectionStart - firstCharIndex);
    //            int spaceIndexInTheLine = GetSpaceOrReturnIndex(sub);
    //            if (spaceIndexInTheLine == 0)
    //                return firstCharIndex;
    //            else
    //                return firstCharIndex + spaceIndexInTheLine + 1;
    //        }
    //    }

    //    public SyntaxRichTextBox()
    //    {
    //        InitializeComponent();
    //    }

    //    /// <summary>
    //    /// WndProc
    //    /// </summary>
    //    /// <param name="m"></param>
    //    protected override void WndProc(ref System.Windows.Forms.Message m)
    //    {
    //        if (m.Msg == 0x00f)
    //        {
    //            if (_paint)
    //                base.WndProc(ref m);
    //            else
    //                m.Result = IntPtr.Zero;
    //        }
    //        else
    //            base.WndProc(ref m);
    //    }

    //    protected override void OnKeyPress(KeyPressEventArgs e)
    //    {
    //        base.OnKeyPress(e);


    //        if (((Control.ModifierKeys) == Keys.Shift) && (e.KeyChar == '>' || e.KeyChar == '<'))
    //        {
    //            TextHasChanged();
    //        }
    //    }

    //    protected override void OnKeyUp(KeyEventArgs e)
    //    {
    //        //Add syntax higlighting when user press Space or Enter
    //        base.OnKeyUp(e);

           
    //        if (e.KeyCode == Keys.Return)
    //        {
    //            TextHasChanged();
    //        }

    //    }

    //    private string GetVisibleText(out int start, out int displayLength)
    //    {

            
    //        int x = this.Location.X + this.Width;
    //        int y = this.Location.Y + this.Height;
    //        Point bottomRight = new Point(x, y);
    //        Point upperLeft = this.Location;
    //        int lowerRight = this.GetCharIndexFromPosition(bottomRight);
    //        int topLeft = this.GetCharIndexFromPosition(upperLeft);
    //        int length = Math.Min(this.Text.Length, (lowerRight + 1 - topLeft));
    //        string displayedText = this.Text.Substring(topLeft, length);

    //        start = topLeft;
    //        displayLength = length;

    //        return displayedText;
    //    }

    //    private int GetSpaceOrReturnIndex(string fragment)
    //    {
    //        fragment = fragment.TrimEnd();
    //        Array chars = fragment.ToCharArray();
    //        Array.Reverse(chars);
    //        int counter = Math.Max(fragment.Length - 1, 0);
    //        foreach (char c in chars)
    //        {
    //            if (c == '<' || c == '\n' || c == '>' || c == '"')
    //                break;
    //            else if (counter == 0)
    //                break;
    //            else
    //                counter -= 1;
    //        }
    //        return counter;

    //    }

    //    /// <summary>
    //    /// OnTextChanged
    //    /// </summary>
    //    private void TextHasChanged()
    //    {
    //        int prevChangeIndex = -1;
    //        prevChangeIndex = this.PreviousChangePoint;

    //        _paint = false;
    //        string myLine = this.Text.Substring(prevChangeIndex, SelectionStart - Math.Max(prevChangeIndex - 1, prevChangeIndex));

    //        ProcessWord(prevChangeIndex, myLine);
    //        _paint = true;
    //    }

    //    /// <summary>
    //    /// Colors all text. Useful when loading a file.
    //    /// </summary>
    //    public void ColorAllLines()
    //    {
    //        this.SelectionStart = 0;

    //        //Regex.Split(this.Text, @"(\s+)");
    //        string[] chunks = Regex.Split(this.Text, @"\n");
    //        _paint = false;
    //        foreach (string chunk in chunks)
    //        {
    //            int len = chunk.Length;
    //            ProcessWord(this.SelectionStart, chunk);
    //            this.SelectionStart += len;
    //        }
    //        _paint = true;



    //    }

    //    private bool IsKeyWord(string text)
    //    {
    //        foreach (string s in this.Settings.Keywords)
    //        {
    //            if (s.ToLower() == text.ToLower()) //not case-sensitive
    //                return true;
    //            continue;
    //        }
    //        return false;
    //    }
    //    private void ProcessWord(int selectionStart, string text)
    //    {
    //        text = text.TrimEnd();
    //        if (text != "" && text != "\n")
    //        {
    //            int originalpos = this.SelectionStart;

    //            string fragment = "";
    //            int lineIndexStart = selectionStart;

    //            if (IsComment(selectionStart, out fragment, out lineIndexStart))
    //            {
    //                this.SelectionStart = lineIndexStart;
    //                this.SelectionLength = fragment.Length;
    //                this.SelectionColor = this.Settings.CommentColor;

    //                //Advance the selection
    //                this.SelectionStart = selectionStart + fragment.Length;
    //            }

    //            else if (IsKeyWord(text))
    //            {
    //                this.SelectionStart = selectionStart;
    //                this.SelectionLength = text.Length;
    //                this.SelectionFont = this.Settings.KeyWordFont.DefinedFont;
    //                this.SelectionColor = this.Settings.KeyWordFont.DefinedColor;
    //            }
    //            else
    //            {
    //                this.SelectionLength = text.Length;
    //                this.ApplyRegex(text, selectionStart);

    //            }
    //            //Restore old position
    //            this.SelectionStart = originalpos;
    //            this.SelectionLength = 0;
    //            this.SelectionColor = _defaultColor;
    //        }

    //    }

    //    private string GetTextAndLineNumber(int selectionStart, out int lineNum)
    //    {
    //        int end = this.Text.IndexOf("\n", selectionStart);
    //        lineNum = -1;
    //        int linecounter = 0;
    //        int totalLength = 0;
    //        foreach (string s in this.Lines)
    //        {

    //            if (selectionStart <= totalLength)
    //            {
    //                lineNum = linecounter;
    //                return this.Lines[linecounter];
    //            }

    //            linecounter += 1;
    //            totalLength += s.Length;
    //        }
    //        //lineNum = this.GetLineFromCharIndex(selectionStart);
    //        return "";
    //    }


    //    private bool IsComment(int selectionStart, out string fragment, out int lineIndexStart)
    //    {
    //        //everyting after the comment should be processed.
    //        int lineNum = -1;
    //        string line = this.GetTextAndLineNumber(selectionStart, out lineNum);
    //        string lineTrimmed = line.TrimStart();

    //        lineIndexStart = this.GetFirstCharIndexFromLine(lineNum);
    //        fragment = line;
    //        //fragment = line.Substring(selectionStart - lineIndexStart);

    //        if (lineTrimmed.StartsWith(this.Settings.Comment))
    //            return true;
    //        else
    //            return false;
    //    }

    //    private void ApplyRegex(string text, int selectionStart)
    //    {
    //        int curPos = selectionStart;
    //        Match regMatch;
    //        SortedDictionary<int, int> matchedIndices = new SortedDictionary<int, int>();
    //        foreach (RegexSettings r in this.Settings.RegexList)
    //        {
    //            bool wasMatched = false;
    //            int start = -1;
    //            int length = -1;
    //            for (regMatch = r.Expression.Match(text); regMatch.Success; regMatch = regMatch.NextMatch())
    //            {
    //                wasMatched = true;
    //                start = curPos + regMatch.Index;
    //                length = regMatch.Length;
    //                this.SelectionStart = start;
    //                this.SelectionLength = length;
    //                SyntaxFont sf = this.Settings.FindFontById(r.FontID);
    //                this.SelectionFont = sf.DefinedFont;
    //                this.SelectionColor = sf.DefinedColor;

    //                matchedIndices.Add(start, length);
    //                //curPos = start + length;

    //            }

    //        }

    //    }



    //    public class SyntaxFont
    //    {
    //        string _id = "";
    //        Color _color;
    //        Font _font;

    //        public Font DefinedFont
    //        {
    //            get { return this._font; }
    //        }
    //        public Color DefinedColor
    //        {
    //            get { return this._color; }
    //        }
    //        public string ID
    //        {
    //            get { return this._id; }
    //        }

    //        public SyntaxFont(string id, string fontname, string color, string size, string style)
    //        {
    //            this._color = Color.FromName(color);
    //            this._id = id;

    //            switch (style.ToLower())
    //            {
    //                case "bold": this._font = new Font(fontname, float.Parse(size), FontStyle.Bold); break;
    //                case "italic": this._font = new Font(fontname, float.Parse(size), FontStyle.Italic); break;
    //                case "strikeout": this._font = new Font(fontname, float.Parse(size), FontStyle.Strikeout); break;
    //                case "underline": this._font = new Font(fontname, float.Parse(size), FontStyle.Underline); break;
    //                default: this._font = new Font(fontname, float.Parse(size), FontStyle.Regular); break;

    //            }

    //        }
    //    }
    //    public class RegexSettings
    //    {
    //        private Regex _expr;
    //        private string _fontid;
    //        private string _desc;
    //        /// <summary>
    //        /// Create RegexSettings instance
    //        /// </summary>
    //        /// <param name="expr">regex expression</param>
    //        /// <param name="c">Color of matched string</param>
    //        public RegexSettings(string expr, string fontid, string desc)
    //        {
    //            this._expr = new Regex(expr, RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnoreCase);
    //            this._fontid = fontid;
    //            this._desc = desc;
    //        }
    //        public string FontID
    //        {
    //            get
    //            {
    //                return this._fontid;
    //            }
    //        }

    //        public string Description
    //        {
    //            get
    //            {
    //                return this._desc;
    //            }
    //        }
    //        public Regex Expression
    //        {
    //            get { return this._expr; }
    //        }

    //    }

    //    /// <summary>
    //    /// Settings for the keywords and colors.
    //    /// </summary>
    //    public class SyntaxSettings
    //    {
    //        List<string> _keywordList = new List<string>();

    //        private List<RegexSettings> _regexList = new List<RegexSettings>();

    //        string _strComment = "";
    //        Color _colorComment = Color.Green;
    //        //static SyntaxFont defFont = new Font("Courier New", 9, FontStyle.Regular);
    //        SyntaxFont keywordFont;// = new Font(defFont, FontStyle.Bold);
    //        SyntaxFont commentFont;// = new Font(defFont, FontStyle.Regular);

    //        Dictionary<string, SyntaxFont> _fontDic = new Dictionary<string, SyntaxFont>();

    //        #region Properties

    //        public SyntaxFont CommentFont
    //        {
    //            get { return commentFont; }
    //        }

    //        //public SyntaxFont EditorDefaultFont
    //        //{
    //        //    get { return defFont; }
    //        //}

    //        public SyntaxFont KeyWordFont
    //        {
    //            get { return this.keywordFont; }
    //        }

    //        public List<RegexSettings> RegexList
    //        {
    //            get { return this._regexList; }
    //        }



    //        /// <summary>
    //        /// A list containing all keywords.
    //        /// </summary>
    //        public List<string> Keywords
    //        {
    //            get { return _keywordList; }
    //        }

    //        /// <summary>
    //        /// A string containing the comment identifier.
    //        /// </summary>
    //        public string Comment
    //        {
    //            get { return _strComment; }
    //            set { _strComment = value; }
    //        }
    //        /// <summary>
    //        /// The color of comments.
    //        /// </summary>
    //        public Color CommentColor
    //        {
    //            get { return _colorComment; }
    //            set { _colorComment = value; }
    //        }

    //        #endregion

    //        public SyntaxFont FindFontById(string id)
    //        {
    //            SyntaxFont sf;
    //            if (this._fontDic.ContainsKey(id))
    //            {
    //                if (this._fontDic.TryGetValue(id, out sf))
    //                    return sf;
    //                else
    //                    throw new KeyNotFoundException("error retrieveing fontid");
    //            }
    //            else
    //                throw new KeyNotFoundException("fontid not found");


    //            //foreach (SyntaxFont sf in this._listFonts)
    //            //{
    //            //    if (sf.ID == id)
    //            //        return sf;
    //            //}

    //            //return null;
    //        }

    //        public void LoadComment(string commentKey, string fontId)
    //        {
    //            this.Comment = commentKey;
    //            this.commentFont = this.FindFontById(fontId);
    //        }

    //        private void LoadComment(XmlDocument doc)
    //        {
    //            string xp = "/SyntaxSettings/Comment";

    //            XmlNode xn = doc.SelectSingleNode(xp);
    //            this.Comment = xn.SelectSingleNode("@startswith").InnerText;
    //            string id = xn.SelectSingleNode("@fontid").InnerText;
    //            this.commentFont = this.FindFontById(id);
    //        }

            
    //        private void LoadKeywords(XmlDocument doc)
    //        {
    //            string xp = "/SyntaxSettings/Keywords";
    //            XmlNode xn = doc.SelectSingleNode(xp);
    //            string fontId = xn.SelectSingleNode("@fontid").InnerText;
    //            this.keywordFont = this.FindFontById(fontId);
    //            XmlNodeList xnl = xn.SelectNodes("word");
    //            foreach (XmlNode n in xnl)
    //            {
    //                string word = n.InnerText;
    //                this.AddKeyWord(word);
    //                //_keywordList.Add(word);
    //            }
    //        }

    //        private void LoadRegexRules(XmlDocument doc)
    //        {
    //            string xp = "/SyntaxSettings/Regex/Expr";
    //            XmlNodeList xnl = doc.SelectNodes(xp);
    //            foreach (XmlNode n in xnl)
    //            {
    //                string id = n.SelectSingleNode("@fontid").InnerText;
    //                string regexpr = n.SelectSingleNode("@rule").InnerText;
    //                string desc = n.SelectSingleNode("@desc").InnerText;
    //                this.AddRegex(regexpr, id, desc);
                 
    //            }
    //        }


    //        private void LoadFonts(XmlDocument doc)
    //        {
    //            string xp = "/SyntaxSettings/Fonts/Font";
    //            XmlNodeList xnl = doc.SelectNodes(xp);

    //            foreach (XmlNode n in xnl)
    //            {
    //                string id = n.SelectSingleNode("@id").InnerText;
    //                string name = n.SelectSingleNode("@name").InnerText;
    //                string size = n.SelectSingleNode("@size").InnerText;
    //                string style = n.SelectSingleNode("@style").InnerText;
    //                string color = n.SelectSingleNode("@color").InnerText;

    //                this.AddFont(id, name, color, size, style);
    //                //SyntaxFont sf = new SyntaxFont(id, name, color, size, style);
    //                //this._fontDic.Add(id, sf);
    //            }

    //        }

    //        public void SetKeywordFont(string fontId)
    //        {
    //            this.keywordFont = this.FindFontById(fontId);
    //        }
            
    //        public void AddFont(string id, string name, string color, string size, string style)
    //        {
    //            if (!this._fontDic.ContainsKey(id))
    //            {
    //                SyntaxFont sf = new SyntaxFont(id, name, color, size, style);
    //                this._fontDic.Add(id,sf);
    //            }


    //        }

    //        public void AddKeyWord(string keyword)
    //        { 
    //            if (!this._keywordList.Contains(keyword))
    //            {
    //                this._keywordList.Add(keyword);
    //            }
    //        }
    //        public void AddRegex(string regexpr, string fontid, string desc)
    //        {
    //            bool exist = this._regexList.Exists(delegate(RegexSettings r) { return r.Expression.ToString() == regexpr; });
    //            if (!exist)
    //                this._regexList.Add(new RegexSettings(regexpr, fontid, desc));
                
    //        }

    //        private void LoadDefaults()
    //        {

    //        }

    //        public void LoadRules(XmlDocument doc)
    //        {
    //            this.LoadFonts(doc);
    //            this.LoadRegexRules(doc);
    //            this.LoadComment(doc);
    //            this.LoadKeywords(doc);
    //        }
    //        public void LoadRules(string file)
    //        {
    //            if (!File.Exists(file))
    //                throw new FileNotFoundException("Rule file does not exist!", file);

    //            XmlDocument doc = new XmlDocument();
    //            doc.Load(file);

    //            this.LoadFonts(doc);
    //            this.LoadRegexRules(doc);
    //            this.LoadComment(doc);
    //            this.LoadKeywords(doc);

    //        }

    //    }
    //}

#endregion



    /// <summary>
    /// Flicker-controlled RichTextbox
    /// </summary>
    public partial class CustomEditorRTB : RichTextBox
    {
       
        

        private bool _paint = true;
        public bool EnablePaint
        {
            set { this._paint = value; }
        }

        public CustomEditorRTB()
        {
            InitializeComponent();
        }

        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            if (m.Msg == 0x00f)
            {
                if (_paint)
                    base.WndProc(ref m);
                else
                    m.Result = IntPtr.Zero;
            }
            else
                base.WndProc(ref m);
        }

        private bool tagPressed = false;
        
        protected override void OnKeyDown(KeyEventArgs e)
        {
            
            base.OnKeyDown(e);
        }
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            
            base.OnKeyPress(e);


            if (((Control.ModifierKeys) == Keys.Shift) && (e.KeyChar == '>' || e.KeyChar == '<'))
            {
              
                //TextHasChanged();
                tagPressed = true;
            }
            else
                tagPressed = false;
            
        }

        //protected override void OnTextChanged(EventArgs e)
        //{
        //   if (!keydown) 
        //       this.ColorAllText();
        //}
        protected override void OnKeyUp(KeyEventArgs e)
        {
            //Add syntax higlighting when user press Space or Enter
            base.OnKeyUp(e);

            if (tagPressed)
                TextHasChanged();
        }

      
    
    
        /// <summary>
        /// Colors all text. Slow for very large XML files
        /// </summary>
        public void ColorAllText()
        {
           // worker.RunWorkerAsync(new WorkerArg(0, this.Text));
            ExecuteColors(this.ColorTextFragment(0, this.Text));
            //EecuteColors(this.ColorTextFragment(0, RTBHelper<RichTextBox>.GetText((RichTextBox)this) ));

        }
        /// <summary>
        /// 
        /// </summary>
        private void TextHasChanged()
        {
            this.tagPressed = false;
           
            //Colors the current line only.
            int startindex = this.GetFirstCharIndexOfCurrentLine();
            int end = this.GetLastIndexToColor(startindex);
            if (startindex <= end)
            {
                string fragment = this.Text.Substring(startindex, end - startindex);
                List<ColoringState>  cs = ColorTextFragment(startindex, fragment);
                ExecuteColors(cs);
            }

        }

        private void ExecuteColors(List<ColoringState> cs)
        {
            if (cs != null)
            {
                this.EnablePaint = false;
                int counter = 0;
                foreach (ColoringState c in cs)
                {
                    counter++;

                    this.SelectionStart = c.SelectionStart;
                    this.SelectionLength = c.SelectionLength;
                    this.SelectionColor = c.CurrentColor;
                    this.SelectionFont = c.CurrentFont;

                    if (counter % 50 == 0)
                        this.EnablePaint = true;
                    else
                        this.EnablePaint = false;
                }

                this.EnablePaint = true;
            }
            
            //reset
            this.SelectionLength = 0;
            this.SelectionStart = this.SelectionStart + 1;
        }

        private List<ColoringState> ColorTextFragment(int start, string fragment)
        {
            int startCopy = start;
            char[] chars = fragment.ToCharArray();
            bool openQoute = false;
            bool openTag = false;
            
            Font fBold = new Font("Microsoft Sans Serif", this.Font.Size, FontStyle.Regular);
            Font fReg = new Font("Microsoft Sans Serif", this.Font.Size, FontStyle.Regular);

            List<ColoringState> crlList = new List<ColoringState>();
            
            Color prevColor = Color.White;
            Color curColor = Color.Maroon;
            int curLength = 0;
            Font curFont = fReg;
            int colorStartPos = start;

            foreach (char c in chars)
            {
                curFont = fReg;
                curLength++;

                if (c == '<')
                {
                    openTag = true;
                    curColor = Color.Maroon;
                }
                else if (c == '>')
                {
                    openTag = false;
                    curColor = Color.Maroon;
                }
                else if (c == '"')
                {
                    if (!openQoute) 
                        openQoute = true;
                    else
                        openQoute = false;

                    curColor = Color.DarkGreen;
                }
                else
                {
                    //tagtext <tagtext>
                    if (openQoute)
                    {
                        curColor = Color.DarkGreen;
                    }
                    else if (openTag)
                    {
                        curColor = Color.DarkBlue;
                    }
                    else
                    {
                        curColor = Color.Black;
                        curFont = fReg;
                    }
                }


                ColoringState cs = new ColoringState(curFont, this.Text.Substring(startCopy, 1), curColor, startCopy, 1);
                crlList.Add(cs);

                startCopy++;
            }

            return Merge(crlList);
        
        }

      

        private List<ColoringState> Merge(List<ColoringState> crlList)
        {
            if (crlList != null && crlList.Count > 0)
            {
                List<ColoringState> csnew = new List<ColoringState>();

                csnew.Add(crlList[0]);
                int lengthCounter = 0;
                ColoringState cur = new ColoringState(crlList[0].CurrentFont, "", crlList[0].CurrentColor, crlList[0].SelectionStart+1, 1); ;
                for (int i = 1; i < crlList.Count; i++)
                {
                    //if (cur == null && lengthCounter == 0)
                    //{
                    //    cur = new ColoringState(crlList[i].CurrentFont, "", crlList[i].CurrentColor, crlList[i].SelectionStart, 0);
                    //    lengthCounter++;
                    //}
                        
                    if (crlList[i].CurrentColor == crlList[i - 1].CurrentColor)
                    {
                        lengthCounter += crlList[i].SelectionLength;
                    }
                    if (crlList[i].CurrentColor != crlList[i - 1].CurrentColor)
                    {

                        cur = new ColoringState(cur.CurrentFont, this.Text.Substring(cur.SelectionStart, cur.SelectionLength), cur.CurrentColor, cur.SelectionStart, lengthCounter);
                        csnew.Add(cur);
                        cur = new ColoringState(crlList[i].CurrentFont, "", crlList[i].CurrentColor, crlList[i].SelectionStart, 0);

                        lengthCounter = crlList[i].SelectionLength;
                    }



                }

                if (lengthCounter > 0)
                {
                    cur = new ColoringState(cur.CurrentFont, this.Text.Substring(cur.SelectionStart, cur.SelectionLength), cur.CurrentColor, cur.SelectionStart, lengthCounter);
                    csnew.Add(cur);
                }


                return csnew;
            }

//            throw new Exception("Cannot merge an empy collection");
            return null;
        }

        private int GetLastIndexToColor(int start)
        {
            int pos = -1;
            int line = this.GetLineFromCharIndex(start);
            string s = this.Lines[line];
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == '<')
                {
                    pos = i;
                }
                if (s[i] == '>')
                {
                    pos = i;
                }
                if (s[i] == '\n')
                {
                    pos = i;
                }
            }

            //pos = this.Text.LastIndexOf('<', start+1);

            //if (pos != -1)
            //    return pos;

            //pos = this.Text.LastIndexOf('>', start+1);
            

            return pos + start + 1;

        }



       


       
    }
}
