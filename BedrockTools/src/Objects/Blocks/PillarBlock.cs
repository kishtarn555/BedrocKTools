using BedrockTools.Nbt.Elements;
using BedrockTools.Objects.Blocks.Util;
using System;

namespace BedrockTools.Objects.Blocks {
    
	public class PillarBlock : Block {
        PillarAxis pillarAxis;

		public PillarBlock(String identifier, PillarAxis axis) : base(identifier) {
            pillarAxis = axis;
		}

        public override NbtCompoundSorted GetBlockState() {
                return new NbtCompoundSorted() {
                { "pillar_axis" , (NbtString)pillarAxis.ToString() }
            };
        }
    }
}
