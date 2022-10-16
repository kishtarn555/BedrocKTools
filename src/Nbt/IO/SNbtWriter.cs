using BedrockTools.Nbt.Elements;
using System;
using System.IO;
using System.Collections.Generic;

namespace BedrockTools.Nbt.IO {
    public class SNbtWriter : NbtWriter {
        TextWriter writer;
        public SNbtWriter(TextWriter writer) {
            this.writer = writer;
        }
        public override void Write(NbtByte nbt) => writer.Write($"{nbt.Value}b");
        public override void Write(NbtShort nbt) => writer.Write($"{nbt.Value}s");
        public override void Write(NbtInt nbt) => writer.Write($"{nbt.Value}");
        public override void Write(NbtLong nbt) => writer.Write($"{nbt.Value}l");
        public override void Write(NbtFloat nbt) => writer.Write($"{nbt.Value}f");
        public override void Write(NbtDouble nbt) => writer.Write($"{nbt.Value}");
        public override void Write(NbtString nbt) => writer.Write($"\"{nbt.Value}\"");
        public override void Write(NbtCompound nbt) {
            bool first = true;
            writer.Write("{");
            foreach (KeyValuePair<string, NbtElement> kvp in nbt) {
                if (!first) 
                    writer.Write(",");
                first = false;
                writer.Write(kvp.Key);
                writer.Write(":");
                WriteNbt(kvp.Value);
            }
            writer.Write("}");
        }
        public override void Write(NbtList nbt) {
            bool first = true;
            writer.Write("[");
            foreach (NbtElement element in nbt) {
                if (!first)
                    writer.Write(",");
                first = false;
                WriteNbt(element);
            }
            writer.Write("]");
        }

        public override void Close() => writer.Close();
    }
}
