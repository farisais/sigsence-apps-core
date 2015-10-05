using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Diagnostics;
using Sigsence.ApplicationElements;

namespace DSA_Teaser
{
    class ObjectAudioIn: ObjectSequence, IDisposable
    {
        private WaveInRecorder _recorder;
        private byte[] _recorderBuffer;
        private WaveOutPlayer _player;
        private byte[] _playerBuffer;
        private FifoStream _stream;
        private WaveFormat _waveFormat;
        private AudioFrame _audioFrame;
        public int _audioSamplesPerSecond = 48000;
        public int _audioFrameSize = 8000;
        public byte _audioBitsPerSample = 16;
        public byte _audioChannels = 2;
        private bool _isPlayer = false;
        private bool _isTest = false;
        public ObjectAudioIn(object control)
            : base(control)
        {
            ImplementUserControl();
        }
        public ObjectAudioIn()
        {
            ImplementUserControl();
        }

        private void ImplementUserControl()
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

        public void InitRecording()
        {
            if (WaveNative.waveInGetNumDevs() == 0)
            {
                
            }
            else
            {
                if (_isPlayer == true)
                    _stream = new FifoStream();
                _audioFrame = new AudioFrame(_isTest);
                StartRecording();
            }
        }

        public void StartRecording()
        {
            StopRecording();
            try
            {
                _waveFormat = new WaveFormat(_audioFrameSize, _audioBitsPerSample, _audioChannels);
                _recorder = new WaveInRecorder(0, _waveFormat, _audioFrameSize * 2, 3, new BufferDoneEventHandler(DataArrived));
                if (_isPlayer == true)
                    _player = new WaveOutPlayer(-1, _waveFormat, _audioFrameSize * 2, 3, new BufferFillEventHandler(Filler));
                //textBox1.AppendText(DateTime.Now.ToString() + " : Audio device initialized\r\n");
                //textBox1.AppendText(DateTime.Now.ToString() + " : Audio device polling started\r\n");
                //textBox1.AppendText(DateTime.Now + " : Samples per second = " + _audioSamplesPerSecond.ToString() + "\r\n");
                //textBox1.AppendText(DateTime.Now + " : Frame size = " + _audioFrameSize.ToString() + "\r\n");
                //textBox1.AppendText(DateTime.Now + " : Bits per sample = " + _audioBitsPerSample.ToString() + "\r\n");
                //textBox1.AppendText(DateTime.Now + " : Channels = " + _audioChannels.ToString() + "\r\n");
            }
            catch (Exception ex)
            {
                //textBox1.AppendText(DateTime.Now + " : Audio exception\r\n" + ex.ToString() + "\r\n");
            }
        }

        private void StopRecording()
        {
            if (_recorder != null)
                try
                {
                    _recorder.Dispose();
                }
                finally
                {
                    _recorder = null;
                }
            if (_isPlayer == true)
            {
                if (_player != null)
                    try
                    {
                        _player.Dispose();
                    }
                    finally
                    {
                        _player = null;
                    }
                _stream.Flush(); // clear all pending data
            }
        }

        private void Filler(IntPtr data, int size)
        {
            if (_isPlayer == true)
            {
                if (_playerBuffer == null || _playerBuffer.Length < size)
                    _playerBuffer = new byte[size];
                if (_stream.Length >= size)
                    _stream.Read(_playerBuffer, 0, size);
                else
                    for (int i = 0; i < _playerBuffer.Length; i++)
                        _playerBuffer[i] = 0;
                System.Runtime.InteropServices.Marshal.Copy(_playerBuffer, 0, data, size);
            }
        }

        private void DataArrived(IntPtr data, int size)
        {
            if (_recorderBuffer == null || _recorderBuffer.Length < size)
                _recorderBuffer = new byte[size];
            if (_recorderBuffer != null)
            {
                System.Runtime.InteropServices.Marshal.Copy(data, _recorderBuffer, 0, size);
                if (_isPlayer == true)
                    _stream.Write(_recorderBuffer, 0, _recorderBuffer.Length);
                //_audioFrame.Process(ref _recorderBuffer);


                double[] _waveLeft = new double[_recorderBuffer.Length / 4];
                double[] _waveRight = new double[_recorderBuffer.Length / 4];
                byte[] arrayByte2 = new byte[_recorderBuffer.Length / 2];
                if (_isTest == false)
                {
                    // Split out channels from sample
                    int h = 0;
                    for (int i = 0; i < _recorderBuffer.Length; i += 4)
                    {
                        
                        _waveLeft[h] = (double)BitConverter.ToInt16(_recorderBuffer, i);
                        _waveRight[h] = (double)BitConverter.ToInt16(_recorderBuffer, i + 2);
                        h++;
                    }

                    h=0;
                    for (int i = 0; i < _recorderBuffer.Length; i += 4)
                    {
                        arrayByte2[h] = _recorderBuffer[i];
                        arrayByte2[h + 1] = _recorderBuffer[i + 1];
                        h += 2;
                    }
                }

                //int indexing = (_audioBitsPerSample/8);
                //double[] values = new double[_recorderBuffer.Length / indexing];
                //int h = 0;
                //for (int i = 0; i < _recorderBuffer.Length; i += indexing)
                //{
                //    values[h] = (double)BitConverter.ToInt16(_recorderBuffer, i);
                //    h++;
                //}
                SignalData = _waveLeft;
                if (obRecord != null)
                {
                    try
                    {
                        if (isRecording)
                        {
                            ((ObjectRecordWAV)obRecord).WAVWriter.WriteData(arrayByte2, 0, arrayByte2.Length);
                            //int iter = 0;
                            //for (int i = 0; i < signalData.Count(); i++)
                            //{
                            //    byte[] temp = BitConverter.GetBytes(Convert.ToInt16(signalData[i]));
                            //    for (int j = 0; j < 2; j++)
                            //    {
                            //        arrayByte2[iter] = temp[j];
                            //        iter++;
                            //    }
                            //}
                            //byte[] arrayByte = EnvironmentFunction.ConvertDoubleToByteArray(signalData, _audioBitsPerSample);
                            //((ObjectRecordWAV)obRecord).WAVWriter.WriteData(arrayByte, 0, arrayByte.Length);
                        }
                    }
                    catch
                    {
                    }
                }

                //OnDataChange(this, new OSequenceDataUpdateEventArgs(signalData));
            }
        }

        #region IDisposable Members

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
