﻿using System.IO;

namespace BedrockTools.Nbt {
    public class NbtShort : NbtPrimitive<short> {
        public override short Value { get; protected set; }

        public override NbtTag Tag => NbtTag.TAG_Short;
        public NbtShort(short value) : base(value) { }
        public override void WriteValue(BinaryWriter writer) {
            writer.Write(Value);
        }
        public override string ToString() {
            return $"{Value}S";
        }
    }
}
