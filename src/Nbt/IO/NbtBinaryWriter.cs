using System;
using System.Collections.Generic;
using System.IO;
using BedrockTools.Nbt.Elements;

namespace BedrockTools.Nbt.IO {
    public class NbtBinaryWriter : NbtWriter {
        BinaryWriter writer;

        public NbtBinaryWriter(BinaryWriter writer) {
            this.writer = writer;
        }

        public NbtBinaryWriter(Stream stream) {
            writer = new BinaryWriter(stream);
        }

        public override void Write(NbtByte nbt) => writer.Write(nbt.Value);
        public override void Write(NbtShort nbt) => writer.Write(nbt.Value);
        public override void Write(NbtInt nbt) => writer.Write(nbt.Value);
        public override void Write(NbtLong nbt) => writer.Write(nbt.Value);
        public override void Write(NbtFloat nbt) => throw new NotImplementedException();
        public override void Write(NbtDouble nbt) => throw new NotImplementedException();
        public override void Write(NbtString nbt) => WriteString(nbt.Value);
        
        public override void Write(NbtCompound nbt) {
            foreach (KeyValuePair<string, NbtElement> kvp in nbt) {
                writer.Write((byte)kvp.Value.Tag);
                WriteString(kvp.Key);
                WriteNbt(kvp.Value);
            }
            writer.Write((byte)NbtTag.TAG_End);
        }
        public override void Write(NbtList nbt) {
            writer.Write((byte)nbt.ElementsType);
            writer.Write((int)nbt.Count);
            foreach (NbtElement element in nbt) {
                WriteNbt(element);
            }
        }

        protected void WriteString(string str) {
            writer.Write((short)str.Length);
            foreach (char c in str) {
                writer.Write(c);
            }
        }

        public void WriteRoot(NbtCompound rootCompound) {
            writer.Write((byte)rootCompound.Tag);
            writer.Write((short)0);
            Write(rootCompound);
        }
        public override void Close() => writer.Close();
    }
}
