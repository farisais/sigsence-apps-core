using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AviFile;

namespace DSA_Teaser
{
    public static class RecordingFunction
    {
        public static AviManager aviManager;
        public static VideoStream vStream;
        public static bool isStarted = false;
        public static bool isSampled = false;
        public static EditableVideoStream editableStream;
        public static bool CreateRecordingProject(string filename)
        {
            bool result = false;
            aviManager = new AviManager(filename, true);
            return result;
        }
        public static AviManager ReadRecordingProjectFile(string filename)
        {
            AviManager aviManager = null;

            return aviManager;
        }
    }
}
