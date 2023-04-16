using BedrockTools.Nbt.Elements;
using BedrockTools.Objects.Blocks.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BedrockTools.Objects.Blocks {
    public class WoodBlock : Block {
        PillarAxis pillarAxis;
        WoodType woodType;
        bool strippedBit;

        public WoodBlock(string identifier, PillarAxis pillarAxis, WoodType woodType, bool strippedBit): base(identifier) {
            this.pillarAxis = pillarAxis;
            this.woodType = woodType;
            this.strippedBit = strippedBit;
        }

        public override NbtCompoundSorted GetBlockState() =>
            new NbtCompoundSorted() {
                { "pillar_axis", (NbtString)pillarAxis },
                { "stripped_bit", (NbtByte)strippedBit },
                { "wood_type", (NbtString)woodType }
            };
    }
}
