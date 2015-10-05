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
using System.Diagnostics;
using System.Drawing.Imaging;

using Touchless.Vision.Camera;

using DSA_Teaser;
using DSA_Teaser.Class;
using DSA_Teaser.Class.Function.ObjectIndicator;

using Sigsence.ApplicationElements;

namespace DSA_Teaser
{
    class ObjectVideo: ObjectIndicator
    {
        private CameraFrameSource _frameSource;
        private static Bitmap _latestFrame;
        public Int32 frameCount;
        public List<Bitmap> frameList;
        public int dataArriveCount = 0;
        public ObjectVideo(object control)
            : base(control)
        {

        }

        public ObjectVideo()
            : base()
        {

        }

        ~ObjectVideo()
        {
            thrashOldCamera();
        }

        protected override void ImplementUserControl()
        {
            PictureBox pictureBox1 = new PictureBox();
            pictureBox1.BackColor = System.Drawing.Color.DarkGray;
            pictureBox1.Location = new System.Drawing.Point(47, 30);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new System.Drawing.Size(307, 229);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            pictureBox1.BackgroundImageLayout = ImageLayout.Zoom;
            pictureBox1.BorderStyle = BorderStyle.Fixed3D;

            controlHandle = pictureBox1;
            InitControl(controlHandle);
            initContextMenuStrip();

            frameList = new List<Bitmap>();
        }

        public void OpenFormCamera()
        {
            FormCameraConfig fCamera = new FormCameraConfig();
            formCamera = fCamera;
            formCamera.FormClosing += new FormClosingEventHandler(formCamera_FormClosing);
            formCamera.Show();
        }

        void formCamera_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (formCamera.isOK)
            {
                videoCamera = formCamera.cameraSelected;
                flipVertical = videoCamera.FlipVertical;
                flipHorizontal = videoCamera.FlipHorizontal;
                thrashOldCamera();
                startCapturing();
            }
        }

        public void startCapturing()
        {
            try
            {
                Camera c = videoCamera;
                setFrameSource(new CameraFrameSource(c));
                _frameSource.Camera.CaptureWidth = ((PictureBox)controlHandle).Width;
                _frameSource.Camera.CaptureHeight = ((PictureBox)controlHandle).Height;
                _frameSource.Camera.Fps = 20;
                _frameSource.NewFrame += new Action<Touchless.Vision.Contracts.IFrameSource, Touchless.Vision.Contracts.Frame, double>(_frameSource_NewFrame);
                //((PictureBox)controlHandle).Paint += new PaintEventHandler(ObjectVideo_Paint);
                _frameSource.StartFrameCapture();
                frameCount = 0;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void ObjectVideo_Paint(object sender, PaintEventArgs e)
        {
            if (_latestFrame != null)
            {
                // Draw the latest image from the active camera
                e.Graphics.DrawImage(_latestFrame, 0, 0, _latestFrame.Width, _latestFrame.Height);
            }
        }

        void _frameSource_NewFrame(Touchless.Vision.Contracts.IFrameSource frameSource, Touchless.Vision.Contracts.Frame frame, double fps)
        {
            _latestFrame = frame.Image;
            Bitmap bmp = EnvironmentFunction.CopyBitmap(_latestFrame);
            ((PictureBox)controlHandle).Image = _latestFrame;
            frameCount++;
            //parentWindow.barStaticItemFrameCount.Caption = frameCount.ToString();

            ((PictureBox)controlHandle).Invalidate();
            if (obRecord != null)
            {
                if (dataArriveCount == 0)
                {
                    ((ObjectRecordAVI)obRecord).InitTempRecordAvi(bmp, this.videoCamera.Fps);
                    dataArriveCount++;
                }
                if (isRecording && dataArriveCount > 0)
                {
                    ((ObjectRecordAVI)obRecord).AddBitmapToTempFile(bmp);
                }
                bmp.Dispose();
            }
        }

        private void setValueLabel(string value)
        {

        }

        private void setFrameSource(CameraFrameSource cameraFrameSource)
        {
            if (_frameSource == cameraFrameSource)
                return;

            _frameSource = cameraFrameSource;
        }

        public void thrashOldCamera()
        {
            // Trash the old camera
            if (_frameSource != null)
            {
                _frameSource.NewFrame -= _frameSource_NewFrame;
                _frameSource.Camera.Dispose();
                setFrameSource(null);
                ((PictureBox)controlHandle).Paint -= new PaintEventHandler(ObjectVideo_Paint);
            }
        }

        public ObjectVideo GetControlHandle()
        {
            ObjectVideo OVideo = controlHandle as ObjectVideo;
         
            return OVideo;
        }

        private Camera videoCamera;
        public Camera VideoCamera
        {
            get
            {
                return videoCamera;
            }
            set
            {
                videoCamera = value;
            }
        }

        private FormCameraConfig formCamera;
        public FormCameraConfig FormCamer
        {
            get
            {
                return formCamera;
            }
            set
            {
                formCamera = value;
            }
        }

        private bool flipVertical;
        public bool FlipVertical
        {
            get
            {
                return flipVertical;
            }
            set
            {
                flipVertical = value;
            }
        }

        private bool flipHorizontal;
        public bool FlipHorizontal
        {
            get
            {
                return flipHorizontal;
            }
            set
            {
                flipHorizontal = value;
            }
        }

        public override void Dispose()
        {
            thrashOldCamera();
            base.Dispose();
        }
    }
}
