using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Reflection;
using System.Xml;
using System.IO;
using System.Runtime;
using System.Runtime.Serialization.Formatters.Binary;
using System.Drawing;
using System.Drawing.Imaging;

using DSA_Teaser;
using DSA_Teaser.Class;
using DSA_Teaser.Class.Function;
using DSA_Teaser.Class.UI;

using NationalInstruments.UI.WindowsForms;
using NationalInstruments.UI;
using NationalInstruments.Analysis.SignalGeneration;
using NationalInstruments.Analysis.Math;
using NationalInstruments.Analysis.Dsp;
using NationalInstruments.DAQmx;

using Touchless.Vision.Camera;

using Sigsence.ApplicationElements;
using Sigsence.ApplicationFunction;

namespace DSA_Teaser
{
    public static class EnvironmentFunction
    {
        
        /// <summary>
        /// Create a file name using date time format
        /// </summary>
        /// <param name="headerFile">String to append in the beginning of the file name</param>
        /// <param name="fileFormat">Format of the file</param>
        /// <returns>File name</returns>
        public static string CreateFileNameByDateTime(string headerFile, string fileFormat)
        {
            string result = "";
            result = headerFile + "_" + DateTime.Now.Day.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Year.ToString() + "_" + DateTime.Now.Hour.ToString() + "-" +
                DateTime.Now.Minute.ToString() + "-" + DateTime.Now.Second.ToString() + "." + fileFormat;
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objectToCopy"></param>
        /// <returns></returns>
        public static T DeepCopy2<T>(object objectToCopy)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(memoryStream, objectToCopy);
                memoryStream.Seek(0, SeekOrigin.Begin);
                return (T)binaryFormatter.Deserialize(memoryStream);
            }
        }

        public static T DeepCopy<T>(T obj)
        {
            if (obj == null)
                throw new ArgumentNullException("Object cannot be null");

            return (T)Process(obj);
        }

        static object Process(object obj)
        {
            if (obj == null)
                return null;

            Type type = obj.GetType();

            if (type.IsValueType || type == typeof(string))
            {
                return obj;
            }
            else if (type.IsArray)
            {
                Type elementType = Type.GetType(
                     type.FullName.Replace("[]", string.Empty));

                var array = obj as Array;

                Array copied = Array.CreateInstance(elementType, array.Length);

                for (int i = 0; i < array.Length; i++)
                {
                    copied.SetValue(Process(array.GetValue(i)), i);
                }

                return Convert.ChangeType(copied, obj.GetType());
            }
            else if (type.IsClass)
            {
                object toret = Activator.CreateInstance(obj.GetType());

                FieldInfo[] fields = type.GetFields(BindingFlags.Public |
                            BindingFlags.NonPublic | BindingFlags.Instance);

                foreach (FieldInfo field in fields)
                {
                    object fieldValue = field.GetValue(obj);

                    if (fieldValue == null)
                        continue;

                    field.SetValue(toret, Process(fieldValue));
                }
                return toret;
            }
            else
                throw new ArgumentException("Unknown type");
        }

        /// <summary>
        /// Write XML nodes for project structure file properties
        /// </summary>
        /// <param name="writeXML">XML writer object</param>
        /// <param name="pInfo">Property info</param>
        /// <param name="Class">Object class to store</param>
        private static void WriteXMLExtractProperties(XmlTextWriter writeXML, PropertyInfo pInfo, object Class)
        {
            Type tp = pInfo.PropertyType;
            if (tp.IsGenericType && tp.GetGenericTypeDefinition() == typeof(List<>))
            {
                Type subTp = tp.GetGenericArguments()[0];
                Type genericList = typeof(List<>).MakeGenericType(subTp);
                var list = (System.Collections.IList)Activator.CreateInstance(genericList);
                list = (System.Collections.IList)pInfo.GetValue(Class, null);
                for (int i = 0; i < list.Count; i++)
                {
                    writeXML.WriteStartElement(pInfo.Name);

                    Type t2 = list[i].GetType();
                    List<PropertyInfo> pInfoList2 = t2.GetProperties().ToList<PropertyInfo>();
                    foreach (PropertyInfo pInfo2 in pInfoList2)
                    {
                        writeXML.WriteStartElement(pInfo2.Name);
                        writeXML.WriteString((pInfo2.GetValue(list[i], null)).ToString());
                        writeXML.WriteEndElement();
                    }
                    writeXML.WriteEndElement();
                }
            }
            else
            {
                writeXML.WriteStartElement(pInfo.Name);
                writeXML.WriteString((pInfo.GetValue(Class, null)).ToString());
                writeXML.WriteEndElement();
            }
        }

        /// <summary>
        /// Write XML nodes for the class define in the working environment
        /// </summary>
        /// <param name="writeXML">XML writer</param>
        /// <param name="Class">Object class to save</param>
        private static void WriteXMLClassProperties(XmlTextWriter writeXML, object Class)
        {
            Type t = Class.GetType();
            List<PropertyInfo> pInfoList = t.GetProperties().ToList<PropertyInfo>();
            foreach (PropertyInfo pInfo in pInfoList)
            {
                WriteXMLExtractProperties(writeXML, pInfo, Class);
            }
        }

        /// <summary>
        /// Function to create sigsence project structure file 
        /// </summary>
        /// <param name="path">location path of the file</param>
        /// <param name="filename">file name</param>
        /// <param name="sigsenceProject">sigsence project class that hold all the information required</param>
        /// <returns>Return boolean whether the creation is successfull or not</returns>
        public static bool CreateProjectXmlFile(string path, string filename, ProjectEnvirontment sigsenceProject)
        {
            bool result = true;
            //try
            //{
            XmlTextWriter writeXML = new XmlTextWriter(path + filename, null);
            writeXML.WriteStartDocument(true);
            writeXML.Formatting = Formatting.Indented;
            writeXML.Indentation = 2;
            writeXML.WriteStartElement("ProjectEnvironment");

            Type t = sigsenceProject.GetType();
            List<PropertyInfo> pInfoList = t.GetProperties().ToList<PropertyInfo>();
            foreach (PropertyInfo pInfo in pInfoList)
            {
                Type tp = pInfo.PropertyType;
                if (tp.IsGenericType && tp.GetGenericTypeDefinition() == typeof(List<>))
                {
                    Type subTp = tp.GetGenericArguments()[0];
                    Type genericList = typeof(List<>).MakeGenericType(subTp);
                    var list = (System.Collections.IList)Activator.CreateInstance(genericList);
                    list = (System.Collections.IList)pInfo.GetValue(sigsenceProject, null);
                    writeXML.WriteStartElement(pInfo.Name);
                    for (int i = 0; i < list.Count; i++)
                    {
                        writeXML.WriteStartElement("WorkingPanel");
                        Type t2 = list[i].GetType();
                        List<PropertyInfo> pInfoList2 = t2.GetProperties().ToList<PropertyInfo>();
                        string pName = (t2.GetProperty("PanelType").GetValue(list[i], null)).ToString();
                        foreach (PropertyInfo pInfo2 in pInfoList2)
                        {
                            //WriteXMLExtractProperties(writeXML, pInfo2, list[i]);
                            if (pInfo2.Name == "UCWorkingPanel" && ((pName == "acquisition") || (pName == "report")))
                            {
                                writeXML.WriteStartElement(pInfo2.Name);
                                UserControlWorkingPanel ucWorkingPanel = (UserControlWorkingPanel)pInfo2.GetValue(list[i], null);
                                writeXML.WriteStartElement("SequenceControlList");
                                for (int j = 0; j < ucWorkingPanel.tbControlPanelAssignment.Rows.Count; j++)
                                {
                                    if (((ObjectSignal)ucWorkingPanel.tbControlPanelAssignment.Rows[j]["control"]).ControlCategory == ControlCategories.Sequence)
                                    {
                                        writeXML.WriteStartElement("ObjectSequence");

                                        ObjectSequence ObIndicator = (ObjectSequence)ucWorkingPanel.tbControlPanelAssignment.Rows[j]["control"];
                                        Type tSequence = ObIndicator.GetType();
                                        List<PropertyInfo> ListPropInfoSequence = tSequence.GetProperties().ToList<PropertyInfo>();
                                        foreach (PropertyInfo pInfoSequence in ListPropInfoSequence)
                                        {
                                            writeXML.WriteStartElement(pInfoSequence.Name);
                                            switch (pInfoSequence.Name)
                                            {
                                                case "SequenceSource":
                                                    if ((pInfoSequence.GetValue(ObIndicator, null)) != null)
                                                    {
                                                        ObjectSignal OSignal = (ObjectSignal)pInfoSequence.GetValue(ObIndicator, null);
                                                        writeXML.WriteString(OSignal.ControlName);
                                                    }
                                                    break;
                                                case "WindowType":
                                                    if ((pInfoSequence.GetValue(ObIndicator, null)) != null)
                                                    {
                                                        ScaledWindow window = (ScaledWindow)pInfoSequence.GetValue(ObIndicator, null);
                                                        writeXML.WriteString(window.WindowType.ToString());
                                                    }
                                                    break;
                                                default:
                                                    if ((pInfoSequence.GetValue(ObIndicator, null)) != null)
                                                    {
                                                        writeXML.WriteString(pInfoSequence.GetValue(ObIndicator, null).ToString());
                                                    }
                                                    break;
                                            }
                                            writeXML.WriteEndElement();
                                        }
                                        writeXML.WriteEndElement();
                                    }
                                }
                                writeXML.WriteEndElement();

                                writeXML.WriteStartElement("IndicatorControlList");
                                for (int j = 0; j < ucWorkingPanel.tbControlPanelAssignment.Rows.Count; j++)
                                {
                                    if (((ObjectSignal)ucWorkingPanel.tbControlPanelAssignment.Rows[j]["control"]).ControlCategory == ControlCategories.Indicator)
                                    {
                                        writeXML.WriteStartElement("ObjectIndicator");
                                        ObjectIndicator ObIndicator = (ObjectIndicator)ucWorkingPanel.tbControlPanelAssignment.Rows[j]["control"];
                                        Type tIndicator = ObIndicator.GetType();
                                        List<PropertyInfo> ListPropInfoIndicator = tIndicator.GetProperties().ToList<PropertyInfo>();
                                        foreach (PropertyInfo pInfoIndicator in ListPropInfoIndicator)
                                        {
                                            writeXML.WriteStartElement(pInfoIndicator.Name);
                                            switch (pInfoIndicator.Name)
                                            {
                                                case "SequenceSource":
                                                    if ((pInfoIndicator.GetValue(ObIndicator, null)) != null)
                                                    {
                                                        ObjectSignal OSignal = (ObjectSignal)pInfoIndicator.GetValue(ObIndicator, null);
                                                        writeXML.WriteString(OSignal.ControlName);
                                                    }
                                                    break;
                                                default:
                                                    if ((pInfoIndicator.GetValue(ObIndicator, null)) != null)
                                                    {
                                                        writeXML.WriteString(pInfoIndicator.GetValue(ObIndicator, null).ToString());
                                                    }
                                                    break;
                                            }
                                            writeXML.WriteEndElement();
                                        }
                                        writeXML.WriteEndElement();
                                    }
                                }
                                writeXML.WriteEndElement();
                                writeXML.WriteEndElement();

                            }
                            else
                            {
                                writeXML.WriteStartElement(pInfo2.Name);
                                writeXML.WriteString((pInfo2.GetValue(list[i], null)).ToString());
                                writeXML.WriteEndElement();
                            }
                        }
                        writeXML.WriteEndElement();
                    }
                    writeXML.WriteEndElement();
                }
                //else if (tp.IsClass && tp.IsMarshalByRef)
                //{
                //        object classNonAnsi = pInfo.GetValue(Class, null);
                //        WriteXMLClassProperties(writeXML, classNonAnsi);
                //}
                else
                {
                    writeXML.WriteStartElement(pInfo.Name);
                    writeXML.WriteString((pInfo.GetValue(sigsenceProject, null)).ToString());
                    writeXML.WriteEndElement();
                }
                //WriteXMLExtractProperties(writeXML, pInfo, sigsenceProject);
            }
            writeXML.WriteEndElement();
            writeXML.WriteEndDocument();
            writeXML.Close();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "XML Write Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    result = false;
            //}
            return result;
        }

        /// <summary>
        /// Funciton to find control by name given a keyword 
        /// </summary>
        /// <param name="tbControl">List table that hold list information of control in the working environment</param>
        /// <param name="name">Keyword name</param>
        /// <returns>Return object signal</returns>
        private static ObjectSignal FindControlByName(DataTable tbControl, string name)
        {
            ObjectSignal result = null;
            for (int j = 0; j < tbControl.Rows.Count; j++)
            {
                ObjectSignal OSignal = (ObjectSignal)tbControl.Rows[j]["control"];
                if (OSignal.ControlName == name)
                {
                    result = OSignal;
                    break;
                }
            }
            return result;
        }

        /// <summary>
        /// Load sigsence project structure file from the specific location
        /// </summary>
        /// <param name="filepathname">file path and the name of the file</param>
        /// <param name="mainForm">Main form where the project structure will be loaded to</param>
        /// <returns>Return project environment class</returns>
        public static ProjectEnvirontment LoadProjectFile(string filepathname, Form1 mainForm)
        {
            XmlDocument xmlDoc = new XmlDocument();
            ProjectEnvirontment projEnvironment = new ProjectEnvirontment();
            List<WorkingPanel> wPanelList = new List<WorkingPanel>();
            xmlDoc.Load(filepathname);
            DataTable tbProjectControlList = new DataTable();
            tbProjectControlList.Columns.Add("ControlName");
            tbProjectControlList.Columns.Add("SequenceSourceName");
            XmlNode ProjectInfNode = xmlDoc.SelectSingleNode("/ProjectEnvironment");
            Type t = projEnvironment.GetType();
            List<PropertyInfo> pInfoList = t.GetProperties().ToList<PropertyInfo>();
            foreach (PropertyInfo pInfo in pInfoList)
            {
                Type tp = pInfo.PropertyType;
                if (tp.IsGenericType && tp.GetGenericTypeDefinition() == typeof(List<>))
                {
                    XmlNode LWPanelNode = xmlDoc.SelectSingleNode("/ProjectEnvironment/ListWorkingPanel");
                    XmlNodeList WPanelNodeList = LWPanelNode.SelectNodes("WorkingPanel");
                    foreach (XmlNode wpNode in WPanelNodeList)
                    {
                        WorkingPanel wPanel = new WorkingPanel(wpNode.SelectSingleNode("PanelName").InnerText, 
                            wpNode.SelectSingleNode("PanelType").InnerText);
                        XmlNode SequenceParentNode = wpNode.ChildNodes[2].ChildNodes[0];
                        XmlNodeList SequenceNodeList = SequenceParentNode.SelectNodes("ObjectSequence");
                        foreach (XmlNode SequenceNode in SequenceNodeList)
                        {
                            switch (SequenceNode.SelectSingleNode("ControlTypeName").InnerText)
                            {
                                case "SignalGenerator":
                                    string signalType = SequenceNode.SelectSingleNode("SignalType").InnerText;
                                    double frequency = Convert.ToDouble(SequenceNode.SelectSingleNode("Frequency").InnerText);
                                    double amplitude = Convert.ToDouble(SequenceNode.SelectSingleNode("Amplitude").InnerText);
                                    double phase = Convert.ToDouble(SequenceNode.SelectSingleNode("Phase").InnerText);
                                    Int32 sampleSize = Convert.ToInt32(SequenceNode.SelectSingleNode("SampleSize").InnerText);
                                    Int32 samplingRate = Convert.ToInt32(SequenceNode.SelectSingleNode("SamplingRate").InnerText);
                                    double dutyCycle = Convert.ToDouble(SequenceNode.SelectSingleNode("DutyCycle").InnerText);

                                    ObjectSignalGenerator OSignalGenerator = new ObjectSignalGenerator(frequency, amplitude, phase, sampleSize, samplingRate, signalType);
                                    //OSignalGenerator.ParentWindow = mainForm;
                                    //OSignalGenerator.ParentUserControl = (UserControlWorkingPanel)wPanel.UCWorkingPanel;
                                    OSignalGenerator.ControlTypeName = (ControlTypeNames)Enum.Parse(typeof(ControlTypeNames), SequenceNode.SelectSingleNode("ControlTypeName").InnerText);
                                    OSignalGenerator.ControlName = SequenceNode.SelectSingleNode("ControlName").InnerText;
                                    Int32 Xsg = Convert.ToInt32(SequenceNode.SelectSingleNode("ControlLocation").InnerText.Split(",".ToCharArray()).ToList<string>()[0].Split("=".ToCharArray()).ToList<string>()[1]);
                                    Int32 Ysg = Convert.ToInt32(SequenceNode.SelectSingleNode("ControlLocation").InnerText.Split(",".ToCharArray()).ToList<string>()[1].Split("=".ToCharArray()).ToList<string>()[1].Replace("}", ""));
                                    OSignalGenerator.ControlLocation = new System.Drawing.Point(Xsg, Ysg);
                                    OSignalGenerator.SequenceTextbox.Text = OSignalGenerator.ControlName;
                                    ((UserControlWorkingPanel)wPanel.UCWorkingPanel).SubscribeSequence(OSignalGenerator);

                                    ((UserControlWorkingPanel)wPanel.UCWorkingPanel).SubscribeObjectTreeview(OSignalGenerator, "sequence");
                                    break;
                                case "DAQDevice":

                                    break;
                                case "FFT":
                                    ObjectFFT OFFT = new ObjectFFT();
                                    //OFFT.ParentWindow = mainForm;
                                    //OFFT.ParentUserControl = (UserControlWorkingPanel)wPanel.UCWorkingPanel;
                                    OFFT.ControlTypeName = (ControlTypeNames)Enum.Parse(typeof(ControlTypeNames), SequenceNode.SelectSingleNode("ControlTypeName").InnerText);
                                    OFFT.ControlName = SequenceNode.SelectSingleNode("ControlName").InnerText;
                                    Int32 Xfft = Convert.ToInt32(SequenceNode.SelectSingleNode("ControlLocation").InnerText.Split(",".ToCharArray()).ToList<string>()[0].Split("=".ToCharArray()).ToList<string>()[1]);
                                    Int32 Yfft = Convert.ToInt32(SequenceNode.SelectSingleNode("ControlLocation").InnerText.Split(",".ToCharArray()).ToList<string>()[1].Split("=".ToCharArray()).ToList<string>()[1].Replace("}", ""));
                                    OFFT.ControlLocation = new System.Drawing.Point(Xfft, Yfft);
                                    OFFT.OutputType = SequenceNode.SelectSingleNode("OutputType").InnerText;
                                    OFFT.WindowType = OFFT.GetSelectedWindow((ScaledWindowType)Enum.Parse(typeof(ScaledWindowType), SequenceNode.SelectSingleNode("WindowType").InnerText));
                                    OFFT.SequenceTextbox.Text = OFFT.ControlName;
                                    ((UserControlWorkingPanel)wPanel.UCWorkingPanel).SubscribeSequence(OFFT);
                                    ((UserControlWorkingPanel)wPanel.UCWorkingPanel).SubscribeObjectTreeview(OFFT, "sequence");
                                    break;
                                case "AudioIn":
                                    ObjectAudioIn OAudio = new ObjectAudioIn();
                                    //OAudio.ParentWindow = mainForm;
                                    //OAudio.ParentUserControl = (UserControlWorkingPanel)wPanel.UCWorkingPanel;
                                    OAudio.ControlTypeName = (ControlTypeNames)Enum.Parse(typeof(ControlTypeNames), SequenceNode.SelectSingleNode("ControlTypeName").InnerText);
                                    OAudio.ControlName = SequenceNode.SelectSingleNode("ControlName").InnerText;
                                    Int32 XAudio = Convert.ToInt32(SequenceNode.SelectSingleNode("ControlLocation").InnerText.Split(",".ToCharArray()).ToList<string>()[0].Split("=".ToCharArray()).ToList<string>()[1]);
                                    Int32 YAudio = Convert.ToInt32(SequenceNode.SelectSingleNode("ControlLocation").InnerText.Split(",".ToCharArray()).ToList<string>()[1].Split("=".ToCharArray()).ToList<string>()[1].Replace("}", ""));
                                    OAudio.ControlLocation = new System.Drawing.Point(XAudio, YAudio);
                                    OAudio.SequenceTextbox.Text = OAudio.ControlName;
                                    ((UserControlWorkingPanel)wPanel.UCWorkingPanel).SubscribeSequence(OAudio);
                                    ((UserControlWorkingPanel)wPanel.UCWorkingPanel).SubscribeObjectTreeview(OAudio, "sequence");
                                    OAudio.InitRecording();
                                    break;
                            }
                            if (SequenceNode.SelectSingleNode("SequenceSource").InnerText != null)
                            {
                                DataRow row = tbProjectControlList.NewRow();
                                row["ControlName"] = SequenceNode.SelectSingleNode("ControlName").InnerText;
                                row["SequenceSourceName"] = SequenceNode.SelectSingleNode("SequenceSource").InnerText;
                                tbProjectControlList.Rows.Add(row);
                            }
                        }

                        XmlNode IndicatorParentNode = wpNode.ChildNodes[2].ChildNodes[1];
                        XmlNodeList IndicatorNodeList = IndicatorParentNode.SelectNodes("ObjectIndicator");
                        foreach (XmlNode IndicatorNode in IndicatorNodeList)
                        {
                            switch (IndicatorNode.SelectSingleNode("ControlTypeName").InnerText)
                            {
                                case "WaveformGraph":
                                    ObjectWaveformGraph OWaveformGraph = new ObjectWaveformGraph();
                                    //OWaveformGraph.ParentWindow = mainForm;
                                    //OWaveformGraph.ParentUserControl = (UserControlWorkingPanel)wPanel.UCWorkingPanel;
                                    OWaveformGraph.ControlName = IndicatorNode.SelectSingleNode("ControlName").InnerText;
                                    Int32 X = Convert.ToInt32(IndicatorNode.SelectSingleNode("ControlLocation").InnerText.Split(",".ToCharArray()).ToList<string>()[0].Split("=".ToCharArray()).ToList<string>()[1]);
                                    Int32 Y = Convert.ToInt32(IndicatorNode.SelectSingleNode("ControlLocation").InnerText.Split(",".ToCharArray()).ToList<string>()[1].Split("=".ToCharArray()).ToList<string>()[1].Replace("}", ""));
                                    OWaveformGraph.ControlLocation = new System.Drawing.Point(X, Y);
                                    OWaveformGraph.ControlHeight = Convert.ToInt32(IndicatorNode.SelectSingleNode("ControlHeight").InnerText);
                                    OWaveformGraph.ControlTypeName = (ControlTypeNames)Enum.Parse(typeof(ControlTypeNames), IndicatorNode.SelectSingleNode("ControlTypeName").InnerText);
                                    OWaveformGraph.ControlWidth = Convert.ToInt32(IndicatorNode.SelectSingleNode("ControlWidth").InnerText);

                                    ((UserControlWorkingPanel)wPanel.UCWorkingPanel).SubscribeIndicator(OWaveformGraph);
                                    OWaveformGraph.GetControlHandle().Caption = OWaveformGraph.ControlName;
                                    ((UserControlWorkingPanel)wPanel.UCWorkingPanel).SubscribeObjectTreeview(OWaveformGraph, "indicator");
                                    break;
                                case "scatterGraph":

                                    break;
                                case "digitalWaveformGraph":

                                    break;
                                case "complexGraph":

                                    break;
                                case "intensityGraph":

                                    break;
                                case "led":

                                    break;
                                case "tank":

                                    break;
                                case "meter":

                                    break;
                                case "gauge":

                                    break;
                                case "thermometer":

                                    break;
                                case "videoCapture":
                                    ObjectVideo OVideo = new ObjectVideo();
                                    //OVideo.ParentWindow = mainForm;
                                    //OVideo.ParentUserControl = (UserControlWorkingPanel)wPanel.UCWorkingPanel;
                                    OVideo.ControlTypeName = ControlTypeNames.Video;
                                    OVideo.ControlName = IndicatorNode.SelectSingleNode("ControlName").InnerText;
                                    Int32 Xvid = Convert.ToInt32(IndicatorNode.SelectSingleNode("ControlLocation").InnerText.Split(",".ToCharArray()).ToList<string>()[0].Split("=".ToCharArray()).ToList<string>()[1]);
                                    Int32 Yvid = Convert.ToInt32(IndicatorNode.SelectSingleNode("ControlLocation").InnerText.Split(",".ToCharArray()).ToList<string>()[1].Split("=".ToCharArray()).ToList<string>()[1].Replace("}", ""));
                                    OVideo.ControlLocation = new System.Drawing.Point(Xvid, Yvid);
                                    OVideo.ControlHeight = Convert.ToInt32(IndicatorNode.SelectSingleNode("ControlHeight").InnerText);
                                    OVideo.ControlWidth = Convert.ToInt32(IndicatorNode.SelectSingleNode("ControlWidth").InnerText);
                                    Camera cameraLoad = null;
                                    foreach (Camera cam in CameraService.AvailableCameras)
                                    {
                                        if (cam.Name == IndicatorNode.SelectSingleNode("VideoCamera").InnerText)
                                        {
                                            cameraLoad = cam;
                                            break;
                                        }
                                    }
                                    OVideo.VideoCamera = cameraLoad;
                                    OVideo.VideoCamera.FlipHorizontal = Convert.ToBoolean(IndicatorNode.SelectSingleNode("FlipHorizontal").InnerText);
                                    OVideo.VideoCamera.FlipVertical = Convert.ToBoolean(IndicatorNode.SelectSingleNode("FlipVertical").InnerText);
                                    OVideo.FlipHorizontal = OVideo.VideoCamera.FlipHorizontal;
                                    OVideo.FlipVertical = OVideo.VideoCamera.FlipVertical;
                                    OVideo.thrashOldCamera();
                                    OVideo.startCapturing();
                                    ((UserControlWorkingPanel)wPanel.UCWorkingPanel).SubscribeIndicator(OVideo);
                                    ((UserControlWorkingPanel)wPanel.UCWorkingPanel).SubscribeObjectTreeview(OVideo, "indicator");
                                    break;
                            }
                            if (IndicatorNode.SelectSingleNode("SequenceSource").InnerText != null)
                            {
                                DataRow row = tbProjectControlList.NewRow();
                                row["ControlName"] = IndicatorNode.SelectSingleNode("ControlName").InnerText;
                                row["SequenceSourceName"] = IndicatorNode.SelectSingleNode("SequenceSource").InnerText;
                                tbProjectControlList.Rows.Add(row);
                            }
                        }

                        for (int i = 0; i < tbProjectControlList.Rows.Count; i++)
                        {
                            ObjectSignal ObjectAssign = FindControlByName(((UserControlWorkingPanel)wPanel.UCWorkingPanel).tbControlPanelAssignment, tbProjectControlList.Rows[i]["ControlName"].ToString());
                            ObjectSignal ObjectSequenceSource = FindControlByName(((UserControlWorkingPanel)wPanel.UCWorkingPanel).tbControlPanelAssignment, tbProjectControlList.Rows[i]["SequenceSourceName"].ToString());
                            if (ObjectSequenceSource != null && ObjectAssign != null)
                            {
                                ObjectAssign.SequenceSource = (ObjectSequence)ObjectSequenceSource;
                            }
                        }
                        wPanelList.Add(wPanel);
                    }

                    projEnvironment.ListWorkingPanel = wPanelList;

                }
                else if (pInfo.Name != "ListWorkingPanel")
                {
                    if (pInfo.PropertyType.Name.Contains("string"))
                    {
                        pInfo.SetValue(projEnvironment, (ProjectInfNode.SelectSingleNode(pInfo.Name.ToString()).InnerText), null);
                    }
                    else if (pInfo.PropertyType.Name.Contains("DateTime"))
                    {
                        pInfo.SetValue(projEnvironment, Convert.ToDateTime(ProjectInfNode.SelectSingleNode(pInfo.Name.ToString()).InnerText), null);
                    }
                    else
                    {
                        pInfo.SetValue(projEnvironment, (ProjectInfNode.SelectSingleNode(pInfo.Name.ToString()).InnerText), null);
                    }
                }
            }

            return projEnvironment;
            //XmlNodeList xmlNodeList = xmlDoc.get
            //XmlTextReader xmlreader = new XmlTextReader(filepathname);
            //while (xmlreader.Read())
            //{
            //    switch (xmlreader.NodeType)
            //    {
            //        case XmlNodeType.Element:
            //            break;
            //        case XmlNodeType.EndElement:
            //            break;
            //        case XmlNodeType.Text:
            //            break;
            //    }
            //}
        }

        /// <summary>
        /// Temmporary function won't be used in the future
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static unsafe Bitmap CopyToNewBitmap(Bitmap source)
        {
            int width = source.Width;
            int height = source.Height;
            BitmapData srcData = source.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, source.PixelFormat);
            byte* src = (byte*)srcData.Scan0.ToPointer();
            Bitmap dstImage = new Bitmap(width, height, source.PixelFormat);
            
            BitmapData dstData = dstImage.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, dstImage.PixelFormat);

            byte* dst = (byte*)dstData.Scan0.ToPointer();
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    *dst = *src;
                }
                src = src + srcData.Stride - srcData.Width;
                dst = dst + dstData.Stride - dstData.Width;
            }
            source.UnlockBits(srcData);
            dstImage.UnlockBits(dstData);
            return dstImage;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="srcBitmap"></param>
        /// <returns></returns>
        static public Bitmap CopyBitmap(Bitmap srcBitmap)
        {
            // Create the new bitmap and associated graphics object
            Bitmap bmp = new Bitmap(srcBitmap.Width, srcBitmap.Height);
            Graphics g = Graphics.FromImage(bmp);

            // Draw the specified section of the source bitmap to the new one
            g.DrawImage(srcBitmap, 0, 0, new Rectangle(0,0,srcBitmap.Width, srcBitmap.Height), GraphicsUnit.Pixel);

            // Clean up
            g.Dispose();

            // Return the bitmap
            return bmp;
        }

        /// <summary>
        /// Convert double data type to byte array
        /// </summary>
        /// <param name="inputData">Array of data in double</param>
        /// <param name="bitsSample">Bit resolution per sample</param>
        /// <returns></returns>
        static public byte[] ConvertDoubleToByteArray(double[] inputData, int bitsSample)
        {
            byte[] result = new byte[inputData.Length * (bitsSample / 8)];
            int indexResult = 0;
            for (int i = 0; i < inputData.Length; i++)
            {
                byte[] doubleByte = BitConverter.GetBytes(inputData[i]);
                for (int j = 7; j > (7 - (bitsSample / 8)); j--)
                {
                    result[indexResult] = doubleByte[j];
                    indexResult++;
                }
            }
            return result;
        }

        static public void AssemblyLoad(string assemblyPath, string className, string methodName, object[] args)
        {
            
        }
    }
}