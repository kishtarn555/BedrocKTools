
namespace BedrockTools.Nbt.Elements {
    public class NbtDouble : NbtPrimitive<double> {
        public override NbtTag Tag => NbtTag.TAG_Double;
        public override double Value { get; protected set; }
        public NbtDouble(double value) : base(value) { }      
        public override string ToString() {
            return $"{Value}";
        }
        public static explicit operator NbtDouble(double val)
            => new NbtDouble(val);
    }

}
