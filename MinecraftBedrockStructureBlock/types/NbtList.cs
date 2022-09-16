using MinecraftBedrockStructureBlock.enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MinecraftBedrockStructureBlock.types {
    public class NbtList : NbtBase { 
        List<NbtBase> elements;
        NbtTypes dataType;
        public NbtList(string name, NbtTypes dataType) : base(name, NbtTypes.TAG_List) {
            elements = new List<NbtBase>();
            this.dataType = dataType;
        }

        public void Add(NbtBase newElement) {
            if (newElement.NType != dataType) {
                throw new Exception(
                    String.Format("Incompatible datatypes, NbtList<{0}> cannot Add NbtType of {1}", dataType, newElement.NType)
                );
            }
            elements.Add(newElement);
        }
        public override void print(BinaryWriter writer, bool named = true) {
            printNameData(writer, named);
            writer.Write((byte)dataType);
            writer.Write((int)elements.Count);
            foreach (NbtBase element in elements) {
                element.print(writer, false);
            }
        }

        public override string ToString() {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("[");
            bool first = true;
            foreach (NbtBase element in elements) {
                if (!first) {
                    stringBuilder.Append(",");
                }
                first = false;
                stringBuilder.Append(element);
            }
            stringBuilder.Append("]");
            return stringBuilder.ToString();
        }
    }
}
