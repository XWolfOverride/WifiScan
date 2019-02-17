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
    class Wifi
    {
        private Wlan.WlanBssEntry network;
        private string ssid;
        private string bssid;
        private DateTime lastSeen;
        private Color color = Color.FromArgb(WifiApi.rnd.Next(200) + 56, WifiApi.rnd.Next(200) + 56, WifiApi.rnd.Next(200) + 56);

        public Wifi(Wlan.WlanBssEntry network)
        {
            this.network = network;
            bssid = network.GetBSSIDHex();
            ssid = network.GetSSID();
            Notify();
        }

        public void Notify()
        {
            lastSeen = DateTime.Now;
        }

        public WifiInfo GetInfo()
        {
            return new WifiInfo(this);
        }

        public string BSSID => bssid;
        public string SSID => ssid;
        public bool Active => (DateTime.Now - lastSeen).Seconds < 10;
        public Color Color => color;
        public uint Signal => network.linkQuality;
        public uint Channel => network.GetChannel();
    }
}
