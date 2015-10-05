using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Threading;
using DSA_Teaser.Class;
using System.IO;

using AviFile;
using wavfile;
using Un4seen.Bass.AddOn;
using Un4seen.Bass.Misc;
using Un4seen.BassWasapi;

using NAudio;
using NAudio.Wave;

namespace DSA_Teaser.Class.UI
{
    public partial class UserControlAnalysisPanel : UserControl
    {
        public Form1 MainForm;
        public UserControlAnalysisPanel()
        {
            InitializeComponent();

        }

        public void InitWorkingPanel()
        {

            foreach (string dir in Directory.GetDirectories(MainForm.SigsenceProject.RecordLocationPath))
            {
                string addDir = dir.Split((@"\").ToCharArray()[0]).ToList<string>()[dir.Split((@"\").ToCharArray()[0]).Count() - 1];
                listBoxControl1.Items.Add(addDir);
            }
        }

        private void trackBarControl1_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}
