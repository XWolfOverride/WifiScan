using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WifiScan
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            PopulateTree();
        }

        #region Tree Management

        private TreeNode GetNodeFor(WifiDevice dev)
        {
            foreach (TreeNode node in tvTree.Nodes)
                if (dev == node.Tag)
                    return node;
            TreeNode nnode = tvTree.Nodes.Add(dev.Name);
            nnode.Tag = dev;
            return nnode;
        }

        private TreeNode GetNodeFor(Wifi wifi, TreeNodeCollection nodes)
        {
            foreach (TreeNode node in nodes)
            {
                if (wifi == node.Tag)
                    return node;
            }
            TreeNode nnode = nodes.Add(wifi.SSID);
            nnode.Tag = wifi;
            nnode.ImageIndex = 1;
            nnode.SelectedImageIndex = 1;
            nnode.ForeColor = wifi.Color;
            return nnode;
        }

        private void PopulateTree()
        {
            ArrayList nodes = new ArrayList(tvTree.Nodes);
            foreach (WifiDevice dev in WifiDevice.List())
            {
                TreeNode node = GetNodeFor(dev);
                nodes.Remove(node);
                Wifi[] wifis = dev.Networks;
                ArrayList netnodes = new ArrayList(node.Nodes);
                foreach (Wifi wifi in wifis)
                {
                    TreeNode nwnode = GetNodeFor(wifi, node.Nodes);
                    netnodes.Remove(nwnode);
                }
                foreach (TreeNode nwdel in netnodes)
                    node.Nodes.Remove(nwdel);
            }
            // remove old
            foreach (TreeNode ndel in nodes)
                tvTree.Nodes.Remove(ndel);
            tvTree.ExpandAll();
            // Take snapshoot of current wifi status and deletes registrations
        }

        #endregion

        private void WifiTimer_Tick(object sender, EventArgs e)
        {
            WifiApi.Snapshoot();
            PopulateTree();
            scMain.Panel2.Invalidate();
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {
            WifiApi.Paint(e.Graphics, scMain.Panel2.ClientSize);
        }
    }
}
