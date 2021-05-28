using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace Storm.UI
{
    public partial class Splash : Form
    {
        
        public Splash()
        {
            InitializeComponent();
            
        }
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            //show for 2 seconds!
            int showSplash = 2;

            try
            {
                string num = System.Configuration.ConfigurationManager.AppSettings["ShowSplash"];

                if (!String.IsNullOrEmpty(num))
                    showSplash = Convert.ToInt32(num);
            }
            catch {}

            if (showSplash > 0)
               System.Threading.Thread.Sleep(showSplash * 1000);
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

      
        private void SplashForm_Load(object sender, EventArgs e)
        {
            AssemblyName n = Assembly.GetExecutingAssembly().GetName();
            Version v = n.Version;

            label1.Text = "v" + v.Major + "." + v.Minor + "." + v.Build;

            this.backgroundWorker2.RunWorkerAsync();

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}