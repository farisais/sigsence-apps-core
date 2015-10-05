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

using LibUsbDotNet;
using LibUsbDotNet.Descriptors;
using LibUsbDotNet.Info;
using LibUsbDotNet.Main;
using LibUsbDotNet.LudnMonoLibUsb;

using DSA_Teaser;
using DSA_Teaser.Class;
using DSA_Teaser.Class.Function.ObjectSequence;

using Sigsence.ApplicationElements;
using Sigsence.ApplicationFunction;

namespace DSA_Teaser
{
    class ObjectDAQ: ObjectSequence
    {
        private delegate void OnDataReceivedDelegate(object sender, EndpointDataEventArgs e);
        private int indexSignalData = 0;
        public ObjectDAQ(object control)
            : base(control)
        {
            ImplementUserControl();
        }
        public ObjectDAQ()
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
            
            UserControlSignalGenerator UCSignalGenerator = new UserControlSignalGenerator();
            List<Control> UCList = new List<Control>();
            UCList.Add(UCSignalGenerator);
            objectUserControl = UCList;

            FormDeviceSelector fDAQSelector = new FormDeviceSelector();
            formDAQSelector = fDAQSelector;
            formDAQSelector.textEditDataBuffer.Text = "200";

            formDAQSelector.FormClosing += new FormClosingEventHandler(formDAQSelector_FormClosing);
            formDAQSelector.Show();
        }

        void formDAQSelector_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!formDAQSelector.isDeviceSelected)
            {
                sequenceTextbox.BackColor = Color.Red;
            }
            else
            {
                sequenceTextbox.BackColor = System.Drawing.SystemColors.Control;
                usbDeviceReg = formDAQSelector.usbDevice;
                StartAcquisition();
            }
            dataBuffer = Convert.ToInt32(formDAQSelector.textEditDataBuffer.Text);
            SignalData = new double[dataBuffer]; //to be specified the size
        }

        protected override void Control_DoubleClick(object sender, EventArgs e)
        {
            FormDeviceSelector fDAQSelector = new FormDeviceSelector();
            formDAQSelector = fDAQSelector;
            formDAQSelector.textEditDataBuffer.Text = dataBuffer.ToString();
            
            formDAQSelector.FormClosing += new FormClosingEventHandler(formDAQSelector_FormClosing);
            formDAQSelector.Show();
        }

        private FormDeviceSelector formDAQSelector;
        public FormDeviceSelector FormDAQSelector
        {
            get
            {
                return formDAQSelector;
            }
            set
            {
                formDAQSelector = value;
            }
        }

        private UsbRegistry usbDeviceReg;
        public UsbRegistry USBDeviceReg
        {
            get
            {
                return usbDeviceReg;
            }
            set
            {
                usbDeviceReg = value;
            }
        }

        private UsbDevice usbDevice;
        public UsbDevice USBDevice
        {
            get
            {
                return usbDevice;
            }
            set
            {
                usbDevice = value;
            }
        }

        private UsbEndpointReader usbReader;
        public UsbEndpointReader USBReader
        {
            get
            {
                return usbReader;
            }
            set
            {
                usbReader = value;
            }
        }

        private Int32 dataBuffer;
        public Int32 DataBuffer
        {
            get
            {
                return dataBuffer;
            }
            set
            {
                dataBuffer = value;
            }
        }

        public void StartAcquisition()
        {
            if (usbDeviceReg.Open(out usbDevice))
            {
                IUsbDevice wholeUsbDevice = usbDevice as IUsbDevice;
                if (!ReferenceEquals(wholeUsbDevice, null))
                {
                    // This is a "whole" USB device. Before it can be used, 
                    // the desired configuration and interface must be selected.

                    // Select config #1
                    wholeUsbDevice.SetConfiguration(1);

                    // Claim interface #0.
                    wholeUsbDevice.ClaimInterface(0);
                }

                byte epNum = byte.Parse("1");
                usbReader = usbDevice.OpenEndpointReader((ReadEndpointID)(epNum | 0x80));
                usbReader.DataReceived += USB_DataReceived;
                usbReader.Flush();
                usbReader.DataReceivedEnabled = true;
            }
            else
            {
                MessageBox.Show("Failed to open USB device.", "USB Connection Error", MessageBoxButtons.OK);
            }
        }

        private void USB_DataReceived(object sender, EndpointDataEventArgs e) 
        {

            Int16 valueTemp = e.Buffer[0];
            double value = ((double)valueTemp / 613) * 3.6;
            if (indexSignalData > (dataBuffer - 1))
            {
                for (int i = 0; i < (dataBuffer - 1); i++)
                {
                    SignalData[i] = SignalData[i + 1];
                }
                SignalData[(dataBuffer - 1)] = value;
            }
            else
            {
                SignalData[indexSignalData] = value; 
                indexSignalData++;
            }
            //OnDataChange(this, new OSequenceDataUpdateEventArgs(signalData));
        }

        private void OnDataReceived(object sender, EndpointDataEventArgs e)
        {
            
        }
        int GetSignalDataIndex(double[] sData)
        {
            int result = -1;
            for (int i = 0; i < sData.Count(); i++)
            {
                if (sData[i] == null)
                {
                    result = i;
                    break;
                }
                result = i;
            }
            return result;
        }
        private void ReconstructSignalData()
        {

        }
    }
}
