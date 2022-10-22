using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Collections;
using System.Collections.Generic;
using BedrockTools.Nbt.Elements;

namespace BedrockToolsMSTest.Utils {
    public class NbtAssert {
        public static void AssertNbt(NbtCompound expected, NbtCompound actual, string message, string path="/") {
            CollectionAssert.AreEquivalent((ICollection)expected.Keys, (ICollection)actual.Keys, $"{message} in {path}: miss match in keys");
            foreach (KeyValuePair<string, NbtElement> kvp in expected) {
                AssertUnspecifiedNbt(kvp.Value, actual[kvp.Key], message+ "", path + kvp.Key+"/");
            }
        }
        public static void AssertNbt(NbtList expected, NbtList actual, string message, string path = "") {
            Assert.AreEqual(expected.ElementsType, actual.ElementsType, $"{message} in {path}: lists are of different types");
            Assert.AreEqual(expected.Count, actual.Count, $"{message} in {path}: lists are of different sizes");
            for (int i = 0; i < expected.Count; i++) {
                AssertUnspecifiedNbt(expected[i], actual[i], message, path);
            }
        }

        private static void AssertUnspecifiedNbt(NbtElement a, NbtElement b, string message, string path) {
            if (a is NbtCompound comA && b is NbtCompound comB) {
                AssertNbt(comA, comB, message, path);
            } else if (a is NbtList listA && b is NbtList listB) {
                AssertNbt(listA, listB, message, path);
            }
        }
    }
}
