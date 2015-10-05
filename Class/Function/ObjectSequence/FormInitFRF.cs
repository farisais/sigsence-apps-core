using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DSA_Teaser.Class.Function.ObjectSequence
{
    public partial class FormInitFRF : Form
    {
        public bool isOK;
        public FormInitFRF()
        {
            InitializeComponent();
        }

        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            isOK = true;
            this.Close();
        }

        private void simpleButtonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
