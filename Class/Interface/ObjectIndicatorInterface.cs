using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sigsence.ApplicationElements;
using Sigsence.ApplicationFunction;
using Sigsence.ProgrammingInterface;

namespace DSA_Teaser.Class.Interface
{
    public class ObjectIndicatorInterface : Sigsence.ApplicationElements.ObjectIndicator, ISigsenceInterface
    {

        #region ISigsenceInterface Members

        public event CallInitFormEventHandler CallInitFormEvent;

        public event ImplementUserControlEventHandler ImplementUserControlEventInf;

        public event ObjectClickEventHandler ObjectClickEventInf;

        public event ObjectDoubleClickEventHandler ObjectDoubleClickEventInf;

        public event ObjectMouseDownEventHandler ObjectMouseDownEventInf;

        public event ObjectMouseMoveEventHandler ObjectMouseMoveEventInf;

        public event ObjectMouseUpEventHandler ObjectMouseUpEventInf;

        public event UpdateFromGridCategoryEventHandler UpdateFromGridCategoryEventInf;

        public void InitializeIconSequence(string imagePath)
        {
            throw new NotImplementedException();
        }

        public object RetrieveVGridCategoryRowValue(string categoryRow, string editorFieldName)
        {
            throw new NotImplementedException();
        }

        public double[] SigsenceSignal
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void SubscribeComboBoxRowToVGrid(string categoryRow, string fieldName, string caption, object value, object[] option)
        {
            throw new NotImplementedException();
        }

        public void SubscribeEditorRowToVGrid(string categoryRow, string fieldName, string caption, object value)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region ISigsenceInterface Members

        public event AssignCustomPropertiesPanelEventHandler AssignCustomPropertiesPanelEventInf;

        public event CallInitFormEventHandler CallInitFormEventInf;

        public event ProcessSequenceSourceEventHandler ProcessSequenceSourceInf;

        #endregion

        #region ISigsenceInterface Members


        public double[] SigsenceSequenceInput
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        #region ISigsenceInterface Members


        public event SequenceDataUpateEventHandler SequenceDataUpdateEventInf;

        #endregion

        #region ISigsenceInterface Members


        public double[] SigsenceSequenceSourceData
        {
            get { throw new NotImplementedException(); }
        }

        public System.Data.DataTable tbObjectList
        {
            get { throw new NotImplementedException(); }
        }

        public void GetAvailableObjectSequence()
        {
            throw new NotImplementedException();
        }

        List<string> ISigsenceInterface.GetAvailableObjectSequence()
        {
            throw new NotImplementedException();
        }

        public void SetSequenceSource(string controlName)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
