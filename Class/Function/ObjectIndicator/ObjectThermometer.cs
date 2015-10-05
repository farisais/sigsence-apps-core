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
    class ObjectThermometer : ObjectIndicator
    {
        public ObjectThermometer(object control)
            : base(control)
        {

        }
        public ObjectThermometer()
            : base()
        {

        }

        protected override void ImplementUserControl()
        {
            Thermometer thermometer = new Thermometer();

            thermometer.Location = new System.Drawing.Point(172, 45);
            thermometer.Name = "thermometer";
            thermometer.Size = new System.Drawing.Size(72, 184);
            thermometer.TabIndex = 0;

            controlHandle = thermometer;
            InitControl(controlHandle);
            initContextMenuStrip();
        }

        public Thermometer GetControlHandle()
        {
            Thermometer OReturn = controlHandle as Thermometer;
            return OReturn;
        }
    }
}
