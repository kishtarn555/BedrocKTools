using System;
using System.Collections.Generic;
using System.Text;
using MinecraftBedrockStructureBlock.enums;
using System.IO;

namespace MinecraftBedrockStructureBlock.types {
    public abstract class NbtBase {
        public readonly NbtTypes NType;
        public string name;

        
        protected NbtBase(string name, NbtTypes NType) {
            this.name = name;
            this.NType = NType;
        }
        public abstract void print(BinaryWriter writer, bool named=true);

        protected void printNameData(BinaryWriter writer, bool named) {
            if (!named) return;
            writer.Write((byte)NType);
            short length = (short)name.Length;
            writer.Write(length);
            foreach (char c in name) {
                writer.Write(c);
            }
        }
    }
}
