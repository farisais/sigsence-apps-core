using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sigsence.ApplicationElements;

namespace DSA_Teaser.Class.UI
{
    class ObjectRecordTdms: ObjectRecording
    {
        public ObjectRecordTdms(string filename, string filenameTemp, string sequenceName, string _dataType) :
            base(filename, filenameTemp, sequenceName, _dataType)
        {
            recordFileName = filename;
            
        }
    }
}
