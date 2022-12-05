using BedrockTools.Objects;
using BedrockTools.Objects.Blocks;
using System;
using System.Collections.Generic;
using System.Text;

namespace BedrockTools.Structure.Features.Patterns {
    //FIXME: Be more flexible with axises
    public class Rings : Feature {
        readonly Block[] colors;
        int scale;
        
        public Rings(Dimensions size, Block[] colors, int scale) : base(size) {
            this.colors = colors;
            this.scale = scale;
        }

        public Rings(int x, int y, int z, Block[] colors, int scale) : base(x, y, z) {
            this.colors = colors;
            this.scale = scale;

        }

        public override Block GetBlock(int x, int y, int z) => colors[(y / scale) % colors.Length];
    }
}
