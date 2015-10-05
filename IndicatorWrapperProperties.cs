using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace DSA_Teaser
{
    class IndicatorWrapperProperties
    {
        private Color lineColor;
        public Color LineColor
        {
            get
            {
                return lineColor;
            }
            set
            {
                lineColor = value;
            }
        }

        private Color backgroundColor;
        public Color BackgroundColor
        {
            get
            {
                return backgroundColor;
            }
            set
            {
                backgroundColor = value;
            }
        }

        private Point location;
        public Point Location
        {
            get
            {
                return location;
            }
            set
            {
                location = value;
            }
        }

        private int width;
        public int Width
        {
            get
            {
                return width;
            }
            set
            {
                width = value;
            }
        }

        private int height;
        public int Height
        {
            get
            {
                return height;
            }
            set
            {
                height = value;
            }
        }
    }
}
