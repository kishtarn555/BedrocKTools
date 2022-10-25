using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using BedrockTools.Nbt;
using BedrockTools.Nbt.Elements;

namespace BedrockToolsMSTest.NbtTests {
    [TestClass]
    public class NbtListTester {
        [TestMethod]
        public void TestList() {
            NbtList list = new NbtList(NbtTag.TAG_Byte);
            list.Add((NbtByte)1);
            list.Add((NbtByte)3);
            list.Add((NbtByte)0);
            sbyte[] expected_list = new sbyte[] { 1, 3, 0 };
            CollectionAssert.AreEqual(expected_list, list.Select(bt => ((NbtByte)bt).Value).ToArray<sbyte>());
        }

        [TestMethod]
        public void TestSNBT() {
            NbtList list = new NbtList(NbtTag.TAG_Byte);
            list.Add((NbtByte)1);
            list.Add((NbtByte)3);
            list.Add((NbtByte)0);
            Assert.AreEqual("[1B,3B,0B]", list.ToString());
        }
        [TestMethod]
        public void TestSNBTSingle() {
            NbtList list = new NbtList(NbtTag.TAG_String);
            list.Add((NbtString)"40");
            Assert.AreEqual("[\"40\"]", list.ToString());
        }
        [TestMethod]
        public void TestSNBTEmpty() {
            NbtList list = new NbtList(NbtTag.TAG_List);
            Assert.AreEqual("[]", list.ToString());
        }
        [TestMethod]
        public void TestEmptyRelativeConstructors() {
            Assert.AreEqual(NbtTag.TAG_Byte,        NbtList.Bytes().ElementsType);
            Assert.AreEqual(NbtTag.TAG_Short,       NbtList.Shorts().ElementsType);
            Assert.AreEqual(NbtTag.TAG_Int,         NbtList.Ints().ElementsType);
            Assert.AreEqual(NbtTag.TAG_Long,        NbtList.Longs().ElementsType);
            Assert.AreEqual(NbtTag.TAG_Float,       NbtList.Floats().ElementsType);
            Assert.AreEqual(NbtTag.TAG_Double,      NbtList.Doubles().ElementsType);
            Assert.AreEqual(NbtTag.TAG_String,      NbtList.Strings().ElementsType);
            Assert.AreEqual(NbtTag.TAG_List,        NbtList.Lists().ElementsType);
            Assert.AreEqual(NbtTag.TAG_Compound,    NbtList.Compounds().ElementsType);
        }

        [TestMethod]
        public void TestRelativeConstructors() {
            NbtList bytes = NbtList.Bytes(new sbyte[] { -1, 2, 3, 4 });
            NbtList shorts = NbtList.Shorts(new short[] {5, -6, 700});
            NbtList ints = NbtList.Ints(new int[] {8, 9, -1000000000 });
            NbtList longs = NbtList.Longs(new long[] { 11, -12, -13, -140000000000 });
            NbtList floats = NbtList.Floats(new float[] { 0.1f, 0.2f, -0.3f, 0 });
            NbtList doubles = NbtList.Doubles(new double[] { 0.3, 0.2, -0.1, 4 });
            NbtList strings = NbtList.Strings(new string[] { "a", "z", "b" });
            NbtList lists = NbtList.Lists(new NbtList[] {NbtList.Ints(new int[] { 1, 2 })});
            NbtList compounds = NbtList.Compounds(new NbtCompound[] { new NbtCompoundOrdered()});

            Assert.AreEqual(NbtTag.TAG_Byte, bytes.ElementsType);
            Assert.AreEqual(NbtTag.TAG_Short, shorts.ElementsType);
            Assert.AreEqual(NbtTag.TAG_Int, ints.ElementsType);
            Assert.AreEqual(NbtTag.TAG_Long, longs.ElementsType);
            Assert.AreEqual(NbtTag.TAG_Float, floats.ElementsType);
            Assert.AreEqual(NbtTag.TAG_Double, doubles.ElementsType);
            Assert.AreEqual(NbtTag.TAG_String, strings.ElementsType);
            Assert.AreEqual(NbtTag.TAG_List, lists.ElementsType);
            Assert.AreEqual(NbtTag.TAG_Compound, compounds.ElementsType);

            CollectionAssert.AreEqual(
                new NbtByte[] {
                    (NbtByte)(-1),
                    (NbtByte)2,
                    (NbtByte)3,
                    (NbtByte)4
                },
                bytes.ToList()
            );
            CollectionAssert.AreEqual(
                new NbtShort[] {
                    (NbtShort)5,
                    (NbtShort)(-6),
                    (NbtShort)700
                },
                shorts.ToList()
            );
            CollectionAssert.AreEqual(
                new NbtInt[] {
                    (NbtInt)8,
                    (NbtInt)9,
                    (NbtInt)(-1000000000)
                },
                ints.ToList()
            );
            CollectionAssert.AreEqual(
                new NbtLong[] {
                    (NbtLong)11,
                    (NbtLong)(-12),
                    (NbtLong)(-13),
                    (NbtLong)(-140000000000)
                },
                longs.ToList()
            );
            CollectionAssert.AreEqual(
                new NbtFloat[] {
                    (NbtFloat)0.1,
                    (NbtFloat)0.2,
                    (NbtFloat)(-0.3f),
                    (NbtFloat)0.0f
                },
                floats.ToList()
            );
            CollectionAssert.AreEqual(
                new NbtDouble[] {
                    (NbtDouble)0.3,
                    (NbtDouble)0.2,
                    (NbtDouble)(-0.1),
                    (NbtDouble)4
                },
                doubles.ToList()
            );
            CollectionAssert.AreEqual(
                new NbtString[] {
                    (NbtString)"a",
                    (NbtString)"z",
                    (NbtString)"b"
                },
                strings.ToList()
            );
            CollectionAssert.AreEqual(
                new NbtString[] {
                    (NbtString)"a",
                    (NbtString)"z",
                    (NbtString)"b"
                },
                strings.ToList()
            );

            CollectionAssert.AreEqual(
                new NbtList[] {
                    NbtList.FromInts(1,2)
                },
                lists.ToList(),
                new IC<NbtElement>()
            );
            CollectionAssert.AreEqual(
                new NbtCompound[] {
                    new NbtCompoundOrdered()
                },
                compounds.ToList(),
                new IC<KeyValuePair<string, NbtElement>>()
            );            
        }
        class IC<T> : IComparer {
            public int Compare(object x, object y) {
                if (x is IEnumerable<T> a && y is IEnumerable<T> b) {
                    return a.SequenceEqual(b)?0:1;
                }
                return -1;
            }
        }
        [TestMethod]
        [ExpectedException(typeof(ArrayTypeMismatchException), "Cannot use NBT of type 'TAG_String' for a NbtList of type 'TAG_Int'")]
        public void TestWrongTagTypeInsertion() {
            NbtList list = NbtList.FromInts(1, 2, 3);
            list.Add((NbtString)"ups, this is a string!");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "NbtList cannot contain null NbtElements")]
        public void TestNullInsertion() {
            NbtList list = NbtList.FromInts(1, 2, 3);
            list.Add(null);
        }

    }
}
