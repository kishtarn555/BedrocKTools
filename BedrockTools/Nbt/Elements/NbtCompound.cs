using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using BedrockTools.Nbt.Util;

namespace BedrockTools.Nbt.Elements {
    public abstract class NbtCompound : NbtElement, IDictionary<string, NbtElement> {
        public override void WriteValue(BinaryWriter writer) {
            foreach (KeyValuePair<string, NbtElement> kvp in this) {
                kvp.Value.WriteTag(writer);
                WriterUtil.WriteString(writer, kvp.Key);
                kvp.Value.WriteValue(writer);
            }
            writer.Write((byte)NbtTag.TAG_End);
            //writer.Write((byte)NbtTag.TAG_End);
        }
        public abstract NbtElement this[string key] { get; set; }

        public override NbtTag Tag => NbtTag.TAG_Compound;

        public abstract ICollection<string> Keys { get; }
        public abstract ICollection<NbtElement> Values { get; }
        public abstract int Count { get; }
        public abstract bool IsReadOnly { get; }

        public abstract void Add(string key, NbtElement value);
        public abstract void Add(KeyValuePair<string, NbtElement> item);
        public abstract void Clear();
        public abstract bool Contains(KeyValuePair<string, NbtElement> item);
        public abstract bool ContainsKey(string key);
        public abstract void CopyTo(KeyValuePair<string, NbtElement>[] array, int arrayIndex);
        public abstract IEnumerator<KeyValuePair<string, NbtElement>> GetEnumerator();
        public abstract bool Remove(string key);
        public abstract bool Remove(KeyValuePair<string, NbtElement> item);
        public abstract bool TryGetValue(string key, [MaybeNullWhen(false)] out NbtElement value);
        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        public override string ToString() {
            string answer = "{";
            bool first = true;
            foreach (KeyValuePair<string, NbtElement> kvp in this) {
                if (!first) answer += ",";
                answer += kvp.Key + ":" + kvp.Value.ToString();
                first = false;
            }
            answer += "}";
            return answer;
        }
    }
}
