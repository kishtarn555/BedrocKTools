//==============================================================================
// THIS CODE WAS AUTOGENERATED BY:
//    BedrockTools_Build.Generator.MinecraftBlocks.VariationGenerator
// Do not modify, any changes will be lost when the generation tool is run again
// version: 0.1.0
//==============================================================================
using BedrockTools.Nbt.Elements;

namespace BedrockTools.Objects.Blocks.Minecraft {
    public class DirtBlock : Block {
        public readonly DirtType Variation;

        public DirtBlock (string identifier, DirtType variation) : base(identifier) {
            Variation = variation;
        }

        public override NbtCompoundSorted GetBlockState () {
            return new NbtCompoundSorted() {
                {"dirt_type", (NbtString)Variation.ToString().ToLower()}
            };
        }

        public enum DirtType {
            Normal,
            Coarse
        }

    }
}
