using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MinecraftBedrockStructureBlock.enums;
namespace MinecraftBedrockStructureBlock.types {
    public class NbtString : NbtBase {
        string value;

        public NbtString(string name, string value): base(name, NbtTypes.TAG_String) {
            this.value = value;
        }

        public override void print(BinaryWriter writer, bool named = true) {
            printNameData(writer, named);
            writer.Write((short)value.Length) ;
            foreach (char c in value) {
                writer.Write(c);
            }
        }

        public override string ToString() {
            return String.Format("\"{0}\"", value);
        }
    }
}
