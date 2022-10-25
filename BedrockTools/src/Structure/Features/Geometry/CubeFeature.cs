using System;
using BedrockTools.Objects;
using BedrockTools.Objects.Blocks;

namespace BedrockTools.Structure.Features.Geometry {
    public class CubeFeature : Feature {
        public Block FillerBlock { get; }
        public CubeFeature(int x,int y, int z, Block fillerBlock) : base(new Dimensions(x,y,z)) { 
            
            FillerBlock = fillerBlock;
        }
        public CubeFeature(Dimensions size, Block fillerBlock):base(size) {
            FillerBlock = fillerBlock;
        }

        public override Block GetBlock(int x, int y, int z) => FillerBlock;
    }
}
