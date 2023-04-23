// MIT License
//
// Copyright (c) 2023 XWolf Override
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using NativeWifi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WifiScan
{
    /// <summary>
    /// Wifi Antenas on computer.
    /// </summary>
    class WifiDevice
    {

        #region Alive Control

        private static WlanClient client = new WlanClient();
        private static List<WifiDevice> devices = new List<WifiDevice>();

        private static WifiDevice GetFor(WlanClient.WlanInterface iface)
        {
            foreach (WifiDevice d in devices)
            {
                if (d.iface.InterfaceGuid == iface.InterfaceGuid)
                    return d;
            }
            WifiDevice ndev = new WifiDevice(iface);
            devices.Add(ndev);
            return ndev;
        }

        public static List<WifiDevice> List()
        {
            List<WifiDevice> del = new List<WifiDevice>(devices); // Devices to delete
            foreach (WlanClient.WlanInterface wlanIface in client.Interfaces)
            {
                WifiDevice dev = GetFor(wlanIface);
                dev.iface = wlanIface;
                del.Remove(dev);
            }
            return new List<WifiDevice>(devices);
        }

        #endregion

        private bool alive;

        public static Wifi[] FetchAllNetworks()
        {
            List<Wifi> wifis = new List<Wifi>();
            foreach (WifiDevice dev in devices)
                foreach (Wifi wifi in dev.Networks)
                    if (!wifis.Contains(wifi))
                        wifis.Add(wifi);
            return wifis.ToArray();
        }

        private WlanClient.WlanInterface iface;
        private DateTime lastScan;
        private List<Wifi> networks = new List<Wifi>();

        private WifiDevice(WlanClient.WlanInterface iface)
        {
            this.iface = iface;
            Scan();
        }

        private Wifi GetFor(Wlan.WlanBssEntry network)
        {
            string bssid = network.GetBSSIDHex();
            foreach (Wifi wifi in networks)
            {
                if (wifi.BSSID == bssid)
                    return wifi;
            }
            Wifi nwifi = new Wifi(network);
            networks.Add(nwifi);
            return nwifi;
        }

        private Wifi[] GetNetworks()
        {
            Scan();
            Wlan.WlanBssEntry[] wlanBssEntries;
            try
            {
                wlanBssEntries = iface.GetNetworkBssList();
            }
            catch
            {
                return new Wifi[0];
            }
            foreach (Wlan.WlanBssEntry network in wlanBssEntries)
            {
                Wifi wifi = GetFor(network);
                wifi.Update(network);
                wifi.Notify();
            }
            Wifi[] all = networks.ToArray();
            foreach (Wifi wifi in all)
                if (!wifi.Active)
                    networks.Remove(wifi);
            return networks.ToArray();
        }

        public void Scan()
        {
            if (DateTime.Now.Subtract(lastScan).Seconds < 3)
                return;
            lastScan = DateTime.Now;
            try
            {
                alive = true;
                iface.Scan();
            }
            catch
            {
                alive = false;
            }
        }

        public Wifi[] Networks => GetNetworks();

        public string Name => iface.InterfaceDescription;
        public bool Alive => alive;
    }
}
