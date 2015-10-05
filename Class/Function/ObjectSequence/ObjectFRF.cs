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
    class ObjectFRF: ObjectSequence
    {
        public delegate void FRFDataUpdate(object sender, OFRFDataUpdateEventArgs e);
        public event FRFDataUpdate UpdateFRFData;

        public ObjectFRF(object control)
            : base(control)
        {
            ImplementUserControl();
        }
        public ObjectFRF()
        {
            ImplementUserControl();
        }
        private void ImplementUserControl()
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

            objectName = "ObjectFRF";
        }

        FormInitFRF formFRF;

        public void InitFormFRF()
        {
            formFRF = new FormInitFRF();
            
            for (int i = 0; i < tbControlPanelAssignment.Rows.Count; i++)
            {
                if (((ObjectSignal)tbControlPanelAssignment.Rows[i]["control"]).ControlCategory == ControlCategories.Sequence)
                {
                    formFRF.checkedListBoxControlSelectResponse.Items.Add(((ObjectSignal)tbControlPanelAssignment.Rows[i]["control"]).ControlName);
                    formFRF.comboBoxEditSelectStimulus.Properties.Items.Add(((ObjectSignal)tbControlPanelAssignment.Rows[i]["control"]).ControlName);
                }
            }
            formFRF.FormClosing += new FormClosingEventHandler(formFRF_FormClosing);
            formFRF.ShowDialog();
        }

        void formFRF_FormClosing(object sender, FormClosingEventArgs e)
        {
            for (int i = 0; i < tbControlPanelAssignment.Rows.Count; i++)
            {
                if (((ObjectSignal)tbControlPanelAssignment.Rows[i]["control"]).ControlName == formFRF.comboBoxEditSelectStimulus.SelectedText)
                {
                    sequenceSource = (ObjectSequence)tbControlPanelAssignment.Rows[i]["control"];
                    break;
                }
            }

            for (int i = 0; i < tbControlPanelAssignment.Rows.Count; i++)
            {
                //if (((ObjectSignal)parentUserControl.tbControlPanelAssignment.Rows[i]["control"]).ControlName == formFRF.checkedListBoxControlSelectResponse.SelectedText)
                //{
                //    sequenceSource = (ObjectSequence)parentUserControl.tbControlPanelAssignment.Rows[i]["control"];
                //    break;
                //}
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
                //ProcessingSignal();
            }
        }

        private void ProcessingSignal()
        {
            for (int i = 0; i < objectResponse.Count; i++)
            {
                Transforms.NetworkFunctions(stimulus, response, _dt, out crossPowerSpectrumMagnitude, out crossPowerSpectrumPhase, out frequencyResponseMagnitude,
                    out frequencyResponsePhase, out coherence, out impulseResponse, out _df);
            }
            
            //OnDataChange(this, new OSequenceDataUpdateEventArgs(signalData));
            UpdateFRFData(this, new OFRFDataUpdateEventArgs(crossPowerSpectrumMagnitude,crossPowerSpectrumPhase, frequencyResponseMagnitude, 
                frequencyResponsePhase, coherence, impulseResponse, _df, stimulus, response));
        }

        private double[] crossPowerSpectrumMagnitude;
        public double[] CrossPowerSpectrumMagnitude
        {
            get
            {
                return crossPowerSpectrumMagnitude;
            }
            set
            {
                crossPowerSpectrumMagnitude = value;
            }
        }

        private double[] crossPowerSpectrumPhase;
        public double[] CrossPowerSpectrumPhase
        {
            get
            {
                return crossPowerSpectrumPhase;
            }
            set
            {
                crossPowerSpectrumPhase = value;
            }
        }

        private double[] frequencyResponseMagnitude;
        public double[] FrequencyResponseMagnitude
        {
            get
            {
                return frequencyResponseMagnitude;
            }
            set
            {
                frequencyResponseMagnitude = value;
            }
        }

        private double[] frequencyResponsePhase;
        public double[] FrequencyResponsePhase
        {
            get
            {
                return frequencyResponsePhase;
            }
            set
            {
                frequencyResponsePhase = value;
            }
        }
        private double[] coherence;
        public double[] Coherence
        {
            get
            {
                return coherence;
            }
            set
            {
                coherence = value;
            }
        }
        private double[] impulseResponse;
        public double[] ImpulseResponse
        {
            get
            {
                return impulseResponse;
            }
            set
            {
                impulseResponse = value;
            }
        }

        private double _df;
        public double df
        {
            get
            {
                return _df;
            }
            set
            {
                _df = value;
            }
        }

        private double[,] stimulus;
        public double[,] Stimulus
        {
            get
            {
                return stimulus;
            }
            set
            {
                stimulus = value;
            }
        }

        private double[,] response;
        public double[,] Response
        {
            get
            {
                return response;
            }
            set
            {
                response = value;
            }
        }

        private double _dt;
        public double dt
        {
            get
            {
                return _dt;
            }
            set
            {
                _dt = value;
            }
        }

        private List<ObjectSequence> objectResponse;
        public List<ObjectSequence> ObjectResponse
        {
            get
            {
                return objectResponse;
            }
            set
            {
                objectResponse = value;
            }
        }
    }
}
