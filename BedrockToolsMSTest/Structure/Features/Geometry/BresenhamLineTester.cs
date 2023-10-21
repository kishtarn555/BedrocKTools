using Microsoft.VisualStudio.TestTools.UnitTesting;

using BedrockTools.Structure.Features.Raster;
using BedrockTools.Objects;
using BedrockTools.Objects.Blocks;
using System.Linq;
using System;

namespace BedrockToolsMSTest.Structure.Features.Geometry {
    [TestClass]
    public class BresenhamLineTester {
        [TestMethod]
        public void BresenhamLineTestX() {
            IntCoords A = new IntCoords(40, 20, 30);
            IntCoords B = new IntCoords(90, 10, 70);
            BresenhamLineFeature besenhamLine = new BresenhamLineFeature(
                new Dimensions(100, 100, 100),
                A, B,
                VanillaBlockFactory.Clay()
            );
            var blocks = besenhamLine.AllBlocks().ToArray();
            Assert.AreEqual(90 - 40 + 1, blocks.Length);
        }
        [TestMethod]
        public void BresenhamLineTestY() {
            IntCoords A = new IntCoords(40, 12,3);
            IntCoords B = new IntCoords(60, 70,6);
            BresenhamLineFeature besenhamLine = new BresenhamLineFeature(
                new Dimensions(100, 100, 100),
                A, B,
                VanillaBlockFactory.Clay()
            );
            var blocks = besenhamLine.AllBlocks().ToArray();
            Assert.AreEqual(70 - 12 + 1, blocks.Length);
        }
        [TestMethod]
        public void BresenhamLineTestZ() {
            IntCoords A = new IntCoords(40, 20, 35);
            IntCoords B = new IntCoords(45, 10, 80);
            BresenhamLineFeature besenhamLine = new BresenhamLineFeature(
                new Dimensions(100, 100, 100),
                A, B,
                VanillaBlockFactory.Clay()
            );
            var blocks = besenhamLine.AllBlocks().ToArray();
            Assert.AreEqual(80 - 35 + 1, blocks.Length);
        }


        [TestMethod]
        public void BresenhamLineTestAllCorners() {
            int[] xs = new int[] { 1, 99 };
            int[] ys = new int[] { 10, 99 };
            int[] zs = new int[] { 10, 99 };
            for (int loops = 0; loops < 3; loops++) {
                for (int i = 0; i < xs.Length; i++) {
                    for (int j = 0; j < ys.Length; j++) {
                        for (int k = 0; k < zs.Length; k++) {
                            IntCoords A = new IntCoords(xs[i & 1], ys[j & 1], zs[k & 1]);
                            IntCoords B = new IntCoords(xs[i ^ 1], ys[j ^ 1], zs[k ^ 1]);
                            BresenhamLineFeature besenhamLine = new BresenhamLineFeature(
                                new Dimensions(100, 100, 100),
                                A, B,
                                VanillaBlockFactory.Clay()
                            );
                            var blocks = besenhamLine.AllBlocks().ToArray();
                            Assert.AreEqual(99, blocks.Length, $"failed assert on {loops} {i}{j}{k} {A}{B}");
                        }
                    }
                }
                (xs,ys,zs)=(ys,zs,xs);
            }
        }
    }
}
