using System;
using System.Collections.Generic;
using System.Text;

namespace MinecraftBedrockStructureBlock.image.util {
    class Color3D {
        public double R, G, B;

        public double this[int i] {
            get {
                if (i == 0) return R;
                if (i == 1) return G;
                if (i == 2) return B;
                throw new IndexOutOfRangeException();
            }
            set {
                if (i == 0) R = value;
                if (i == 1) G = value;
                if (i == 2) B = value;
                throw new IndexOutOfRangeException();
            }
        }

        public Color3D(double r, double g, double b) {
            R = r;
            G = g;
            B = b;
        }

        public Color3D(System.Drawing.Color col) {
            R = col.R;
            G = col.G;
            B = col.B;
        }

        public System.Drawing.Color getColor() {
            return System.Drawing.Color.FromArgb(255,(int)R, (int)G, (int)B);
        }

        public void Clamp() {
            if (R < 0) R = 0;
            if (R > 255) R = 255;
            if (G < 0) G = 0;
            if (G > 255) G = 255;
            if (B < 0) B = 0;
            if (B > 255) B = 255;
        }

        public static Color3D operator +(Color3D a, Color3D b) => new Color3D(a.R + b.R, a.G + b.G, a.B + b.B);
        public static Color3D operator -(Color3D a, Color3D b) => new Color3D(a.R - b.R, a.G - b.G, a.B - b.B);
        public static Color3D operator *(double a, Color3D b) => new Color3D(a * b.R, a * b.G, a * b.B);
        public static Color3D operator *(Color3D a, double b) => b*a;


        public static Color3D operator /(Color3D a, double b) => new Color3D(a.R/b, a.G/b, a.B/b);



    }
}
