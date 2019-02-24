using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WifiScan
{
    class WifiConf
    {
        public Color Color { get; set; } = Color.FromArgb(WifiApi.rnd.Next(220) + 36, WifiApi.rnd.Next(220) + 36, WifiApi.rnd.Next(220) + 36);
        public bool Visible { get; set; } = true;
    }
}
