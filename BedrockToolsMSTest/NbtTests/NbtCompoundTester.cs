using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;
using BedrockToolsMSTest.Utils;

using BedrockTools.Nbt.Elements;

namespace BedrockToolsMSTest.NbtTests {
    [TestClass]
    public class NbtCompoundTester {
        [TestMethod]
        public void TestNbtCompoundOrdered() {
            NbtCompound compound = new NbtCompoundOrdered() {
                {"one", new NbtCompoundOrdered() {
                    {"number",  (NbtInt)3}
                }},
                {"two", (NbtString)"message" }
            };
            CollectionAssert.AreEqual(new string[] { "one", "two" }, (ICollection)compound.Keys);
            Assert.IsTrue(compound["one"] is NbtCompound);
            Assert.IsTrue(compound["two"] is NbtString);
            CollectionAssert.AreEqual(new string[] { "number" }, (ICollection)(compound["one"] as NbtCompound).Keys);
            CollectionAssert.AreEqual(new string[] { "number" }, (ICollection)(compound["one"] as NbtCompound).Keys);
            Assert.AreEqual("message", (compound["two"] as NbtString).Value);
            compound["newElement"] = (NbtString)"str";
            CollectionAssert.AreEqual(new string[] { "one", "two", "newElement" }, (ICollection)compound.Keys);
            Assert.IsTrue(compound["newElement"] is NbtString);
            Assert.AreEqual((NbtInt)3, compound["one", "number"]);
            Assert.AreEqual((NbtString)"message", compound["two"]);
            compound.Clear();
            Assert.AreEqual(0, compound.Count, "Element count was not the expected value after clearing");
            CollectionAssert.AreEqual(new string[] { }, (ICollection)compound.Keys);
            
        }

        [TestMethod]
        public void TestNbtCompoundSorted() {
            NbtCompound compound = new NbtCompoundSorted() {
                {"b", new NbtCompoundSorted() {
                    {"number",  (NbtByte)3}
                }},
                {"a", (NbtString)"message" },
                {"c", (NbtInt)3 }
            };
            CollectionAssert.AreEqual(new string[] { "a", "b", "c" }, (ICollection)compound.Keys);
            Assert.AreEqual(3, compound.Count, "Element count was not the expected value");
            Assert.IsTrue(compound["b"] is NbtCompound);
            Assert.IsTrue(compound["a"] is NbtString);
            Assert.IsTrue(compound["c"] is NbtInt);
            Assert.IsTrue(compound["b", "number"] is NbtByte);
            compound["barry"] = (NbtInt)10;
            CollectionAssert.AreEqual(new string[] { "a", "b", "barry", "c" }, (ICollection)compound.Keys);
            compound.Add("z", NbtList.Empty());
            CollectionAssert.AreEqual(new string[] { "a", "b", "barry", "c", "z" }, (ICollection)compound.Keys);
            Assert.AreEqual(5, compound.Count, "Element count was not the expected value");
            compound.Clear();
            Assert.AreEqual(0, compound.Count, "Element count was not the expected value after clearing");
            CollectionAssert.AreEqual(new string[] { }, (ICollection)compound.Keys);
        }
        [TestMethod]
        public void TestNbtUnordered() {
            NbtCompound compound = new NbtCompoundUnordered() {
                {"b", new NbtCompoundUnordered() {
                    {"number",  (NbtByte)3}
                }},
                {"a", (NbtString)"message" },
                {"c", (NbtInt)3 }
            };
            CollectionAssert.AreEquivalent(new string[] { "a", "b", "c" }, (ICollection)compound.Keys);
            Assert.AreEqual(3, compound.Count, "Element count was not the expected value");
            Assert.IsTrue(compound["b"] is NbtCompound);
            Assert.IsTrue(compound["a"] is NbtString);
            Assert.IsTrue(compound["c"] is NbtInt);
            Assert.IsTrue(compound["b", "number"] is NbtByte);
            compound["barry"] = (NbtInt)10;
            CollectionAssert.AreEquivalent(new string[] { "a", "b", "barry", "c" }, (ICollection)compound.Keys);
            compound.Add("z", NbtList.Empty());
            CollectionAssert.AreEquivalent(new string[] { "a", "b", "barry", "c", "z" }, (ICollection)compound.Keys);
            Assert.AreEqual(5, compound.Count, "Element count was not the expected value");
            compound.Clear();
            Assert.AreEqual(0, compound.Count, "Element count was not the expected value after clearing");
            CollectionAssert.AreEqual(new string[] { }, (ICollection)compound.Keys);
        }

        [TestMethod]
        public void TestExtractMethod() {
            NbtInt value = new NbtInt(24);
            NbtString dummy = new NbtString("hello");
            NbtCompound compound = new NbtCompoundOrdered()
            {
                { "child", new  NbtCompoundOrdered() {
                    { "value", value }
                }},
                { "dummy", dummy }
            };
            NbtElement expected_dummy = compound.Extract("dummy");

            Assert.IsTrue(expected_dummy is NbtString, "Expected and NbtString under compound.extract('dummy')");
            Assert.AreEqual(expected_dummy as NbtString, dummy, "Got another element under compound.extract('dummy')");
            NbtElement expected_value = compound.Extract("child", "value");
            Assert.IsTrue(expected_value is NbtInt, "Expected and NbtString under compound.extract('child', 'value')");
            Assert.AreEqual(expected_value as NbtInt, value, "Got another nbt element under compound.extract('child', 'value')");
        }
    }
}
