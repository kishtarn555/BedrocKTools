using System;
using System.Collections.Generic;
using System.Text;
using MinecraftBedrockStructureBlock.types;
namespace MinecraftBedrockStructureBlock.structure {
    public abstract class NbtRepresentableObject {
        public abstract NbtBase GetNBT();
    }
}
