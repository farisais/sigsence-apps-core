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
    public class ObjectRecordWrap: ObjectRecording
    {
        public ObjectRecordWrap(string filename)
            : base(filename)
        {
            recordingWrapFile = filename;
        }

        public void CreateStructureRecordingFile()
        {
            XmlTextWriter writeXML = new XmlTextWriter(recordingWrapFile, null);
            writeXML.WriteStartDocument(true);
            writeXML.Formatting = Formatting.Indented;
            writeXML.Indentation = 2;
            writeXML.WriteStartElement("ObjectRecording");
            //
            writeXML.WriteStartElement("Recording_Date");
            writeXML.WriteString(recordDate.ToString());
            writeXML.WriteEndElement();
            //
            writeXML.WriteStartElement("Recording_Start");
            writeXML.WriteString(recordTimeStart.ToString());
            writeXML.WriteEndElement();
            //
            writeXML.WriteStartElement("Recording_End");
            writeXML.WriteString(RecordTimeEnd.ToString());
            writeXML.WriteEndElement();
            //
            writeXML.WriteStartElement("ListVideo");
            ///
            for (int i = 0; i < tbResourceFile.Rows.Count; i++)
            {
                if (tbResourceFile.Rows[i]["data_type"].ToString() == "video")
                {
                    writeXML.WriteStartElement("VideoObject");
                    ////
                    writeXML.WriteStartElement("FileName");
                    writeXML.WriteString(tbResourceFile.Rows[i]["file_name"].ToString());
                    writeXML.WriteEndElement();
                    ////
                    writeXML.WriteStartElement("FileExtension");
                    writeXML.WriteString(tbResourceFile.Rows[i]["file_extension"].ToString());
                    writeXML.WriteEndElement();
                    ////
                    writeXML.WriteStartElement("DataType");
                    writeXML.WriteString(tbResourceFile.Rows[i]["data_type"].ToString());
                    writeXML.WriteEndElement();
                    ////
                    writeXML.WriteStartElement("ObjectSequence");
                    writeXML.WriteString(tbResourceFile.Rows[i]["ObjectSequence"].ToString());
                    writeXML.WriteEndElement();
                    ////
                    writeXML.WriteEndElement();
                }
            }
            ///
            writeXML.WriteEndElement();
            writeXML.WriteStartElement("ListSignal");
            ///
            for (int i = 0; i < tbResourceFile.Rows.Count; i++)
            {
                if (tbResourceFile.Rows[i]["data_type"].ToString() == "signal")
                {
                    writeXML.WriteStartElement("SignalObject");
                    ////
                    writeXML.WriteStartElement("FileName");
                    writeXML.WriteString(tbResourceFile.Rows[i]["file_name"].ToString());
                    writeXML.WriteEndElement();
                    ////
                    writeXML.WriteStartElement("FileExtension");
                    writeXML.WriteString(tbResourceFile.Rows[i]["file_extension"].ToString());
                    writeXML.WriteEndElement();
                    ////
                    writeXML.WriteStartElement("DataType");
                    writeXML.WriteString(tbResourceFile.Rows[i]["data_type"].ToString());
                    writeXML.WriteEndElement();
                    ////
                    writeXML.WriteStartElement("ObjectSequence");
                    writeXML.WriteString(tbResourceFile.Rows[i]["ObjectSequence"].ToString());
                    writeXML.WriteEndElement();
                    ////
                    writeXML.WriteEndElement();
                }
            }
            ///
            writeXML.WriteEndElement();
            //
            writeXML.WriteEndElement();
            writeXML.WriteEndDocument();
            writeXML.Close();

        }
    }
}
