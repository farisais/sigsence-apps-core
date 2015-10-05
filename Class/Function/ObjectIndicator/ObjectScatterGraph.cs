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
    class ObjectScatterGraph: ObjectIndicator, IGraphProperties
    {
        int xAxisMax;
        int xAxisMin;
        int yAxisMax;
        int yAxisMin;
        public ObjectScatterGraph(object control)
            : base(control)
        {
            
        }
        public ObjectScatterGraph()
            : base()
        {

        }

        protected override void ImplementUserControl()
        {
            ScatterGraph scatterGraph = new ScatterGraph();
            ScatterPlot scatterPlot = new ScatterPlot();
            XAxis xAxis = new XAxis();
            YAxis yAxis = new YAxis();

            scatterGraph.Border = NationalInstruments.UI.Border.Raised;
            scatterGraph.Location = new System.Drawing.Point(83, 52);
            scatterGraph.Name = "scatterGraph";
            scatterGraph.PlotAreaBorder = NationalInstruments.UI.Border.ThinFrame3D;
            scatterGraph.Plots.AddRange(new NationalInstruments.UI.ScatterPlot[] {
            scatterPlot});
            scatterGraph.Size = new System.Drawing.Size(272, 168);
            scatterGraph.TabIndex = 0;
            scatterGraph.UseColorGenerator = true;
            scatterGraph.XAxes.AddRange(new NationalInstruments.UI.XAxis[] {
            xAxis});
            scatterGraph.YAxes.AddRange(new NationalInstruments.UI.YAxis[] {
            yAxis});

            scatterPlot.XAxis = xAxis;
            scatterPlot.YAxis = yAxis;

            controlHandle = scatterGraph;
            InitControl(controlHandle);
            initContextMenuStrip();
        }

        public ScatterGraph GetControlHandle()
        {
            ScatterGraph OReturn = controlHandle as ScatterGraph;
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
