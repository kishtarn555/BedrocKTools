using System;
using BedrockTools.Nbt.Elements;

namespace BedrockTools.Objects.Minecraft.Blocks {
    public abstract class VanillaBlock : Block{
        public VanillaBlock(string identifier): base(identifier) {}
        public VanillaBlock(string identifier, NbtCompoundSorted blockstates): base(identifier, blockstates) {}
        public VanillaBlock(string identifier, string blockstates) : base(identifier, blockstates) { }
    }
}
