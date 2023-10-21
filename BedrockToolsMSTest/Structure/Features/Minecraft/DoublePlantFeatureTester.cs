using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using BedrockTools.Objects.Blocks.Util;
using BedrockTools.Objects;
using BedrockTools.Structure.Features.Minecraft;
using BedrockTools.Objects.Blocks;

namespace BedrockToolsMSTest.Structure.Features.Minecraft {
    [TestClass]
    public class DoublePlantFeatureTester {
        [TestMethod]
        public void DoublePlantFeatureTest() {
            DoublePlantFeature doublePlantFeature = new DoublePlantFeature(DoublePlantType.Fern);

            Assert.IsNotNull(doublePlantFeature);
            Assert.AreEqual(doublePlantFeature.Size, new Dimensions(1, 2, 1), "Size is wrong");
            Block expectedLower = VanillaBlockFactory.DoublePlant(DoublePlantType.Fern, false);
            Block expectedUpper = VanillaBlockFactory.DoublePlant(DoublePlantType.Fern, true);
            Assert.AreEqual(doublePlantFeature.GetBlock(0,0,0),expectedLower);
            Assert.AreEqual(doublePlantFeature.GetBlock(0,1,0),expectedUpper);
        }
    }
}
