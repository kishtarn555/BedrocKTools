using System;
using System.Collections.Generic;
using System.Text;
using MinecraftBedrockStructureBlock.types;

namespace MinecraftBedrockStructureBlock.structure.block {
    public class Block: NbtRepresentableObject {
        public string identifier;
        public NbtSortedCompound blockStates;
        
        const int VERSION = 17959425;
        public Block(string identifier, NbtSortedCompound blockStates) {
            this.identifier = identifier;
            this.blockStates = blockStates;
            blockStates.name = "states";
        }

        public override NbtBase GetNBT() {
            NbtCompound block = new NbtCompound("");
            block.Add(new NbtString("name", identifier));
            block.Add(blockStates);
            block.Add(new NbtInt("version", VERSION));
            return block;
        }

        public override bool Equals(object obj) {
            return obj is Block block &&
                   identifier == block.identifier &&
                   this.ToString() == block.ToString();
        }

        public override int GetHashCode() {
            return ToString().GetHashCode();
        }

        public override string ToString() {
            return String.Format("[Block] {0} {1}", identifier, blockStates);
        }
    }
}
