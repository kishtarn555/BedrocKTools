using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace BedrockTools.Nbt.Elements {
    /// <summary>
    /// This is an NbtCompound based on C# dictionary, recomended for performance when you dont care about order.
    /// </summary>
    public class NbtCompoundUnordered : NbtCompound {
        protected Dictionary<string, NbtElement> elements;
        public NbtCompoundUnordered() {
            elements = new Dictionary<string, NbtElement>();
        }

        public override NbtElement this[string key] { get => elements[key]; set => elements[key] = value; }

        public override ICollection<string> Keys => elements.Keys;

        public override ICollection<NbtElement> Values => elements.Values;

        public override int Count => elements.Count;

        public override bool IsReadOnly => false;

        public override void Add(string key, NbtElement value) => elements.Add(key, value);

        public override void Add(KeyValuePair<string, NbtElement> item) => elements.Add(item.Key, item.Value);

        public override void Clear() => elements.Clear();

        public override bool Contains(KeyValuePair<string, NbtElement> item) => ((IDictionary<string, NbtElement>)elements).Contains(item);

        public override bool ContainsKey(string key) => elements.ContainsKey(key);

        public override void CopyTo(KeyValuePair<string, NbtElement>[] array, int arrayIndex) => ((IDictionary<string, NbtElement>)elements).CopyTo(array, arrayIndex);

        public override IEnumerator<KeyValuePair<string, NbtElement>> GetEnumerator() => elements.GetEnumerator();

        public override bool Remove(string key) => elements.Remove(key);

        public override bool Remove(KeyValuePair<string, NbtElement> item) => ((IDictionary<string, NbtElement>)elements).Remove(item);

        public override bool TryGetValue(string key, [MaybeNullWhen(false)] out NbtElement value) => elements.TryGetValue(key, out value);
    }
}
