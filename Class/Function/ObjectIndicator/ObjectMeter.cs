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
    class ObjectMeter : ObjectIndicator
    {
        public ObjectMeter(object control)
            : base(control)
        {

        }
        public ObjectMeter()
            : base()
        {

        }

        protected override void ImplementUserControl()
        {
            Meter meter = new Meter();

            meter.Border = NationalInstruments.UI.Border.ThinFrame3D;
            meter.Location = new System.Drawing.Point(59, 65);
            meter.Name = "meter";
            meter.Size = new System.Drawing.Size(214, 100);
            meter.TabIndex = 0;

            controlHandle = meter;
            InitControl(controlHandle);
            initContextMenuStrip();
        }

        public Meter GetControlHandle()
        {
            Meter OReturn = controlHandle as Meter;
            return OReturn;
        }
    }
}
