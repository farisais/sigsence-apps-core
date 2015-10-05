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
    class ObjectDigitalWaveformGraph: ObjectIndicator, IGraphProperties
    {
        int xAxisMax;
        int xAxisMin;
        int yAxisMax;
        int yAxisMin;

        public ObjectDigitalWaveformGraph(object control)
            : base(control)
        {
            
        }
        public ObjectDigitalWaveformGraph()
            : base()
        {

        }

        protected override void ImplementUserControl()
        {
            DigitalWaveformGraph digitalWaveformGraph = new DigitalWaveformGraph();
            digitalWaveformGraph.Location = new System.Drawing.Point(91, 50);
            digitalWaveformGraph.Border = NationalInstruments.UI.Border.ThinFrame3D;
            digitalWaveformGraph.Name = "digitalWaveformGraph";
            digitalWaveformGraph.Size = new System.Drawing.Size(272, 168);
            digitalWaveformGraph.TabIndex = 0;

            controlHandle = digitalWaveformGraph;
            InitControl(controlHandle);
            initContextMenuStrip();
        }

        public DigitalWaveformGraph GetControlHandle()
        {
            DigitalWaveformGraph OReturn = controlHandle as DigitalWaveformGraph;
            return OReturn;
        }

        #region IGraphProperties Members

        int IGraphProperties.XAxisMax
        {
            get
            {
                return xAxisMax;
            }
            set
            {
                xAxisMax = value;
            }
        }

        int IGraphProperties.XAxisMin
        {
            get
            {
                return xAxisMin;
            }
            set
            {
                xAxisMin = value;
            }
        }

        int IGraphProperties.YAxisMax
        {
            get
            {
                return yAxisMax;
            }
            set
            {
                yAxisMin = value;
            }
        }

        int IGraphProperties.YAxisMin
        {
            get
            {
                return yAxisMin;
            }
            set
            {
                yAxisMin = value;
            }
        }

        #endregion
    }
}
