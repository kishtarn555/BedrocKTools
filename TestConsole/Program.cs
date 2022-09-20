using System;
using System.IO;
using System.Drawing;
using MinecraftBedrockStructureBlock.structure;
using MinecraftBedrockStructureBlock.types;
using MinecraftBedrockStructureBlock.image;
namespace TestConsole {
    class Program {
        static void Main(string[] args) {
            
            string MojangCom =
                Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    @"Packages\Microsoft.MinecraftUWP_8wekyb3d8bbwe\LocalState\games\com.mojang"
                    );
            McStructure mystuct = ImageStructureConverter.DiffDithering("scene.png");
            BinaryWriter bw = new BinaryWriter(new FileStream(
                Path.Combine(MojangCom, @"development_behavior_packs\moveit\structures\moveit\scene.mcstructure"), FileMode.Create));
            Console.WriteLine();
            NbtBase res = mystuct.GetNBT();
            //Console.WriteLine(res);
            //Console.WriteLine(res);
            res.print(bw, true);
            bw.Close();
        }
    }
}
