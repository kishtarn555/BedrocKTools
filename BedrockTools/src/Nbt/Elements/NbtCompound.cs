using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace BedrockTools.Nbt.Elements {
    public abstract class NbtCompound : NbtElement, IDictionary<string, NbtElement> {

        public abstract NbtElement this[string key] { get; set; }
        public NbtElement this[string key, string secondkey, params string[] childKeys] {
            get {
                NbtElement child = this[key];
                if ((child is NbtCompound childSecond)) {
                    child = childSecond[secondkey];
                }
                else {
                    throw new Exception($"{child.Tag} is not NbtCompound");
                }
                foreach (string childKey in childKeys) {
                    if ((child is NbtCompound childCompound)) {
                        child = childCompound[childKey];
                    }
                    else {
                        throw new Exception($"{child.Tag} is not NbtCompound");
                    }
                }
                return child;
            } 
        }

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

        public NbtElement Extract(params string[] route) {
            NbtCompound target = this;
            for (int i=0; i < route.Length-1; i++) {
                target = (NbtCompound)target[route[i]];
            }
            return target[route[route.Length-1]];
        }
    }
}
