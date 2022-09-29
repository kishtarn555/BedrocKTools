using System.IO;

namespace BedrockTools.Nbt {
    public abstract class NbtElement {
        public abstract NbtTag Tag { get; }
        public void WriteTag(BinaryWriter writer) {
            writer.Write((byte)Tag);
        }
        public abstract void WriteValue(BinaryWriter writer);


    }
}
