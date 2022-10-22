using System;
using System.Collections.Generic;
using System.Text;
using MinecraftBedrockStructureBlock.types;

namespace MinecraftBedrockStructureBlock.structure.block {
    public class BlockEntity : Block {
        public NbtCompound entityData;
        public BlockEntity(string identifier, NbtSortedCompound blockStates, NbtCompound entityData) : base(identifier, blockStates) {
            this.entityData = entityData;
        }

        public override bool Equals(object obj) {
            return base.Equals(obj);
        }

        public override int GetHashCode() {
            return base.GetHashCode();
        }
    }
}
