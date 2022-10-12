//==============================================================================
// THIS CODE WAS AUTOGENERATED BY:
//    BedrockTools_Build.Generator.MinecraftBlocks.VariationGenerator
// Do not modify, any changes will be lost when the generation tool is run again
// version: 0.1.0
//==============================================================================
using BedrockTools.Nbt.Elements;

namespace BedrockTools.Objects.Blocks.Minecraft {
    public class StoneBricksBlock : Block {
        public readonly StoneBricksType Variation;

        public StoneBricksBlock (string identifier, StoneBricksType variation) : base(identifier) {
            Variation = variation;
        }

        public override NbtCompoundSorted GetBlockState () {
            return new NbtCompoundSorted() {
                {"stone_brick_type", (NbtString)Variation.ToString().ToLower()}
            };
        }

        public enum StoneBricksType {
            Default,
            Chiseled,
            Cracked,
            Mossy,
            Smooth
        }

    }
}