using Microsoft.VisualStudio.TestTools.UnitTesting;
using BedrockTools.Nbt;
using BedrockTools.Nbt.Elements;
using System.Linq;

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
    }
}
