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
        public enum WifiRangle { G2_4, G5, G60 };
        private Wlan.WlanBssEntry network;
        private string ssid;
        private string bssid;
        private DateTime lastSeen;
        private WifiConf conf;
        private WifiRangle range;
        private uint channel;


        public Wifi(Wlan.WlanBssEntry network)
        {
            this.network = network;
            bssid = network.GetBSSIDHex();
            ssid = network.GetSSID();
            conf = WifiApi.GetConf(bssid);
            Notify();
            if (network.chCenterFrequency > 5000000)
            {
                range = WifiRangle.G5;
                channel = ((network.chCenterFrequency - 5150000) / 1000) / 5 + 30;
            }
            else
            {
                range = WifiRangle.G2_4;
                channel = (network.chCenterFrequency - 2400000) / 5000;
            }
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

        public override string ToString()
        {
            return $"Wifi: {DisplayName}";
        }

        private string GetRangeName()
        {
            switch (range)
            {
                case WifiRangle.G2_4:
                    return "2.4G";
                case WifiRangle.G5:
                    return "5G";
                case WifiRangle.G60:
                    return "60G";
            }
            return "?G";
        }

        public string BSSID => bssid;
        public string SSID => ssid;

        public string DisplayName => string.IsNullOrWhiteSpace(ssid) ? $"[{bssid}]" : ssid;
        public bool Active => (DateTime.Now - lastSeen).Seconds < 10;
        public uint Signal => network.linkQuality;
        public uint Channel => channel;
        public uint Frequency => network.chCenterFrequency;
        public WifiRangle Range => range;
        public string RangeName => GetRangeName();
        public WifiConf Conf => conf;
    }
}
