using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MicroCenter.Classi
{
    public class RGB__HSV
    {
        public static Color ConvertHsvToRgb(int h, int s, int v)
        {
            // Normalizzazione dei valori nel range standard
            double hue = (h / 512.0) * 360.0;  // Scala H da [0, 512] a [0, 360]
            double saturation = s / 255.0;      // Scala S da [0, 255] a [0, 1]
            double value = v / 255.0;           // Scala V da [0, 255] a [0, 1]

            int hi = (int)(hue / 60) % 6;
            double f = (hue / 60) - Math.Floor(hue / 60);

            double p = value * (1 - saturation);
            double q = value * (1 - f * saturation);
            double t = value * (1 - (1 - f) * saturation);

            double r = 0, g = 0, b = 0;

            switch (hi)
            {
                case 0: r = value; g = t; b = p; break;
                case 1: r = q; g = value; b = p; break;
                case 2: r = p; g = value; b = t; break;
                case 3: r = p; g = q; b = value; break;
                case 4: r = t; g = p; b = value; break;
                case 5: r = value; g = p; b = q; break;
            }

            return Color.FromRgb((byte)(r * 255), (byte)(g * 255), (byte)(b * 255));
        }

    }
}
