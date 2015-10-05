using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DSA_Teaser.Class.UI
{
    public class WorkingPanel
    {
        public WorkingPanel()
        {
            ucWorkingPanel = new UserControl();
        }
        public WorkingPanel(string _panelName, string _panelType)
        {
            panelName = _panelName;
            panelType = _panelType;
            switch (_panelType)
            {
                case "acquisition":
                    ucWorkingPanel = new UserControlWorkingPanel();
                    break;
                case "analysis":
                    ucWorkingPanel = new UserControlAnalysisPanel();
                    break;
                case "report":
                    ucWorkingPanel = new UserControlWorkingPanel();
                    break;
            }      
        }

        private string panelName;
        public string PanelName
        {
            get
            {
                return panelName;
            }
            set
            {
                panelName = value;
            }
        }

        private string panelType;
        public string PanelType
        {
            get
            {
                return panelType;
            }
            set
            {
                panelType = value;
            }
        }

        private UserControl ucWorkingPanel;
        public UserControl UCWorkingPanel
        {
            get
            {
                return ucWorkingPanel;
            }
            set
            {
                ucWorkingPanel = value;
            }
        }

        //private UserControlAnalysisPanel ucAnalysisPanel;
        //public UserControlAnalysisPanel UCAnalysisPanel
        //{
        //    get
        //    {
        //        return ucAnalysisPanel;
        //    }
        //    set
        //    {
        //        ucAnalysisPanel = value;
        //    }
        //}
    }
}
