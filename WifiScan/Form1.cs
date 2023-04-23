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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WifiScan
{
    public partial class Form1 : Form
    {
        // Create a Font object for the node tags.
        Font tagFont = new Font("Helvetica", 7, FontStyle.Bold);

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
            objArgs = new object[] { ControlStyles.AllPaintingInWmPaint |
                             ControlStyles.OptimizedDoubleBuffer, true };
            objMethodInfo.Invoke(tvTree, objArgs);
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
            TreeNode nnode = nodes.Add(wifi.DisplayName);
            nnode.Tag = wifi;
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
            tvTree.BeginUpdate();
            ArrayList nodesToDelete = new ArrayList(tvTree.Nodes);// Fill with all (discard found later)
            foreach (WifiDevice dev in WifiDevice.List())
            {
                TreeNode node = GetNodeFor(dev);
                bool wasEmpty = node.Nodes.Count == 0;
                nodesToDelete.Remove(node);
                Wifi[] wifis = dev.Networks;
                ArrayList disabledNetNodes = new ArrayList(node.Nodes);// Fill with all (discard found later)
                foreach (Wifi wifi in wifis)
                {
                    TreeNode nwnode = GetNodeFor(wifi, node.Nodes);
                    if (nwnode.Text != wifi.DisplayName)
                        nwnode.Text = wifi.DisplayName; // Update network name (for configuration changes)
                    nwnode.ImageIndex = nwnode.SelectedImageIndex = 1;
                    disabledNetNodes.Remove(nwnode);
                }
                foreach (TreeNode nwdel in disabledNetNodes)
                {
                    nwdel.ImageIndex = nwdel.SelectedImageIndex = 2;
                }
                if (wasEmpty && node.Nodes.Count > 0)
                    node.Expand();
            }
            // remove old
            foreach (TreeNode ndel in nodesToDelete)
                tvTree.Nodes.Remove(ndel);
            tvTree.EndUpdate();
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
            }
            scMain.Panel2.Invalidate();
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

        private void tvTree_DoubleClick(object sender, EventArgs e)
        {
            if (tvTree.SelectedNode == null)
                return;
            Wifi w = tvTree.SelectedNode.Tag as Wifi;
            if (w == null)
                return;
            w.Conf.Visible = !w.Conf.Visible;
            UpdateNode(w);
        }

        private void tvTree_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                if (tvTree.SelectedNode == null)
                    return;
                Wifi w = tvTree.SelectedNode.Tag as Wifi;
                if (w == null)
                    return;
                w.Conf.Visible = !w.Conf.Visible;
                UpdateNode(w);
            }
        }

        private void tvTree_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            if (imageListDevices.Images.Count == 0)
                return;
            if (e.Node.Tag is Wifi w)
            {
                //bool selected = (e.State & TreeNodeStates.Selected) != 0;
                bool focused = (e.State & TreeNodeStates.Focused) != 0;
                using (Brush color = new SolidBrush(w.Conf.Color))
                using (Brush bbg = new SolidBrush(focused ? SystemColors.Highlight : tvTree.BackColor))
                using (Brush bfont = new SolidBrush(focused ? SystemColors.HighlightText : tvTree.ForeColor))
                {
                    Font nodeFont = e.Node.NodeFont ?? tvTree.Font;
                    e.Graphics.FillRectangle(bbg, e.Bounds);
                    int x = e.Bounds.X + 15;
                    int y = e.Bounds.Y;
                    if (e.Node.StateImageIndex >= 0)
                        e.Graphics.DrawImage(imageListDevices.Images[e.Node.StateImageIndex], x, y+10, 15, 15);
                    x += 10;
                    imageListDevices.Draw(e.Graphics, x, y, e.Node.ImageIndex);
                    x += 25;
                    y++;
                    e.Graphics.FillRectangle(color, new Rectangle(x,y,5,e.Bounds.Height-2));
                    x += 5;
                    e.Graphics.DrawString(e.Node.Text, nodeFont, bfont, x, y);
                    x += 5;
                    y += 13;
                    e.Graphics.DrawString($"{w.RangeName}, Ch: {w.Channel}, Power: {w.Signal}%", tagFont, Brushes.Gray, x, y);
                    
                }
            }
            else
                e.DrawDefault = true;

        }
    }
}
