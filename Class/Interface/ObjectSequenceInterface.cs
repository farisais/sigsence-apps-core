using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sigsence.ApplicationElements;
using Sigsence.ApplicationFunction;
using Sigsence.ProgrammingInterface;
using Sigsence.ProgrammingInterface.Extend;

namespace DSA_Teaser.Class.Interface
{
    public class ObjectSequenceInterface: ObjectSequence, ISigsenceInterface
    {
        public ObjectSequenceInterface(string imagePath)
            : base()
        {
            InitializeIcon(imagePath);
        }

        public ObjectSequenceInterface(object control)
            : base(control)
        {

        }

        public override void AssignCustomPropertiesPanel()
        {
            base.AssignCustomPropertiesPanel();
            if (AssignCustomPropertiesPanelEventInf != null)
            {
                AssignCustomPropertiesPanelEventInf();
            }
        }

        public override void UpdateFromGridCategory(string fieldName)
        {
            base.UpdateFromGridCategory(fieldName);
            if (UpdateFromGridCategoryEventInf != null)
            {
                UpdateFromGridCategoryEventInf(fieldName);
            }
        }

        protected override void ImplementUserControl()
        {
            base.ImplementUserControl();
        }

        protected override void Control_Click(object sender, EventArgs e)
        {
            base.Control_Click(sender, e);
            if (ObjectClickEventInf != null)
            {
                ObjectClickEventInf(sender, e);
            }
        }

        protected override void Control_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            base.Control_MouseDown(sender, e);
            if (ObjectMouseDownEventInf != null)
            {
                ObjectMouseDownEventInf(sender, e);
            }
        }

        protected override void Control_DoubleClick(object sender, EventArgs e)
        {
            base.Control_DoubleClick(sender, e);
            if (ObjectDoubleClickEventInf != null)
            {
                ObjectDoubleClickEventInf(sender, e);
            }
        }

        protected override void Control_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            base.Control_MouseMove(sender, e);
            if (ObjectMouseMoveEventInf != null)
            {
                ObjectMouseMoveEventInf(sender, e);
            }
        }

        protected override void Control_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            base.Control_MouseUp(sender, e);
            if (ObjectMouseUpEventInf != null)
            {
                ObjectMouseUpEventInf(sender, e);
            }
        }

        public void CallInitForm()
        {
            CallInitFormEventInf();
        }

        private string resourceImage;
        public string ResourceImage
        {
            get
            {
                return resourceImage;
            }
            set
            {
                resourceImage = value;
            }
        }

        protected override void AssignSequence()
        {
            base.AssignSequence();
            ProcessSequenceSourceInf();
            SequenceSource.UpdateData += new DataUpdate(SequenceSource_UpdateData);
        }

        void SequenceSource_UpdateData(object sender, OSequenceDataUpdateEventArgs e)
        {
            SequenceDataUpdateEventInf(sender, new ObjDataUpdateEventArgsInf(e.DataUpdate));
        }

        #region ISigsenceInterface Members

        public event ImplementUserControlEventHandler ImplementUserControlEventInf;

        public void InitializeIconSequence(string imagePath)
        {
            InitializeIcon(imagePath);
        }

        public event ObjectClickEventHandler ObjectClickEventInf;

        public event ObjectDoubleClickEventHandler ObjectDoubleClickEventInf;

        public event ObjectMouseDownEventHandler ObjectMouseDownEventInf;

        public event ObjectMouseMoveEventHandler ObjectMouseMoveEventInf;

        public event ObjectMouseUpEventHandler ObjectMouseUpEventInf;

        public object RetrieveVGridCategoryRowValue(string categoryRow, string editorFieldName)
        {
            object result = GetVGridCategoryRowValue(categoryRow, editorFieldName);
            return result;
        }

        public double[] SigsenceSignal
        {
            get
            {
                return SignalData;
            }
            set
            {
                SignalData = value;
            }
        }

        public void SubscribeComboBoxRowToVGrid(string categoryRow, string fieldName, string caption, object value, object[] option)
        {
            AddComboBoxRowToVGrid(categoryRow, fieldName, caption, value, option);
        }

        public void SubscribeEditorRowToVGrid(string categoryRow, string fieldName, string caption, object value)
        {
            AddEditorRowToVGrid(categoryRow, fieldName, caption, value);
        }

        public event UpdateFromGridCategoryEventHandler UpdateFromGridCategoryEventInf;

        public event AssignCustomPropertiesPanelEventHandler AssignCustomPropertiesPanelEventInf;

        public event CallInitFormEventHandler CallInitFormEventInf;

        public event ProcessSequenceSourceEventHandler ProcessSequenceSourceInf;

        public event SequenceDataUpateEventHandler SequenceDataUpdateEventInf;

        public double[] SigsenceSequenceSourceData
        {
            get { return SequenceSource.SignalData; }
        }

        public System.Data.DataTable tbObjectList
        {
            get { return TbControlPanelAssignment; }
        }

        public void SetSequenceSource(string controlName)
        {
            for (int i = 0; i < TbControlPanelAssignment.Rows.Count; i++)
            {
                if (((ObjectSequence)TbControlPanelAssignment.Rows[i]["control"]).ControlName == controlName)
                {
                    SequenceSource = (ObjectSequence)TbControlPanelAssignment.Rows[i]["control"];
                    break;
                }
            }
        }

        List<string> ISigsenceInterface.GetAvailableObjectSequence()
        {
            List<string> result = new List<string>();
            for (int i = 0; i < TbControlPanelAssignment.Rows.Count; i++)
            {
                if (((ObjectSignal)TbControlPanelAssignment.Rows[i]["control"]).ControlCategory == ControlCategories.Sequence)
                {
                    result.Add(((ObjectSequence)TbControlPanelAssignment.Rows[i]["control"]).ControlName);
                }
            }
            return result;
        }
        #endregion
    }
}
