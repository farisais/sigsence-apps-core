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

using NationalInstruments.UI.WindowsForms;
using NationalInstruments.UI;
using NationalInstruments.Analysis.SignalGeneration;
using NationalInstruments.Analysis.Math;
using NationalInstruments.Analysis.Dsp;
using NationalInstruments.DAQmx;

using DSA_Teaser;
using DSA_Teaser.Class;
using DSA_Teaser.Class.Function.ObjectSequence;

using Sigsence.ApplicationElements;
using Sigsence.ApplicationFunction;

namespace DSA_Teaser
{
    class ObjectFFT: ObjectSequence
    {
        private ObjectSequence OSequence;
        public ObjectFFT(object control)
            : base(control)
        {
            SignalData = new double[200]; //to be specified the size
        }
        public ObjectFFT()
            : base()
        {
            SignalData = new double[200]; //to be specified the size
        }

        protected override void ImplementUserControl()
        {
            PictureBox pictureBox = new PictureBox();
            pictureBox.Image = global::DSA_Teaser.Properties.Resources.FFT_Icon;
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
            //textboxSequence.Location = new System.Drawing.Point(35, 43);
            textboxSequence.Text = controlName;
            textboxSequence.TextAlign = HorizontalAlignment.Center;
            textboxSequence.BorderStyle = BorderStyle.None;
            textboxSequence.ReadOnly = true;
            textboxSequence.BackColor = System.Drawing.Color.White;
            sequenceTextbox = textboxSequence;

            UserControlSignalGenerator UCSignalGenerator = new UserControlSignalGenerator();
            List<Control> UCList = new List<Control>();
            UCList.Add(UCSignalGenerator);
            objectUserControl = UCList;
        }

        public void InitFormFFT()
        {
            FormFFTSourceSelect formFFT = new FormFFTSourceSelect();
            formFFTSource = formFFT;

            for (int i = 0; i < tbControlPanelAssignment.Rows.Count; i++)
            {
                if (((ObjectSignal)tbControlPanelAssignment.Rows[i]["control"]).ControlCategory == ControlCategories.Sequence)
                {
                    if (((ObjectSignal)tbControlPanelAssignment.Rows[i]["control"]).ControlName != this.controlName)
                    {
                        formFFTSource.listBoxControlSource.Items.Add(((ObjectSequence)tbControlPanelAssignment.Rows[i]["control"]).ControlName);
                    }
                }
            }
            formFFTSource.comboBoxEditWindow.SelectedIndex = 0;
            formFFTSource.comboBoxEditWindow.Select();
            formFFTSource.comboBoxEditOutput.SelectedIndex = 0;
            formFFTSource.comboBoxEditOutput.Select();

            formFFTSource.FormClosing += new FormClosingEventHandler(formFFTSource_FormClosing);
            formFFTSource.ShowDialog();
        }

        void formFFTSource_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (formFFTSource.isOKButton)
            {
                string selectedObject = formFFTSource.listBoxControlSource.SelectedValue.ToString();
                for (int i = 0; i < tbControlPanelAssignment.Rows.Count; i++)
                {
                    if (((ObjectSignal)tbControlPanelAssignment.Rows[i]["control"]).ControlName == selectedObject)
                    {
                        OSequence = (ObjectSequence)tbControlPanelAssignment.Rows[i]["control"];
                        break;
                    }
                }
                windowType = GetSelectedWindow((ScaledWindowType)Enum.Parse(typeof(ScaledWindowType), formFFTSource.comboBoxEditWindow.SelectedItem.ToString()));
                outputType = formFFTSource.comboBoxEditOutput.SelectedText;
                SequenceSource = OSequence;
            }
        }

        protected override void Control_DoubleClick(object sender, EventArgs e)
        {
            FormFFTSourceSelect formFFT = new FormFFTSourceSelect();
            formFFTSource = formFFT;

            for (int i = 0; i < tbControlPanelAssignment.Rows.Count; i++)
            {
                if (((ObjectSignal)tbControlPanelAssignment.Rows[i]["control"]).ControlCategory == ControlCategories.Sequence)
                {
                    if (((ObjectSignal)tbControlPanelAssignment.Rows[i]["control"]).ControlName != this.controlName)
                    {
                        formFFTSource.listBoxControlSource.Items.Add(((ObjectSequence)tbControlPanelAssignment.Rows[i]["control"]).ControlName);
                    }
                }
            }

            formFFTSource.comboBoxEditWindow.SelectedItem = windowType.WindowType.ToString();
            formFFTSource.comboBoxEditWindow.Select();

            formFFTSource.comboBoxEditOutput.SelectedItem = outputType;
            formFFTSource.comboBoxEditOutput.Select();

            formFFTSource.listBoxControlSource.SelectedValue = SequenceSource.ControlName;

            formFFTSource.FormClosing += new FormClosingEventHandler(formFFTSource_FormClosing);
            formFFTSource.Show();
        }

        public ScaledWindow GetSelectedWindow(ScaledWindowType scaledWindowSelected)
        {
            switch (scaledWindowSelected)
            {
                case ScaledWindowType.Blackman:
                    return ScaledWindow.CreateBlackmanWindow();
                case ScaledWindowType.BlackmanHarris:
                    return ScaledWindow.CreateBlackmanHarrisWindow();
                case ScaledWindowType.BlackmanHarris4Term:
                    return ScaledWindow.CreateBlackmanHarris4TermWindow();
                case ScaledWindowType.BlackmanHarris7Term:
                    return ScaledWindow.CreateBlackmanHarris7TermWindow();
                case ScaledWindowType.BlackmanNuttall:
                    return ScaledWindow.CreateBlackmanNuttallWindow();
                case ScaledWindowType.DolphChebyshev:
                    return ScaledWindow.CreateDolphChebyshevWindow();
                case ScaledWindowType.ExactBlackman:
                    return ScaledWindow.CreateExactBlackmanWindow();
                case ScaledWindowType.FlatTop:
                    return ScaledWindow.CreateFlatTopWindow();
                case ScaledWindowType.Gaussian:
                    return ScaledWindow.CreateGaussianWindow();
                case ScaledWindowType.Hamming:
                    return ScaledWindow.CreateHammingWindow();
                case ScaledWindowType.Hanning:
                    return ScaledWindow.CreateHanningWindow();
                case ScaledWindowType.Kaiser:
                    return ScaledWindow.CreateKaiserWindow();
                case ScaledWindowType.LowSidelobe:
                    return ScaledWindow.CreateLowSideLobeWindow();
                case ScaledWindowType.Rectangular:
                    return ScaledWindow.CreateRectangularWindow();
                case ScaledWindowType.Triangle:
                    return ScaledWindow.CreateTriangleWindow();
                default:
                    return ScaledWindow.CreateHanningWindow();

            }
        }
        public double GetFrequency(double[] waveform)
        {
            double a = 20;
            return a;
        }

        private FormFFTSourceSelect formFFTSource;
        public FormFFTSourceSelect FormFFTSource
        {
            get
            {
                return formFFTSource;
            }
            set
            {
                formFFTSource = value;
            }
        }

        private ScaledWindow windowType;
        public ScaledWindow WindowType
        {
            get
            {
                return windowType;
            }
            set
            {
                windowType = value;
            }
        }

        private string outputType;
        public string OutputType
        {
            get
            {
                return outputType;
            }
            set
            {
                outputType = value;
            }
        }

        private double[] signalSourceData;
        public double[] SignalSourceData
        {
            get
            {
                return signalSourceData;
            }
            set
            {
                signalSourceData = value;
                ProcessingSignal();
            }
        }

        private void ProcessingSignal()
        {
            double[] tempData = EnvironmentFunction.DeepCopy<double[]>(signalSourceData);
            windowType.Apply(tempData);
            Transforms.PowerSpectrum(tempData);
            Int32 Size = Convert.ToInt32(tempData.Count() / 2);
            double[] halfValues = new Double[Size];
            
            for (int x = 0; x < Size; x++)
            {
                halfValues[x] = tempData[x];

                if (outputType == "DB")
                {
                    halfValues[x] = 20 * Math.Log10(halfValues[x]); //User chose dB
                }
            }
            SignalData = halfValues;
            byte[] arrayByte = new byte[SignalData.Count() * 4];
            if (isRecording)
            {
                //int iter = 0;
                //for (int i = 0; i < signalData.Count(); i++)
                //{
                //    byte[] temp = BitConverter.GetBytes(Convert.ToInt32(signalData[i]));
                //    for (int j = 0; j < 4; j++)
                //    {
                //        arrayByte[iter] = temp[j];
                //        iter++;
                //    }
                //}

                //((ObjectRecordWAV)obRecord).WAVWriter.WriteData(arrayByte, 0, arrayByte.Length);
                for (int i = 0; i < SignalData.Length; i++)
                {
                    ((ObjectRecordWAV)obRecord).WAVWriter.WriteSample((float)SignalData[i]);
                }
            }
            //OnDataChange(this, new OSequenceDataUpdateEventArgs(signalData));
        }

        protected override void AssignSequence()
        {
            SignalSourceData = EnvironmentFunction.DeepCopy<double[]>(SequenceSource.SignalData);
            SequenceSource.UpdateData += new DataUpdate(SequenceSource_UpdateData);
        }

        void SequenceSource_UpdateData(object sender, OSequenceDataUpdateEventArgs e)
        {
            SignalSourceData = e.DataUpdate;
        }
    }
}
