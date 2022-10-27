using Microsoft.VisualStudio.TestTools.UnitTesting;


using BedrockTools.Objects;
using BedrockTools.Objects.Blocks;
using BedrockTools.Objects.Blocks.Util;

namespace BedrockToolsMSTest.ObjectsTests.Blocks {
    [TestClass]
    public class StairsBlockTester {

        public StairsBlock[] combs = new StairsBlock[]{
            VanillaBlockFactory.AcaciaStairs(BlockOrientation.North, false),
            VanillaBlockFactory.AcaciaStairs(BlockOrientation.West, false),
            VanillaBlockFactory.AcaciaStairs(BlockOrientation.South, false),
            VanillaBlockFactory.AcaciaStairs(BlockOrientation.East, false),
            VanillaBlockFactory.AcaciaStairs(BlockOrientation.North, true),
            VanillaBlockFactory.AcaciaStairs(BlockOrientation.West, true),
            VanillaBlockFactory.AcaciaStairs(BlockOrientation.South, true),
            VanillaBlockFactory.AcaciaStairs(BlockOrientation.East, true),
        };

        [TestMethod]
        public void TestRotation() {
            Assert.AreEqual(combs[1], combs[0].Transform(McTransform.Identity.Rotate(McRotation.n90)));
            Assert.AreEqual(combs[2], combs[1].Transform(McTransform.Identity.Rotate(McRotation.n90)));
            Assert.AreEqual(combs[3], combs[2].Transform(McTransform.Identity.Rotate(McRotation.n90)));
            Assert.AreEqual(combs[0], combs[3].Transform(McTransform.Identity.Rotate(McRotation.n90)));
            Assert.AreEqual(combs[5], combs[4].Transform(McTransform.Identity.Rotate(McRotation.n90)));
            Assert.AreEqual(combs[6], combs[5].Transform(McTransform.Identity.Rotate(McRotation.n90)));
            Assert.AreEqual(combs[7], combs[6].Transform(McTransform.Identity.Rotate(McRotation.n90)));
            Assert.AreEqual(combs[4], combs[7].Transform(McTransform.Identity.Rotate(McRotation.n90)));

            Assert.AreEqual(combs[2], combs[0].Transform(McTransform.Identity.Rotate(McRotation.n180)));
            Assert.AreEqual(combs[3], combs[1].Transform(McTransform.Identity.Rotate(McRotation.n180)));
            Assert.AreEqual(combs[0], combs[2].Transform(McTransform.Identity.Rotate(McRotation.n180)));
            Assert.AreEqual(combs[1], combs[3].Transform(McTransform.Identity.Rotate(McRotation.n180)));
            Assert.AreEqual(combs[6], combs[4].Transform(McTransform.Identity.Rotate(McRotation.n180)));
            Assert.AreEqual(combs[7], combs[5].Transform(McTransform.Identity.Rotate(McRotation.n180)));
            Assert.AreEqual(combs[4], combs[6].Transform(McTransform.Identity.Rotate(McRotation.n180)));
            Assert.AreEqual(combs[5], combs[7].Transform(McTransform.Identity.Rotate(McRotation.n180)));

            Assert.AreEqual(combs[3], combs[0].Transform(McTransform.Identity.Rotate(McRotation.n270)));
            Assert.AreEqual(combs[0], combs[1].Transform(McTransform.Identity.Rotate(McRotation.n270)));
            Assert.AreEqual(combs[1], combs[2].Transform(McTransform.Identity.Rotate(McRotation.n270)));
            Assert.AreEqual(combs[2], combs[3].Transform(McTransform.Identity.Rotate(McRotation.n270)));
            Assert.AreEqual(combs[7], combs[4].Transform(McTransform.Identity.Rotate(McRotation.n270)));
            Assert.AreEqual(combs[4], combs[5].Transform(McTransform.Identity.Rotate(McRotation.n270)));
            Assert.AreEqual(combs[5], combs[6].Transform(McTransform.Identity.Rotate(McRotation.n270)));
            Assert.AreEqual(combs[6], combs[7].Transform(McTransform.Identity.Rotate(McRotation.n270)));
        }

        [TestMethod]
        public void TestReflection() {
            Assert.AreEqual(combs[0], (StairsBlock)combs[0].Transform(McTransform.Identity.MirrorX()));
            Assert.AreEqual(combs[3], combs[1].Transform(McTransform.Identity.MirrorX()));
            Assert.AreEqual(combs[2], combs[2].Transform(McTransform.Identity.MirrorX()));
            Assert.AreEqual(combs[1], combs[3].Transform(McTransform.Identity.MirrorX()));
            Assert.AreEqual(combs[4], combs[4].Transform(McTransform.Identity.MirrorX()));
            Assert.AreEqual(combs[7], combs[5].Transform(McTransform.Identity.MirrorX()));
            Assert.AreEqual(combs[6], combs[6].Transform(McTransform.Identity.MirrorX()));
            Assert.AreEqual(combs[5], combs[7].Transform(McTransform.Identity.MirrorX()));


            Assert.AreEqual(combs[2], combs[0].Transform(McTransform.Identity.MirrorZ()));
            Assert.AreEqual(combs[1], combs[1].Transform(McTransform.Identity.MirrorZ()));
            Assert.AreEqual(combs[0], combs[2].Transform(McTransform.Identity.MirrorZ()));
            Assert.AreEqual(combs[3], combs[3].Transform(McTransform.Identity.MirrorZ()));
            Assert.AreEqual(combs[6], combs[4].Transform(McTransform.Identity.MirrorZ()));
            Assert.AreEqual(combs[5], combs[5].Transform(McTransform.Identity.MirrorZ()));
            Assert.AreEqual(combs[4], combs[6].Transform(McTransform.Identity.MirrorZ()));
            Assert.AreEqual(combs[7], combs[7].Transform(McTransform.Identity.MirrorZ()));
        }
    }
}
