using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Threading;

namespace DSA_Teaser
{
    
    class ToolStripItemCust : ToolStripItem
    {
        public delegate void ItemClickUpdate(object sender, ToolStripItemEventArgs e);
        public event ItemClickUpdate UpdateItem;

        public ToolStripItemCust()
        {
            this.Click += new EventHandler(ToolStripItemCust_Click);
        }

        void ToolStripItemCust_Click(object sender, EventArgs e)
        {
            //UpdateItem(this, new ToolStripItemClickEventArgs(this.
        }
    }
}
