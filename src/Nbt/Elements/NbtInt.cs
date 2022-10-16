
namespace BedrockTools.Nbt.Elements {
    public class NbtInt : NbtPrimitive<int> {
        public override NbtTag Tag => NbtTag.TAG_Int;
        public override int Value { get; protected set; }
        public NbtInt(int value) : base(value) { }
        
        public override string ToString() {
            return $"{Value}";
        }
        public static explicit operator NbtInt(int val)
            => new NbtInt(val);
    }
}
