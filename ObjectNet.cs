using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using Sigsence.ApplicationElements;

namespace DSA_Teaser
{
    class ObjectNet
    {
        public ObjectNet(string name, ObjectSequence inObj, ObjectSequence outObj, Control _ControlHandle)
        {
            netName = name;
            inputObject = inObj;
            inputObject.UpdatePosition += new ObjectSequence.ObjectPosistionMove(inputObject_UpdatePosition);

            outputObject = outObj;
            outputObject.UpdatePosition += new ObjectSequence.ObjectPosistionMove(outputObject_UpdatePosition);

            canvas = _ControlHandle.CreateGraphics();
        }

        void outputObject_UpdatePosition(object sender, ObjectPositionMoveEventArgs e)
        {
            
        }

        void inputObject_UpdatePosition(object sender, ObjectPositionMoveEventArgs e)
        {
            
        }

        private string netName;
        public string NetName
        {
            get
            {
                return netName;
            }
            set
            {
                netName = value;
            }
        }

        private Point startPoint;
        public Point StartPoint
        {
            get
            {
                return startPoint;
            }
            set
            {
                startPoint = value;
            }
        }

        private Point endPoint;
        public Point EndPoint
        {
            get
            {
                return endPoint;
            }
            set
            {
                endPoint = value;
            }
        }

        private ObjectSequence inputObject;
        public ObjectSequence InputObject
        {
            get
            {
                return inputObject;
            }
            set
            {
                inputObject = value;
            }
        }

        private ObjectSequence outputObject;
        public ObjectSequence OutputObject
        {
            get
            {
                return outputObject;
            }
            set
            {
                outputObject = value;
            }
        }

        private Graphics canvas;
        public Graphics Canvas
        {
            get
            {
                return canvas;
            }
            set
            {
                canvas = value;
            }
        }

        private Control canvasControlHandle;
        public Control CanvasControlHandle
        {
            get
            {
                return canvasControlHandle;
            }
            set
            {
                canvasControlHandle = value;
            }
        }

        private void CreateLine(Point lineStart, Point lineEnd)
        {

        }


    }
}
