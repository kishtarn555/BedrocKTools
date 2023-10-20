using BedrockTools.Nbt.Elements;
using BedrockTools.Objects.Blocks.Util;
using BedrockTools.Objects.Blocks;
using BedrockTools.Objects;
using BedrockTools.Structure.Features.Nature.TreeA;
using BedrockTools.Structure.Features;
using BedrockTools.Structure;
using System;

namespace TestConsole {
    public class TreeTests {
        class LeaveBlockHacked : Block {
            public LeaveBlockHacked(string identifier) : base(identifier) {
            }

            public override NbtCompoundSorted GetBlockState() {
                return new NbtCompoundSorted() {
                    {"old_leaf_type", (NbtString)("oak") },
                    {"persistent_bit", (NbtByte)(1) },
                    {"update_bit", (NbtByte)(0) },
                };
            }
        }

        static McStructure Tree1Test() {
            Feature line = new Tree(
                VanillaBlockFactory.Wood(PillarAxis.Y, WoodType.Oak),
                new LeaveBlockHacked("minecraft:leaves"),
                new Dimensions(75, 75, 75),
                new Tree.TreeParams()
                {
                    rootHeight = 10,
                    rootWidth = 10f,
                    rootSegmentHeight = 4,
                    trunkHeight = 40,
                    trunkWidth = 8f,
                    trunkSegments = 3,
                    trunkDeviation = 2f,
                    branches = 6,
                    branchLength = 4f,
                    branchWidth = 2.5f,
                    branchMinWidthDecay = 0.90f,
                    branchMaxWidthDecay = 0.95f,
                    branchExtendChance = 0.95f,
                    branchTwist = 0.261799f,

                    splitProbability = 0.3f,
                    splitMinAngle = 0.261799f * 3f,
                    splitMaxAngle = 0.261799f * 5f,

                    leaveCount = 7,
                    leaveLenght = 9,
                    leaveWidth = 1f,
                    leaveAngle = MathF.PI * 3f / 2f

                }
            ); ;
            McStructure structure = new McStructure(new Dimensions(150, 150, 150));
            line.PlaceInStructure(McTransform.Identity.Translate(75, 0, 75), structure);
            return structure;
        }
    }
}
