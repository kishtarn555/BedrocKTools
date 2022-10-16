using System;
using BedrockTools.Nbt.Elements;
using BedrockTools.Objects.Blocks;

namespace BedrockTools.Structure.Template {
    public class TemplateBlock : Block {
        public readonly int TemplateIndex;

        public static string StringIdentifier="bedrocktools:template";
        public TemplateBlock(int index) : base(StringIdentifier) {
            TemplateIndex = index;
        }

        public override NbtCompoundSorted GetBlockState() {
            return new NbtCompoundSorted() { { "template_index", (NbtInt)TemplateIndex } };
        }

        public override NbtCompoundOrdered GetStructureBlock() {
            throw new NotImplementedException("Template block is a aux block, it is not meant to be used as an actual block");
        }
        public override bool Equals(object obj) {
            if (obj is TemplateBlock template) {
                return TemplateIndex == template.TemplateIndex;
            }
            return false;
        }

        public override int GetHashCode() => HashCode.Combine(Identifier, TemplateIndex);
    }
}
