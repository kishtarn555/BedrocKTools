using System;
using System.IO;
using System.Drawing;
//using MinecraftBedrockStructureBlock.structure;
//using MinecraftBedrockStructureBlock.types;
//using MinecraftBedrockStructureBlock.image;
using BedrockTools.Objects.Blocks;
using BedrockTools.Objects.Blocks.Minecraft;
using BedrockTools.Objects;
using BedrockTools.Structure;
using BedrockTools.Nbt.IO;
using BedrockTools.Structure.Features.Util;
using BedrockTools.Structure.Features.Geometry;
using BedrockTools.Structure.Features.Modifier;
using BedrockTools.Structure.Features.Patterns;
namespace TestConsole {
    class Program {

        static McStructure AxisTest() {
            McStructure mcstructure = new McStructure(new Dimensions(20, 25, 20));
            AxisFeature ax1 = new AxisFeature(8, 10, 10);
            AxisFeature ax2 = new AxisFeature(3, 2, 5);
            ax1.AddSubfeature(McTransform.Identity.Translate(3, 6, 1).Rotate(McRotation.n90), ax2);

            ax1.PlaceInStructure(McTransform.Identity.Translate(1, 2, 3).Rotate(McRotation.n90), mcstructure);
            return mcstructure;
        }
        static McStructure CircleTest() {
            McStructure mcstructure = new McStructure(new Dimensions(100, 25, 100));
            CircleFeature circle = new CircleFeature(
                new Dimensions(100, 1, 100),
                FillMode.Solid, PlaneType.XZ,
                VanillaBlockFactory.Quartz(BedrockTools.Objects.Blocks.Minecraft.QuartzBlock.QuartzType.Default)
            );
            circle.PlaceInStructure(new McTransform(new IntCoords(0, 2, 0)), mcstructure);
            return mcstructure;
        }
        static McStructure CircleTestThin() {
            McStructure mcstructure = new McStructure(new Dimensions(100, 25, 100));
            CircleFeature circle = new CircleFeature(
                new Dimensions(100, 1, 100),
                FillMode.BorderThin, PlaneType.XZ,
                VanillaBlockFactory.Quartz(BedrockTools.Objects.Blocks.Minecraft.QuartzBlock.QuartzType.Default)
            );
            circle.PlaceInStructure(new McTransform(new IntCoords(0, 2, 0)), mcstructure);
            return mcstructure;
        }
        static McStructure CircleTestThick() {
            McStructure mcstructure = new McStructure(new Dimensions(100, 25, 100));
            CircleFeature circle = new CircleFeature(
                new Dimensions(80, 1, 100),
                FillMode.BorderThick, PlaneType.XZ,
                VanillaBlockFactory.Quartz(BedrockTools.Objects.Blocks.Minecraft.QuartzBlock.QuartzType.Default)
            );
            circle.PlaceInStructure(new McTransform(new IntCoords(0, 2, 0)), mcstructure);
            return mcstructure;
        }
        static McStructure HalfCircleTest() {
            McStructure mcstructure = new McStructure(new Dimensions(100, 25, 100));
            CircleFeature circle = new CircleFeature(
                new Dimensions(100, 1, 100),
                FillMode.Solid, PlaneType.XZ,
                VanillaBlockFactory.Quartz(BedrockTools.Objects.Blocks.Minecraft.QuartzBlock.QuartzType.Smooth)
            );
            CubeFeature mask = new CubeFeature(
                new Dimensions(50, 1, 100),
                VanillaBlockFactory.Ice()
                );
            MaskModifier maskModifier = new MaskModifier(circle, mask, McTransform.Identity.Translate(10, 0, 0));
            maskModifier.PlaceInStructure(new McTransform(new IntCoords(0, 2, 0)), mcstructure);
            return mcstructure;
        }

        static McStructure SpehreTest2() {
            McStructure mcstructure = new McStructure(new Dimensions(106, 24, 130));
            Rings chess = new Rings(
                new Dimensions(106, 24, 130),
                new Block[]{
                    VanillaBlockFactory.CrimsonPlanks(),
                    VanillaBlockFactory.Wool(BedrockTools.Objects.Blocks.Util.BlockColorValue.Silver),
                },
                5
            );

            SphereFeature sphere = new SphereFeature(
                new Dimensions(106, 24, 130),
                FillMode.BorderThick,
                VanillaBlockFactory.CrimsonPlanks()
            );
            MaskModifier mask = new MaskModifier(chess, sphere);
            mask.PlaceInStructure(new McTransform(new IntCoords(0, -12, 0)), mcstructure);
            return mcstructure;
        }
        static McStructure SpehreTest() {
            McStructure mcstructure = new McStructure(new Dimensions(106, 100, 130));
            SphereFeature sphere = new SphereFeature(
                new Dimensions(100, 100, 100),
                FillMode.BorderThin,
                VanillaBlockFactory.Quartz(BedrockTools.Objects.Blocks.Minecraft.QuartzBlock.QuartzType.Default)
            );
            sphere.PlaceInStructure(new McTransform(new IntCoords(0, 0, 0)), mcstructure);
            return mcstructure;
        }


        static McStructure SpehreTest3() {
            McStructure mcstructure = new McStructure(new Dimensions(106, 100, 130));

            CubeFeature cube = new CubeFeature(
               new Dimensions(106, 100, 130),
               FillMode.Solid,
               VanillaBlockFactory.Air()
           );

            cube.PlaceInStructure(new McTransform(new IntCoords(0, 0, 0)), mcstructure);

            SphereFeature sphere = new SphereFeature(
                new Dimensions(100, 100, 100),
                FillMode.Solid,
                VanillaBlockFactory.Stone(StoneBlock.StoneType.Stone)
            ) ;
            SphereFeature sphere2 = new SphereFeature(
                new Dimensions(50, 50,50),
                FillMode.Solid,
                VanillaBlockFactory.CrimsonPlanks()
            );
            SubstractModifier substract = new SubstractModifier(sphere, sphere2, McTransform.Identity.Translate(65,65,65));
            substract.PlaceInStructure(new McTransform(new IntCoords(0, 0, 0)), mcstructure);
            return mcstructure;
        }
        static McStructure Air() {
            McStructure mcstructure = new McStructure(new Dimensions(106, 100, 130));
            CubeFeature cube = new CubeFeature(
                new Dimensions(106, 100, 130),
                FillMode.Solid,
                VanillaBlockFactory.Air()
            );
           
            cube.PlaceInStructure(new McTransform(new IntCoords(0, 0, 0)), mcstructure);
            return mcstructure;
        }
        static void Main(string[] args) {
            McStructure mcstructure = SpehreTest2();
            string MojangCom =
                Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    @"Packages\Microsoft.MinecraftUWP_8wekyb3d8bbwe\LocalState\games\com.mojang"
                    );
            BinaryWriter bw = new BinaryWriter(new FileStream(
               Path.Combine(MojangCom, @"development_behavior_packs\moveit\structures\moveit\hf.mcstructure"), FileMode.Create));
            McStructureSerializer serializer = new McStructureSerializer(mcstructure);
            new NbtBinaryWriter(bw).WriteRoot(serializer.GetStructureAsNbt());
            bw.Close();
            return;
        }
    }
}
