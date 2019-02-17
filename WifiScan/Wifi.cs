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
        private Color color;

        public Wifi(Wlan.WlanBssEntry network)
        {
            this.network = network;
            bssid = network.GetBSSIDHex();
            ssid = network.GetSSID();
            color = WifiApi.ColorForBSSID(bssid);
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

        public void Update(Wlan.WlanBssEntry network)
        {
            this.network = network;
        }

        public string BSSID => bssid;
        public string SSID => ssid;
        public bool Active => (DateTime.Now - lastSeen).Seconds < 10;
        public Color Color => color;
        public uint Signal => network.linkQuality;
        public uint Channel => network.GetChannel();
    }
}
