
namespace BedrockTools.Nbt.Elements {
    public class NbtByte : NbtPrimitive<sbyte> {
        public override sbyte Value { get; protected set; }
        public override NbtTag Tag => NbtTag.TAG_Byte;
        public  NbtByte(sbyte value): base(value) { }
       
        public override string ToString() {
            return $"{Value}B";
        }

        public static explicit operator NbtByte(sbyte val)
            => new NbtByte(val);

        public static explicit operator NbtByte(bool val) {
            if (val) {
                return new NbtByte(1);
            } else {
                return new NbtByte(0);
            }
        }
    }
}
