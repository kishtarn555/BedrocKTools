using BedrockTools.Objects.Blocks.Minecraft;
using BedrockTools.Objects.Blocks.Util;
using BedrockTools.Objects.Blocks;
using BedrockTools.Objects;
using BedrockTools.Structure.Advanced.Obj;
using BedrockTools.Structure.Advanced.Vox;
using BedrockTools.Structure.Advanced;
using BedrockTools.Structure.Features.Geometry;
using BedrockTools.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole {
    internal class Test3D {


        static McStructure TeapotTest() {
            Dimensions size = new Dimensions(200, 200, 200);
            var triangles = TrianglesFromObjParser.ParseObjFileTo3DTriangles(
                "teapot.obj",
                size,
                FillMode.Solid,
                VanillaBlockFactory.Quartz(BedrockTools.Objects.Blocks.Minecraft.QuartzBlock.QuartzType.Default),
                2f,
                30f
            );
            McStructure mcstructure = new McStructure(size);
            triangles.ForEach(triangle => {
                McTransform r = triangle.ResizeToBoundingBox();
                triangle.PlaceInStructure(r, mcstructure);
            });
            return mcstructure;

        }



        static McStructure TeapotTest2() {
            Dimensions size = new Dimensions(200, 100, 160);
            return TrianglesFromObjParser.IntersectionTriangleObjToStruct(
                "teapot.obj",
                size,
                VanillaBlockFactory.Quartz(BedrockTools.Objects.Blocks.Minecraft.QuartzBlock.QuartzType.Default),
                30f
            );

        }

        static McStructure CowTest() {
            Dimensions size = new Dimensions(200, 200, 200);
            var triangles = TrianglesFromObjParser.ParseObjFileTo3DTriangles(
                "cow.obj",
                size,
                FillMode.Solid,
                VanillaBlockFactory.Quartz(BedrockTools.Objects.Blocks.Minecraft.QuartzBlock.QuartzType.Default),
                2f,
                19f
            );
            McStructure mcstructure = new McStructure(size);
            triangles.ForEach(triangle => {
                McTransform r = triangle.ResizeToBoundingBox();
                triangle.PlaceInStructure(r, mcstructure);
            });
            return mcstructure;

        }

        static McStructure CowTest2() {
            Dimensions size = new Dimensions(200, 130, 80);
            return TrianglesFromObjParser.IntersectionTriangleObjToStruct(
                "cow.obj",
                size,
                VanillaBlockFactory.Quartz(QuartzBlock.QuartzType.Default),
                19f
            );

        }
        static McStructure ObjTextureTest() {
            Dimensions size = new Dimensions(100, 130, 80);
            Block[,] blocks = new Block[8, 8];
            for (int i = 0; i < 8; i++) {
                for (int j = 0; j < 8; j++) {
                    blocks[i, j] = VanillaBlockFactory.Wool((BlockColorValue)((i * 8 + j) % 16));
                }
            }
            return TrianglesFromObjParser.IntersectionTriangleObjToStruct(
                "test.obj",
                size,
                new UVBlockPalette(
                    blocks, 8, 8),
                12f
            );

        }


        public static McStructure CastleTest() {
            Dimensions size = new Dimensions(300, 130, 300);

            return TrianglesFromObjParser.IntersectionTriangleObjToStruct(
                "castle.obj",
                size,
                     VanillaBlockFactory.Stone(StoneBlock.StoneType.Stone),
                0.25f
            );
        }


        static IBlockPalette DefaultPalette() {
            return new BlockPaletteDictionary(new Dictionary<int, Block>() {
                    { 1, VanillaBlockFactory.Planks(PlanksBlock.PlanksType.Oak)},
                    { 2, VanillaBlockFactory.Planks(PlanksBlock.PlanksType.Birch)},
                    { 3, VanillaBlockFactory.Planks(PlanksBlock.PlanksType.Jungle)},
                    { 4, VanillaBlockFactory.Planks(PlanksBlock.PlanksType.Spruce)},
                    { 5, VanillaBlockFactory.Planks(PlanksBlock.PlanksType.Acacia)},
                    { 6, VanillaBlockFactory.Planks(PlanksBlock.PlanksType.Dark_Oak)},
                    { 7, VanillaBlockFactory.CrimsonPlanks()},
                    { 8, VanillaBlockFactory.WarpedPlanks() },

                    { 9, VanillaBlockFactory.Concrete(BlockColorValue.White ) },
                    { 10, VanillaBlockFactory.Concrete(BlockColorValue.Orange ) },
                    { 11, VanillaBlockFactory.Concrete(BlockColorValue.Magneta ) },
                    { 12, VanillaBlockFactory.Concrete(BlockColorValue.Light_Blue ) },
                    { 13, VanillaBlockFactory.Concrete(BlockColorValue.Yellow ) },
                    { 14, VanillaBlockFactory.Concrete(BlockColorValue.Lime ) },
                    { 15, VanillaBlockFactory.Concrete(BlockColorValue.Pink ) },
                    { 16, VanillaBlockFactory.Concrete(BlockColorValue.Gray ) },
                    { 17, VanillaBlockFactory.Concrete(BlockColorValue.Silver) },
                    { 18, VanillaBlockFactory.Concrete(BlockColorValue.Cyan) },
                    { 19, VanillaBlockFactory.Concrete(BlockColorValue.Purple) },
                    { 20, VanillaBlockFactory.Concrete(BlockColorValue.Blue) },
                    { 21, VanillaBlockFactory.Concrete(BlockColorValue.Brown) },
                    { 22, VanillaBlockFactory.Concrete(BlockColorValue.Green) },
                    { 23, VanillaBlockFactory.Concrete(BlockColorValue.Red) },
                    { 24, VanillaBlockFactory.Concrete(BlockColorValue.Black) },

                    { 25, VanillaBlockFactory.Glass() },
                    { 26, VanillaBlockFactory.Grass() },
                });
        }

        static McStructure VoxTest() {
            MagicaVoxImporter magicaVoxImporter = new MagicaVoxImporter(
                DefaultPalette()
            );
            return magicaVoxImporter.Import("house.vox");
        }
    }
}
