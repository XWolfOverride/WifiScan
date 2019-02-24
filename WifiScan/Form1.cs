using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
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
            DblBuf();
            ShowWifi(null);
        }

        private void DblBuf()
        {
            var objMethodInfo = typeof(Control).GetMethod("SetStyle", BindingFlags.NonPublic | BindingFlags.Instance);

            var objArgs = new object[] { ControlStyles.AllPaintingInWmPaint |
                             ControlStyles.UserPaint |
                             ControlStyles.OptimizedDoubleBuffer, true };

            objMethodInfo.Invoke(scMain.Panel1, objArgs);
            objMethodInfo.Invoke(scMain.Panel2, objArgs);
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
                if (wifi.BSSID == ((Wifi)node.Tag).BSSID)
                {
                    node.Tag = wifi;
                    return node;
                }
            TreeNode nnode = nodes.Add(wifi.SSID);
            nnode.Tag = wifi;
            nnode.ImageIndex = 1;
            nnode.SelectedImageIndex = 1;
            nnode.ForeColor = wifi.Conf.Color;
            return nnode;
        }

        private TreeNode[] GetNodesFor(Wifi wifi)
        {
            List<TreeNode> result = new List<TreeNode>();
            foreach (TreeNode nodeHW in tvTree.Nodes)
            {
                TreeNodeCollection nodes = nodeHW.Nodes;
                foreach (TreeNode node in nodes)
                    if (wifi.BSSID == ((Wifi)node.Tag).BSSID)
                        result.Add(node);
            }
            return result.ToArray();
        }

        private void PopulateTree()
        {
            ArrayList nodes = new ArrayList(tvTree.Nodes);
            foreach (WifiDevice dev in WifiDevice.List())
            {
                TreeNode node = GetNodeFor(dev);
                bool empty = node.Nodes.Count == 0;
                nodes.Remove(node);
                Wifi[] wifis = dev.Networks;
                ArrayList netnodes = new ArrayList(node.Nodes);
                foreach (Wifi wifi in wifis)
                {
                    TreeNode nwnode = GetNodeFor(wifi, node.Nodes);
                    nwnode.ImageIndex = nwnode.SelectedImageIndex = 1;
                    nwnode.ForeColor = wifi.Conf.Color;
                    netnodes.Remove(nwnode);
                }
                foreach (TreeNode nwdel in netnodes)
                {
                    nwdel.ImageIndex = nwdel.SelectedImageIndex = 2;  //node.Nodes.Remove(nwdel);
                }
                if (empty && node.Nodes.Count > 0)
                    node.Expand();
            }
            // remove old
            foreach (TreeNode ndel in nodes)
                tvTree.Nodes.Remove(ndel);
        }

        #endregion

        private void ShowWifi(Wifi w)
        {
            pwinfo.Tag = w;
            if (w == null)
                pwinfo.Visible = false;
            else
            {
                WifiApi.HighlightBSSID = w.BSSID;
                lbInfo.Text = w.SSID;
                lbInfo2.Text = w.Active ? w.BSSID + ", Ch:" + w.Channel + ", Power:" + w.Signal : w.BSSID;
                pbSignal.Value = w.Active ? (int)w.Signal : 0;
                btColor.BackColor = w.Conf.Color;
                cbVisible.Checked = w.Conf.Visible;
                pwinfo.Visible = true;
            }
            scMain.Panel2.Invalidate();
        }

        private void UpdateNode(Wifi w)
        {
            if (w == null)
                return;
            TreeNode[] ns = GetNodesFor(w);
            foreach (TreeNode n in ns)
            {
                n.StateImageIndex = w.Conf.Visible ? -1 : 3;
                n.ForeColor = w.Conf.Color;
            }
        }

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

        private void tvTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (tvTree.SelectedNode == null)
                return;
            ShowWifi(tvTree.SelectedNode.Tag as Wifi);
        }

        private void btColor_Click(object sender, EventArgs e)
        {
            Wifi w = pwinfo.Tag as Wifi;
            if (w == null)
                return;
            ColorDialog cd = new ColorDialog();
            cd.Color = w.Conf.Color;
            if (cd.ShowDialog() == DialogResult.OK)
            {
                w.Conf.Color = cd.Color;
                ShowWifi(w);
                UpdateNode(w);
            }
        }

        private void cbVisible_CheckedChanged(object sender, EventArgs e)
        {
            Wifi w = pwinfo.Tag as Wifi;
            if (w == null)
                return;
            w.Conf.Visible = cbVisible.Checked;
            ShowWifi(w);
            UpdateNode(w);
        }
    }
}
