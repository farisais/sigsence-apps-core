using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DSA_Teaser.Class.UI
{
    public class ProjectEnvirontment
    {
        private string projectName;
        public string ProjectName
        {
            get
            {
                return projectName;
            }
            set
            {
                projectName = value;
            }
        }

        private string fileName;
        public string FileName
        {
            get
            {
                return fileName;
            }
            set
            {
                fileName = value;
            }
        }

        private string locationPath;
        public string LocationPath
        {
            get
            {
                return locationPath;
            }
            set
            {
                locationPath = value;
            }
        }

        private string recordLocationPath;
        public string RecordLocationPath
        {
            get
            {
                return recordLocationPath;
            }
            set
            {
                recordLocationPath = value;
            }
        }

        private string projectType;
        public string ProjectType
        {
            get
            {
                return projectType;
            }
            set
            {
                projectType = value;
            }
        }

        private List<WorkingPanel> listWorkingPanel;
        public List<WorkingPanel> ListWorkingPanel
        {
            get
            {
                return listWorkingPanel;
            }
            set
            {
                listWorkingPanel = value;
            }
        }

        private DateTime projectCreationDate;
        public DateTime ProjectCreationDate
        {
            get
            {
                return projectCreationDate;
            }
            set
            {
                projectCreationDate = value;
            }
        }
    }
}
