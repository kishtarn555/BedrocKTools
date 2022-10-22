﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;

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
            Assert.IsTrue(compound["b"] is NbtCompound);
            Assert.IsTrue(compound["a"] is NbtString);
            Assert.IsTrue(compound["c"] is NbtInt);
            Assert.IsTrue(compound["b", "number"] is NbtByte);

        }
    }
}
