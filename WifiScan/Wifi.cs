using NativeWifi;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WifiScan
{
    static class Wifi
    {
        public static Random rnd = new Random();
        private static Pen pLine = new Pen(Color.Silver);
        private static Pen pLineDark = new Pen(Color.Gray);
        private static Font font = new Font(FontFamily.GenericSansSerif, 8);
        private static Brush bText = new SolidBrush(Color.Silver);

        static Dictionary<string, Wlan.WlanBssEntry> networks = new Dictionary<string, Wlan.WlanBssEntry>();
        static List<List<Wlan.WlanBssEntry>> snap = new List<List<Wlan.WlanBssEntry>>();
        static Dictionary<string, Color> colors = new Dictionary<string, Color>();

        public static string GetBSSIDHex(this Wlan.WlanBssEntry network)
        {
            byte[] macAddr = network.dot11Bssid;
            StringBuilder tMac = new StringBuilder();
            for (int i = 0; i < macAddr.Length; i++)
                tMac.Append(macAddr[i].ToString("x2").PadLeft(2, '0').ToUpper());
            return tMac.ToString();
        }

        public static string GetSSID(this Wlan.WlanBssEntry network)
        {
            return ASCIIEncoding.ASCII.GetString(network.dot11Ssid.SSID).ToString();
        }

        public static void Register(this Wlan.WlanBssEntry network)
        {
            string bssid = network.GetBSSIDHex();
            Wlan.WlanBssEntry net;
            if (networks.TryGetValue(bssid, out net) && net.linkQuality > network.linkQuality)
                return;
            networks[bssid] = network;
        }

        public static void Snapshoot()
        {
            List<Wlan.WlanBssEntry> step = new List<Wlan.WlanBssEntry>();
            step.AddRange(networks.Values);
            snap.Add(step);
            networks.Clear();
        }

        private static List<Wlan.WlanBssEntry> GetLastScan()
        {
            if (snap.Count < 1)
                return new List<Wlan.WlanBssEntry>();
            return snap[snap.Count - 1];
        }

        private static List<Wlan.WlanBssEntry> GetNetworksInChannel(int ch)
        {
            return null;
            //List<Wlan.WlanBssEntry> scan = GetLastScan();
            //foreach(Wlan.WlanBssEntry wifi in scan)
            //{
            //    if (wifi.ch)
            //}
        }

        #region Charting
        const int CHART_FOOT = 20;

        private static Color GetNetworkColor(string bssid)
        {
            Color cl;
            if (!colors.TryGetValue(bssid, out cl))
                colors[bssid] = cl = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
            return cl;
        }

        public static void Paint(Graphics g, Size sz)
        {
            g.Clear(Color.Black);
            //Draw Guidelines
            float scale = g.DpiX / 96;
            float hh2 = sz.Height / 2f;
            g.DrawLine(pLine, new PointF(0, hh2), new PointF(sz.Width, hh2));
            for (int i = 1; i < 4; i++)
            {
                float hl = (hh2 * i) / 4;
                float hl2 = hl + hh2;
                g.DrawLine(pLineDark, new PointF(0, hl), new PointF(sz.Width, hl));
                g.DrawLine(pLineDark, new PointF(0, hl2), new PointF(sz.Width, hl2));
            }
            //Draw Channels
            List<Wlan.WlanBssEntry> scan = GetLastScan();
            float chw6 = (sz.Width * 4) / 16f;
            foreach (Wlan.WlanBssEntry wifi in scan)
            {
                float he = (sz.Height * wifi.linkQuality) / 100f;
                Pen pw = new Pen(GetNetworkColor(wifi.GetBSSIDHex()), 2f);
                g.DrawEllipse(pw, new RectangleF(0, sz.Height - (he / 2f), chw6, he));
                //wifi.chCenterFrequency
            }
            for (int i = 1; i <= 14; i++)
            {
                float x = (sz.Width * (i + 1)) / 16f;
                g.DrawString("" + i, font, bText, new PointF(x, sz.Height - (CHART_FOOT * scale)));
            }
        }
        #endregion
    }
}
