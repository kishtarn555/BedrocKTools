using System.IO;

using BedrockTools.Nbt;
using BedrockTools.Nbt.Elements;
using BedrockTools.Nbt.IO;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BedrockToolsMSTest.NbtTests.IO {
    [TestClass]
    public class NbtBinaryWriterTester {
        [TestMethod]
        public void TestNbtBynary() {
            MemoryStream memory = new MemoryStream();
            BinaryWriter binaryWriter = new BinaryWriter(memory);
            NbtCompoundOrdered root = new NbtCompoundOrdered();
            NbtCompoundOrdered dummy = new NbtCompoundOrdered();
            root.Add("dummy", dummy);
            root.Add("index", new NbtInt(1000000000));
            root.Add("bt", new NbtByte(-12));
            root.Add("st", new NbtShort(1300));
            root.Add("ln", new NbtLong(14000000000000000));
            NbtList version = new NbtList(NbtTag.TAG_Int);
            version.Add(new NbtInt(1));
            version.Add(new NbtInt(19));
            version.Add(new NbtInt(3));
            version.Add(new NbtInt(0));
            dummy.Add("version", version);
            new NbtBinaryWriter(binaryWriter).WriteRoot(root);
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
            {
                Assert.AreEqual((byte)3, reader.ReadByte());//TAG_int
                Assert.AreEqual((short)5, reader.ReadInt16());
                char[] read = reader.ReadChars(5);
                CollectionAssert.AreEqual("index".ToCharArray(), read);
                Assert.AreEqual(1000000000, reader.ReadInt32());
            }
            {
                Assert.AreEqual((byte)1, reader.ReadByte());//TAG_byte
                Assert.AreEqual((short)2, reader.ReadInt16());
                char[] read = reader.ReadChars(2);
                CollectionAssert.AreEqual("bt".ToCharArray(), read);
                Assert.AreEqual(-12, reader.ReadSByte());
            }
            {
                Assert.AreEqual((byte)2, reader.ReadByte());//TAG_short
                Assert.AreEqual((short)2, reader.ReadInt16());
                char[] read = reader.ReadChars(2);
                CollectionAssert.AreEqual("st".ToCharArray(), read);
                Assert.AreEqual(1300, reader.ReadInt16());
            }
            {
                Assert.AreEqual((byte)4, reader.ReadByte());//TAG_long
                Assert.AreEqual((short)2, reader.ReadInt16());
                char[] read = reader.ReadChars(2);
                CollectionAssert.AreEqual("ln".ToCharArray(), read);
                Assert.AreEqual(14000000000000000, reader.ReadInt64());
            }
            Assert.AreEqual(0x00, reader.ReadByte());//Compound end
            Assert.AreEqual(reader.BaseStream.Length, reader.BaseStream.Position, "there was more data than expected");
            
        }
    }
}
