using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentacion.Helpers
{
    public static class Resources
    {
        public static Size size_startupView { get; private set; }
        public static Size size_verinfoView { get; private set; }

        static Resources()
        {
            size_startupView = new Size(1080, 720);
            size_verinfoView = new Size(1000, 680);
        }
    }
}
