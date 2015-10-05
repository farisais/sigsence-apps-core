using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;

using DevExpress.XtraVerticalGrid;
using DevExpress.XtraVerticalGrid.Rows;
using DevExpress.XtraEditors.Repository;

using NationalInstruments.Analysis.SignalGeneration;
using NationalInstruments.Analysis.Math;
using NationalInstruments.Analysis.Dsp;

using Sigsence.ApplicationElements;
using Sigsence.ApplicationFunction;

namespace DSA_Teaser
{
    class ObjectSignalGenerator: ObjectSequence
    {
        public ObjectSignalGenerator(object control)
            : base(control)
        {
            SignalData = CreateSignal();
            ImplementUserControl();
        }

        public ObjectSignalGenerator(object control, Form1 parentForm, double _frequency, double _amplitude, double _phase, string _signalType)
            : base(control)
        {
            SignalData = CreateSignal();
            ImplementUserControl();
        }

        public ObjectSignalGenerator(double _frequency, double _amplitude, double _phase, int _sampleSize, int _samplingRate ,string _signalType)
        {

            frequency = _frequency;
            amplitude = _amplitude;
            phase = _phase;
            sampleSize = _sampleSize;
            samplingRate = _samplingRate;
            signalType = _signalType;

            SignalData = CreateSignal();
        }

        public ObjectSignalGenerator()
        {
            SignalData = CreateSignal();
        }

        protected override void ImplementUserControl()
        {
            PictureBox pictureBox = new PictureBox();
            pictureBox.Image = global::DSA_Teaser.Properties.Resources.Icon_Signal_Generator;
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

        //protected override void vGridControlProperties_FocusedRowChanged(object sender, DevExpress.XtraVerticalGrid.Events.FocusedRowChangedEventArgs e)
        //{
        //    base.vGridControlProperties_FocusedRowChanged(sender, e);

        //    if (((ObjectSignal)parentUserControl.tbControlPanelAssignment.Rows[parentUserControl.GetSelectedControlIndex()]["control"]).ControlName == this.controlName)
        //    {
        //        if (e.OldRow != null && e.OldRow.Index >= 0)
        //        {
        //            string caption = e.OldRow.Properties.Caption;
        //            UpdateSignalData(caption);
        //            UpdateTextBox(caption);
        //        }
        //    }
        //}

        //protected override void vGridControlProperties_EditorKeyDown(object sender, KeyEventArgs e)
        //{
        //    base.vGridControlProperties_EditorKeyDown(sender, e);

        //    if (((ObjectSignal)parentUserControl.tbControlPanelAssignment.Rows[parentUserControl.GetSelectedControlIndex()]["control"]).ControlName == this.controlName)
        //    {
        //        string caption = parentWindow.vGridControlProperties.FocusedRow.Properties.Caption;
        //        if (e.KeyData == Keys.Enter)
        //        {
        //            UpdateSignalData(caption);
        //            UpdateTextBox(caption);
        //        }
        //    }
        //}

        private void UpdateTextBox(string caption)
        {
            if (caption == "Name")
            {
                sequenceTextbox.Text = ControlName;
            }
        }

        public override void UpdateFromGridCategory(string caption)
        {
            switch (caption)
            {
                case "SignalType":
                    signalType = VGridMainForm.Rows["categoryRowData"].ChildRows["SignalType"].Properties.Value.ToString();
                    //OnDataChange(this, new OSequenceDataUpdateEventArgs(CreateSignal()));
                    CreateSignal();
                    break;
                case "Frequency":
                    frequency = Convert.ToDouble(VGridMainForm.Rows["categoryRowData"].ChildRows["Frequency"].Properties.Value);
                    //OnDataChange(this, new OSequenceDataUpdateEventArgs(CreateSignal()));
                    CreateSignal();
                    break;
                case "Amplitude":
                    amplitude = Convert.ToDouble(VGridMainForm.Rows["categoryRowData"].ChildRows["Amplitude"].Properties.Value);
                    //OnDataChange(this, new OSequenceDataUpdateEventArgs(CreateSignal()));
                    CreateSignal();
                    break;
                case "SampleSize":
                    sampleSize = Convert.ToInt32(VGridMainForm.Rows["categoryRowData"].ChildRows["SampleSize"].Properties.Value);
                    //OnDataChange(this, new OSequenceDataUpdateEventArgs(CreateSignal()));
                    CreateSignal();
                    break;
                case "SamplingRate":
                    samplingRate = Convert.ToInt32(VGridMainForm.Rows["categoryRowData"].ChildRows["SamplingRate"].Properties.Value);
                    //OnDataChange(this, new OSequenceDataUpdateEventArgs(CreateSignal()));
                    CreateSignal();
                    break;
                case "Phase":
                    phase = Convert.ToDouble(VGridMainForm.Rows["categoryRowData"].ChildRows["Phase"].Properties.Value);
                    //OnDataChange(this, new OSequenceDataUpdateEventArgs(CreateSignal()));
                    CreateSignal();
                    break;
                case "DutyCycle":
                    dutyCycle = Convert.ToDouble(VGridMainForm.Rows["categoryRowData"].ChildRows["DutyCycle"].Properties.Value);
                    //OnDataChange(this, new OSequenceDataUpdateEventArgs(CreateSignal()));
                    CreateSignal();
                    break;
            }
        }

        public override void AssignCustomPropertiesPanel()
        {
            base.AssignCustomPropertiesPanel();

            AddComboBoxRowToVGrid(VGridCategoryRow.categoryRowData.ToString(), "SignalType", "Signal Type", signalType, 
                new object[] { "Sine", "Noise", "Square", "Saw" });
            AddEditorRowToVGrid(VGridCategoryRow.categoryRowData.ToString(), "Amplitude", "Amplitude", amplitude);
            AddEditorRowToVGrid(VGridCategoryRow.categoryRowData.ToString(), "Frequency", "Frequency", frequency);
            AddEditorRowToVGrid(VGridCategoryRow.categoryRowData.ToString(), "Phase", "Phase", phase);
            AddEditorRowToVGrid(VGridCategoryRow.categoryRowData.ToString(), "SampleSize", "Sample Size", sampleSize);
            AddEditorRowToVGrid(VGridCategoryRow.categoryRowData.ToString(), "SamplingRate", "Sampling Rate", samplingRate);
            AddEditorRowToVGrid(VGridCategoryRow.categoryRowData.ToString(), "DutyCycle", "Duty Cycle", dutyCycle);
        }

        protected override void Control_Click(object sender, EventArgs e)
        {
            base.Control_Click(sender, e);
            //AssignCustomPropertiesPanel();
        }

        private string signalType;
        public string SignalType
        {
            get
            {
                return signalType;
            }
            set
            {
                signalType = value;
            }
        }

        private double frequency;
        public double Frequency
        {
            get
            {
                return frequency;
            }
            set
            {
                frequency = value;
            }
        }

        private double amplitude;
        public double Amplitude
        {
            get
            {
                return amplitude;
            }
            set
            {
                amplitude = value;
            }
        }

        private double phase;
        public double Phase
        {
            get
            {
                return phase;
            }
            set
            {
                phase = value;
            }
        }

        private int sampleSize;
        public int SampleSize
        {
            get
            {
                return sampleSize;
            }
            set
            {
                sampleSize = value;
            }
        }

        private int samplingRate;
        public int SamplingRate
        {
            get
            {
                return samplingRate;
            }
            set
            {
                samplingRate = value;
            }
        }

        private System.Windows.Forms.Timer simulationTimer;
        public System.Windows.Forms.Timer SimulationTimer
        {
            get
            {
                return simulationTimer;
            }
            set
            {
                simulationTimer = value;
            }
        }

        private double dutyCycle;
        public double DutyCycle
        {
            get
            {
                return dutyCycle;
            }
            set
            {
                dutyCycle = value;
            }
        }

        //public List<ObjectIndicator> IndicatorCollection;
        //private List<ObjectIndicator> indicatorCollection
        //{
        //    get
        //    {
        //        return indicatorCollection;
        //    }
        //    set
        //    {
        //        indicatorCollection = value;
        //        AssignTaskToIndicator();
        //    }
        //}

        ///// <summary>
        ///// Assign signal generated to indicator collection within this class
        ///// </summary>
        //private void AssignTaskToIndicator()
        //{
            
        //}

        public void StartSimulation(int acqInterval)
        {
            simulationTickInterval = acqInterval;
            simulationTimer.Interval = simulationTickInterval;

            simulationTimer.Tick += new EventHandler(simulationTimer_Tick);
            simulationTimer.Start();
        }

        protected virtual void simulationTimer_Tick(object sender, EventArgs e)
        {

        }

        private double[] CreateSignal()
        {

            //Try catch untuk semua parameter yang akan digunakan (freq,amp,phase,sampleSize,samplingRate)
            double[] signal = new double[sampleSize];
            switch (signalType)
            {
                case "Sine":
                    SineSignal _sineSignal = new SineSignal(frequency, amplitude, phase);
                    signal = _sineSignal.Generate(samplingRate, sampleSize);
                    break;
                case "Noise":
                    WhiteNoiseSignal _whiteNoise = new WhiteNoiseSignal(amplitude, DateTime.Now.Second);
                    signal = _whiteNoise.Generate(samplingRate, sampleSize);
                    break;
                case "Square":
                    SquareSignal _squareSignal = new SquareSignal(frequency, amplitude, phase, dutyCycle);
                    signal = _squareSignal.Generate(samplingRate, sampleSize);
                    break;
                case "Saw":
                    SawtoothSignal _sawSignal = new SawtoothSignal(frequency, amplitude, phase);
                    signal = _sawSignal.Generate(samplingRate, sampleSize);
                    break;
            }

            //SignalGenerator generator = new SignalGenerator(
            SignalData = signal;
            return signal;
        }
        private int simulationTickInterval;
        public int SimulationTickInterval
        {
            get
            {
                return simulationTickInterval;
            }
            set
            {
                simulationTickInterval = value;
            }
        }
    }
}
