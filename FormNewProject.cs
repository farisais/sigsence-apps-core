using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DSA_Teaser
{
    public partial class FormNewProject : Form
    {
        public bool buttonOK = false;
        public string fileName;
        public string locationPath;
        public string projectType;
        public FormNewProject()
        {
            InitializeComponent();
            textEditPath.Text = @"C:\Users\faris\Documents\Sigsence DeskApp V.1.0\Projects";
            textEditFileName.Text = "SigsenceProject";
        }

        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            buttonOK = true;
            projectType = radioGroupProjectType.Properties.Items[radioGroupProjectType.SelectedIndex].Description;
            fileName = textEditFileName.Text;
            locationPath = textEditPath.Text;
            this.Close();
        }

        private void simpleButtonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void simpleButtonBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.SelectedPath = @"C:\Users\faris\Documents\Sigsence DeskApp V.1.0\Projects";
            folderBrowserDialog.Description = "Please select location to save your Sigsence Solution File";
            DialogResult result = folderBrowserDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                locationPath = folderBrowserDialog.SelectedPath;
                textEditPath.Text = locationPath;
            }
        }

        private void saveFileNewProject_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {

        }

    }
}
