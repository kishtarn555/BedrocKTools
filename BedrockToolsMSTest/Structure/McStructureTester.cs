using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Linq;
using BedrockToolsMSTest.Utils;

using BedrockTools.Nbt.Elements;
using BedrockTools.Objects;
using BedrockTools.Objects.Blocks;
using BedrockTools.Structure;

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


        [TestMethod]
        public void TestEmptyNbt() {
            McStructure mcStructure = new McStructure(new Dimensions(2,3,4));
            NbtCompound root = new NbtCompoundOrdered() {
                {"format_version", (NbtInt)1 },
                {"size", NbtList.FromInts(2,3,4)},
                {"structure", new NbtCompoundOrdered() {
                    {"block_indices", NbtList.FromLists(
                        NbtList.FromInts(Enumerable.Repeat(-1, 24).ToArray()),
                        NbtList.FromInts(Enumerable.Repeat(-1, 24).ToArray())
                    )},
                    {"entities", NbtList.Empty() },
                    {"palette", new NbtCompoundOrdered{
                        {"default", new NbtCompoundOrdered() {
                            {"block_palette", NbtList.Empty() },
                            {"block_position_data", new NbtCompoundOrdered() }
                        }}
                    }},
                }},
                {"structure_world_origin", NbtList.FromInts(0,0,0) }
            };
            NbtAssert.AssertNbt(root, mcStructure.GetStructureAsNbt(), "Nbt structure missmatch");
        }

        [TestMethod]
        public void TestBlockNbt() {
            McStructure mcStructure = new McStructure(new Dimensions(2, 2, 2));
            mcStructure.SetBlock(0, 0, 0, VanillaBlockFactory.Stone(BedrockTools.Objects.Blocks.Minecraft.StoneBlock.StoneType.Stone));
            mcStructure.SetBlock(0, 0, 1, VanillaBlockFactory.Stone(BedrockTools.Objects.Blocks.Minecraft.StoneBlock.StoneType.Andesite));
            mcStructure.SetBlock(0, 1, 0, VanillaBlockFactory.Glass());
            NbtCompound root = new NbtCompoundOrdered() {
                {"format_version", (NbtInt)1 },
                {"size", NbtList.FromInts(2,2,2)},
                {"structure", new NbtCompoundOrdered() {
                    {"block_indices", NbtList.FromLists(
                        NbtList.FromInts(new int[]{0,1,2,-1,-1,-1,-1,-1}),
                        NbtList.FromInts(Enumerable.Repeat(-1, 8).ToArray())
                    )},
                    {"entities", NbtList.Empty() },
                    {"palette", new NbtCompoundOrdered{
                        {"default", new NbtCompoundOrdered() {
                            {"block_palette", NbtList.FromCompounds(
                                new NbtCompoundOrdered() {
                                    { "name", (NbtString)"minecraft:stone"},
                                    { "version", (NbtInt)Block.STRUCTURE_VERSION},
                                    { "states", new NbtCompoundOrdered() {
                                        {"stone_type", (NbtString)"stone" }
                                    }},
                                },
                                new NbtCompoundOrdered() {
                                    { "name", (NbtString)"minecraft:stone"},
                                    { "version", (NbtInt)Block.STRUCTURE_VERSION},
                                    { "states", new NbtCompoundOrdered() {
                                        {"stone_type", (NbtString)"andesite" }
                                    }},
                                },
                                new NbtCompoundOrdered() {
                                    { "name", (NbtString)"minecraft:glass"},
                                    { "version", (NbtInt)Block.STRUCTURE_VERSION},
                                    { "states", new NbtCompoundOrdered() {}},
                                }
                            )},
                            {"block_position_data", new NbtCompoundOrdered() }
                        }}
                    }},
                }},
                {"structure_world_origin", NbtList.FromInts(0,0,0) }
            };
            NbtAssert.AssertNbt(root, mcStructure.GetStructureAsNbt(), "Nbt structure missmatch");
        }

    }
}
