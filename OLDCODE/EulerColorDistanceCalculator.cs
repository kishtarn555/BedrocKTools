using System;
using System.Collections.Generic;
using System.Drawing;

namespace MinecraftBedrockStructureBlock.image {
    class EulerColorDistanceCalculator: ColorDistanceCalculator {
        public override double calcDistance(Color a, Color b) {
            double dr = a.R - b.R;
            double dg = a.G - b.G;
            double db = a.B - b.B;

            return dr * dr + dg * dg + db * db;
        }
    }
}
