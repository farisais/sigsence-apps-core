using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Data;
using System.Xml;

using AviFile;
using wavfile;

using Sigsence.ApplicationElements;

namespace DSA_Teaser
{
    public class ObjectRecordAVI: ObjectRecording
    {
        public ObjectRecordAVI(string filenameAvi, string filenameTempAvi, string sequenceName, string dataType)
            : base(filenameAvi, filenameTempAvi, sequenceName, dataType) 
        {
            CreateProjectRecording();
        }

        #region Buffer Compression Parameter
        //
        private ImageCodecInfo jpgCodecInfo;
        private System.Drawing.Imaging.Encoder myEncoder;
        private EncoderParameters encParameters;
        private EncoderParameter encParameter;
        //
        #endregion


        /// <summary>
        /// Get encoder parameter System.Drawing.Imaging
        /// </summary>
        /// <param name="format">Format file</param>
        /// <returns></returns>
        private ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }

        protected override void CreateProjectRecording()
        {
            aviManager = new AviManager(recordFileName, false);
            aviCompressOption = new Avi.AVICOMPRESSOPTIONS();
            //Xvid MPEG-4 Compression
            aviCompressOption.cbParms = 3532;
            aviCompressOption.fccHandler = 1684633208;

            // setting up initial buffer parameter
            tempFileStream = new FileStream(tempFile, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            jpgCodecInfo = GetEncoder(ImageFormat.Jpeg);
            myEncoder = System.Drawing.Imaging.Encoder.Quality;
            encParameters = new EncoderParameters(1);
            encParameter = new EncoderParameter(myEncoder, 50L);
            encParameters.Param[0] = encParameter;
            // end setting up initial buffer parameter
        }

        //child-avi
        public void InitTempRecordAvi(Bitmap bmp, int AviFrameRate)
        {
            //Get the initial Length of the data
            MemoryStream mo = new MemoryStream();
            bmp.Save(mo, jpgCodecInfo, encParameters);
            byte[] bmpData = mo.GetBuffer();
            bmpLength = bmpData.Length;

            AddBitmapToTempFile(bmp);
            sampleRate = AviFrameRate;
            isSampled = true;
            isStarted = true;
            
        }

        public void TransferTempRecordFile()
        {
            FileStream fStream = File.Open(tempFile, FileMode.Open, FileAccess.Read);
            byte[] bmpData = new byte[bmpLength];
            int bytesRead;
            int count = 0;

            while ((bytesRead = fStream.Read(bmpData, 0, bmpLength)) > 0)
            {
                ImageConverter ImageConvert = new ImageConverter();
                Image img = (Image)ImageConvert.ConvertFrom(bmpData);
                Bitmap bmp = new Bitmap(img);
                if (count == 0)
                {
                    InitRecordingToolsAvi(bmp, sampleRate);
                }
                else
                {
                    vidStream.AddFrame(bmp);
                }
                bmp.Dispose();
                count++;
            }
            fStream.Close();
            File.Delete(tempFile);
            aviManager.Close();
        }

        //child-avi
        public void InitRecordingToolsAvi(Bitmap bmp, int AviFrameRate)
        {
            if (aviManager != null)
            {
                aviManager.AddVideoStream(aviCompressOption, AviFrameRate, bmp);
                vidStream = aviManager.GetOpenStream(0);
            }
        }

        public override void AddBitmapToTempFile(Bitmap bmp)
        {
            if (tempFileStream.CanWrite)
            {
                MemoryStream mo = new MemoryStream();
                bmp.Save(mo, jpgCodecInfo, encParameters);
                byte[] bmpData = mo.GetBuffer();
                tempFileStream.Write(bmpData, 0, bmpData.Length);
                frameCount++;
            }
        }

        //child-avi
        protected List<string> listAviFile;
        public List<string> ListAviFile
        {
            get
            {
                return listAviFile;
            }
            set
            {
                listAviFile = value;
            }
        }

        //child-avi
        protected AviManager aviManager;
        public AviManager AVIManager
        {
            get
            {
                return aviManager;
            }
            set
            {
                aviManager = value;
            }
        }

        //child-avi
        protected VideoStream vidStream;
        public VideoStream VidStream
        {
            get
            {
                return vidStream;
            }
            set
            {
                vidStream = value;
            }
        }

        //child-avi
        protected EditableVideoStream editableVidStream;
        public EditableVideoStream EditableVidStream
        {
            get
            {
                return editableVidStream;
            }
            set
            {
                editableVidStream = value;
            }
        }

        //child-avi
        protected Avi.AVICOMPRESSOPTIONS aviCompressOption;
        public Avi.AVICOMPRESSOPTIONS AviCompressOption
        {
            get
            {
                return aviCompressOption;
            }
            set
            {
                aviCompressOption = value;
            }
        }

        //child-avi
        protected Int32 frameCount;
        public Int32 FrameCount
        {
            get
            {
                return frameCount;
            }
            set
            {
                frameCount = value;
            }
        }
    }
}
