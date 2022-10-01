using Microsoft.VisualStudio.TestTools.UnitTesting;
using BedrockTools.Nbt;
using BedrockTools.Nbt.Util;
using BedrockTools.Nbt.Elements;
using System.IO;

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
        public void TestNbtBynary() {
            MemoryStream memory = new MemoryStream();
            BinaryWriter binaryWriter = new BinaryWriter(memory);
            NbtCompoundOrdered root = new NbtCompoundOrdered();
            NbtCompoundOrdered dummy = new NbtCompoundOrdered();
            root.Add("dummy",dummy);
            root.Add("index", new NbtInt(123));
            NbtList version = new NbtList(NbtTag.TAG_Int);
            version.Add(new NbtInt(1));
            version.Add(new NbtInt(19));
            version.Add(new NbtInt(3));
            version.Add(new NbtInt(0));
            dummy.Add("version", version);
            WriterUtil.WriteRootCompound(binaryWriter,root);
            BinaryReader reader = new BinaryReader(new MemoryStream(memory.ToArray()));
            Assert.AreEqual(0x0A, reader.ReadByte());//TAG_Compound
            Assert.AreEqual(0x00, reader.ReadByte());//NameLenght
            Assert.AreEqual(0x00, reader.ReadByte());
            Assert.AreEqual(0x0A, reader.ReadByte());//TAG_Compound
            Assert.AreEqual((short)5, reader.ReadInt16());
            CollectionAssert.AreEqual("dummy".ToCharArray(), reader.ReadChars(5));
            Assert.AreEqual(0x09, reader.ReadByte());//TAG_List
            Assert.AreEqual((short)"version".Length, reader.ReadInt16());//length
            CollectionAssert.AreEqual("version".ToCharArray(), reader.ReadChars("version".Length));
            Assert.AreEqual(0x03, reader.ReadByte());//Tag_INT
            Assert.AreEqual(4, reader.ReadInt32());//Length
            Assert.AreEqual(1, reader.ReadInt32());
            Assert.AreEqual(19, reader.ReadInt32());
            Assert.AreEqual(3, reader.ReadInt32());
            Assert.AreEqual(0, reader.ReadInt32());
            Assert.AreEqual(0x00, reader.ReadByte());//Compound end
            Assert.AreEqual((byte)3, reader.ReadByte());//TAG_int
            Assert.AreEqual((short)5, reader.ReadInt16());
            char[] read = reader.ReadChars(5);
            CollectionAssert.AreEqual("index".ToCharArray(), read);
            Assert.AreEqual(123, reader.ReadInt32());
            Assert.AreEqual(0x00, reader.ReadByte());//Compound end
            Assert.AreEqual(reader.BaseStream.Length, reader.BaseStream.Position, "there was more data than expected");
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
