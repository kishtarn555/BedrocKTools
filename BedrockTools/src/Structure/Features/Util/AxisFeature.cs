﻿using BedrockTools.Objects;
using BedrockTools.Objects.Blocks;
using System;
using System.Collections.Generic;
using System.Text;

namespace BedrockTools.Structure.Features.Util {
    public class AxisFeature : Feature {

        public AxisFeature(Dimensions Size) : base(Size) { }
        public AxisFeature(int xLength, int yLength, int zLength) : base(xLength, yLength, zLength) { }
        public override Block GetBlock(int x, int y, int z) {
            if (x * y * z != 0)
                return null;
            if (x == 0 && y == 0 && z == 0)
                return VanillaBlockFactory.EndBricks();
            else if (((x == 0) ^ (y == 0) ^ (z == 0)))
                return null;
            else if (x != 0)
                return VanillaBlockFactory.Concrete(Objects.Blocks.Util.BlockColorValue.Red);
            else if (y != 0)
                return VanillaBlockFactory.Concrete(Objects.Blocks.Util.BlockColorValue.Green);
            else // if(z != 0)
                return VanillaBlockFactory.Concrete(Objects.Blocks.Util.BlockColorValue.Blue);

        }
    }
}
