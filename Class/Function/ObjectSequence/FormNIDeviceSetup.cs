using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using NationalInstruments;
using NationalInstruments.UI.WindowsForms;
using NationalInstruments.UI;
using NationalInstruments.Analysis.SignalGeneration;
using NationalInstruments.Analysis.Math;
using NationalInstruments.Analysis.Dsp;
using NationalInstruments.DAQmx;


namespace DSA_Teaser
{
    public partial class FormNIDeviceSetup : Form
    {
        public bool isOK = false;
        private AnalogMultiChannelReader analogInReader;
        public Task myTask;
        private Task runningTask;
        private AsyncCallback analogCallback;
        private AnalogWaveform<double>[] data;
        private Double sampleRate;
        private Int32 sampleRead;
        public double resolution;
        public FormNIDeviceSetup()
        {
            InitializeComponent();
            try
            {
                comboBoxChannel.Properties.Items.AddRange(DaqSystem.Local.GetPhysicalChannels(PhysicalChannelTypes.AI, PhysicalChannelAccess.External));
            }
            catch (DaqException dex)
            {
                MessageBox.Show(dex.Message);
            }

            if (comboBoxChannel.Properties.Items.Count > 0)
            {
                comboBoxChannel.SelectedIndex = 0;
                checkEditAutoScale.CheckState = CheckState.Checked;
                waveformGraphScope.YAxes[0].Mode = AxisMode.AutoScaleLoose;
                waveformGraphScope.XAxes[0].Mode = AxisMode.AutoScaleExact;
                sampleRate = Convert.ToDouble(textEditRate.EditValue);
                sampleRead = Convert.ToInt32(textEditSamplePerCh.EditValue);
                StartScope();
            }

        }

        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            isOK = true;
            runningTask = null;
            try
            {
                myTask.Dispose();
            }
            catch (DaqException dex)
            {
                MessageBox.Show(dex.Message, "DAQ Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            this.Close();
        }

        private void simpleButtonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void checkEditAutoScale_CheckedChanged(object sender, EventArgs e)
        {
            waveformGraphScope.Plots[0].ClearData();
        }

        private void comboBoxChannel_SelectedValueChanged(object sender, EventArgs e)
        {
            waveformGraphScope.Plots[0].ClearData();
            StartScope();
        }

        private void StartScope()
        {
            if (runningTask == null)
            {
                try
                {
                    // Create a new task
                    myTask = new Task();

                    // Create a virtual channel
                    myTask.AIChannels.CreateVoltageChannel(comboBoxChannel.Text, "",
                        (AITerminalConfiguration)(-1), Convert.ToDouble(spinEditMinVal.Value),
                        Convert.ToDouble(spinEditMaxVal.Value), AIVoltageUnits.Volts);

                    // Configure the timing parameters
                    myTask.Timing.ConfigureSampleClock("", Convert.ToDouble(sampleRate),
                        SampleClockActiveEdge.Rising, SampleQuantityMode.ContinuousSamples, Convert.ToInt32(sampleRead));

                    // Verify the Task
                    myTask.Control(TaskAction.Verify);

                    runningTask = myTask;
                    analogInReader = new AnalogMultiChannelReader(myTask.Stream);
                    analogCallback = new AsyncCallback(AnalogInCallback);

                    // Use SynchronizeCallbacks to specify that the object 
                    // marshals callbacks across threads appropriately.
                    analogInReader.SynchronizeCallbacks = true;
                    analogInReader.BeginReadWaveform(Convert.ToInt32(sampleRead),
                        analogCallback, myTask);
                }
                catch (DaqException exception)
                {
                    // Display Errors
                    MessageBox.Show(exception.Message);
                    runningTask = null;
                    myTask.Dispose();
                }
            }
        }

        private void AnalogInCallback(IAsyncResult ar)
        {
            try
            {
                if (runningTask != null && runningTask == ar.AsyncState)
                {
                    // Read the available data from the channels
                    data = analogInReader.EndReadWaveform(ar);

                    double[] values = data[0].GetRawData(0, data[0].SampleCount);

                    waveformGraphScope.Plots[0].PlotWaveformAppend(data[0]);

                    analogInReader.BeginMemoryOptimizedReadWaveform(Convert.ToInt32(sampleRate),
                        analogCallback, myTask, data);

                }
            }
            catch (DaqException exception)
            {
                // Display Errors
                MessageBox.Show(exception.Message);
                runningTask = null;
                myTask.Dispose();
            }

        }

        private void textEditSamplePerCh_EditValueChanged(object sender, EventArgs e)
        {
            waveformGraphScope.Plots[0].ClearData();
            sampleRead = Convert.ToInt32(textEditSamplePerCh.EditValue);
        }

        private void textEditRate_EditValueChanged(object sender, EventArgs e)
        {
            waveformGraphScope.Plots[0].ClearData();
            sampleRate = Convert.ToDouble(textEditRate.EditValue);
        }
    }
}
