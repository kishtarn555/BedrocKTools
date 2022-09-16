using MinecraftBedrockStructureBlock.types;
using System;
using System.Collections.Generic;
using System.Text;

namespace MinecraftBedrockStructureBlock.structure.block {
    class Palette : NbtRepresentableObject {
        HashSet<Block> blocks;

        public Palette() {
            blocks = new HashSet<Block>();
        }
        public void RegisterBlock(Block block) {
            blocks.Add(block);
        }
        public override NbtBase GetNBT() {
            throw new NotImplementedException();
        }
    }
}
