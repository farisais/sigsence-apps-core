using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DSA_Teaser
{
    public class IndicatorPanel: Control
    {
        public IndicatorPanel()
        {

        }

        private IndicatorCollection collection;
        public IndicatorCollection Collection
        {
            get
            {
                return collection;
            }
            set
            {
                collection = value;
            }
        }
    }
}
