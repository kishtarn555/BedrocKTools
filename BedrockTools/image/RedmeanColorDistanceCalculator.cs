using System.Drawing;

namespace MinecraftBedrockStructureBlock.image {
    public class RedmeanColorDistanceCalculator : ColorDistanceCalculator {
        
        public override double calcDistance(Color a, Color b) {
            double dr = a.R - b.R;
            double dg = a.G - b.G;
            double db = a.B - b.B;
            double rt = 0.5 * (((double)a.R) + ((double)b.R));
            double dc = (2.0 + rt / 256.0) * dr * dr
                + 4 * dg * dg
                + (2.0 + (255.0 - rt) / 256.0) * db * db;
            return dc;
        }
        
    }
}
