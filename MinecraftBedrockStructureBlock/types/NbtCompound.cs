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
    }
}
