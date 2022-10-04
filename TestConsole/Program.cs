using System;
using System.IO;
using System.Drawing;
//using MinecraftBedrockStructureBlock.structure;
//using MinecraftBedrockStructureBlock.types;
//using MinecraftBedrockStructureBlock.image;
using BedrockTools.Nbt.Util;
using BedrockTools.Objects.Blocks.Util;
using BedrockTools.Objects.Blocks;
using BedrockTools.Structure;
namespace TestConsole {
    class Program {
        static void Main(string[] args) {
            McStructure mcstructure = new McStructure(5, 8, 7);
            for (int i = 0; i < mcstructure.sizeX; i++)
                for (int j = 0; j < mcstructure.sizeY; j++)
                    for (int k = 0; k < mcstructure.sizeZ; k++)
                        mcstructure.setBlock(i, j, k, VanillaBlockFactory.Air);
                        for (int i =0; i < 5; i++) {
                mcstructure.setBlock(i, 0, 0, VanillaBlockFactory.Stairs.Planks.Oak(BlockOrientation.North));
                mcstructure.setBlock(i, 0, 2, VanillaBlockFactory.Stairs.Planks.Oak(BlockOrientation.East));
                mcstructure.setBlock(i, 0, 4, VanillaBlockFactory.Stairs.Planks.Oak(BlockOrientation.South));
                mcstructure.setBlock(i, 0, 6, VanillaBlockFactory.Stairs.Planks.Oak(BlockOrientation.West));


                mcstructure.setBlock(i, 7, 0, VanillaBlockFactory.Stairs.Planks.Oak(BlockOrientation.North, true));
                mcstructure.setBlock(i, 7, 2, VanillaBlockFactory.Stairs.Planks.Oak(BlockOrientation.East, true));
                mcstructure.setBlock(i, 7, 4, VanillaBlockFactory.Stairs.Planks.Oak(BlockOrientation.South, true));
                mcstructure.setBlock(i, 7, 6, VanillaBlockFactory.Stairs.Planks.Oak(BlockOrientation.West, true));
            }
            string MojangCom =
                Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    @"Packages\Microsoft.MinecraftUWP_8wekyb3d8bbwe\LocalState\games\com.mojang"
                    );
            BinaryWriter bw = new BinaryWriter(new FileStream(
               Path.Combine(MojangCom, @"development_behavior_packs\moveit\structures\moveit\delta.mcstructure"), FileMode.Create));
            
            WriterUtil.WriteRootCompound(bw, mcstructure.ToNbt());
            bw.Close();
            return;
            /*
            string MojangCom =
                Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    @"Packages\Microsoft.MinecraftUWP_8wekyb3d8bbwe\LocalState\games\com.mojang"
                    );
            McStructure mystuct = ImageStructureConverter.DiffDithering("d.png");
            BinaryWriter bw = new BinaryWriter(new FileStream(
                Path.Combine(MojangCom, @"development_behavior_packs\moveit\structures\moveit\d.mcstructure"), FileMode.Create));
            Console.WriteLine();
            LabColorDistanceCalculator c = new LabColorDistanceCalculator();
            Console.WriteLine(c.calcDistance(Color.FromArgb(219, 86, 86), Color.FromArgb(232, 39, 143)));
            NbtBase res = mystuct.GetNBT();
            //Console.WriteLine(res);
            //Console.WriteLine(res);
            res.print(bw, true);
            bw.Close();
            */
        }
    }
}
