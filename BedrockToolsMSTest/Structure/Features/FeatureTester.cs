using Microsoft.VisualStudio.TestTools.UnitTesting;

using BedrockTools.Objects;
using BedrockTools.Objects.Blocks;
using BedrockTools.Objects.Blocks.Util;
using BedrockTools.Structure;
using BedrockTools.Structure.Features;
using BedrockTools.Structure.Features.Util;

namespace BedrockToolsMSTest.Structure.Features {
    [TestClass]
    public class FeatureTester {
        [TestMethod]
        public void TestSimple() {
            McStructure mc = new McStructure(new Dimensions(10, 3, 10));
            AxisFeature axis = new AxisFeature(4, 2, 3);

            axis.PlaceInStructure(new McTransform(new IntCoords(3, 1, 2)), mc);
            Block[,,] expected = new Block[10, 3, 10];

            expected[3, 1, 2] = VanillaBlockFactory.EndBricks();

            expected[4, 1, 2] = VanillaBlockFactory.Concrete(BlockColorValue.Red);
            expected[5, 1, 2] = VanillaBlockFactory.Concrete(BlockColorValue.Red);
            expected[6, 1, 2] = VanillaBlockFactory.Concrete(BlockColorValue.Red);

            expected[3, 1, 3] = VanillaBlockFactory.Concrete(BlockColorValue.Blue);
            expected[3, 1, 4] = VanillaBlockFactory.Concrete(BlockColorValue.Blue);

            expected[3, 2, 2] = VanillaBlockFactory.Concrete(BlockColorValue.Green);
            Block[,,] actual = mc.GetBlocks();
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestSimpleN90() {
            McStructure mc = new McStructure(new Dimensions(10, 3, 10));
            AxisFeature axis = new AxisFeature(4, 2, 3);

            axis.PlaceInStructure(
                new McTransform(
                    new IntCoords(3, 1, 2), 
                    McRotation.n90, 
                    false, 
                    false
                ),
                mc
            );
            Block[,,] expected = new Block[10, 3, 10];

            expected[5, 1, 2] = VanillaBlockFactory.EndBricks();

            expected[5, 1, 3] = VanillaBlockFactory.Concrete(BlockColorValue.Red);
            expected[5, 1, 4] = VanillaBlockFactory.Concrete(BlockColorValue.Red);
            expected[5, 1, 5] = VanillaBlockFactory.Concrete(BlockColorValue.Red);

            expected[4, 1, 2] = VanillaBlockFactory.Concrete(BlockColorValue.Blue);
            expected[3, 1, 2] = VanillaBlockFactory.Concrete(BlockColorValue.Blue);

            expected[5, 2, 2] = VanillaBlockFactory.Concrete(BlockColorValue.Green);
            Block[,,] actual = mc.GetBlocks();
            CollectionAssert.AreEqual(expected, actual);
        }
    }
}
