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
using System.IO;


using NationalInstruments.UI.WindowsForms;
using NationalInstruments.UI;
using NationalInstruments.Analysis.SignalGeneration;
using NationalInstruments.Analysis.Math;
using NationalInstruments.Analysis.Dsp;
using NationalInstruments.DAQmx;

using Sigsence.ApplicationElements;

namespace DSA_Teaser
{
    class ObjectSound: ObjectSequence
    {
        public ObjectSound(object control)
            : base(control)
        {
            ImplementUserControl();
        }
        public ObjectSound()
        {
            ImplementUserControl();
            Microsoft.DirectX.DirectSound.Device device = new Microsoft.DirectX.DirectSound.Device();
            
        }

        private void ImplementUserControl()
        {
            PictureBox pictureBox = new PictureBox();
            pictureBox.Image = global::DSA_Teaser.Properties.Resources.DAQ_Icon;
            pictureBox.Location = new System.Drawing.Point(65, 67);
            pictureBox.Name = "pictureBox1";
            pictureBox.Size = new System.Drawing.Size(43, 43);
            pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            pictureBox.TabIndex = 0;
            pictureBox.TabStop = false;
            pictureBox.Padding = new Padding(3);
            controlHandle = pictureBox;

            InitControl(controlHandle);

            //Label for the sequence icon
            TextBox textboxSequence = new TextBox();
            textboxSequence.Multiline = true;
            textboxSequence.Size = new System.Drawing.Size(103, 14);
            textboxSequence.Location = new System.Drawing.Point(35, 110);
            textboxSequence.Text = controlName;
            textboxSequence.TextAlign = HorizontalAlignment.Center;
            textboxSequence.BorderStyle = BorderStyle.None;
            textboxSequence.ReadOnly = true;
            textboxSequence.BackColor = System.Drawing.Color.White;
            sequenceTextbox = textboxSequence;

            UserControlSignalGenerator UCSignalGenerator = new UserControlSignalGenerator();
            List<Control> UCList = new List<Control>();
            UCList.Add(UCSignalGenerator);
            objectUserControl = UCList;


        }

    }
}
