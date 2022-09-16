using MinecraftBedrockStructureBlock.enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MinecraftBedrockStructureBlock.types {
    public class NbtSortedCompound : NbtBase {
        SortedList<string, NbtBase> elements;
        public NbtSortedCompound(string name) : base(name, NbtTypes.TAG_Compound) {
            elements = new SortedList<string, NbtBase>();
        }
        public void Add(NbtBase newElement) {
            elements.Add(newElement.name,newElement);
        }


        public override void print(BinaryWriter writer, bool named = true) {
            printNameData(writer, named);
            foreach (NbtBase element in elements.Values) {
                element.print(writer, true);
            }
            writer.Write((byte)NbtTypes.TAG_End);

        }

        public override string ToString() {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("{");
            foreach (NbtBase element in elements.Values) {
                if (!first) stringBuilder.Append(',');
                first = false;
                stringBuilder.Append(element.name+":"+element.ToString());
            }
            stringBuilder.Append("}");
            return stringBuilder.ToString();
        }
    }
}
