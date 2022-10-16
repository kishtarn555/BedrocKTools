using System.IO;
using BedrockTools.Nbt.Util;

namespace BedrockTools.Nbt.Elements {
    public class NbtString : NbtPrimitive<string> {        
        public override NbtTag Tag => NbtTag.TAG_String;
        public override string Value { get; protected set; }
        public NbtString(string value):base(value) { }
        public override void WriteValue(BinaryWriter writer) {
            WriterUtil.WriteString(writer, Value);
        }
        public override string ToString() {
            return $"\"{Value}\"";
        }
        public static explicit operator NbtString(string val)
            => new NbtString(val);
    }
}
