using System;
using BedrockTools.Nbt.Elements;

namespace BedrockTools.Objects.Blocks {
    public abstract class EntityBlock : Block {
        public  EntityBlock(string identifier): base(identifier) {

        }
        public abstract NbtCompoundSorted GetEntityData();

    }
}
