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
using Un4seen.Bass.AddOn;
using Un4seen.Bass.Misc;
using Un4seen.BassWasapi;

using NationalInstruments;
using NationalInstruments.UI.WindowsForms;
using NationalInstruments.UI;
using NationalInstruments.Analysis.SignalGeneration;
using NationalInstruments.Analysis.Math;
using NationalInstruments.Analysis.Dsp;
using NationalInstruments.DAQmx;
using NationalInstruments.Tdms;

using NAudio;
using NAudio.Wave;

using Sigsence.ApplicationElements;
using Sigsence.ApplicationFunction;

namespace DSA_Teaser
{
    public class ObjectRecordWAV: ObjectRecording
    {
        public ObjectRecordWAV(string filename, string filenameTemp, string sequenceName, string dataType)
            : base(filename, filenameTemp, sequenceName, dataType)
        {
            CreateProjectRecording();
        }

        protected override void CreateProjectRecording()
        {
            wavCreator = new WAVFile();
            tdmsFile = new TdmsFile(recordFileName, new TdmsFileOptions());
        }

        //child-wav
        public void InitRecordingToolsWav()
        {
            //if (wavCreator != null)
            //{
            //    //As default signal save in mono type wav file
            //    wavCreator.Create(recordFileName, false, sampleRate, bitSample, false);
            //    wavCreator.Open(recordFileName, WAVFile.WAVFileMode.READ_WRITE);
            //}

            //wavWriter = new WaveWriter(recordFileName, 2, sampleRate, bitSample, false);
            waveFormat = new NAudio.Wave.WaveFormat(sampleRate, bitSample, 1);
            wavWriter = new WaveFileWriter(recordFileName, waveFormat);
        }

        public void InitRecordingToolTdms(string sequenceName)
        {
            string channelGroupName = sequenceName;
            TdmsChannelGroup channelGroup = new TdmsChannelGroup(channelGroupName);
            tdmsGroupCollection = tdmsFile.GetChannelGroups();
            if (tdmsGroupCollection.Contains(channelGroupName))
            {
                channelGroup = tdmsGroupCollection[channelGroupName];
            }
            else
            {
                tdmsGroupCollection.Add(channelGroup);
            }

            // Set up the channel.
            string dataChannelName = sequenceName + "-channel";
            TdmsChannel dataChannel = new TdmsChannel(dataChannelName, TdmsDataType.Double);
            TdmsChannelCollection channels = channelGroup.GetChannels();
            if (channels.Contains(dataChannelName))
            {
                dataChannel = channels[dataChannelName];
            }
            else
            {
                channels.Add(dataChannel);
            }
        }

        //child-wav
        protected List<string> listWavFile;
        public List<string> ListWavFile
        {
            get
            {
                return listWavFile;
            }
            set
            {
                listWavFile = value;
            }
        }

        //child-wav
        protected WAVFile wavCreator;
        public WAVFile WavCreator
        {
            get
            {
                return wavCreator;
            }
            set
            {
                wavCreator = value;
            }
        }

        protected WaveFileWriter wavWriter;
        public WaveFileWriter WAVWriter
        {
            get
            {
                return wavWriter;
            }
            set
            {
                wavWriter = value;
            }
        }

        protected NAudio.Wave.WaveFormat waveFormat;
        public NAudio.Wave.WaveFormat WaveFormat
        {
            get
            {
                return waveFormat;
            }
            set
            {
                waveFormat = value;
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

        protected TdmsChannelGroupCollection tdmsGroupCollection;
        public TdmsChannelGroupCollection TDMSGroupCollection
        {
            get
            {
                return tdmsGroupCollection;
            }
            set
            {
                tdmsGroupCollection = value;
            }
        }
    }
}
