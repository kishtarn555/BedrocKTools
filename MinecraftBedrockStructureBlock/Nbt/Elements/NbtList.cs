using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
            this.elements = new List<NbtElement>();
        }

        public NbtList(NbtTag elementsType, IEnumerable<NbtElement> collection) {
            this.ElementsType = elementsType;
            foreach (NbtElement element in collection) {
                Validate(element);
            }
            this.elements = new List<NbtElement>(collection);
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

        public static NbtList Byte() => new NbtList(NbtTag.TAG_Byte);
        public static NbtList Short() => new NbtList(NbtTag.TAG_Int);
        public static NbtList Int() => new NbtList(NbtTag.TAG_Int);
        public static NbtList Long() => new NbtList(NbtTag.TAG_Int);
        public static NbtList Compound() => new NbtList(NbtTag.TAG_Compound);
        public static NbtList End() => new NbtList(NbtTag.TAG_End);
        public static NbtList Empty() => new NbtList(NbtTag.TAG_End);

        public static NbtList Byte(IEnumerable<sbyte> collection) => new NbtList(NbtTag.TAG_Byte, collection.Select(number => new NbtByte(number)));
        public static NbtList Short(IEnumerable<short> collection) => new NbtList(NbtTag.TAG_Short, collection.Select(number => new NbtShort(number)));
        public static NbtList Int(IEnumerable<int> collection) => new NbtList(NbtTag.TAG_Int, collection.Select(number => new NbtInt(number)));
        public static NbtList Long(IEnumerable<long> collection) => new NbtList(NbtTag.TAG_Long, collection.Select(number => new NbtLong(number)));

        public static NbtList FromBytes(params sbyte[] input) => NbtList.Byte(input);
        public static NbtList FromShorts(params short[] input) => NbtList.Short(input);
        public static NbtList FromInts(params int[] input) => NbtList.Int(input);
        public static NbtList FromLongs(params long[] input) => NbtList.Long(input);
    }

}
