using System;
using System.IO;
using System.Drawing;
//using MinecraftBedrockStructureBlock.structure;
//using MinecraftBedrockStructureBlock.types;
//using MinecraftBedrockStructureBlock.image;
using BedrockTools.Objects.Blocks;
using BedrockTools.Objects;
using BedrockTools.Structure;
using BedrockTools.Nbt.IO;
using BedrockTools.Structure.Features.Util;
using BedrockTools.Structure.Features.Geometry;
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
        static void Main(string[] args) {
            McStructure mcstructure = CircleTestThin();
            string MojangCom =
                Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    @"Packages\Microsoft.MinecraftUWP_8wekyb3d8bbwe\LocalState\games\com.mojang"
                    );
            BinaryWriter bw = new BinaryWriter(new FileStream(
               Path.Combine(MojangCom, @"development_behavior_packs\moveit\structures\moveit\delta.mcstructure"), FileMode.Create));
            
            new NbtBinaryWriter(bw).WriteRoot(mcstructure.GetStructureAsNbt());
            bw.Close();
            return;
        }
    }
}
