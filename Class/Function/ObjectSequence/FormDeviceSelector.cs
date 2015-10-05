using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using LibUsbDotNet;
using LibUsbDotNet.Descriptors;
using LibUsbDotNet.Info;
using LibUsbDotNet.Main;
using LibUsbDotNet.LudnMonoLibUsb;

namespace DSA_Teaser.Class.Function.ObjectSequence
{
    public partial class FormDeviceSelector : Form
    {
        private UsbRegDeviceList mDevList;
        private Int32 cursorPos;
        public bool isDeviceSelected = false;
        public UsbRegistry usbDevice;
        private DataTable tbDeviceList;

        public FormDeviceSelector()
        {
            InitializeComponent();
            cursorPos = 0;
            tbDeviceList = new DataTable();
            tbDeviceList.Columns.Add("index", typeof(int));
            tbDeviceList.Columns.Add("device", typeof(UsbRegistry));
        }

        private void refreshDeviceList()
        {
            mDevList = UsbDevice.AllDevices;
            toolStripStatusLabelStatusFound.Text = mDevList.Count.ToString();
            comboBoxEditDeviceList.Properties.Items.Clear();
            int index = 0;
            foreach (UsbRegistry device in mDevList)
            {
                string sAdd = string.Format("Vid:0x{0:X4} Pid:0x{1:X4} (rev:{2}) - {3}",
                                            device.Vid,
                                            device.Pid,
                                            (ushort)device.Rev,
                                            device[SPDRP.DeviceDesc]);

                comboBoxEditDeviceList.Properties.Items.Add(sAdd);

                DataRow rowadd = tbDeviceList.NewRow();
                rowadd["index"] = index;
                rowadd["device"] = device;
                tbDeviceList.Rows.Add(rowadd);
                index++;
            }

            if (mDevList.Count == 0)
            {
                toolStripStatusLabelStatusFound.ForeColor = Color.Red;
                WriteTextRichTextBox("Could not find DAQ Device", Color.Red);
                WriteTextRichTextBox(System.Environment.NewLine, Color.Black);
                WriteTextRichTextBox("Please check the connection", Color.Black);
                WriteTextRichTextBox(System.Environment.NewLine, Color.Black);
                WriteTextRichTextBox(System.Environment.NewLine, Color.Black);
            }
            else
            {
                toolStripStatusLabel1.ForeColor = Color.FromKnownColor(KnownColor.ControlText);
                isDeviceSelected = true;
            }
        }

        private void WriteTextRichTextBox(string message, Color fcolor)
        {
            int start = richTextBoxCommand.TextLength;
            richTextBoxCommand.AppendText(message);
            richTextBoxCommand.SelectionStart = start;
            richTextBoxCommand.SelectionLength = message.Length;
            richTextBoxCommand.SelectionColor = fcolor;

            richTextBoxCommand.ScrollToCaret();
            
        }

        private void FormDeviceSelector_Load(object sender, EventArgs e)
        {
            refreshDeviceList();
        }

        private void simpleButtonRefreshList_Click(object sender, EventArgs e)
        {
            refreshDeviceList();
        }

        private void simpleButtonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            if (comboBoxEditDeviceList.SelectedIndex >= 0)
            {
                usbDevice = (UsbRegistry)tbDeviceList.Rows[comboBoxEditDeviceList.SelectedIndex]["device"];
                isDeviceSelected = true;
            }
            this.Close();
        }
    }
}
