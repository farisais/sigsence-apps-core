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
    class ObjectComplexGraph : ObjectIndicator, IGraphProperties
    {
        int xAxisMax;
        int xAxisMin;
        int yAxisMax;
        int yAxisMin;

        public ObjectComplexGraph(object control)
            : base(control)
        {

        }

        public ObjectComplexGraph()
            : base()
        {

        }

        protected override void ImplementUserControl()
        {
            ComplexGraph complexGraph = new ComplexGraph();
            ComplexPlot complexPlot = new ComplexPlot();
            ComplexXAxis complexXAxis = new ComplexXAxis();
            ComplexYAxis complexYAxis = new ComplexYAxis();

            complexGraph.Border = NationalInstruments.UI.Border.ThinFrame3D;
            complexGraph.Location = new System.Drawing.Point(71, 101);
            complexGraph.Name = "complexGraph";
            complexGraph.Plots.AddRange(new NationalInstruments.UI.ComplexPlot[] {
            complexPlot});
            complexGraph.Size = new System.Drawing.Size(272, 168);
            complexGraph.TabIndex = 0;
            complexGraph.UseColorGenerator = true;
            complexGraph.XAxes.AddRange(new NationalInstruments.UI.ComplexXAxis[] {
            complexXAxis});
            complexGraph.YAxes.AddRange(new NationalInstruments.UI.ComplexYAxis[] {
            complexYAxis});
            complexPlot.XAxis = complexXAxis;
            complexPlot.YAxis = complexYAxis;

            controlHandle = complexGraph;
            InitControl(controlHandle);
            initContextMenuStrip();
        }

        public ComplexGraph GetControlHandle()
        {
            ComplexGraph OReturn = controlHandle as ComplexGraph;
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
