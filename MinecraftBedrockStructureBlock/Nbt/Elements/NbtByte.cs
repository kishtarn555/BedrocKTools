using System.IO;


namespace BedrockTools.Nbt.Elements {
    public class NbtByte : NbtPrimitive<sbyte> {
        public override sbyte Value { get; protected set; }
        public override NbtTag Tag => NbtTag.TAG_Byte;
        public  NbtByte(sbyte value): base(value) { }
        public override void WriteValue(BinaryWriter writer) {
            writer.Write(Value);
        }
        public override string ToString() {
            return $"{Value}B";
        }

        public static explicit operator NbtByte(sbyte val)
            => new NbtByte(val);
    }
}
