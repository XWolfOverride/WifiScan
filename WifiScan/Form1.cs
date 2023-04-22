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
        Font tagFont = new Font("Helvetica", 8, FontStyle.Bold);

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
            //objMethodInfo.Invoke(tvTree, objArgs);
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
            TreeNode nnode = nodes.Add("");
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
                    if (string.IsNullOrEmpty(wifi.SSID))
                    {
                        nwnode.Text = $"[{wifi.BSSID}]"; // Update network name (for configuration changes)
                    }
                    else
                    {
                        nwnode.Text = wifi.SSID; // Update network name (for configuration changes)
                    }
                    nwnode.ImageIndex = nwnode.SelectedImageIndex = 1;
                    nwnode.BackColor = wifi.Conf.Color;
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
                n.BackColor = w.Conf.Color;
            }
            scMain.Panel2.Invalidate();
        }

        // Returns the bounds of the specified node, including the region 
        // occupied by the node label and any node tag displayed.
        private Rectangle NodeBounds(TreeNode node)
        {
            // Set the return value to the normal node bounds.
            Rectangle bounds = node.Bounds;
            if (node.Tag != null)
            {
                // Retrieve a Graphics object from the TreeView handle
                // and use it to calculate the display width of the tag.
                Graphics g = tvTree.CreateGraphics();
                int tagWidth = (int)g.MeasureString
                    (node.Tag.ToString(), tagFont).Width + 6;

                // Adjust the node bounds using the calculated value.
                bounds.Offset(tagWidth / 2, 0);
                bounds = Rectangle.Inflate(bounds, tagWidth / 2, 0);
                g.Dispose();
            }

            return bounds;
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
            // Draw the background and node text for a selected node.
            if ((e.State & TreeNodeStates.Selected) != 0)
            {
                // Draw the background of the selected node. The NodeBounds
                // method makes the highlight rectangle large enough to
                // include the text of a node tag, if one is present.
                e.Graphics.FillRectangle(Brushes.Green, NodeBounds(e.Node));

                // Retrieve the node font. If the node font has not been set,
                // use the TreeView font.
                Font nodeFont = e.Node.NodeFont;
                if (nodeFont == null) nodeFont = ((System.Windows.Forms.TreeView)sender).Font;

                // Draw the node text.
                e.Graphics.DrawString(e.Node.Text, nodeFont, Brushes.White,
                    Rectangle.Inflate(e.Bounds, 2, 0));
            }

            // Use the default background and node text.
            else
            {
                e.DrawDefault = true;
            }

            // If a node tag is present, draw its string representation 
            // to the right of the label text.
            if (e.Node.Tag != null)
            {
                e.Graphics.DrawString(e.Node.Tag.ToString(), tagFont,
                    Brushes.Yellow, e.Bounds.Right + 2, e.Bounds.Top);
            }

            // If the node has focus, draw the focus rectangle large, making
            // it large enough to include the text of the node tag, if present.
            if ((e.State & TreeNodeStates.Focused) != 0)
            {
                using (Pen focusPen = new Pen(Color.Black))
                {
                    focusPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                    Rectangle focusBounds = NodeBounds(e.Node);
                    focusBounds.Size = new Size(focusBounds.Width - 1,
                    focusBounds.Height - 1);
                    e.Graphics.DrawRectangle(focusPen, focusBounds);
                }
            }
        }
    }
}
