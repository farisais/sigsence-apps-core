using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using NationalInstruments.UI.WindowsForms;
using NationalInstruments.UI;
using NationalInstruments.Analysis.SignalGeneration;
using NationalInstruments.Analysis.Math;
using NationalInstruments.Analysis.Dsp;
using NationalInstruments.DAQmx;

namespace DSA_Teaser.Class.Function.ObjectSequence
{
    public partial class FormFFTSourceSelect : Form
    {
        public bool isOKButton;
        public FormFFTSourceSelect()
        {
            InitializeComponent();
            foreach (string name in Enum.GetNames(typeof(ScaledWindowType)))
                comboBoxEditWindow.Properties.Items.Add(name);

            comboBoxEditWindow.SelectedIndex = 0;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            isOKButton = true;
            this.Close();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            isOKButton = false;
            this.Close();
        }
    }
}
