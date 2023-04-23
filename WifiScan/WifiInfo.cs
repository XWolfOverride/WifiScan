using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WifiScan
{
    class WifiInfo
    {
        WifiConf c;
        public WifiInfo(Wifi wifi)
        {
            BSSID = wifi.BSSID;
            SSID = wifi.SSID;
            Signal = wifi.Signal;
            Channel = wifi.Channel;
            Range = wifi.Range;
            c = wifi.Conf;
        }

        public string BSSID { get; }
        public string SSID { get; }
        public uint Signal { get; }
        public uint Channel { get; }
        public Wifi.WifiRangle Range { get; }
        public WifiConf Conf => c;
    }
}
