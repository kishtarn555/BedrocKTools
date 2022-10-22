using Microsoft.VisualStudio.TestTools.UnitTesting;


using BedrockTools.Nbt;
using BedrockTools.Nbt.Elements;

namespace BedrockToolsMSTest.NbtTests {
    [TestClass]
    public class NbtTester {
        [TestMethod]
        public void TestPrimitives() {
            NbtByte nbyte = new NbtByte(1);
            NbtShort nshort = new NbtShort(2);
            NbtInt nint = new NbtInt(100);
            NbtLong nlong = new NbtLong(11024L);

            Assert.AreEqual(1, nbyte.Value);
            Assert.AreEqual(2, nshort.Value);
            Assert.AreEqual(100, nint.Value);
            Assert.AreEqual(11024L, nlong.Value);


            Assert.AreEqual(NbtTag.TAG_Byte, nbyte.Tag);
            Assert.AreEqual(NbtTag.TAG_Short, nshort.Tag);
            Assert.AreEqual(NbtTag.TAG_Int, nint.Tag);
            Assert.AreEqual(NbtTag.TAG_Long, nlong.Tag);
        }

        
        [TestMethod]
        public void TestSNBT() {
            NbtCompound root = new NbtCompoundOrdered() {
                {"byte", (NbtByte)(-10)},
                {"short", (NbtShort)(123)},
                {"int", (NbtInt)(100050)},
                {"long", (NbtLong)(1000000000000000L)},
                {"string", (NbtString)"teststring" },
                {"list", NbtList.FromInts(1,3,2) },
            };
            Assert.AreEqual("{byte:-10B,short:123S,int:100050,long:1000000000000000L,string:\"teststring\",list:[1,3,2]}", root.ToString());
        }
       
    }
}
