using System.IO;

namespace BedrockTools.Nbt {
    public class NbtInt : NbtPrimitive<int> {
        public override NbtTag Tag => NbtTag.TAG_Int;
        public override int Value { get; protected set; }
        public NbtInt(int value) : base(value) { }
        public override void WriteValue(BinaryWriter writer) {
            writer.Write(Value);
        }
        public override string ToString() {
            return $"{Value}";
        }
    }
}
