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
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;


using NationalInstruments.UI.WindowsForms;
using NationalInstruments.UI;
using NationalInstruments.Analysis.SignalGeneration;
using NationalInstruments.Analysis.Math;
using NationalInstruments.Analysis.Dsp;

using Sigsence.ApplicationElements;
using Sigsence.ApplicationFunction;

namespace DSA_Teaser
{
    [Serializable()]
    class ObjectWaveformGraph : ObjectIndicator, IGraphProperties
    {
        int xAxisMax;
        int xAxisMin;
        int yAxisMax;
        int yAxisMin;
        bool firstSet = true;
        List<double> dataBuffer;
        double[] data;
        List<double> dataPl;
        Int32 maxDataPos;
        Thread PlotingProc;
        List<double> dataPlot;
        Int32 dataUpdateSize;
        bool realTimeObject;
        //private delegate void GraphProcessing(List<double> edata);

        public ObjectWaveformGraph(object control)
            : base(control)
        {

        }

        public ObjectWaveformGraph()
            : base()
        {

        }

        protected override void ImplementUserControl()
        {
            WaveformGraph waveformGraph = new WaveformGraph();

            WaveformPlot waveformPlot = new WaveformPlot();
            XAxis xAxis = new XAxis();
            YAxis yAxis = new YAxis();

            xAxis.AutoMinorDivisionFrequency = 5;
            xAxis.MajorDivisions.GridVisible = true;
            xAxis.MinorDivisions.GridVisible = true;
            xAxis.MajorDivisions.LabelFormat = new NationalInstruments.UI.FormatString(NationalInstruments.UI.FormatStringMode.DateTime, "mm:ss.fff");

            yAxis.AutoMinorDivisionFrequency = 5;
            yAxis.MajorDivisions.GridVisible = true;
            yAxis.MinorDivisions.GridVisible = true;

            waveformPlot.XAxis = xAxis;
            waveformPlot.YAxis = yAxis;

            XYCursor cursor = new XYCursor();
            cursor.SnapMode = CursorSnapMode.Floating;
            cursor.HorizontalCrosshairMode = CursorCrosshairMode.None;

            waveformGraph.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            waveformGraph.Border = NationalInstruments.UI.Border.ThinFrame3D;
            waveformGraph.CaptionFont = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            waveformGraph.ForeColor = System.Drawing.Color.White;
            waveformGraph.Location = new System.Drawing.Point(44, 33);
            waveformGraph.Name = "waveformGraph";
            waveformGraph.Cursor = System.Windows.Forms.Cursors.Arrow;
            waveformGraph.Cursors.Add(cursor);
            waveformGraph.Caption = waveformGraph.Name;
            waveformGraph.CaptionBackColor = Color.LightGray;
            waveformGraph.PlotAreaBorder = NationalInstruments.UI.Border.Dashed;
            waveformGraph.Plots.AddRange(new NationalInstruments.UI.WaveformPlot[] {
            waveformPlot});
            waveformGraph.Size = new System.Drawing.Size(518, 168);
            waveformGraph.TabIndex = 0;
            waveformGraph.UseColorGenerator = true;
            waveformGraph.XAxes.AddRange(new NationalInstruments.UI.XAxis[] {
            xAxis});
            waveformGraph.YAxes.AddRange(new NationalInstruments.UI.YAxis[] {
            yAxis});


            #region
            /*waveformGraph.AllowDrop = true;
            waveformGraph.Border = NationalInstruments.UI.Border.ThinFrame3D;
            waveformGraph.Cursor = System.Windows.Forms.Cursors.Arrow;
            waveformGraph.Location = new System.Drawing.Point(0, 0);
            waveformGraph.Name = "waveformGraph";
            waveformGraph.Caption = waveformGraph.Name;// +" " + IndicatorList.Count.ToString();
            waveformGraph.CaptionBackColor = Color.SlateGray;
            waveformGraph.Plots.AddRange(new NationalInstruments.UI.WaveformPlot[] {
            waveformPlot});
            waveformGraph.Cursors.Add(cursor);
            waveformGraph.Size = new System.Drawing.Size(340, 184);
            waveformGraph.TabIndex = 2;
            waveformGraph.UseColorGenerator = true;
            waveformGraph.XAxes.AddRange(new NationalInstruments.UI.XAxis[] {
            xAxis});
            waveformGraph.YAxes.AddRange(new NationalInstruments.UI.YAxis[] {
            yAxis});*/

            #endregion

            cursor.Plot = waveformPlot;
            controlHandle = waveformGraph;
            InitControl(controlHandle);
            initContextMenuStrip();

            plotHistoryTime = 100; //10 Second history time

            timerGraphUpdate = new System.Windows.Forms.Timer();
            timerGraphUpdate.Interval = 1;
            timerGraphUpdate.Tick += new EventHandler(timerGraphUpdate_Tick);
        }

        private int plotHistoryTime;
        public int PlotHistoryTime
        {
            get
            {
                return plotHistoryTime;
            }
            set
            {
                plotHistoryTime = value;
            }
        }

        void timerGraphUpdate_Tick(object sender, EventArgs e)
        {
            //int range = 160;
            if (realTimeObject && dataBuffer.Count > 0)
            {
                List<double> dataTemp = new List<double>();
                dataTemp.AddRange(dataBuffer.GetRange(0, dataUpdateSize));
                dataBuffer.RemoveRange(0, dataUpdateSize);
                dataPlot.AddRange(dataTemp);
                dataPlot.RemoveRange(0, dataUpdateSize);
                (controlHandle as WaveformGraph).Plots[0].PlotY(dataPlot.ToArray());
            }
        }


        protected override void setData(double[] data)
        {
            WaveformGraph Wave = controlHandle as WaveformGraph;
            if ((controlHandle as WaveformGraph).InvokeRequired)
            {
                UpdateGraphData Update = new UpdateGraphData(setData);
                Wave.Invoke(Update, new object[] { data });
                //OWInvoke(Update, new object[] { data });
            }
            else
            {
                try
                {
                    (controlHandle as WaveformGraph).Plots[0].PlotY(data);
                }
                catch (ObjectDisposedException ex)
                {

                }
            }
        }

        protected override void Control_Click(object sender, EventArgs e)
        {
            base.Control_Click(sender, e);
        }

        private int cursorArea = 10;
        private bool isNotInCursor(Point location, WaveformGraph wfGraph)
        {
            bool result = true;
            for (int i = 0; i < wfGraph.Cursors.Count; i++)
            {
                //Mouse is in cursor
                double XRelativePos = wfGraph.Cursors[i].XPosition - wfGraph.XAxes[0].Range.Minimum;
                double XCursorCoordinate = ((XRelativePos / (wfGraph.XAxes[0].Range.Maximum - wfGraph.XAxes[0].Range.Minimum)) * wfGraph.PlotAreaBounds.Width);
                XCursorCoordinate += ((wfGraph.Width - wfGraph.PlotAreaBounds.Width) / 2);
                double YRelativePos = wfGraph.YAxes[0].Range.Maximum - (wfGraph.Cursors[i].YPosition - wfGraph.YAxes[0].Range.Minimum);
                double YCursorCoordinate = ((YRelativePos / (wfGraph.YAxes[0].Range.Maximum - wfGraph.YAxes[0].Range.Minimum)) * wfGraph.PlotAreaBounds.Height);
                YCursorCoordinate += wfGraph.PlotAreaBounds.Location.Y;
                if (((XCursorCoordinate + cursorArea) > location.X && (XCursorCoordinate - cursorArea) < location.X)
                    || ((YCursorCoordinate + cursorArea) > location.Y && (YCursorCoordinate - cursorArea) < location.Y))
                {
                    result = false;
                    break;
                }
            }
            return result;
        }
        protected override void Control_MouseDown(object sender, MouseEventArgs e)
        {
            WaveformGraph wfGraph = (WaveformGraph)controlHandle;
            bool check = isNotInCursor(e.Location, wfGraph);
            if (e.X < wfGraph.PlotAreaBounds.X || e.X > (wfGraph.PlotAreaBounds.X + wfGraph.PlotAreaBounds.Width)
                || e.Y < wfGraph.PlotAreaBounds.Y || e.Y > (wfGraph.PlotAreaBounds.Y + wfGraph.PlotAreaBounds.Height)
                || ((e.X > wfGraph.PlotAreaBounds.X && e.X < (wfGraph.PlotAreaBounds.X + wfGraph.Width)) && isNotInCursor(e.Location, wfGraph)))
            {
                base.Control_MouseDown(sender, e);
            }
        }

        protected override void Control_MouseMove(object sender, MouseEventArgs e)
        {
            WaveformGraph wfGraph = (WaveformGraph)controlHandle;
            bool check = isNotInCursor(e.Location, wfGraph);
            if (e.X < wfGraph.PlotAreaBounds.X || e.X > (wfGraph.PlotAreaBounds.X + wfGraph.PlotAreaBounds.Width)
                || e.Y < wfGraph.PlotAreaBounds.Y || e.Y > (wfGraph.PlotAreaBounds.Y + wfGraph.PlotAreaBounds.Height)
                || ((e.X > wfGraph.PlotAreaBounds.X && e.X < (wfGraph.PlotAreaBounds.X + wfGraph.Width)) && isNotInCursor(e.Location, wfGraph)))
            {
                base.Control_MouseMove(sender, e);
            }
        }

        protected override void Control_MouseUp(object sender, MouseEventArgs e)
        {
            WaveformGraph wfGraph = (WaveformGraph)controlHandle;
            bool check = isNotInCursor(e.Location, wfGraph);
            if (e.X < wfGraph.PlotAreaBounds.X || e.X > (wfGraph.PlotAreaBounds.X + wfGraph.PlotAreaBounds.Width)
                || e.Y < wfGraph.PlotAreaBounds.Y || e.Y > (wfGraph.PlotAreaBounds.Y + wfGraph.PlotAreaBounds.Height)
                || ((e.X > wfGraph.PlotAreaBounds.X && e.X < (wfGraph.PlotAreaBounds.X + wfGraph.Width)) && isNotInCursor(e.Location, wfGraph)))
            {
                base.Control_MouseUp(sender, e);
            }
        }

        protected override void AssignSequence()
        {
            WaveformGraph wg = controlHandle as WaveformGraph;
            wg.XAxes[0].Mode = AxisMode.AutoScaleExact;
            wg.YAxes[0].Mode = AxisMode.AutoScaleLoose;
            wg.Plots[0].AntiAliased = true;
            wg.Plots[0].SmoothUpdates = true;

            if (SequenceSource.ControlTypeName == ControlTypeNames.AudioIn || SequenceSource.ControlTypeName == ControlTypeNames.DeviceNI)
            {
                Type T = typeof(ObjectAudioIn);
                wg.XAxes[0].Mode = AxisMode.Fixed;
                wg.YAxes[0].Mode = AxisMode.AutoScaleLoose;
                wg.YAxes[0].Range = new Range(-10000, 10000);
                wg.XAxes[0].Range = new Range(0, 64000);
                maxDataPos = (Int32)(wg.XAxes[0].Range.Maximum - wg.XAxes[0].Range.Minimum);
                dataPlot = new List<double>();
                double[] dataTemp = new double[maxDataPos];
                data = new double[maxDataPos];
                dataPlot.AddRange(dataTemp);
                dataPl = new List<double>();
                dataPl.AddRange(dataTemp);
                wg.Plots[0].HistoryCapacity = 64000;
            }
            else
            {
                if (SequenceSource.SignalData != null)
                {
                    dataPlot = new List<double>(SequenceSource.SignalData);
                }
                else
                {
                    dataPlot = new List<double>();
                }
                setData(dataPlot.ToArray());
            }

            if (firstSet)
            {
                dataBuffer = new List<double>();
                SequenceSource.UpdateData += new ObjectSequence.DataUpdate(SequenceSource_UpdateData);
                firstSet = false;
                //timerGraphUpdate.Start();
                if (sequenceSource.ControlTypeName == ControlTypeNames.AudioIn || sequenceSource.ControlTypeName == ControlTypeNames.DeviceNI || sequenceSource.ControlTypeName == ControlTypeNames.FFT)
                {
                    switch (sequenceSource.ControlTypeName)
                    {
                        case ControlTypeNames.AudioIn:
                            dataUpdateSize = ((ObjectAudioIn)sequenceSource)._audioFrameSize / 18;
                            break;
                        case ControlTypeNames.DeviceNI:
                            dataUpdateSize = ((ObjectDeviceNI)sequenceSource).SampleRate / 25;
                            break;
                    }

                    realTimeObject = true;
                }
            }
        }

        private T transform<T>(object input)
        {
            return (T)input; 
        }

        void SequenceSource_UpdateData(object sender, OSequenceDataUpdateEventArgs e)
        {
            //dataBuffer.AddRange(e.DataUpdate);
            PlotingProc = new Thread(new ParameterizedThreadStart(GraphPlotting));
            PlotingProc.Start(e.DataUpdate);

            //ThreadPool.QueueUserWorkItem(state =>
            //{
            //    dataPlot.RemoveRange(0, e.DataUpdate.Length);
            //    dataPlot.AddRange(e.DataUpdate);
            //    setData(dataPlot.ToArray());
            //});
        }

        protected override void InitPlayerMode()
        {
            playerSource.UpdateData += new ObjectPlayer.DataUpdate(playerSource_UpdateData);
        }

        void playerSource_UpdateData(object sender, OSequenceDataUpdateEventArgs e)
        {
            PlotingProc = new Thread(new ParameterizedThreadStart(GraphPlotting));
            PlotingProc.Start(e.DataUpdate);
        }

        private void GraphPlotting(object info)
        {
            try
            {
                double[] edata = info as double[];
                if (edata.Length > dataPlot.Count)
                {
                    dataPlot.RemoveRange(0, dataPlot.Count);
                    dataPlot.AddRange(edata);
                }
                else
                {
                    dataPlot.RemoveRange(0, edata.Length);
                    dataPlot.AddRange(edata);
                }
                setData(dataPlot.ToArray());
            }
            catch (ArgumentException ex)
            {
            }
        }

        protected override void setProperties(ObjectSignal Osignal)
        {
            WaveformGraph wg = controlHandle as WaveformGraph;
            wg.Caption = Osignal.ControlName;
        }

        protected override void DisposeSequenceSource()
        {
            if (sequenceSource != null)
            {
                sequenceSource.UpdateData -= SequenceSource_UpdateData;
                sequenceSource = null;
                GC.SuppressFinalize(this);
            }
        }

        public WaveformGraph GetControlHandle()
        {
            WaveformGraph OReturn = controlHandle as WaveformGraph;
            return OReturn;
        }

        private System.Windows.Forms.Timer timerGraphUpdate;
        public System.Windows.Forms.Timer TimerGraphUpdate
        {
            get
            {
                return timerGraphUpdate;
            }
            set
            {
                timerGraphUpdate = value;
            }
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

        public override void Dispose()
        {
            if (sequenceSource != null)
            {
                sequenceSource.UpdateData -= SequenceSource_UpdateData;
            }
            base.Dispose();
        }
    }
}
