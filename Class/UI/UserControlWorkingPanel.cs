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
using DSA_Teaser.Class;
using System.IO;

using DevExpress.XtraVerticalGrid;
using DevExpress.XtraVerticalGrid.Rows;

using Emgu.CV;
using Emgu.CV.UI;
using Emgu.CV.Structure;
using Emgu.Util;

using AviFile;

using Microsoft.VisualBasic.PowerPacks;

using Sigsence.ApplicationElements;
using Sigsence.ApplicationFunction;
using Sigsence.ProgrammingInterface;

using DSA_Teaser.Class.Interface;

namespace DSA_Teaser
{
    public partial class UserControlWorkingPanel : UserControl
    {
        //Properties for UserControlWorkingPanel//
        public Form1 MainForm;
        private List<ObjectIndicator> IndicatorList;
        private List<ObjectSequence> SequenceObjectList;
        public int IndexSequenceObjectSelected;
        public int IndexIndicatorControlSelected;
        public DataTable tbControlPanelAssignment;
        private Capture _capture;
        public bool isRecording = false;
        public ObjectRecordWrap OBRecordWrap;
        public DataTable tbSequenceConnection;
        public ShapeContainer shapeContainer;
        public DataTable TbLineSequence;
        private string AddinPath = @"\Addin";
        private DataTable tbAssemblyList;
        //Properties for UserControlWorkingPanel//

        public UserControlWorkingPanel()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            tbControlPanelAssignment = new DataTable();
            tbControlPanelAssignment.Columns.Add("index");
            tbControlPanelAssignment.Columns.Add("control", typeof(ObjectSignal));
            tbControlPanelAssignment.Columns.Add("isSelected");

            IndicatorList = new List<ObjectIndicator>();
            SequenceObjectList = new List<ObjectSequence>();
            //MainForm.treeViewSolutionList.SelectedNode = MainForm.treeViewSolutionList.Nodes[0].Nodes[0];
            shapeContainer = new ShapeContainer();
            shapeContainer.Location = new Point(0, 0);
            shapeContainer.Size = new Size(20, 20);
            this.panelSequence.Controls.Add(shapeContainer);
            InitTbLineSequence();
            InitTbAssemblyList();

            List<string> fileList = Directory.GetFiles(Directory.GetCurrentDirectory() + AddinPath, "*.dll").ToList<string>();
            foreach (string file in fileList)
            {
                Assembly asm = Assembly.LoadFrom(file);

                foreach (Type t in asm.GetTypes())
                {
                    foreach (Type iface in t.GetInterfaces())
                    {
                        if (iface.Name == "ISigsenceAccess")
                        {
                            ISigsenceAccess sigsenceAccess = (ISigsenceAccess)Activator.CreateInstance(t);
                            InsertToAssemblyList(sigsenceAccess);
                            switch (sigsenceAccess.IControlType)
                            {
                                case InterfaceControlType.Indicator:
                                    ToolStripMenuItem menuItemIndicator = (ToolStripMenuItem)contextMenuStripIndicatorPanel.Items[0];
                                    menuItemIndicator.DropDownItems.Add(sigsenceAccess.ObjectName);
                                    menuItemIndicator.DropDownItems[menuItemIndicator.DropDownItems.Count - 1].Tag = 
                                        tbAssemblyList.Rows[tbAssemblyList.Rows.Count - 1]["guid"];

                                    menuItemIndicator.DropDownItems[menuItemIndicator.DropDownItems.Count - 1].Click += 
                                        new EventHandler(contextMenuItemObjectCustomIndicator_Click);
                                    break;
                                case InterfaceControlType.Sequence:
                                    ToolStripMenuItem menuItemSequence = (ToolStripMenuItem)contextMenuStripSequencePanel.Items[0];
                                    menuItemSequence.DropDownItems.Add(sigsenceAccess.ObjectName);
                                    menuItemSequence.DropDownItems[menuItemSequence.DropDownItems.Count - 1].Tag = 
                                        tbAssemblyList.Rows[tbAssemblyList.Rows.Count - 1]["guid"];

                                    menuItemSequence.DropDownItems[menuItemSequence.DropDownItems.Count - 1].Click += 
                                        new EventHandler(contextMenuItemObjectCustomSequence_Click);
                                    break;
                            }
                        }
                    }
                }
            }
        }

        void contextMenuItemObjectCustomSequence_Click(object sender, EventArgs e)
        {
            ISigsenceAccess sigsenceAccess = GetInterfaceSigsenceFromList(((Guid)((ToolStripMenuItem)sender).Tag));
            ObjectSequenceInterface objSequenceInterface = new ObjectSequenceInterface(sigsenceAccess.ResourceImagePath);
            sigsenceAccess.InitObject((ISigsenceInterface)objSequenceInterface);

            objSequenceInterface.ObjectClickedEvent += new ObjectSignal.ObjectClick(ObjectControlClick);
            objSequenceInterface.ObjectMouseDownEvent += new ObjectSignal.ObjectMouseDown(ObjectControlMouseDown);

            objSequenceInterface.TbControlPanelAssignment = tbControlPanelAssignment;
            objSequenceInterface.VGridMainForm = MainForm.vGridControlProperties;
            objSequenceInterface.TreeViewSolutionList = MainForm.treeViewSolutionList;
            objSequenceInterface.PanelIndicator = panelIndicator;
            objSequenceInterface.PanelSequence = panelSequence;
            objSequenceInterface.TbLineSequence = TbLineSequence;
            objSequenceInterface.ShapeContainer = shapeContainer;

            objSequenceInterface.ControlTypeName = ControlTypeNames.SequenceCustom;
            objSequenceInterface.ControlName = WorkingEnvironmentFunction.CreateControlName
                ("SequenceCustom", "sequence", tbControlPanelAssignment);
            SubscribeSequence(objSequenceInterface);
            objSequenceInterface.SequenceTextbox.Text = objSequenceInterface.ControlName;
            objSequenceInterface.CallInitForm();

            SubscribeObjectTreeview(objSequenceInterface, "sequence");
        }

        void contextMenuItemObjectCustomIndicator_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void InitTbAssemblyList()
        {
            tbAssemblyList = new DataTable();
            tbAssemblyList.Columns.Add("interface", typeof(ISigsenceAccess));
            tbAssemblyList.Columns.Add("guid", typeof(System.Guid));
        }

        private void InsertToAssemblyList(ISigsenceAccess sigsenceAccess)
        {
            DataRow row = tbAssemblyList.NewRow();
            System.Guid guid = Guid.NewGuid();
            row["interface"] = sigsenceAccess;
            row["guid"] = guid;
            tbAssemblyList.Rows.Add(row);
        }

        //private ObjectSignal GetAssemblyFromList(Guid guid)
        //{
        //    ObjectSignal result = null;
            
        //    for (int i = 0; i < tbAssemblyList.Rows.Count; i++)
        //    {
        //        if (((Guid)tbAssemblyList.Rows[i]["guid"]).Equals(guid))
        //        {
        //            ISigsenceAccess iface = (ISigsenceAccess)tbAssemblyList.Rows[i]["interface"];
        //            result = iface.ObjectCustom;
        //        }
        //    }
        //    return result;
        //}

        private ISigsenceAccess GetInterfaceSigsenceFromList(Guid guid)
        {
            ISigsenceAccess result = null;

            for (int i = 0; i < tbAssemblyList.Rows.Count; i++)
            {
                if (((Guid)tbAssemblyList.Rows[i]["guid"]).Equals(guid))
                {
                    result = (ISigsenceAccess)tbAssemblyList.Rows[i]["interface"];
                }
            }
            return result;
        }

        private void InitTbLineSequence()
        {
            TbLineSequence = new DataTable();
            TbLineSequence.Columns.Add("ObjectIn");
            TbLineSequence.Columns.Add("Line", typeof(LineShape));
            TbLineSequence.Columns.Add("ObjectOut");
        }
       
        void _capture_ImageGrabbed(object sender, EventArgs e)
        {
            Image<Bgr, Byte> frame = _capture.RetrieveBgrFrame();
        }

        public void SubscribeIndicator(object indicatorObj)
        {
            ObjectIndicator indicatorAdd = (ObjectIndicator)indicatorObj;
            indicatorAdd.IndexList = IndicatorList.Count();
            //indicatorAdd.ControlLocation = new Point(100, 100);//lastPosRightClickInPanelIndicator;
            IndicatorList.Add(indicatorAdd);
            panelIndicator.Controls.Add((Control)indicatorAdd.ControlHandle);
            //panelIndicator.Controls[indicatorAdd.IndexList].ContextMenuStrip = contextMenuStripIndicator;

            DataRow addRow = tbControlPanelAssignment.NewRow();
            addRow["index"] = tbControlPanelAssignment.Rows.Count;
            addRow["control"] = indicatorAdd;
            addRow["isSelected"] = 0;

            tbControlPanelAssignment.Rows.Add(addRow);

            panelIndicator.Controls[panelIndicator.Controls.Count - 1].Click += new EventHandler(Indicator_Click);
        }

        
        public void SubscribeSequence(object sequenceObj)
        {
            ObjectSequence sequenceAdd = (ObjectSequence)sequenceObj;
            sequenceAdd.IndexList = SequenceObjectList.Count();
            SequenceObjectList.Add(sequenceAdd);
            panelSequence.Controls.Add((Control)sequenceAdd.ControlHandle);
            panelSequence.Controls.Add(sequenceAdd.SequenceTextbox);
            panelSequence.Controls[sequenceAdd.IndexList].ContextMenuStrip = contextMenuStripSequence;
            panelSequence.Controls[sequenceAdd.IndexList].Click += new EventHandler(SequenceObject_Click);

            DataRow rowAdd = tbControlPanelAssignment.NewRow();
            rowAdd["index"] = tbControlPanelAssignment.Rows.Count;
            rowAdd["control"] = sequenceAdd;
            rowAdd["isSelected"] = 0;

            tbControlPanelAssignment.Rows.Add(rowAdd);
        }

        public void InitTreviewEvent()
        {
            MainForm.treeViewSolutionList.NodeMouseClick += new TreeNodeMouseClickEventHandler(treeViewSolutionList_NodeMouseClick);
        }

        void treeViewSolutionList_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Level == 2)
            {
                GetControlByName(e.Node.Name).AssignCustomPropertiesPanel();
                ChangeFocusControl(GetControlByName(e.Node.Name)); 
            }
        }

        public void SubscribeObjectTreeview(object OSignalObj, string type)
        {
            ObjectSignal OSignal = (ObjectSignal)OSignalObj;
            TreeNode nodeAdd = new TreeNode();
            nodeAdd.Name = OSignal.ControlName;
            nodeAdd.Text = OSignal.ControlName;
            switch (type)
            {
                case "indicator":
                    MainForm.treeViewSolutionList.Nodes[0].Nodes[1].Nodes.Add(nodeAdd);
                    break;
                case "sequence":
                    MainForm.treeViewSolutionList.Nodes[0].Nodes[0].Nodes.Add(nodeAdd);
                    break;
            }
        }

        ////public void UnSubscribeObjectTreeview(string OSignal, ControlCategories? type)
        ////{
        ////    switch (type)
        ////    {
        ////        case ControlCategories.Indicator:
        ////            MainForm.treeViewSolutionList.Nodes[0].Nodes[1].Nodes.RemoveByKey(OSignal);
        ////            break;
        ////        case ControlCategories.Sequence:
        ////            MainForm.treeViewSolutionList.Nodes[0].Nodes[0].Nodes.RemoveByKey(OSignal);
        ////            break;
        ////    }
        ////}

        private void ObjectControlClick(object sender, EventArgs e)
        {
            //Put these in the function UpdatePanelProperties()
            //MessageBox.Show(((ObjectSignal)sender).ControlName, "Multiple Event Handler Test");

            MainForm.vGridControlProperties.FocusedRowChanged -= vGridControlProperties_FocusedRowChanged;
            MainForm.vGridControlProperties.FocusedRow = null;
            MainForm.vGridControlProperties.FocusedRowChanged += new DevExpress.XtraVerticalGrid.Events.FocusedRowChangedEventHandler(vGridControlProperties_FocusedRowChanged);
            MainForm.categoryRowAppearance.ChildRows.Clear();
            MainForm.categoryRowBehavior.ChildRows.Clear();
            MainForm.categoryRowData.ChildRows.Clear();
            MainForm.categoryRowDesign.ChildRows.Clear();
            MainForm.categoryRowLayout.ChildRows.Clear();
            //AssignBasicPropertiesPanel();
        }

        private void ObjectControlMouseDown(object sender, EventArgs e)
        {
            ObjectSignal OSignal = sender as ObjectSignal;
            if (!OSignal.ControlMouseDown)
            {
                ChangeFocusControl(OSignal);
                UpdateTreeview();
            }
        }

        private void ChangeFocusControl(ObjectSignal OSignal) //Move
        {
            OSignal.ControlSelected = true;
            UnassignControlFocus();
            OSignal.ControlBackColor = OSignal.ControlBackColorFocus;
            SetSelectedControl(OSignal);
        }

        //private void AssignBasicPropertiesPanel()
        //{
        //    ObjectSignal OSignal = (ObjectSignal)tbControlPanelAssignment.Rows[GetSelectedControlIndex()]["control"];
        //    EditorRow editorRow = new EditorRow();
        //    editorRow.Name = "BackColor";
        //    editorRow.Properties.Caption = "BackColor";
        //    editorRow.Properties.RowEdit =MainForm.repositoryItemColorEditBackColor;
        //    editorRow.Properties.Value = OSignal.ControlBackColor;
        //    MainForm.categoryRowAppearance.ChildRows.Add(editorRow);

        //    editorRow = new EditorRow();
        //    editorRow.Name = "Name";
        //    editorRow.Properties.Caption = "Name";
        //    editorRow.Properties.Value = OSignal.ControlName;
        //    MainForm.categoryRowDesign.ChildRows.Add(editorRow);

        //    editorRow = new EditorRow();
        //    editorRow.Name = "Location";
        //    editorRow.Properties.Caption = "Location";
        //    MainForm.categoryRowLayout.ChildRows.Add(editorRow);

        //    EditorRow editorRowSub = new EditorRow();
        //    editorRow.Name = "X";
        //    editorRowSub.Properties.Caption = "X";
        //    editorRowSub.Properties.Value = OSignal.ControlLocation.X;
        //    editorRow.ChildRows.Add(editorRowSub);

        //    editorRowSub = new EditorRow();
        //    editorRow.Name = "Y";
        //    editorRowSub.Properties.Caption = "Y";
        //    editorRowSub.Properties.Value = OSignal.ControlLocation.Y;
        //    editorRow.ChildRows.Add(editorRowSub);
        //}

        //public void UpdateBasicProperties(string caption)
        //{
        //    ObjectSignal OSignal = (ObjectSignal)tbControlPanelAssignment.Rows[GetSelectedControlIndex()]["control"];
        //    switch (caption)
        //    {
        //        case "Name":
        //            if (EnvironmentFunction.validateName(MainForm.categoryRowDesign.ChildRows["Name"].Properties.Value.ToString(),
        //                tbControlPanelAssignment))
        //            {
        //                switch (OSignal.ControlCategory)
        //                {
        //                    case ControlCategories.Indicator:
        //                        MainForm.treeViewSolutionList.Nodes[0].Nodes[1].Nodes[OSignal.ControlName].Text = MainForm.categoryRowDesign.ChildRows["Name"].Properties.Value.ToString();
        //                        MainForm.treeViewSolutionList.Nodes[0].Nodes[1].Nodes[OSignal.ControlName].Name = MainForm.categoryRowDesign.ChildRows["Name"].Properties.Value.ToString();
        //                        break;
        //                    case ControlCategories.Sequence:
        //                        MainForm.treeViewSolutionList.Nodes[0].Nodes[0].Nodes[OSignal.ControlName].Text = MainForm.categoryRowDesign.ChildRows["Name"].Properties.Value.ToString();
        //                        MainForm.treeViewSolutionList.Nodes[0].Nodes[0].Nodes[OSignal.ControlName].Name = MainForm.categoryRowDesign.ChildRows["Name"].Properties.Value.ToString();
        //                        break;
        //                }
        //                OSignal.ControlName = MainForm.categoryRowDesign.ChildRows["Name"].Properties.Value.ToString();
        //            }
        //            else
        //            {
        //                MainForm.categoryRowDesign.ChildRows["Name"].Properties.Value = OSignal.ControlName;
        //                return;
        //            }
        //            break;
        //        case "X":
        //            Point px = new Point(Convert.ToInt16(MainForm.categoryRowLayout.ChildRows[0].ChildRows[0].Properties.Value), OSignal.ControlLocation.Y);
        //            OSignal.ControlLocation = px;
        //            break;
        //        case "Y":
        //            Point py = new Point(OSignal.ControlLocation.X, Convert.ToInt16(MainForm.categoryRowLayout.ChildRows[0].ChildRows[1].Properties.Value));
        //            OSignal.ControlLocation = py;
        //            break;
        //    }
        //}

        #region VerticalGrid Parent Window Definition

        private void InitVGridProperties()//move
        {
            MainForm.vGridControlProperties.FocusedRowChanged += new DevExpress.XtraVerticalGrid.Events.FocusedRowChangedEventHandler(vGridControlProperties_FocusedRowChanged);
            MainForm.vGridControlProperties.EditorKeyDown += new KeyEventHandler(vGridControlProperties_EditorKeyDown);
        }

        private void vGridControlProperties_FocusedRowChanged(object sender, DevExpress.XtraVerticalGrid.Events.FocusedRowChangedEventArgs e)
        {
            ObjectSignal OSignal = (ObjectSignal)tbControlPanelAssignment.Rows[WorkingEnvironmentFunction.GetSelectedControlIndex(tbControlPanelAssignment)]["control"];
            if (e.OldRow != null && e.OldRow.Index >= 0)
            {
                string fieldName = e.OldRow.Properties.Row.Name;
                OSignal.UpdateBasicProperties(fieldName);
                OSignal.UpdateFromGridCategory(fieldName);
            }
        }

        private void vGridControlProperties_EditorKeyDown(object sender, KeyEventArgs e)
        {
            ObjectSignal OSignal = (ObjectSignal)tbControlPanelAssignment.Rows[WorkingEnvironmentFunction.GetSelectedControlIndex(tbControlPanelAssignment)]["control"];
            string fieldName = MainForm.vGridControlProperties.FocusedRow.Properties.Row.Name;
            if (e.KeyData == Keys.Enter)
            {
                OSignal.UpdateBasicProperties(fieldName);
                OSignal.UpdateFromGridCategory(fieldName);
            }
        }

        #endregion

        #region TreeView Parent Window Definition

        private void UpdateTreeview()//Move
        {
            ObjectSignal OSignal = (ObjectSignal)tbControlPanelAssignment.Rows[WorkingEnvironmentFunction.GetSelectedControlIndex(tbControlPanelAssignment)]["control"];
            switch (OSignal.ControlCategory)
            {
                case ControlCategories.Indicator:
                    MainForm.treeViewSolutionList.SelectedNode = MainForm.treeViewSolutionList.Nodes[0].Nodes[1].Nodes[OSignal.ControlName];
                    break;
                case ControlCategories.Sequence:
                    MainForm.treeViewSolutionList.SelectedNode = MainForm.treeViewSolutionList.Nodes[0].Nodes[0].Nodes[OSignal.ControlName];
                    break;
            }
            MainForm.treeViewSolutionList.Select();
        }

        #endregion

        private void customObjectIndicatorToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void customObjectSequenceToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        
        private void waveformGraphToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ObjectWaveformGraph OWaveformGraph = new ObjectWaveformGraph();
            OWaveformGraph.ObjectClickedEvent +=new ObjectSignal.ObjectClick(ObjectControlClick);
            OWaveformGraph.ObjectMouseDownEvent += new ObjectSignal.ObjectMouseDown(ObjectControlMouseDown);
            //OWaveformGraph.ParentWindow = MainForm;
            //OWaveformGraph.ParentUserControl = this;
            OWaveformGraph.TbControlPanelAssignment = tbControlPanelAssignment;
            OWaveformGraph.VGridMainForm = MainForm.vGridControlProperties;
            OWaveformGraph.TreeViewSolutionList = MainForm.treeViewSolutionList;
            OWaveformGraph.PanelIndicator = panelIndicator;
            OWaveformGraph.PanelSequence = panelSequence;
            
            OWaveformGraph.ControlName = WorkingEnvironmentFunction.CreateControlName("waveformGraph", "indicator", tbControlPanelAssignment);
            OWaveformGraph.ControlTypeName = ControlTypeNames.WaveFormGraph;
            SubscribeIndicator(OWaveformGraph);
            OWaveformGraph.GetControlHandle().Caption = OWaveformGraph.ControlName;
            SubscribeObjectTreeview(OWaveformGraph, "indicator");
            //OWaveformGraph.initContextMenuStrip();
        }

        

        private void scatterGraphToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ObjectIndicator indicatorAdd = new ObjectIndicator(scatterGraph, this);
            ObjectScatterGraph OScatterGraph = new ObjectScatterGraph();
            OScatterGraph.ObjectClickedEvent += new ObjectSignal.ObjectClick(ObjectControlClick);
            OScatterGraph.ObjectMouseDownEvent += new ObjectSignal.ObjectMouseDown(ObjectControlMouseDown);
            //OScatterGraph.ParentWindow = MainForm;
            //OScatterGraph.ParentUserControl = this;

            OScatterGraph.TbControlPanelAssignment = tbControlPanelAssignment;
            OScatterGraph.VGridMainForm = MainForm.vGridControlProperties;
            OScatterGraph.TreeViewSolutionList = MainForm.treeViewSolutionList;
            OScatterGraph.PanelIndicator = panelIndicator;
            OScatterGraph.PanelSequence = panelSequence;

            OScatterGraph.ControlName = WorkingEnvironmentFunction.CreateControlName("scatterGraph", "indicator", tbControlPanelAssignment);
            SubscribeIndicator(OScatterGraph);
            OScatterGraph.GetControlHandle().Caption = OScatterGraph.ControlName;

            SubscribeObjectTreeview(OScatterGraph, "indicator");
        }

        private void digitalWaveformGraphToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ObjectIndicator indicatorAdd = new ObjectIndicator(digitalWaveformGraph, this);
            ObjectDigitalWaveformGraph ODigitalWaveformGraph = new ObjectDigitalWaveformGraph();
            //ODigitalWaveformGraph.ParentWindow = MainForm;
            //ODigitalWaveformGraph.ParentUserControl = this;

            ODigitalWaveformGraph.TbControlPanelAssignment = tbControlPanelAssignment;
            ODigitalWaveformGraph.VGridMainForm = MainForm.vGridControlProperties;
            ODigitalWaveformGraph.TreeViewSolutionList = MainForm.treeViewSolutionList;
            ODigitalWaveformGraph.PanelIndicator = panelIndicator;
            ODigitalWaveformGraph.PanelSequence = panelSequence;

            ODigitalWaveformGraph.ControlName = WorkingEnvironmentFunction.CreateControlName("digitalWaveformGraph", "indicator", tbControlPanelAssignment);
            SubscribeIndicator(ODigitalWaveformGraph);
            ODigitalWaveformGraph.GetControlHandle().Caption = ODigitalWaveformGraph.ControlName;

            SubscribeObjectTreeview(ODigitalWaveformGraph, "indicator");
           
        }

        private void complexGraphToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ObjectIndicator indicatorAdd = new ObjectIndicator(complexGraph, this);
            ObjectComplexGraph OComplexGraph = new ObjectComplexGraph();
            //OComplexGraph.ParentWindow = MainForm;
            //OComplexGraph.ParentUserControl = this;

            OComplexGraph.TbControlPanelAssignment = tbControlPanelAssignment;
            OComplexGraph.VGridMainForm = MainForm.vGridControlProperties;
            OComplexGraph.TreeViewSolutionList = MainForm.treeViewSolutionList;
            OComplexGraph.PanelIndicator = panelIndicator;
            OComplexGraph.PanelSequence = panelSequence;

            OComplexGraph.ControlName = WorkingEnvironmentFunction.CreateControlName("complexGraph", "indicator", tbControlPanelAssignment);
            SubscribeIndicator(OComplexGraph);
            OComplexGraph.GetControlHandle().Caption = OComplexGraph.ControlName;

            SubscribeObjectTreeview(OComplexGraph, "indicator");
        }

        private void intensityGraphToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ObjectIndicator indicatorAdd = new ObjectIndicator(intensityGraph, this);
            ObjectIntensityGraph OIntensityGraph = new ObjectIntensityGraph();
            //OIntensityGraph.ParentWindow = MainForm;
            //OIntensityGraph.ParentUserControl = this;

            OIntensityGraph.TbControlPanelAssignment = tbControlPanelAssignment;
            OIntensityGraph.VGridMainForm = MainForm.vGridControlProperties;
            OIntensityGraph.TreeViewSolutionList = MainForm.treeViewSolutionList;
            OIntensityGraph.PanelIndicator = panelIndicator;
            OIntensityGraph.PanelSequence = panelSequence;

            OIntensityGraph.ControlName = WorkingEnvironmentFunction.CreateControlName("intensityGraph", "indicator", tbControlPanelAssignment);
            SubscribeIndicator(OIntensityGraph);
            OIntensityGraph.GetControlHandle().Caption = OIntensityGraph.ControlName;

            SubscribeObjectTreeview(OIntensityGraph, "indicator");
        }

        private void ledToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ObjectIndicator indicatorAdd = new ObjectIndicator(led, this);
            ObjectLed OLed = new ObjectLed();
            //OLed.ParentWindow = MainForm;
            //OLed.ParentUserControl = this;

            OLed.TbControlPanelAssignment = tbControlPanelAssignment;
            OLed.VGridMainForm = MainForm.vGridControlProperties;
            OLed.TreeViewSolutionList = MainForm.treeViewSolutionList;
            OLed.PanelIndicator = panelIndicator;
            OLed.PanelSequence = panelSequence;

            OLed.ControlName = WorkingEnvironmentFunction.CreateControlName("led", "indicator", tbControlPanelAssignment);
            SubscribeIndicator(OLed);
            OLed.GetControlHandle().Caption = OLed.ControlName;

            SubscribeObjectTreeview(OLed, "indicator");
        }

        private void tankToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ObjectTank OTank = new ObjectTank();
            //OTank.ParentWindow = MainForm;
            //OTank.ParentUserControl = this;

            OTank.TbControlPanelAssignment = tbControlPanelAssignment;
            OTank.VGridMainForm = MainForm.vGridControlProperties;
            OTank.TreeViewSolutionList = MainForm.treeViewSolutionList;
            OTank.PanelIndicator = panelIndicator;
            OTank.PanelSequence = panelSequence;

            OTank.ControlName = WorkingEnvironmentFunction.CreateControlName("tank", "indicator", tbControlPanelAssignment);
            SubscribeIndicator(OTank);
            OTank.GetControlHandle().Caption = OTank.ControlName;

            SubscribeObjectTreeview(OTank, "indicator");
        }

        private void meterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ObjectMeter OMeter = new ObjectMeter();
            //OMeter.ParentWindow = MainForm;
            //OMeter.ParentUserControl = this;

            OMeter.TbControlPanelAssignment = tbControlPanelAssignment;
            OMeter.VGridMainForm = MainForm.vGridControlProperties;
            OMeter.TreeViewSolutionList = MainForm.treeViewSolutionList;
            OMeter.PanelIndicator = panelIndicator;
            OMeter.PanelSequence = panelSequence;

            OMeter.ControlName = WorkingEnvironmentFunction.CreateControlName("meter", "indicator", tbControlPanelAssignment);
            SubscribeIndicator(OMeter);
            OMeter.GetControlHandle().Caption = OMeter.ControlName;

            SubscribeObjectTreeview(OMeter, "indicator");
        }

        private void valueDisplayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DevExpress.XtraGauges.Win.GaugeControl gaugeControl = new DevExpress.XtraGauges.Win.GaugeControl();
            DevExpress.XtraGauges.Win.Gauges.Digital.DigitalGauge digitalGauge = new DevExpress.XtraGauges.Win.Gauges.Digital.DigitalGauge();
            DevExpress.XtraGauges.Win.Gauges.Digital.DigitalBackgroundLayerComponent digitalBackgroundLayerComponent = new DevExpress.XtraGauges.Win.Gauges.Digital.DigitalBackgroundLayerComponent();

            gaugeControl.AutoLayout = false;
            gaugeControl.Gauges.AddRange(new DevExpress.XtraGauges.Base.IGauge[] {
            digitalGauge});
            gaugeControl.Location = new System.Drawing.Point(221, 79);
            gaugeControl.Name = "gaugeControl";
            gaugeControl.Size = new System.Drawing.Size(229, 86);
            gaugeControl.TabIndex = 0;

            digitalGauge.AppearanceOff.ContentBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:#FFC0C0");
            digitalGauge.AppearanceOn.ContentBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:#A40000");
            digitalGauge.BackgroundLayers.AddRange(new DevExpress.XtraGauges.Win.Gauges.Digital.DigitalBackgroundLayerComponent[] {
            digitalBackgroundLayerComponent});
            digitalGauge.Bounds = new System.Drawing.Rectangle(3, 2, 221, 80);
            digitalGauge.DigitCount = 5;
            digitalGauge.Name = "digitalGauge";
            digitalGauge.Text = "1802";

            digitalBackgroundLayerComponent.BottomRight = new DevExpress.XtraGauges.Core.Base.PointF2D(259.8125F, 99.9625F);
            digitalBackgroundLayerComponent.Name = "digitalBackgroundLayerComponent";
            digitalBackgroundLayerComponent.ShapeType = DevExpress.XtraGauges.Core.Model.DigitalBackgroundShapeSetType.Style12;
            digitalBackgroundLayerComponent.TopLeft = new DevExpress.XtraGauges.Core.Base.PointF2D(20F, 0F);
            digitalBackgroundLayerComponent.ZOrder = 1000;

            ObjectIndicator indicatorAdd = new ObjectIndicator(gaugeControl);
            //indicatorAdd.ParentUserControl = this;
            SubscribeIndicator(indicatorAdd);
        }

        private void gaugeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ObjectGauge OGauge = new ObjectGauge();
            //OGauge.ParentWindow = MainForm;
            //OGauge.ParentUserControl = this;

            OGauge.TbControlPanelAssignment = tbControlPanelAssignment;
            OGauge.VGridMainForm = MainForm.vGridControlProperties;
            OGauge.TreeViewSolutionList = MainForm.treeViewSolutionList;
            OGauge.PanelIndicator = panelIndicator;
            OGauge.PanelSequence = panelSequence;

            OGauge.ControlName = WorkingEnvironmentFunction.CreateControlName("gauge", "indicator", tbControlPanelAssignment);
            SubscribeIndicator(OGauge);
            OGauge.GetControlHandle().Caption = OGauge.ControlName;

            SubscribeObjectTreeview(OGauge, "indicator");
        }

        private void thermometerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ObjectThermometer OThermometer = new ObjectThermometer();
            //OThermometer.ParentWindow = MainForm;
            //OThermometer.ParentUserControl = this;

            OThermometer.TbControlPanelAssignment = tbControlPanelAssignment;
            OThermometer.VGridMainForm = MainForm.vGridControlProperties;
            OThermometer.TreeViewSolutionList = MainForm.treeViewSolutionList;
            OThermometer.PanelIndicator = panelIndicator;

            OThermometer.PanelSequence = panelSequence;
            OThermometer.ControlName = WorkingEnvironmentFunction.CreateControlName("thermometer", "indicator", tbControlPanelAssignment);
            SubscribeIndicator(OThermometer);
            OThermometer.GetControlHandle().Caption = OThermometer.ControlName;

            SubscribeObjectTreeview(OThermometer, "indicator");
        }

        private void videoCaptureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ObjectVideo OVideo = new ObjectVideo();
            OVideo.OpenFormCamera();
            //OVideo.ParentWindow = MainForm;
            //OVideo.ParentUserControl = this;

            OVideo.TbControlPanelAssignment = tbControlPanelAssignment;
            OVideo.VGridMainForm = MainForm.vGridControlProperties;
            OVideo.TreeViewSolutionList = MainForm.treeViewSolutionList;
            OVideo.PanelIndicator = panelIndicator;
            OVideo.PanelSequence = panelSequence;

            OVideo.ObjectClickedEvent += new ObjectSignal.ObjectClick(ObjectControlClick);
            OVideo.ObjectMouseDownEvent += new ObjectSignal.ObjectMouseDown(ObjectControlMouseDown);

            OVideo.ControlTypeName = ControlTypeNames.Video;
            OVideo.ControlName = WorkingEnvironmentFunction.CreateControlName("videoCapture", "indicator", tbControlPanelAssignment);
            SubscribeIndicator(OVideo);

            SubscribeObjectTreeview(OVideo, "indicator");
        }

        private void signalGeneratorToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void digitalSignalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ObjectSignalGenerator OSignalGenerator = new ObjectSignalGenerator(10, 10, 0, 400, 400, "Sine");
            OSignalGenerator.ObjectClickedEvent += new ObjectSignal.ObjectClick(ObjectControlClick);
            OSignalGenerator.ObjectMouseDownEvent += new ObjectSignal.ObjectMouseDown(ObjectControlMouseDown);
            //OSignalGenerator.ParentWindow = MainForm;
            //OSignalGenerator.ParentUserControl = this;

            OSignalGenerator.TbControlPanelAssignment = tbControlPanelAssignment;
            OSignalGenerator.VGridMainForm = MainForm.vGridControlProperties;
            OSignalGenerator.TreeViewSolutionList = MainForm.treeViewSolutionList;
            OSignalGenerator.PanelIndicator = panelIndicator;
            OSignalGenerator.PanelSequence = panelSequence;
            OSignalGenerator.TbLineSequence = TbLineSequence;
            OSignalGenerator.ShapeContainer = shapeContainer;

            OSignalGenerator.ControlTypeName = ControlTypeNames.SignalGenerator;
            OSignalGenerator.ControlName = WorkingEnvironmentFunction.CreateControlName("signalGenerator", "sequence", tbControlPanelAssignment);
            SubscribeSequence(OSignalGenerator);
            OSignalGenerator.SequenceTextbox.Text = OSignalGenerator.ControlName;

            SubscribeObjectTreeview(OSignalGenerator, "sequence");
            
        }

        

        private void signalGeneratorToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            ObjectDAQ OSignalGenerator = new ObjectDAQ();
            //OSignalGenerator.ParentWindow = MainForm;
            //OSignalGenerator.ParentUserControl = this;
            OSignalGenerator.ControlTypeName = ControlTypeNames.DeviceNI;
            OSignalGenerator.ControlName = WorkingEnvironmentFunction.CreateControlName("DAQDevice", "sequence", tbControlPanelAssignment);
            SubscribeSequence(OSignalGenerator);
            OSignalGenerator.SequenceTextbox.Text = OSignalGenerator.ControlName;

            SubscribeObjectTreeview(OSignalGenerator, "sequence");
        }

        private void fFTToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ObjectFFT OFFT = new ObjectFFT();
            //OFFT.ParentWindow = MainForm;
            //OFFT.ParentUserControl = this;

            OFFT.TbControlPanelAssignment = tbControlPanelAssignment;
            OFFT.VGridMainForm = MainForm.vGridControlProperties;
            OFFT.TreeViewSolutionList = MainForm.treeViewSolutionList;
            OFFT.PanelIndicator = panelIndicator;
            OFFT.PanelSequence = panelSequence;
            OFFT.TbLineSequence = TbLineSequence;
            OFFT.ShapeContainer = shapeContainer;

            OFFT.ObjectClickedEvent += new ObjectSignal.ObjectClick(ObjectControlClick);
            OFFT.ObjectMouseDownEvent += new ObjectSignal.ObjectMouseDown(ObjectControlMouseDown);

            OFFT.ControlTypeName = ControlTypeNames.FFT;
            OFFT.ControlName = WorkingEnvironmentFunction.CreateControlName("FFT", "sequence", tbControlPanelAssignment);
            SubscribeSequence(OFFT);
            OFFT.SequenceTextbox.Text = OFFT.ControlName;
            
            SubscribeObjectTreeview(OFFT, "sequence");
            OFFT.InitFormFFT();
        }

        private void audioInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ObjectAudioIn OAudio = new ObjectAudioIn();
            //OAudio.ParentWindow = MainForm;
            //OAudio.ParentUserControl = this;

            OAudio.TbControlPanelAssignment = tbControlPanelAssignment;
            OAudio.VGridMainForm = MainForm.vGridControlProperties;
            OAudio.TreeViewSolutionList = MainForm.treeViewSolutionList;
            OAudio.PanelIndicator = panelIndicator;
            OAudio.PanelSequence = panelSequence;
            OAudio.TbLineSequence = TbLineSequence;
            OAudio.ShapeContainer = shapeContainer;

            OAudio.ControlTypeName = ControlTypeNames.AudioIn;
            OAudio.ObjectClickedEvent += new ObjectSignal.ObjectClick(ObjectControlClick);
            OAudio.ObjectMouseDownEvent += new ObjectSignal.ObjectMouseDown(ObjectControlMouseDown);
            OAudio.ControlName = WorkingEnvironmentFunction.CreateControlName("AudioIn", "sequence", tbControlPanelAssignment);
            SubscribeSequence(OAudio);
            OAudio.SequenceTextbox.Text = OAudio.ControlName;
            SubscribeObjectTreeview(OAudio, "sequence");
            OAudio.InitRecording();
        }

        private void nIDeviceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ObjectDeviceNI ODeviceNI = new ObjectDeviceNI();
            //ODeviceNI.ParentWindow = MainForm;
            //ODeviceNI.ParentUserControl = this;

            ODeviceNI.TbControlPanelAssignment = tbControlPanelAssignment;
            ODeviceNI.VGridMainForm = MainForm.vGridControlProperties;
            ODeviceNI.TreeViewSolutionList = MainForm.treeViewSolutionList;
            ODeviceNI.PanelIndicator = panelIndicator;
            ODeviceNI.PanelSequence = panelSequence;
            ODeviceNI.TbLineSequence = TbLineSequence;
            ODeviceNI.ShapeContainer = shapeContainer;

            ODeviceNI.ObjectClickedEvent += new ObjectSignal.ObjectClick(ObjectControlClick);
            ODeviceNI.ObjectMouseDownEvent += new ObjectSignal.ObjectMouseDown(ObjectControlMouseDown);

            ODeviceNI.ControlTypeName = ControlTypeNames.DeviceNI;
            ODeviceNI.ControlName = WorkingEnvironmentFunction.CreateControlName("DeviceNI", "sequence", tbControlPanelAssignment);
            SubscribeSequence(ODeviceNI);
            ODeviceNI.SequenceTextbox.Text = ODeviceNI.ControlName;
            SubscribeObjectTreeview(ODeviceNI, "sequence");
            ODeviceNI.InitForm();
        }

        void SequenceObject_Click(object sender, EventArgs e)
        {
            //MainForm.label1.Text = SequenceObjectList[IndexSequenceObjectSelected].ControlName;
            //MainForm.label2.Text = IndexSequenceObjectSelected.ToString();
            //MainForm.label3.Text = SequenceObjectList[IndexSequenceObjectSelected].ControlType.ToString();
        }

        private void Indicator_Click(object sender, EventArgs e)
        {
            //MainForm.label1.Text = IndicatorList[IndexIndicatorControlSelected].ControlName;
            //MainForm.label2.Text = IndexIndicatorControlSelected.ToString();
            //MainForm.label3.Text = IndicatorList[IndexIndicatorControlSelected].ControlType.ToString();
        }

        public void UnassignIndicatorFocus()
        {
            if (IndicatorList.Count() - 1 >= IndexIndicatorControlSelected)
            {
                IndicatorList[IndexIndicatorControlSelected].ControlBackColor = SystemColors.Control;
                IndicatorList[IndexIndicatorControlSelected].ControlSelected = false;
            }
        }

        public void UnassignSequenceObjectFocus()
        {
            if (SequenceObjectList.Count() - 1 >= IndexSequenceObjectSelected)
            {
                SequenceObjectList[IndexSequenceObjectSelected].ControlBackColor = SystemColors.Control;
                SequenceObjectList[IndexSequenceObjectSelected].ControlSelected = false;
            }
        }

        public void UnassignControlFocus()
        {
            if (WorkingEnvironmentFunction.GetSelectedControlIndex(tbControlPanelAssignment) > -1)
            {
                ObjectSignal ControlChangeFocus = (ObjectSignal)(tbControlPanelAssignment.Rows[WorkingEnvironmentFunction.GetSelectedControlIndex(tbControlPanelAssignment)]["control"]);

                ControlChangeFocus.ControlBackColor = ControlChangeFocus.ControlBackColorUnfocus;
                ControlChangeFocus.ControlSelected = false;
            }
        }

        public void DeleteIndicatoronPanel(int indexIndicator) 
        {
            IndicatorList.RemoveAt(indexIndicator);
            RearrangeIndexList(indexIndicator, "indicator");
            panelIndicator.Controls.RemoveAt(indexIndicator);
        }

        public void DeleteSequenceOnPanel(int indexSequence)
        {
            SequenceObjectList.RemoveAt(indexSequence);
            RearrangeIndexList(indexSequence, "sequence");

            panelSequence.Controls.RemoveAt(indexSequence);
        }

        public void RearrangeIndexList(int indexStart, string listIdentifier)
        {
            switch (listIdentifier)
            {
                case "indicator":
                    for (int i = indexStart; i < IndicatorList.Count(); i++)
                    {
                        IndicatorList[i].IndexList = IndicatorList[i].IndexList - 1;
                    }
                    break;
                case "sequence":
                    for (int i = indexStart; i < SequenceObjectList.Count(); i++)
                    {
                        SequenceObjectList[i].IndexList = SequenceObjectList[i].IndexList - 1;
                    }
                    break;
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteIndicatoronPanel(IndexIndicatorControlSelected);
        }

        private void panelSequence_ControlAdded(object sender, ControlEventArgs e)
        {
            //ToolStripMenuItem itemMenu = contextMenuStripIndicator.Items[1] as ToolStripMenuItem;
            //itemMenu.DropDownItems.Add("Output Signal Generator " + (SequenceObjectList.Count - 1).ToString());
            //itemMenu.DropDownItems[itemMenu.DropDownItems.Count - 1].Click += new EventHandler(contextMenuStripIndicatorOutput_Click);
            
        }

        void contextMenuStripIndicatorOutput_Click(object sender, EventArgs e)
        {
            //ToolStripItem Item = e.ClickedItem;
            IndicatorList[IndexIndicatorControlSelected].SequenceSource = SequenceObjectList[0];
        }

        public void SetSelectedControl(object control)
        {
            for (int i = 0; i < tbControlPanelAssignment.Rows.Count; i++)
            {
                if (((ObjectSignal)tbControlPanelAssignment.Rows[i]["control"]).ControlName == ((ObjectSignal)control).ControlName)
                {
                    int result = WorkingEnvironmentFunction.GetSelectedControlIndex(tbControlPanelAssignment);
                    if (result != -1)
                    {
                        tbControlPanelAssignment.Rows[result]["isSelected"] = 0;
                    }
                    tbControlPanelAssignment.Rows[i]["isSelected"] = 1;
                }
            }
        }

        //public int GetSelectedControlIndex()
        //{
        //    int result = -1;
        //    for (int i = 0; i < tbControlPanelAssignment.Rows.Count; i++)
        //    {
        //        if (tbControlPanelAssignment.Rows[i]["isSelected"].ToString() == "1")
        //        {
        //            result = i;
        //            break;
        //        }
        //    }
        //    return result;
        //}

        private ObjectSignal GetControlByName(string name)
        {
            ObjectSignal result = null;

            for (int i = 0; i < tbControlPanelAssignment.Rows.Count; i++)
            {
                if (((ObjectSignal)tbControlPanelAssignment.Rows[i]["control"]).ControlName == name)
                {
                    result = (ObjectSignal)tbControlPanelAssignment.Rows[i]["control"];
                    break;
                }
            }

            return result;
        }

        //public void RearrangeIndexDatatable(DataTable tbArrange)
        //{
        //    for (int i = 0; i < tbArrange.Rows.Count; i++)
        //    {
        //        tbArrange.Rows[i]["index"] = i;
        //    }
        //}

        //public void RemoveItemDatatableProperties(int index, DataTable tbRemove)
        //{
        //    tbRemove.Rows.RemoveAt(index);
        //    RearrangeIndexDatatable(tbRemove);
        //}

        private ControlTypeNames? CheckObjectFFTHasSequence(ObjectFFT oFFT)
        {
            if (oFFT.SequenceSource != null)
            {
                switch (oFFT.SequenceSource.ControlTypeName)
                {
                    case ControlTypeNames.AudioIn:
                        return oFFT.SequenceSource.ControlTypeName;
                        break;
                    case ControlTypeNames.DeviceNI:
                        return oFFT.SequenceSource.ControlTypeName;
                        break;
                }
            }
            else
            {
                return null;
            }
            return null;
        }
        public void StartRecording()
        {
            if (tbControlPanelAssignment.Rows.Count > 0)
            {
                isRecording = true;
                for (int i = 0; i < tbControlPanelAssignment.Rows.Count; i++)
                {
                    ((ObjectSignal)tbControlPanelAssignment.Rows[i]["control"]).IsRecording = true;
                }

                string DirectoryNameforRecord = MainForm.SigsenceProject.RecordLocationPath + @"\" + 
                    EnvironmentFunction.CreateFileNameByDateTime("Record", "");
                Directory.CreateDirectory(DirectoryNameforRecord);
                OBRecordWrap = new ObjectRecordWrap(DirectoryNameforRecord + @"\" + 
                    EnvironmentFunction.CreateFileNameByDateTime("Record", "xml"));

                for (int i = 0; i < tbControlPanelAssignment.Rows.Count; i++)
                {
                    ObjectSignal OSignal = (ObjectSignal)tbControlPanelAssignment.Rows[i]["control"];
                    if (OSignal.ControlTypeName == ControlTypeNames.Video)
                    {
                        ObjectRecordAVI OBRecordAvi= new ObjectRecordAVI(DirectoryNameforRecord + @"\" +
                            EnvironmentFunction.CreateFileNameByDateTime(OSignal.ControlName + "_Record", "avi"),
                            MainForm.SigsenceProject.LocationPath + @"\" + 
                            EnvironmentFunction.CreateFileNameByDateTime(OSignal.ControlName + "_Record", "txt"),
                            OSignal.ControlName, "video");

                        ((ObjectVideo)OSignal).ObRecord = OBRecordAvi;
                    }

                    if (OSignal.ControlTypeName == ControlTypeNames.AudioIn)
                    {
                        ObjectRecordWAV OBRecordWav = new ObjectRecordWAV(DirectoryNameforRecord + @"\" +
                            EnvironmentFunction.CreateFileNameByDateTime(OSignal.ControlName + "_Record", "wav"),
                            MainForm.SigsenceProject.LocationPath + @"\" +
                            EnvironmentFunction.CreateFileNameByDateTime(OSignal.ControlName + "_Record", "txt"),
                            OSignal.ControlName, "signal");

                        ((ObjectAudioIn)OSignal).ObRecord = OBRecordWav;
                        ((ObjectAudioIn)OSignal).ObRecord.SampleRate = ((ObjectAudioIn)OSignal)._audioFrameSize;
                        ((ObjectAudioIn)OSignal).ObRecord.BitSample = ((ObjectAudioIn)OSignal)._audioBitsPerSample;
                        ((ObjectRecordWAV)((ObjectAudioIn)OSignal).ObRecord).InitRecordingToolsWav();
                        
                    }

                    if (OSignal.ControlTypeName == ControlTypeNames.DeviceNI)
                    {
                        ObjectRecordWAV OBRecordWav = new ObjectRecordWAV(DirectoryNameforRecord + @"\" +
                            EnvironmentFunction.CreateFileNameByDateTime(OSignal.ControlName + "_Record", "wav"),
                            MainForm.SigsenceProject.LocationPath + @"\" +
                            EnvironmentFunction.CreateFileNameByDateTime(OSignal.ControlName + "_Record", "txt"),
                            OSignal.ControlName, "signal");

                        ((ObjectDeviceNI)OSignal).ObRecord = OBRecordWav;
                        ((ObjectDeviceNI)OSignal).ObRecord.SampleRate = ((ObjectDeviceNI)OSignal).SampleRate;
                        ((ObjectDeviceNI)OSignal).ObRecord.BitSample = 32;//Convert.ToInt16(((ObjectDeviceNI)OSignal).BitsPerSample);
                        ((ObjectRecordWAV)((ObjectDeviceNI)OSignal).ObRecord).InitRecordingToolsWav();

                    }
                    if (OSignal.ControlTypeName == ControlTypeNames.FFT)
                    {
                        ControlTypeNames? checkFFT = CheckObjectFFTHasSequence((ObjectFFT)OSignal);

                        ObjectRecordWAV OBRecordWav = new ObjectRecordWAV(DirectoryNameforRecord + @"\" +
                            EnvironmentFunction.CreateFileNameByDateTime(OSignal.ControlName + "_Record", "wav"),
                            MainForm.SigsenceProject.LocationPath + @"\" +
                            EnvironmentFunction.CreateFileNameByDateTime(OSignal.ControlName + "_Record", "txt"),
                            OSignal.ControlName, "signal");

                        ((ObjectFFT)OSignal).ObRecord = OBRecordWav;
                        switch(checkFFT)
                        {
                            case ControlTypeNames.AudioIn:
                                ((ObjectFFT)OSignal).ObRecord.SampleRate = 
                                    ((ObjectAudioIn)(((ObjectFFT)OSignal).SequenceSource))._audioFrameSize;
                                ((ObjectFFT)OSignal).ObRecord.BitSample = 
                                    ((ObjectAudioIn)(((ObjectFFT)OSignal).SequenceSource))._audioBitsPerSample;
                                break;
                            case ControlTypeNames.DeviceNI:
                                ((ObjectFFT)OSignal).ObRecord.SampleRate = 
                                    ((ObjectDeviceNI)(((ObjectFFT)OSignal).SequenceSource)).SampleRate;
                                ((ObjectFFT)OSignal).ObRecord.BitSample =
                                    Convert.ToInt16(((ObjectDeviceNI)(((ObjectFFT)OSignal).SequenceSource)).BitsPerSample);
                                break;
                        }
                        ((ObjectRecordWAV)((ObjectFFT)OSignal).ObRecord).InitRecordingToolsWav();

                    }
                }

                OBRecordWrap.RecordDate = DateTime.Now;
                OBRecordWrap.RecordTimeStart = DateTime.Now;
            }
        }

        public void StopRecording()
        {
            isRecording = false;
            ObjectVideo oVideo = null;
            ObjectAudioIn oAudio = null;
            for (int i = 0; i < tbControlPanelAssignment.Rows.Count; i++)
            {
                ((ObjectSignal)tbControlPanelAssignment.Rows[i]["control"]).IsRecording = false;
            }

            for (int i = 0; i < tbControlPanelAssignment.Rows.Count; i++)
            {
                ObjectSignal OBSignal = ((ObjectSignal)tbControlPanelAssignment.Rows[i]["control"]);
                switch(OBSignal.ControlTypeName)
                {
                    case ControlTypeNames.Video:
                        oVideo = ((ObjectVideo)tbControlPanelAssignment.Rows[i]["control"]);
                        oVideo.dataArriveCount = 0;
                        ((ObjectRecordAVI)oVideo.ObRecord).TempFileStream.Close();
                        Thread.Sleep(1500);
                        ((ObjectRecordAVI)oVideo.ObRecord).TransferTempRecordFile();

                        foreach (DataRow row in oVideo.ObRecord.TbResourceFile.Rows)
                        {
                            OBRecordWrap.TbResourceFile.Rows.Add(row.ItemArray);
                        }

                        break;

                    case ControlTypeNames.AudioIn:
                        oAudio = ((ObjectAudioIn)tbControlPanelAssignment.Rows[i]["control"]);
                        //((ObjectRecordWAV)oAudio.ObRecord).WavCreator.Close();
                        ((ObjectRecordWAV)oAudio.ObRecord).WAVWriter.Close();
                        foreach (DataRow row in oAudio.ObRecord.TbResourceFile.Rows)
                        {
                            OBRecordWrap.TbResourceFile.Rows.Add(row.ItemArray);
                        }

                        break;

                    case ControlTypeNames.DeviceNI:
                        ObjectDeviceNI oDeviceNI = ((ObjectDeviceNI)tbControlPanelAssignment.Rows[i]["control"]);
                        //((ObjectRecordWAV)oAudio.ObRecord).WavCreator.Close();
                        ((ObjectRecordWAV)oDeviceNI.ObRecord).WAVWriter.Close();
                        foreach (DataRow row in oDeviceNI.ObRecord.TbResourceFile.Rows)
                        {
                            OBRecordWrap.TbResourceFile.Rows.Add(row.ItemArray);
                        }

                        break;

                    case ControlTypeNames.FFT:
                        ObjectFFT oFFT = ((ObjectFFT)tbControlPanelAssignment.Rows[i]["control"]);
                        //((ObjectRecordWAV)oAudio.ObRecord).WavCreator.Close();
                        ((ObjectRecordWAV)oFFT.ObRecord).WAVWriter.Close();
                        foreach (DataRow row in oFFT.ObRecord.TbResourceFile.Rows)
                        {
                            OBRecordWrap.TbResourceFile.Rows.Add(row.ItemArray);
                        }

                        break;
                }
                OBRecordWrap.RecordTimeEnd = DateTime.Now;
                OBRecordWrap.CreateStructureRecordingFile();
            }
        }

        public void CreateRecordingFile()
        {
            //Create xml recording file

            //Create tdms file

            //Create the avi file
        }
    }
}
