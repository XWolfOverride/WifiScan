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
        public WifiInfo(Wifi wifi)
        {
            SSID = wifi.SSID;
            Signal = wifi.Signal;
            Channel = wifi.Channel;
            Color = wifi.Color;
        }

        public string SSID { get; }
        public uint Signal { get; }
        public uint Channel { get; }
        public Color Color { get; }
    }
}
