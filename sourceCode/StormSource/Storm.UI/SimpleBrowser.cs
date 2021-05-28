using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Storm.UI
{
    public partial class SimpleBrowser : Form
    {
  
        public SimpleBrowser()
        {
            InitializeComponent();
        }

        public SimpleBrowser(string doc)
        {
            InitializeComponent();
            this.webBrowser1.DocumentText = doc;
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
