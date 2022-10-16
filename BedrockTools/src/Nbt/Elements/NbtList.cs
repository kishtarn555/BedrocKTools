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

        public static NbtList Bytes() 
            => new NbtList(NbtTag.TAG_Byte);
        public static NbtList Shorts() 
            => new NbtList(NbtTag.TAG_Int);
        public static NbtList Ints() 
            => new NbtList(NbtTag.TAG_Int);
        public static NbtList Longs() 
            => new NbtList(NbtTag.TAG_Int);
        public static NbtList Floats() 
            => new NbtList(NbtTag.TAG_Float);
        public static NbtList Doubles() 
            => new NbtList(NbtTag.TAG_Double);
        public static NbtList Strings() 
            => new NbtList(NbtTag.TAG_String);
        public static NbtList Lists() 
            => new NbtList(NbtTag.TAG_Double);
        public static NbtList Compounds() 
            => new NbtList(NbtTag.TAG_Compound);
        public static NbtList Empty() 
            => new NbtList(NbtTag.TAG_End);

        public static NbtList Bytes(IEnumerable<sbyte> collection) 
            => new NbtList(NbtTag.TAG_Byte, collection.Select(number => new NbtByte(number)));
        public static NbtList Shorts(IEnumerable<short> collection) 
            => new NbtList(NbtTag.TAG_Short, collection.Select(number => new NbtShort(number)));
        public static NbtList Ints(IEnumerable<int> collection)     
            => new NbtList(NbtTag.TAG_Int, collection.Select(number => new NbtInt(number)));
        public static NbtList Longs(IEnumerable<long> collection) 
            => new NbtList(NbtTag.TAG_Long, collection.Select(number => new NbtLong(number)));
        public static NbtList Floats(IEnumerable<float> collection) 
            => new NbtList(NbtTag.TAG_Float, collection.Select(number => new NbtFloat(number)));
        public static NbtList Doubles(IEnumerable<double> collection) 
            => new NbtList(NbtTag.TAG_Double, collection.Select(number => new NbtDouble(number)));
        public static NbtList Strings(IEnumerable<string> collection)
            => new NbtList(NbtTag.TAG_String, collection.Select(str => new NbtString(str)));
        public static NbtList Lists(IEnumerable<NbtList> collection)
            => new NbtList(NbtTag.TAG_Compound, collection);
        public static NbtList Compounds(IEnumerable<NbtCompound> collection)
            => new NbtList(NbtTag.TAG_Compound, collection);

        public static NbtList FromBytes(params sbyte[] values) 
            => NbtList.Bytes(values);
        public static NbtList FromShorts(params short[] values) 
            => NbtList.Shorts(values);
        public static NbtList FromInts(params int[] values) 
            => NbtList.Ints(values);
        public static NbtList FromLongs(params long[] values) 
            => NbtList.Longs(values);
        public static NbtList FromFloats(params float[] values) 
            => NbtList.Floats(values);
        public static NbtList FromDoubles(params double[] values) 
            => NbtList.Doubles(values);
        public static NbtList FromStrings(params string[] values)
            => NbtList.Strings(values);
        public static NbtList FromLists(params NbtList[] values)
            => NbtList.Lists(values);
        public static NbtList FromCompounds(params NbtCompound[] values)
            => NbtList.Compounds(values);
    }

}
