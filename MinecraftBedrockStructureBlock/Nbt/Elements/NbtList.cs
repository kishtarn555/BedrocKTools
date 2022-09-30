using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace BedrockTools.Nbt.Elements {
    public class NbtList : NbtElement, IList<NbtElement> {
        public override NbtTag Tag => NbtTag.TAG_List;
        public NbtTag ElementsType { get; protected set; }
        List<NbtElement> elements;
        public int Count => elements.Count;

        public bool IsReadOnly => false;

        public NbtElement this[int index] { get => elements[index]; set {
                Validate(value);
                elements[index] = value;
            }
        }

        public NbtList(NbtTag elementsType) {
            this.ElementsType = elementsType;
            elements = new List<NbtElement>();
        }

        public override void WriteValue(BinaryWriter writer) {
            writer.Write((byte)ElementsType);
            writer.Write((int)elements.Count);
            foreach (NbtElement element in elements) {
                element.WriteValue(writer);
            }
        }

        void Validate(NbtElement element) {
            if (element == null)
                throw new ArgumentNullException("NbtList cannot contain null NbtElements");
            if (element.Tag != ElementsType)
                throw new ArgumentException($"Cannot use NBT of type '{element.Tag}' for a NbtList of type '{ElementsType}'");
        }

        public int IndexOf(NbtElement item) => elements.IndexOf(item);

        public void Insert(int index, NbtElement item) {
            Validate(item);
            elements.Insert(index, item);
        }

        public void RemoveAt(int index) => elements.RemoveAt(index);

        public void Add(NbtElement item)  {
            Validate(item);
            elements.Add(item);
        }

        public void Clear() => elements.Clear();

        public bool Contains(NbtElement item) => elements.Contains(item);

        public void CopyTo(NbtElement[] array, int arrayIndex) => elements.CopyTo(array, arrayIndex);

        public bool Remove(NbtElement item) => elements.Remove(item);

        public IEnumerator<NbtElement> GetEnumerator() => elements.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public override string ToString() {
            string answer = "[";
            bool first = true;
            foreach (NbtElement element in this) {
                if (!first) answer += ",";
                answer += element.ToString();
                first = false;
            }
            answer += "]";
            return answer;
        }
    }
}
