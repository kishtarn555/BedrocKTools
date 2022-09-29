using System.IO;
using BedrockTools.Nbt.Util;

namespace BedrockTools.Nbt {
    public class NbtString : NbtPrimitive<string> {        
        public override NbtTag Tag => NbtTag.TAG_String;
        public override string Value { get; protected set; }
        public NbtString(string value):base(value) { }
        public override void WriteValue(BinaryWriter writer) {
            WriterUtil.WriteString(writer, Value);
        }
    }
}
