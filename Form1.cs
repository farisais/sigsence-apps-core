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
using DSA_Teaser.Class.UI;
using Microsoft.DirectX;
using Microsoft.DirectX.DirectSound;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Diagnostics;
using System.Xml;

using wavfile;
using NationalInstruments.UI.WindowsForms;
using NationalInstruments.UI;
using NationalInstruments.Analysis.SignalGeneration;
using NationalInstruments.Analysis.Math;
using NationalInstruments.Analysis.Dsp;

namespace DSA_Teaser
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// Class to hold all the information regarding the current project
        /// </summary>
        private ProjectEnvirontment sigsenceProject;
        public ProjectEnvirontment SigsenceProject
        {
            get
            {
                return sigsenceProject;
            }
            set
            {
                sigsenceProject = value;
            }
        }

        /// <summary>
        /// Form class that shown on the first start of the application
        /// </summary>
        private FormNewProject newProjectForm;
        public FormNewProject NewProjectForm
        {
            get
            {
                return newProjectForm;
            }
            set
            {
                newProjectForm = value;
            }
        }
        public int range = 160;
        public Form1()
        {
            //Initialise all components define in this main form
            InitializeComponent();

            //======================================================
            //Splash screen dummy
            //======================================================
            Thread t1 = new Thread(new ThreadStart(SplashForm));
            t1.Start();
            Thread.Sleep(1000);
            t1.Abort();
            Thread.Sleep(1000);
            //======================================================

            //Initiate working panel user control
            UserControlWorkingPanel wPanel = new UserControlWorkingPanel();
            //Refer the main form inside working panel class
            wPanel.MainForm = this;
            
            //Temporary step
            barEditItemRange.EditValue = range;
        }

        /// <summary>
        /// Function to load the splash form
        /// </summary>
        private void SplashForm()
        {
            SplashScreen splashScreen = new SplashScreen();
            splashScreen.ShowDialog();
            splashScreen.Dispose();
        }

        /// <summary>
        /// Event handler for new project button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItemNewProject_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //Create new form for new project
            newProjectForm = new FormNewProject();

            //Register the event handler
            newProjectForm.FormClosing += new FormClosingEventHandler(newProjectForm_FormClosing);

            //Show the form
            newProjectForm.ShowDialog();
        }

        /// <summary>
        /// Event handler called when the new project form is closed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void newProjectForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Execute if the button OK clicked
            if (newProjectForm.buttonOK)
            {
                //Get the location to save the project structure file
                bool found = false;
                string location = newProjectForm.locationPath;
                foreach (string dir in Directory.GetDirectories(newProjectForm.locationPath))
                {
                    if ((newProjectForm.locationPath + "\\" + newProjectForm.fileName) == dir)
                    {
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    Directory.SetCurrentDirectory(newProjectForm.locationPath);
                    Directory.CreateDirectory(newProjectForm.fileName);
                    location = newProjectForm.locationPath + "\\" + newProjectForm.fileName;
                }
                else
                {
                    location = newProjectForm.locationPath + "\\" + newProjectForm.fileName;
                }

                found = false;

                //Create recording directory of the current project
                foreach (string dir in Directory.GetDirectories(location))
                {
                    if (dir == "Record")
                    {
                        found = true;
                    }
                }

                string recordDirectory = location + @"\\Recording";
                if (!found)
                {
                    Directory.CreateDirectory(recordDirectory);
                }
                CreateProject(newProjectForm.fileName, location, newProjectForm.projectType, recordDirectory);
            }
        }

        /// <summary>
        /// Function to create project structure
        /// </summary>
        /// <param name="filename">File name for the project structure file</param>
        /// <param name="location">location path of the project struture file</param>
        /// <param name="projectType">The type of the project</param>
        /// <param name="recordDirectory">Directory path of the record file</param>
        void CreateProject(string filename, string location, string projectType, string recordDirectory)
        {
            //Create sigsence project class and store all the information inside
            sigsenceProject = new ProjectEnvirontment();
            sigsenceProject.FileName = filename;
            sigsenceProject.LocationPath = location;
            sigsenceProject.ProjectType = projectType;
            sigsenceProject.ProjectName = filename;
            sigsenceProject.RecordLocationPath = recordDirectory;
            sigsenceProject.ProjectCreationDate = DateTime.Now;

            //List all working panel in the project structure
            sigsenceProject.ListWorkingPanel = new List<DSA_Teaser.Class.UI.WorkingPanel>();
            sigsenceProject.ListWorkingPanel.Add(new DSA_Teaser.Class.UI.WorkingPanel(filename + "_Acquisition", "acquisition"));
            sigsenceProject.ListWorkingPanel.Add(new DSA_Teaser.Class.UI.WorkingPanel(filename + "_Analysis", "analysis"));
            sigsenceProject.ListWorkingPanel.Add(new DSA_Teaser.Class.UI.WorkingPanel(filename + "_Report", "report"));
            treeViewSolutionList.Nodes.Add(filename + "_Project");
            treeViewSolutionList.Nodes[0].Nodes.Add("Sequence");
            treeViewSolutionList.Nodes[0].Nodes.Add("Indicator");
            InitWorkingEnvironment();

            //Notify user that the project structure file has been setup
            if (EnvironmentFunction.CreateProjectXmlFile(location + @"\", filename + ".ssp", sigsenceProject))
            {
                barStaticItemNotif.Caption = filename + ".ssp" + " created in " + location;
            }
            else
            {
                barStaticItemNotif.Caption = "failed to create " + filename + ".ssp" + " in " + location;
            }
        }

        /// <summary>
        /// Function to create the working environment 
        /// </summary>
        void InitWorkingEnvironment()
        {
            UserControl wPanel = null;
            for (int i = 0; i < sigsenceProject.ListWorkingPanel.Count(); i++)
            {
                switch (sigsenceProject.ListWorkingPanel[i].PanelType)
                {
                    case "acquisition":
                        wPanel = sigsenceProject.ListWorkingPanel[i].UCWorkingPanel;
                        ((UserControlWorkingPanel)wPanel).MainForm = this;
                        ((UserControlWorkingPanel)wPanel).InitTreviewEvent();
                        break;
                    case "analysis":
                        wPanel = sigsenceProject.ListWorkingPanel[i].UCWorkingPanel;
                        ((UserControlAnalysisPanel)wPanel).MainForm = this;
                        ((UserControlAnalysisPanel)wPanel).InitWorkingPanel();
                        break;
                    case "report":
                        wPanel = sigsenceProject.ListWorkingPanel[i].UCWorkingPanel;
                        ((UserControlWorkingPanel)wPanel).MainForm = this;
                        break;
                }
                
                DevExpress.XtraTab.XtraTabPage tabPage = new DevExpress.XtraTab.XtraTabPage();
                tabPage.Controls.Add(wPanel);
                wPanel.Dock = DockStyle.Fill;
                tabPage.Name = sigsenceProject.ListWorkingPanel[i].PanelName;
                tabPage.Text = sigsenceProject.ListWorkingPanel[i].PanelName;
                xtraTabControlMain.TabPages.Add(tabPage);
            }
        }

        /// <summary>
        /// Function to create working environment based on the project type
        /// </summary>
        /// <param name="projectType">Project type identifier</param>
        private void InitWorkingEnvironment(string projectType)
        {
            switch (projectType)
            {
                case "Empty Project":
                    break;
                case "DAQ Assistance":
                    break;
                case "Recording Assistance":
                    break;
            }
        }

        /// <summary>
        /// Function to save current project to structure to sigsence project structure file
        /// </summary>
        /// <param name="sender">object that send the event</param>
        /// <param name="e">Arguments that passed by the object</param>
        private void barButtonItemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (EnvironmentFunction.CreateProjectXmlFile(sigsenceProject.LocationPath + @"\", sigsenceProject.FileName + ".ssp", sigsenceProject))
            {
                barStaticItemNotif.Caption = sigsenceProject.FileName + ".ssp" + " saved in " + sigsenceProject.LocationPath;
            }
            else
            {
                barStaticItemNotif.Caption = "failed to save " + sigsenceProject.FileName + ".ssp" + " in " + sigsenceProject.LocationPath;
            }
        }

        /// <summary>
        /// Event handler to open and load project structure file and initate it on the working environment
        /// </summary>
        /// <param name="sender">Object that raise the event</param>
        /// <param name="e">Argument that passed by the object</param>
        private void barButtonItemOpen_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenFileDialog openfileForm = new OpenFileDialog();
            openfileForm.Filter = "Sigsence Project File (*.ssp)|*.ssp|All files (*.*)|*.*";
            openfileForm.FilterIndex = 1;
            openfileForm.RestoreDirectory = true;
            openfileForm.InitialDirectory = @"C:\Users\faris\Documents\Sigsence DeskApp V.1.0\Projects";

            if (openfileForm.ShowDialog() == DialogResult.OK)
            {
                treeViewSolutionList.Nodes.Add("Project");
                treeViewSolutionList.Nodes[0].Nodes.Add("Sequence");
                treeViewSolutionList.Nodes[0].Nodes.Add("Indicator");
                sigsenceProject = EnvironmentFunction.LoadProjectFile(openfileForm.FileName, this);
                treeViewSolutionList.Nodes[0].Text = sigsenceProject.ProjectName + "_Project";
                InitWorkingEnvironment();
            }
        }

        /// <summary>
        /// Event handler that called to record all the activity within the working environment in the current project
        /// </summary>
        /// <param name="sender">Object that raise the event</param>
        /// <param name="e">Argument that passed by the object</param>
        private void barButtonItemRecording_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            UserControlWorkingPanel wPanel = sigsenceProject.ListWorkingPanel[0].UCWorkingPanel as UserControlWorkingPanel;
            if (!wPanel.isRecording)
            {
                barButtonItemRecord.ImageIndex = 3;
                wPanel.StartRecording();
            }
            else
            {
                barButtonItemRecord.ImageIndex = 2;
                wPanel.StopRecording();
            }
        }

        //Temporary function
        private void barEditItemRange_EditValueChanged(object sender, EventArgs e)
        {
            range = Convert.ToInt32(barEditItemRange.EditValue);
        }

        //Temporary function
        private void barButtonItem37_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            WAVFile wavFile = new WAVFile();
            wavFile.Open(@"C:\Users\faris\Documents\Sigsence DeskApp V.1.0\Projects\sigsenceProject\Audio\Record_13-10-2012_18-19-46.wav", WAVFile.WAVFileMode.READ);
            //wavFile.Open(@"C:\Users\faris\Documents\Sigsence DeskApp V.1.0\Projects\sigsenceProject\Audio\doh.wav", WAVFile.WAVFileMode.READ);
        }

        //Temporary function
        private void hideContainerLeft_Click(object sender, EventArgs e)
        {

        }
    }
}
