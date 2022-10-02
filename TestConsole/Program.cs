using System;
using System.IO;
using System.Drawing;
//using MinecraftBedrockStructureBlock.structure;
//using MinecraftBedrockStructureBlock.types;
//using MinecraftBedrockStructureBlock.image;
using BedrockTools.Nbt.Elements;
using BedrockTools.Nbt.Util;
using BedrockTools.Objects.Minecraft;
using BedrockTools.Structure;
namespace TestConsole {
    class Program {
        static void Main(string[] args) {
            McStructure mcstructure = new McStructure(10, 1, 10);
            for (int i =0; i < 10; i++) {
                for (int j=0; j <10; j++) {
                    if ((i+j)%2==0) {
                        mcstructure.setBlock(i, 0, j, MinecraftBlockPrefabs.Instance.GetPrefabByName("Planks.Oak"));
                    } else {
                        mcstructure.setBlock(i, 0, j, MinecraftBlockPrefabs.Instance.GetPrefabByName("Dirt.Normal"));

                    }
                }
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
