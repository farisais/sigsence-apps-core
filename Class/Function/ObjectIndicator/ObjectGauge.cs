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
    class ObjectGauge : ObjectIndicator
    {
        public ObjectGauge(object control)
            : base(control)
        {
            ImplementUserControl();
        }
        public ObjectGauge()
            : base()
        {

        }

        protected override void ImplementUserControl()
        {
            Gauge gauge = new Gauge();

            gauge.Location = new System.Drawing.Point(146, 74);
            gauge.Name = "gauge";
            gauge.Size = new System.Drawing.Size(160, 152);
            gauge.TabIndex = 0;

            controlHandle = gauge;
            InitControl(gauge);
            initContextMenuStrip();
        }

        protected override void AssignSequence()
        {
            Gauge g = controlHandle as Gauge;
            g.Value = SequenceSource.SignalData[SequenceSource.SignalData.Length - 1];
            SequenceSource.UpdateData += new ObjectSequence.DataUpdate(SequenceSource_UpdateData);
        }

        void SequenceSource_UpdateData(object sender, OSequenceDataUpdateEventArgs e)
        {
            ThreadPool.QueueUserWorkItem(state =>
            {
                setData(e.DataUpdate);
            });
        }

        protected override void setData(double[] data)
        {
            //if ((controlHandle as Gauge).InvokeRequired)
            //{
            //    UpdateGraphData Update = new UpdateGraphData(setData);
            //    parentWindow.Invoke(Update, new object[] { data });
            //}
            //else
            //{
            //    try
            //    {
            //        (controlHandle as Gauge).Value = data[data.Length - 1];
            //    }
            //    catch (ObjectDisposedException ex)
            //    {
            //    }
            //}
        }

        public Gauge GetControlHandle()
        {
            Gauge OReturn = controlHandle as Gauge;
            return OReturn;
        }
    }
}
