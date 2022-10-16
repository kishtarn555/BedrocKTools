using BedrockTools.Nbt.Elements;
using System;
using System.Collections.Generic;
using System.Text;

namespace BedrockTools.Objects.Blocks {
    public class UnitBlock : Block {
        public UnitBlock(string identifier): base(identifier) {

        }
        public override NbtCompoundSorted GetBlockState() => new NbtCompoundSorted();
    }
}
