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

using NationalInstruments;
using NationalInstruments.UI.WindowsForms;
using NationalInstruments.UI;
using NationalInstruments.Analysis.SignalGeneration;
using NationalInstruments.Analysis.Math;
using NationalInstruments.Analysis.Dsp;
using NationalInstruments.DAQmx;

using Sigsence.ApplicationElements;

namespace DSA_Teaser
{
    class ObjectDeviceNI: ObjectSequence
    {
        private AnalogMultiChannelReader analogInReader;
        private Task myTask;
        private Task runningTask;
        private AsyncCallback analogCallback;

        private AnalogWaveform<double>[] data;
        private FormNIDeviceSetup formNIDevice;

        public ObjectDeviceNI(object control)
            : base(control)
        {
            ImplementUserControl();
        }

        public ObjectDeviceNI()
        {
            ImplementUserControl();
        }

        private void ImplementUserControl()
        {
            PictureBox pictureBox = new PictureBox();
            pictureBox.Image = global::DSA_Teaser.Properties.Resources.DAQ_Icon;
            pictureBox.Location = new System.Drawing.Point(65, 67);
            pictureBox.Name = "pictureBox1";
            pictureBox.Size = new System.Drawing.Size(43, 43);
            pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            pictureBox.TabIndex = 0;
            pictureBox.TabStop = false;
            pictureBox.Padding = new Padding(3);
            controlHandle = pictureBox;

            InitControl(controlHandle);

            //Label for the sequence icon
            TextBox textboxSequence = new TextBox();
            textboxSequence.Multiline = true;
            textboxSequence.Size = new System.Drawing.Size(103, 14);
            textboxSequence.Location = new System.Drawing.Point(35, 110);
            textboxSequence.Text = controlName;
            textboxSequence.TextAlign = HorizontalAlignment.Center;
            textboxSequence.BorderStyle = BorderStyle.None;
            textboxSequence.ReadOnly = true;
            textboxSequence.BackColor = System.Drawing.Color.White;
            sequenceTextbox = textboxSequence;
        }

        void formNIDevice_FormClosing(object sender, FormClosingEventArgs e)
        {
            sampleRate = Convert.ToInt32(formNIDevice.textEditRate.EditValue);
            sampleRead = Convert.ToInt32(formNIDevice.textEditSamplePerCh.EditValue);
            channelName = formNIDevice.comboBoxChannel.Text;
            minimumValue = Convert.ToDouble(formNIDevice.spinEditMinVal.Value);
            maximumValue = Convert.ToDouble(formNIDevice.spinEditMaxVal.Value);
            bitsPerSample = formNIDevice.resolution;
            StartAcquisition();
        }

        public void InitForm()
        {
            formNIDevice = new FormNIDeviceSetup();
            formNIDevice.FormClosing += new FormClosingEventHandler(formNIDevice_FormClosing);
            formNIDevice.ShowDialog();
        }

        private void StartAcquisition()
        {
            if (runningTask == null)
            {
                try
                {
                    // Create a new task
                    myTask = new Task();

                    // Create a virtual channel
                    myTask.AIChannels.CreateVoltageChannel(channelName, "",
                        (AITerminalConfiguration)(-1), Convert.ToDouble(minimumValue),
                        Convert.ToDouble(maximumValue), AIVoltageUnits.Volts);

                    // Configure the timing parameters
                    myTask.Timing.ConfigureSampleClock("", Convert.ToDouble(sampleRate),
                        SampleClockActiveEdge.Rising, SampleQuantityMode.ContinuousSamples, 1000);

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

                    bitsPerSample = myTask.AIChannels[0].Resolution;
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

        private void StopAcquisition()
        {

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
                    float[] dataFloat = new float[data[0].SampleCount];
                    SignalData = values;
                    byte[] arrayByte = new byte[SignalData.Count() * 4];

                    for (int i = 0; i < SignalData.Length; i++)
                    {
                        dataFloat[i] = Convert.ToSingle(SignalData[i]);
                    }
                    byte[] dataByte = myTask.Stream.ReadRaw(data[0].SampleCount);

                    float[] dataFloat2 = new float[data[0].SampleCount];

                    for (int i = 0; i < SignalData.Length; i++)
                    {
                        dataFloat2[i] = BitConverter.ToSingle(dataByte, i * 4);
                    }
                    //Record Data
                    if (isRecording)
                    {
                        int iter = 0;
                        for (int i = 0; i < SignalData.Count(); i++)
                        {
                            byte[] temp = BitConverter.GetBytes(dataFloat[i]);
                            for (int j = 0; j < 4; j++)
                            {
                                arrayByte[iter] = temp[j];
                                iter++;
                            }
                        }
                        
                        //byte[] arrayByte = EnvironmentFunction.ConvertDoubleToByteArray(signalData, Convert.ToInt16(bitsPerSample));
                        ((ObjectRecordWAV)obRecord).WAVWriter.Write(arrayByte, 0, arrayByte.Length);
                        //((ObjectRecordWAV)obRecord).WAVWriter.WriteSamples(dataFloat, 0, dataFloat.Length);
                        //for (int i = 0; i < signalData.Length; i++)
                        //{
                        //    ((ObjectRecordWAV)obRecord).WAVWriter.WriteSample((float)signalData[i]);
                        //}
                    }

                    //OnDataChange(this, new OSequenceDataUpdateEventArgs(values));

                    analogInReader.BeginMemoryOptimizedReadWaveform(Convert.ToInt32(sampleRead),
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

        private Double minimumValue;
        public Double MinimumValue
        {
            get
            {
                return minimumValue;
            }
            set
            {
                minimumValue = value;
            }
        }

        private Double maximumValue;
        public Double MaximumValue
        {
            get
            {
                return maximumValue;
            }
            set
            {
                maximumValue = value;
            }
        }

        private Int32 sampleRate;
        public Int32 SampleRate
        {
            get
            {
                return sampleRate;
            }
            set
            {
                sampleRate = value;
            }
        }

        private Int32 sampleRead;
        public Int32 SampleRead
        {
            get
            {
                return sampleRead;
            }
            set
            {
                sampleRead = value;
            }
        }

        private double bitsPerSample;
        public double BitsPerSample
        {
            get
            {
                return bitsPerSample;
            }
            set
            {
                bitsPerSample = value;
            }
        }

        private string channelName;
        public string ChannelName
        {
            get
            {
                return channelName;
            }
            set
            {
                channelName = value;
            }
        }

        public override void Dispose()
        {
            //if (myTask != null)
            //    //        {
            //    //            runningTask = null;
            //    //            myTask.Dispose();
            //    //        }
            base.Dispose();
        }
    }
}
