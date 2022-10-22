using Microsoft.VisualStudio.TestTools.UnitTesting;
using BedrockTools.Structure;
using BedrockTools.Objects;
using BedrockTools.Objects.Blocks;

namespace BedrockToolsMSTest.Structure {
    [TestClass]
    public class McStructureTester {
        [TestMethod]
        public void TestEmptyStructure() {
            McStructure structure = new McStructure(new Dimensions(5, 7, 9));
            Assert.AreEqual(5, structure.Size.X, "X dimensions did not match expected value");
            Assert.AreEqual(7, structure.Size.Y, "Y dimensions did not match expected value");
            Assert.AreEqual(9, structure.Size.Z, "Z dimensions did not match expected value");
            Block[,,] expectedBlocks = new Block[5, 7, 9];
            CollectionAssert.AreEqual(expectedBlocks, structure.GetBlocks());
            Assert.AreEqual(IntCoords.Zero, structure.Origin);
        }

        [TestMethod]
        public void TestGetBlocks() {
            McStructure structure = new McStructure(new Dimensions(5, 7, 9));
            Block[,,] expectedBlocks = new Block[5, 7, 9];
            CollectionAssert.AreEqual(expectedBlocks, structure.GetBlocks());
            Block[,,] blocks = structure.GetBlocks();
            blocks[0, 0, 0] = VanillaBlockFactory.Dirt(
                BedrockTools.Objects.Blocks.Minecraft.DirtBlock.DirtType.Normal
            );
            CollectionAssert.AreEqual(
                expectedBlocks,
                structure.GetBlocks(),
                "Block changed after modifying array return by GetBlocks, when it shouldn't"
            );
            Assert.AreNotEqual(
                blocks[0, 0, 0],
                structure.GetBlock(0, 0, 0),
                "Block in 0,0,0 was changed when changing what should have been the copy returned by getblocks"
            );
        }
        [TestMethod]
        public void TestSetBlock() {
            McStructure structure = new McStructure(new Dimensions(10, 10, 10));
            Block[,,] expected = new Block[10, 10, 10];
            CollectionAssert.AreEqual(expected, structure.GetBlocks());
            structure.SetBlock(2, 3, 4, VanillaBlockFactory.Glass());
            expected[2, 3, 4] = VanillaBlockFactory.Glass();
            CollectionAssert.AreEqual(expected, structure.GetBlocks());
            expected[1, 8, 5] = VanillaBlockFactory.Air();
            structure.SetBlock(new IntCoords(1, 8, 5), VanillaBlockFactory.Air());

        }
    }
}
