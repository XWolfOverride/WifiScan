using NativeWifi;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WifiScan
{
    static class WifiApi
    {
        public static Random rnd = new Random();
        private static Pen pLineLight = new Pen(Color.WhiteSmoke, 3);
        private static Pen pLine = new Pen(Color.Silver);
        private static Pen pLineDark = new Pen(Color.Gray);
        private static Pen pLineDark2 = new Pen(Color.FromArgb(30, 30, 30));
        private static Font font = new Font(FontFamily.GenericSansSerif, 8);
        private static Brush bText = new SolidBrush(Color.Silver);

        static List<List<WifiInfo>> snap = new List<List<WifiInfo>>();
        static Dictionary<string, WifiConf> confs = new Dictionary<string, WifiConf>();

        #region Network Extensions
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
            string ssid = ASCIIEncoding.ASCII.GetString(network.dot11Ssid.SSID).ToString();
            ssid = ssid.Replace('\0', ' ');
            ssid = ssid.Trim();
            return ssid;
        }

        #endregion

        public static void Snapshoot()
        {
            Dictionary<string, WifiInfo> step = new Dictionary<string, WifiInfo>();
            foreach (Wifi wifi in WifiDevice.FetchAllNetworks())
                if (wifi.Active)
                {
                    WifiInfo prior;
                    if (!(step.TryGetValue(wifi.BSSID, out prior) && prior.Signal > wifi.Signal))
                        step[wifi.BSSID] = wifi.GetInfo();
                }
            snap.Add(step.Values.ToList());
        }

        private static List<WifiInfo> GetLastScan()
        {
            if (snap.Count < 1)
                return new List<WifiInfo>();
            return snap[snap.Count - 1];
        }

        private static List<WifiInfo> GetNetworksInChannel(Wifi.WifiRangle range, int ch)
        {
            List<WifiInfo> result = new List<WifiInfo>();
            List<WifiInfo> scan = GetLastScan();
            foreach (WifiInfo wii in scan)
                if (wii.Range == range && wii.Channel == ch)
                    result.Add(wii);
            return result;
        }

        #region Charting
        const int CHART_FOOT = 20;

        public static void Paint(Graphics g, Size sz)
        {
            g.Clear(Color.Black);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            //Draw Guidelines
            float scale = g.DpiX / 96;
            float hh2 = sz.Height / 2f;
            //Vertical (channels)
            float chw6 = (sz.Width * 4) / 16f;
            for (int i = 1; i <= 14; i++)
            {
                int x = (int)((sz.Width * (i + 1)) / 16f);
                g.DrawLine(pLineDark2, new PointF(x, sz.Height), new PointF(x, hh2));
                g.DrawString("" + i, font, bText, new PointF(x - 10, sz.Height - (CHART_FOOT * scale)));
            }
            //Horizontal
            g.DrawLine(pLineLight, new Point(0, (int)hh2), new Point(sz.Width, (int)hh2));
            for (int i = 1; i < 4; i++)
            {
                float hl = (hh2 * i) / 4;
                float hl2 = hl + hh2;
                g.DrawLine(pLineDark, new PointF(0, hl), new PointF(sz.Width, hl));
                g.DrawLine(pLineDark, new PointF(0, hl2), new PointF(sz.Width, hl2));
            }
            //Draw Channels
            for (int i = 1; i <= 14; i++)
            {
                int x = (int)((sz.Width * (i + 1)) / 16f);
                List<WifiInfo> chw = GetNetworksInChannel(Wifi.WifiRangle.G2_4, i);
                foreach (WifiInfo wii in chw)
                {
                    if (!wii.Conf.Visible)
                        continue;
                    float he = (sz.Height * wii.Signal) / 100f;
                    Pen pw = new Pen(wii.Conf.Color, wii.BSSID == HighlightBSSID ? 7f : 2f);
                    g.DrawEllipse(pw, new RectangleF(x - (chw6 / 2), sz.Height - (he / 2f), chw6, he));
                }
            }
            //Signal History
            if (snap.Count < 2)
                return;
            int ii = snap.Count - 1;
            int xx = sz.Width;
            while (ii > 0 && xx > 0)
            {
                List<WifiInfo> wiis = snap[ii];
                foreach (WifiInfo wii in wiis)
                {
                    if (!wii.Conf.Visible)
                        continue;
                    List<WifiInfo> wolds = snap[ii - 1];
                    WifiInfo wold = null;
                    foreach (WifiInfo woldc in wolds)
                        if (woldc.BSSID == wii.BSSID)
                        {
                            wold = woldc;
                            break;
                        }
                    if (wold == null)
                        continue;
                    float he = hh2 - ((hh2 * wii.Signal) / 100f);
                    float heo = hh2 - ((hh2 * wold.Signal) / 100f);
                    Pen pw = new Pen(wii.Conf.Color, wii.BSSID == HighlightBSSID ? 7f : 2f);
                    pw.StartCap = LineCap.Round;
                    pw.EndCap = LineCap.Round;
                    g.DrawLine(pw, new PointF(xx, he), new PointF(xx - 10, heo));
                }
                ii--;
                xx -= 10;
            }
        }
        #endregion

        #region WifiConf
        public static WifiConf GetConf(string bssid)
        {
            WifiConf cnf;
            if (confs.TryGetValue(bssid, out cnf))
                return cnf;
            return confs[bssid] = new WifiConf();
        }
        #endregion

        public static string HighlightBSSID { get; set; }

    }
}
