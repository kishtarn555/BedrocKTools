using BedrockTools.Objects;
using BedrockTools.Objects.Blocks;
using System;
using System.Collections.Generic;
using System.Text;

namespace BedrockTools.Structure.Features.Patterns {
    public class Chess3D : Feature {
        Block colorOne;
        Block colorTwo;
        int scale;
        public Chess3D(Dimensions size, Block colorOne, Block colorTwo, int scale) : base(size) {
            this.colorOne = colorOne;
            this.colorTwo = colorTwo;
            this.scale = scale;
        }
        public Chess3D(int x,int y,int z, Block colorOne, Block colorTwo, int scale) : base(x,y,z) {
            this.colorOne = colorOne;
            this.colorTwo = colorTwo;
            this.scale = scale;
        }

        public override Block GetBlock(int x, int y, int z) =>
            (x/scale + y/scale + z/scale) % 2 == 0 ? colorOne : colorTwo;
    }
}
