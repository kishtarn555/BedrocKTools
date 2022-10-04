using System;
using System.Collections.Generic;
using System.Drawing;

namespace MinecraftBedrockStructureBlock.image {
    public abstract class ColorDistanceCalculator {
        
        
        public abstract double calcDistance(Color a, Color b);
    }
}
