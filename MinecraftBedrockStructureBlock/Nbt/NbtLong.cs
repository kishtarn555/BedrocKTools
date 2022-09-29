using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BedrockTools.Nbt {
    public class NbtLong : NbtPrimitive<long> {
        public override long Value { get; protected set; }
        public override NbtTag Tag => NbtTag.TAG_Long;
        public  NbtLong(long value): base(value) { }
        public override void WriteValue(BinaryWriter writer) {
            writer.Write(Value);
        }
    }
}
