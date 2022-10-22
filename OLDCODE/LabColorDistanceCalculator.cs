using System.Drawing;
using System;
namespace MinecraftBedrockStructureBlock.image {
    public class LabColorDistanceCalculator : ColorDistanceCalculator {
        double f(double t) {
            double d = 6.0/29.0;
            if (t > d*d*d) 
                return Math.Pow(t, 1.0 / 3.0);
            return t / (3.0 * d * d) + 4.0 / 29.0;
        }
        double[] conversion(double[] color) {
            double[] XYZ = new double[3];
            for (int i = 0; i < 3; i++) {
                color[i] /= 255.0;
                if (color[i] <= 0.04045) 
                    color[i] /= 12.92;
                else 
                    color[i] = Math.Pow((color[i] + 0.055) / 1.055, 2.4);
                color[i] *= 100.0;
            }
            XYZ[0] = ((color[0] * 0.4124) + (color[1] * 0.3576) + (color[2] * 0.1805));
            XYZ[1] = ((color[0] * 0.2126) + (color[1] * 0.7152) + (color[2] * 0.0722));
            XYZ[2] = ((color[0] * 0.0193) + (color[1] * 0.1192) + (color[2] * 0.9505));

            XYZ[0] /= 95.047;
            XYZ[1] /= 100.0;
            XYZ[2] /= 108.8840;
            for (int i = 0; i < 3; i++) {
                XYZ[i] = f(XYZ[i]);
            }        
            double[] Lab = new double[3];
            Lab[0] = 116.0 * XYZ[1] - 16.0;
            Lab[1] = 500.0 * (XYZ[0] - XYZ[1]);
            Lab[2] = 200.0 * (XYZ[1] - XYZ[2]);
            return Lab;
        }
        //https://en.wikipedia.org/wiki/Color_difference
        public override double calcDistance(Color a, Color b) {
            double[] sa = conversion(new double[] { a.R, a.G, a.B });
            double[] sb = conversion(new double[] { b.R, b.G, b.B });

            double dL = sa[0] - sb[0];
            double C1 = Math.Sqrt(sa[1] * sa[1] + sa[2] * sa[2]);
            double C2 = Math.Sqrt(sb[1] * sb[1] + sb[2] * sb[2]);
            double dCab = C1 - C2;
            double da = sa[1] - sb[1];
            double db = sa[2] - sb[2];
            double v = da * da + db * db - dCab * dCab;
            if (v < 0)
                v = 0; // Sometimes v is small, and due to impression it goes negative.
            double dHab = Math.Sqrt(v);
            double Sl = 1;
            double Sc = 1 + 0.045 * C1;
            double Sh = 1 + 0.015 * C1;
            return Math.Sqrt(Math.Pow(dL / Sl, 2) + Math.Pow(dCab / Sc, 2) +Math.Pow(dHab/Sh,2));
        }
        
    }
}
