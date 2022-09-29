using System.IO;

namespace BedrockTools.Nbt {
    public class NbtByte : NbtPrimitive<byte> {
        public override byte Value { get; protected set; }
        public override NbtTag Tag => NbtTag.TAG_Byte;
        public  NbtByte(byte value): base(value) { }
        public override void WriteValue(BinaryWriter writer) {
            writer.Write(Value);
        }
    }
}
