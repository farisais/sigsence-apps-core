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
    class ObjectIntensityGraph: ObjectIndicator, IGraphProperties
    {

        int xAxisMax;
        int xAxisMin;
        int yAxisMax;
        int yAxisMin;

        public ObjectIntensityGraph(object control)
            : base(control)
        {

        }
        public ObjectIntensityGraph()
            : base()
        {

        }

        protected override void ImplementUserControl()
        {
            IntensityGraph intensityGraph = new IntensityGraph();
            ColorScale colorScale = new ColorScale();
            IntensityPlot intensityPlot = new IntensityPlot();
            IntensityXAxis intensityXAxis = new IntensityXAxis();
            IntensityYAxis intensityYAxis = new IntensityYAxis();

            intensityGraph.Border = NationalInstruments.UI.Border.ThinFrame3D;
            intensityGraph.ColorScales.AddRange(new NationalInstruments.UI.ColorScale[] {
            colorScale});
            intensityGraph.Location = new System.Drawing.Point(164, 50);
            intensityGraph.Name = "intensityGraph";
            intensityGraph.Plots.AddRange(new NationalInstruments.UI.IntensityPlot[] {
            intensityPlot});
            intensityGraph.Size = new System.Drawing.Size(272, 168);
            intensityGraph.TabIndex = 0;
            intensityGraph.XAxes.AddRange(new NationalInstruments.UI.IntensityXAxis[] {
            intensityXAxis});
            intensityGraph.YAxes.AddRange(new NationalInstruments.UI.IntensityYAxis[] {
            intensityYAxis});

            intensityPlot.ColorScale = colorScale;
            intensityPlot.XAxis = intensityXAxis;
            intensityPlot.YAxis = intensityYAxis;

            controlHandle = intensityGraph;
            InitControl(controlHandle);
            initContextMenuStrip();
        }

        public IntensityGraph GetControlHandle()
        {
            IntensityGraph OReturn = controlHandle as IntensityGraph;
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
