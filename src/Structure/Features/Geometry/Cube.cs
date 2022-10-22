using System;
using BedrockTools.Objects;
using BedrockTools.Objects.Blocks;

namespace BedrockTools.Structure.Features.Geometry {
    public class Cube : Feature {
        public Block FillerBlock { get; }
        public Cube(int x,int y, int z, Block fillerBlock) : base(new Dimensions(x,y,z)) { 
            
            FillerBlock = fillerBlock;
        }
        public Cube(Dimensions size, Block fillerBlock):base(size) {
            FillerBlock = fillerBlock;
        }

        public override Block GetBlock(int x, int y, int z) => FillerBlock;
    }
}
