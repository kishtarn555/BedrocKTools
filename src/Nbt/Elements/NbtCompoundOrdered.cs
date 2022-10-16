using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace BedrockTools.Nbt.Elements {
    /// <summary>
    /// This is a NbtCompound that keeps insertion order, however operations are O(N)
    /// </summary>
    public class NbtCompoundOrdered : NbtCompound {
        List<KeyValuePair<string, NbtElement>> elements;
        public NbtCompoundOrdered() {
            elements = new List<KeyValuePair<string, NbtElement>>();
        }

        public override NbtElement this[string key] {
            get => elements.Single(element => element.Key == key).Value;
            set {
                KeyValuePair<string, NbtElement> kvp = new KeyValuePair<string, NbtElement>(key, value);
                int index = elements.FindIndex(element => element.Key == key);
                if (index >= 0)
                    elements[index] = kvp;
                else
                    elements.Add(kvp);
            }
        }

        public override ICollection<string> Keys => elements.Select(element => element.Key).ToList();

        public override ICollection<NbtElement> Values => elements.Select(element => element.Value).ToList();

        public override int Count => elements.Count;

        public override bool IsReadOnly => false;

        public override void Add(string key, NbtElement value) {
            if (elements.Any(element => element.Key == key)) 
                throw new ArgumentException($"NbtElement with duplicate key added {key}");
            elements.Add(new KeyValuePair<string, NbtElement>(key, value));
        }

        public override void Add(KeyValuePair<string, NbtElement> item) {
            if (elements.Any(element => element.Key == item.Key))
                throw new ArgumentException($"NbtElement with duplicate key added {item.Key}");
            elements.Add(item);
        }

        public override void Clear() => elements.Clear();

        public override bool Contains(KeyValuePair<string, NbtElement> item) => elements.Contains(item);

        public override bool ContainsKey(string key) => Keys.Contains(key);

        public override void CopyTo(KeyValuePair<string, NbtElement>[] array, int arrayIndex) => elements.CopyTo(array, arrayIndex);
        

        public override IEnumerator<KeyValuePair<string, NbtElement>> GetEnumerator() {
            return elements.GetEnumerator();
        }

        public override bool Remove(string key) => elements.RemoveAll(element => element.Key == key) > 0;
        

        public override bool Remove(KeyValuePair<string, NbtElement> item) => elements.Remove(item);

        public override bool TryGetValue(string key, [MaybeNullWhen(false)] out NbtElement value) {
            int index = elements.FindIndex(element => element.Key == key);
            if (index >= 0) {
                value = elements[index].Value;
                return true;
            }
            value = null;
            return false;
        }
    }
}
