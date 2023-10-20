using System;
using BedrockTools.Objects.Blocks;
using BedrockTools.Objects;
using BedrockTools.Objects.Entities;

namespace BedrockTools.Structure.Features {
    public class StructureFeature:Feature {

        readonly IMcStructure Structure;
        
        public StructureFeature(IMcStructure structure) : base(structure.Size) {
            Structure = structure;
        }

        public override Block GetBlock(int x, int y, int z) => Structure.GetBlock(x,y,z);
    }
}
