using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Data;
using System.Xml;

using NationalInstruments;
using NationalInstruments.UI.WindowsForms;
using NationalInstruments.UI;
using NationalInstruments.Analysis.SignalGeneration;
using NationalInstruments.Analysis.Math;
using NationalInstruments.Analysis.Dsp;
using NationalInstruments.DAQmx;
using NationalInstruments.Tdms;

using AviFile;
using wavfile;
using Un4seen.Bass.AddOn;
using Un4seen.Bass.Misc;
using Un4seen.BassWasapi;

using NAudio;
using NAudio.Wave;

using Sigsence.ApplicationElements;

using Sigsence.ApplicationElements;
using Sigsence.ApplicationFunction;


namespace DSA_Teaser
{
    class ObjectPlayerWAV: ObjectPlayer
    {
        public ObjectPlayerWAV(string _fileName, DateTime _recordTimerStart, DateTime _recordTimerEnd)
            : base(_fileName, _recordTimerStart, _recordTimerEnd)
        {
            InitComponent();
        }

        private void InitComponent()
        {
            wavReader = new WaveFileReader(fileName);
            sampleRate = wavReader.WaveFormat.SampleRate;
            bitsPerSample = wavReader.WaveFormat.BitsPerSample;

            
        }

        protected WaveFileReader wavReader;
        public WaveFileReader WAVReader
        {
            get
            {
                return wavReader;
            }
            set
            {
                wavReader = value;
            }
        }

        protected TdmsFile tdmsFile;
        public TdmsFile TDMSFile
        {
            get
            {
                return tdmsFile;
            }
            set
            {
                tdmsFile = value;
            }
        }
    }
}
