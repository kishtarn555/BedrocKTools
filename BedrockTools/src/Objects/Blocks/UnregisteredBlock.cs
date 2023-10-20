using BedrockTools.Nbt.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BedrockTools.Objects.Blocks {
    internal class UnregisteredBlock : Block {
        public NbtCompoundSorted BlockState { get; private set; }
        public UnregisteredBlock(string identifier, NbtCompoundSorted blockState) : base(identifier) {
            BlockState = blockState;
        }


        public override NbtCompoundSorted GetBlockState() => BlockState;
    }
}
