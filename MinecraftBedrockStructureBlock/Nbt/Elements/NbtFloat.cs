using System;
using System.IO;

namespace BedrockTools.Nbt.Elements {
    class NbtFloat : NbtPrimitive<float> {
        public override NbtTag Tag => NbtTag.TAG_Float;
        public override float Value { get; protected set; }
        public NbtFloat(float value) : base(value) { }
        public override void WriteValue(BinaryWriter writer) {
            writer.Write(Value);
        }
        public override string ToString() {
            return $"{Value}f";
        }
        public static explicit operator NbtFloat(float val)
            => new NbtFloat(val);
    }
}
