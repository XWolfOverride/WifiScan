using NativeWifi;
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
        private WlanClient client = new WlanClient();

        public Form1()
        {
            InitializeComponent();
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            PopulateTree();
        }

        #region Tree Management

        private TreeNode GetNodeFor(WlanClient.WlanInterface wlanIface)
        {
            foreach (TreeNode node in tvTree.Nodes)
            {
                Guid? nguid = node.Tag as Guid?;
                if (nguid == wlanIface.InterfaceGuid)
                    return node;
            }
            TreeNode nnode = tvTree.Nodes.Add(wlanIface.InterfaceDescription);
            nnode.Tag = wlanIface.InterfaceGuid;
            return nnode;
        }

        private TreeNode GetNodeFor(Wlan.WlanBssEntry network, TreeNodeCollection nodes)
        {
            string nbssid = network.GetBSSIDHex();
            foreach (TreeNode node in nodes)
            {
                String bssid = node.Tag as String;
                if (bssid == nbssid)
                    return node;
            }
            TreeNode nnode = nodes.Add(network.GetSSID());
            nnode.Tag = nbssid;
            nnode.ImageIndex = 1;
            nnode.SelectedImageIndex = 1;
            return nnode;
        }
        
        private void PopulateTree()
        {
            ArrayList nodes = new ArrayList(tvTree.Nodes);
            foreach (WlanClient.WlanInterface wlanIface in client.Interfaces)
            {
                TreeNode node = GetNodeFor(wlanIface);
                nodes.Remove(node);
                Wlan.WlanBssEntry[] wlanBssEntries = wlanIface.GetNetworkBssList();
                ArrayList netnodes = new ArrayList(wlanBssEntries);
                foreach (Wlan.WlanBssEntry network in wlanBssEntries)
                {
                    TreeNode nwnode = GetNodeFor(network, node.Nodes);
                    netnodes.Remove(nwnode);
                    network.Register();
                }
                foreach (TreeNode nwdel in nodes)
                    node.Nodes.Remove(nwdel);
            }
            // remove old
            foreach (TreeNode ndel in nodes)
                tvTree.Nodes.Remove(ndel);
            tvTree.ExpandAll();
            // Take snapshoot of current wifi status and deletes registrations
            Wifi.Snapshoot();
        }

        #endregion

        private void WifiTimer_Tick(object sender, EventArgs e)
        {
            PopulateTree();
            scMain.Panel2.Invalidate();
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {
            Wifi.Paint(e.Graphics, scMain.Panel2.ClientSize);
        }
    }
}
