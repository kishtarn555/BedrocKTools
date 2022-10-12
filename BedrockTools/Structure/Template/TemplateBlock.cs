using System;
using BedrockTools.Nbt.Elements;
using BedrockTools.Objects.Blocks;

namespace BedrockTools.Structure.Template {
    public class TemplateBlock : Block {
        public readonly int TemplateIndex;

        public TemplateBlock(int index) : base("bedrocktools:template") {
            TemplateIndex = index;
        }

        public override NbtCompoundSorted GetBlockState() {
            return new NbtCompoundSorted() { { "template_index", (NbtInt)TemplateIndex } };
        }

        public override NbtCompoundOrdered GetStructureBlock() {
            throw new NotImplementedException();
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
