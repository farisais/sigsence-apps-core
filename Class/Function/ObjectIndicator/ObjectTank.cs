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
    class ObjectTank: ObjectIndicator
    {
        public ObjectTank(object control)
            : base(control)
        {
 
        }

        public ObjectTank()
            : base()
        {

        }

        protected override void ImplementUserControl()
        {
            Tank tank = new Tank();

            tank.Location = new System.Drawing.Point(102, 55);
            tank.Name = "tank";
            tank.Size = new System.Drawing.Size(110, 152);
            tank.TabIndex = 0;

            controlHandle = tank;
            InitControl(controlHandle);
            initContextMenuStrip();
        }

        public Tank GetControlHandle()
        {
            Tank OReturn = controlHandle as Tank;
            return OReturn;
        }
    }
}
