using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Touchless.Vision.Camera;

namespace DSA_Teaser.Class.Function.ObjectIndicator
{
    public partial class FormCameraConfig : Form
    {
        public Camera cameraSelected;
        public bool isOK = false;
        public FormCameraConfig()
        {
            InitializeComponent();

            comboBoxEditCameraList.Properties.Items.Clear();
            foreach (Camera cam in CameraService.AvailableCameras)
            {
                comboBoxEditCameraList.Properties.Items.Add(cam);
            }
            if (comboBoxEditCameraList.Properties.Items.Count > 0)
            {
                comboBoxEditCameraList.SelectedIndex = 0;
                comboBoxEditCameraList.Select();

                cameraSelected = (Camera)comboBoxEditCameraList.SelectedItem;
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //OK
            isOK = true;
            this.Close();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            //Cancel
            this.Close();
        }

        private void checkEditFlipV_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEditFlipV.Checked)
            {
                cameraSelected.FlipVertical = true;
            }
            else
            {
                cameraSelected.FlipVertical = false;
            }
        }

        private void checkEditFlipH_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEditFlipH.Checked)
            {
                cameraSelected.FlipHorizontal = true;
            }
            else
            {
                cameraSelected.FlipHorizontal = false;
            }
        }

        private void comboBoxEditCameraList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxEditCameraList.Properties.Items.Count > 0 && comboBoxEditCameraList.SelectedIndex >= 0)
            {
                cameraSelected = (Camera)comboBoxEditCameraList.SelectedItem;
            }
        }
    }
}
