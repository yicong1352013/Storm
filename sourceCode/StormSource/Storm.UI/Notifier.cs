using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Storm.UI
{
    public partial class Notifier : Form
    {
        private bool showing;
        private bool disposeWhenFinished;
        private bool forceClose;

        public bool DisposeWhenFinished
        {
            get { return disposeWhenFinished; }
            set { disposeWhenFinished = value; }
        }

        public int ShowFor
        {
            get { return this.timerCloser.Interval; }
            set { this.timerCloser.Interval = value; }
        }

        public string Message
        {
            get { return this.richTextBox1.Text; }
            set { this.richTextBox1.Text = value; }
        }


        public Notifier()
        {
            InitializeComponent();
            this.richTextBox1.ContentsResized += new ContentsResizedEventHandler(richTextBox1_ContentsResized);
            this.showing = true;
            this.Opacity = 0.0;
            this.pictureBox1.Focus();
            

        }

        void richTextBox1_ContentsResized(object sender, ContentsResizedEventArgs e)
        {
            ((RichTextBox)sender).Height = e.NewRectangle.Height + 5;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Notifier_Load(object sender, EventArgs e)
        {
            RestartTimers();
            //this.ParentForm.Focus();
        }

        private void RestartTimers()
        {
            this.timerFader.Enabled = true;
            this.timerCloser.Enabled = true;
            this.timerFader.Start();
            this.timerCloser.Start();
        }

        private void timerCloser_Tick(object sender, EventArgs e)
        {
            this.Close();
        }

        private void timerFader_Tick(object sender, EventArgs e)
        {

            if (showing)
            {
                //fade in?
                if (this.Opacity < 1)
                {
                    this.Opacity += 0.1;
                }
                else
                {
                    this.timerFader.Stop();
                }
            }
            else
            {
                //Fade out
                if (this.Opacity > 0)
                {
                    this.Opacity -= 0.1;
                }
                else
                {
                    this.timerFader.Stop();
                    forceClose = true;
                    this.Close();
                    if (this.disposeWhenFinished)
                        this.Dispose();
                }


            }
        }

        private void Notifier_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!forceClose)
            {
                //m_origDialogResult = this.DialogResult;
                e.Cancel = true;
                showing = false;
                this.timerFader.Start();
            }

            //this.timerFader.Enabled = false;
            //this.timerCloser.Enabled = false;

        }
    }
}
