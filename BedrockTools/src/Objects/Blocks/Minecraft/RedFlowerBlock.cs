//==============================================================================
// THIS CODE WAS AUTOGENERATED BY:
//    BedrockTools_Build.Generator.MinecraftBlocks.VariationGenerator
// Do not modify, any changes will be lost when the generation tool is run again
// version: 0.1.0
//==============================================================================
using BedrockTools.Nbt.Elements;

namespace BedrockTools.Objects.Blocks.Minecraft {
    public class RedFlowerBlock : Block {
        public readonly RedFlowerType Variation;

        public RedFlowerBlock (string identifier, RedFlowerType variation) : base(identifier) {
            Variation = variation;
        }

        public override NbtCompoundSorted GetBlockState () {
            return new NbtCompoundSorted() {
                {"flower_type", (NbtString)Variation.ToString().ToLower()}
            };
        }

        public enum RedFlowerType {
            poppy,
            orchid,
            allium,
            houstonia,
            tulip_red,
            tulip_orange,
            tulip_white,
            tulip_pink,
            oxeye,
            cornflower,
            lily_of_the_valley
        }

    }
}
