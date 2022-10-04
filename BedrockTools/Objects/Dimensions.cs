using System;
using System.Collections.Generic;
using System.Text;

namespace BedrockTools.Objects {
    public struct Dimensions {
        public int X;
        public int Y;
        public int Z;

        public Dimensions(int x, int y, int z) {
            X = x;
            Y = y;
            Z = z;
        }
    }
}
