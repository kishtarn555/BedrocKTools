using MinecraftBedrockStructureBlock.enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MinecraftBedrockStructureBlock.types {
    public class NbtInt : NbtBase {
        int value;
        public NbtInt(string name, int value) : base(name, NbtTypes.TAG_Int) {
            this.value = value;
        }

        public override void print(BinaryWriter writer, bool named = true) {
            printNameData(writer, named);
            writer.Write(value);
        }

        public override string ToString() {
            return value.ToString();
        }

    }
}
