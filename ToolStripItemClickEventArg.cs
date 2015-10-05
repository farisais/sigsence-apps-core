using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DSA_Teaser
{
    class ToolStripItemClickEventArgs : EventArgs
    {
        public ToolStripItemClickEventArgs(int _itemClick)
        {
            itemClick = _itemClick;
        }
        private int itemClick;
        public int ItemClick
        {
            get
            {
                return itemClick;
            }
            set
            {
                itemClick = value;
            }
        }
    }
}
