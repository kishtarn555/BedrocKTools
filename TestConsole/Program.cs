﻿using System;
using System.IO;
using System.Drawing;
//using MinecraftBedrockStructureBlock.structure;
//using MinecraftBedrockStructureBlock.types;
//using MinecraftBedrockStructureBlock.image;
using BedrockTools.Nbt.Util;
using BedrockTools.Objects.Blocks.Util;
using BedrockTools.Objects.Blocks;
using BedrockTools.Objects;
using BedrockTools.Structure;
namespace TestConsole {
    class Program {
        static void Main(string[] args) {
            McStructure mcstructure = new McStructure(new Dimensions(5,8,7));
            mcstructure.FillVoidWithAir();
                        for (int i =0; i < 5; i++) {
                mcstructure.SetBlock(i, 0, 0, VanillaBlockFactory.Stairs.Planks.Oak(BlockOrientation.North));
                mcstructure.SetBlock(i, 0, 2, VanillaBlockFactory.Stairs.Planks.Oak(BlockOrientation.East));
                mcstructure.SetBlock(i, 0, 4, VanillaBlockFactory.Stairs.Planks.Oak(BlockOrientation.South));
                mcstructure.SetBlock(i, 0, 6, VanillaBlockFactory.Stairs.Planks.Oak(BlockOrientation.West));


                mcstructure.SetBlock(i, 7, 0, VanillaBlockFactory.Stairs.Planks.Oak(BlockOrientation.North, true));
                mcstructure.SetBlock(i, 7, 2, VanillaBlockFactory.Stairs.Planks.Oak(BlockOrientation.East, true));
                mcstructure.SetBlock(i, 7, 4, VanillaBlockFactory.Stairs.Planks.Oak(BlockOrientation.South, true));
                mcstructure.SetBlock(i, 7, 6, VanillaBlockFactory.Stairs.Planks.Oak(BlockOrientation.West, true));
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
