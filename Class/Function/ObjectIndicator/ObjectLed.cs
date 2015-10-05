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

using NationalInstruments.UI.WindowsForms;
using NationalInstruments.UI;
using NationalInstruments.Analysis.SignalGeneration;
using NationalInstruments.Analysis.Math;
using NationalInstruments.Analysis.Dsp;
using Sigsence.ApplicationElements;

namespace DSA_Teaser
{
    class ObjectLed: ObjectIndicator
    {
        public ObjectLed(object control)
            : base(control)
        {

        }
        public ObjectLed()
            : base()
        {

        }

        protected override void ImplementUserControl()
        {
            Led led = new Led();

            led.LedStyle = NationalInstruments.UI.LedStyle.Round3D;
            led.Location = new System.Drawing.Point(178, 24);
            led.Name = "led";
            led.Size = new System.Drawing.Size(64, 64);
            led.TabIndex = 0;

            controlHandle = led;
            InitControl(controlHandle);
            initContextMenuStrip();
        }

        public Led GetControlHandle()
        {
            Led OReturn = controlHandle as Led;
            return OReturn;
        }
    }
}
