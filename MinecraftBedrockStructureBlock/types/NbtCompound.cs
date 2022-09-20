using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MinecraftBedrockStructureBlock.enums;

namespace MinecraftBedrockStructureBlock.types {
    public class NbtCompound : NbtBase {
        List<NbtBase> elementList;
        public NbtCompound(string name): base(name, NbtTypes.TAG_Compound)  {
            elementList = new List<NbtBase>();
        }
           
        public NbtBase this[string key] {
            get {
                foreach (NbtBase element in elementList) {
                    if (element.name == key)
                        return element;
                }
                return null;
            }
        }

        public void Add(NbtBase newElement) {
            elementList.Add(newElement);
        }

        public override void print(BinaryWriter writer, bool named = true) {
            printNameData(writer, named);
            foreach (NbtBase element in elementList) {
                element.print(writer, true);
            }
            writer.Write((byte)NbtTypes.TAG_End);
        }

        public List<NbtBase> getList() {
            return elementList;
        }
        public override string ToString() {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("{");
            bool first = true;
            foreach (NbtBase element in elementList) {
                if (!first) stringBuilder.Append(',');
                first = false;
                stringBuilder.Append(element.name + ":" + element.ToString());
            }
            stringBuilder.Append("}");
            return stringBuilder.ToString();
        }
    }
}
