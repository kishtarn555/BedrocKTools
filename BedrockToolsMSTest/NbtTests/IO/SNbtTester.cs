using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using BedrockToolsMSTest.Utils;

using BedrockTools.Nbt.Elements;
using BedrockTools.Nbt.IO;

namespace BedrockToolsMSTest.NbtTests.IO {
    [TestClass]
    public class SNbtTester {

        [TestMethod]
        public void TestPrimitivesInt() {
            StringWriter stringWriter = new StringWriter();
            SNbtWriter snbt = new SNbtWriter(stringWriter);

            NbtByte nbtByte = new NbtByte(1);
            NbtShort nbtShort = new NbtShort(2);
            NbtInt nbtInt = new NbtInt(3);
            NbtLong nbtLong = new NbtLong(4);

            NbtCompound compound = new NbtCompoundOrdered() {
                {"byte", nbtByte },
                {"short", nbtShort },
                {"int", nbtInt },
                {"long", nbtLong },
            };

            snbt.Write(compound);
            snbt.Close();
            Assert.AreEqual("{byte:1b,short:2s,int:3,long:4l}", stringWriter.ToString());
            SNbtParser<NbtCompoundOrdered> parser = new SNbtParser<NbtCompoundOrdered>(stringWriter.ToString());
            NbtAssert.AssertUnspecifiedNbt(compound, parser.Parse(), "Bad parse");
        }

        [TestMethod]
        public void TestSring() {
            StringWriter stringWriter = new StringWriter();
            SNbtWriter snbt = new SNbtWriter(stringWriter);
            NbtCompound compound = new NbtCompoundOrdered() {
                {"str", (NbtString)"message" }
            };
            snbt.Write(compound);
            snbt.Close();
            Assert.AreEqual("{str:\"message\"}", stringWriter.ToString());
            SNbtParser<NbtCompoundOrdered> parser = new SNbtParser<NbtCompoundOrdered>(stringWriter.ToString());
            NbtAssert.AssertUnspecifiedNbt(compound, parser.Parse(), "Bad parse");
        }
        [TestMethod]
        public void TestList() {
            StringWriter stringWriter = new StringWriter();
            SNbtWriter snbt = new SNbtWriter(stringWriter);
            NbtCompound compound = new NbtCompoundOrdered() {
                {"multiple", NbtList.FromLongs(4,3,2,1,2,3,4) },
                {"single", NbtList.FromLongs(-10) }
            };
            snbt.Write(compound);
            snbt.Close();
            Assert.AreEqual("{multiple:[4l,3l,2l,1l,2l,3l,4l],single:[-10l]}", stringWriter.ToString());
            SNbtParser<NbtCompoundOrdered> parser = new SNbtParser<NbtCompoundOrdered>(stringWriter.ToString());
            NbtAssert.AssertUnspecifiedNbt(compound, parser.Parse(), "Bad parse");
        }

        [TestMethod]
        public void TestFloatDouble() {
            StringWriter stringWriter = new StringWriter();
            SNbtWriter snbt = new SNbtWriter(stringWriter);
            NbtCompound compound = new NbtCompoundOrdered() {
                {"double", (NbtDouble)2.71828183 },
                {"float", (NbtFloat)3.1415f }
            };
            snbt.Write(compound);
            snbt.Close();
            Assert.AreEqual("{double:2.71828183,float:3.1415f}", stringWriter.ToString());
            SNbtParser<NbtCompoundOrdered> parser = new SNbtParser<NbtCompoundOrdered>(stringWriter.ToString());
            NbtAssert.AssertUnspecifiedNbt(compound, parser.Parse(), "Bad parse");
        }


    }
}
