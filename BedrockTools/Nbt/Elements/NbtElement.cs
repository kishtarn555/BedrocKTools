using System.IO;
using BedrockTools.Nbt.Util;
namespace BedrockTools.Nbt.Elements {
    public abstract class NbtElement {
        public abstract NbtTag Tag { get; }
        public void WriteTag(BinaryWriter writer) {
            writer.Write((byte)Tag);
        }
        public abstract void WriteValue(BinaryWriter writer);


    }
}
