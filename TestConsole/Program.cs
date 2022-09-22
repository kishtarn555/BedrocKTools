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
            McStructure mystuct = ImageStructureConverter.DiffDithering("lenna.png");
            BinaryWriter bw = new BinaryWriter(new FileStream(
                Path.Combine(MojangCom, @"development_behavior_packs\moveit\structures\moveit\lnna.mcstructure"), FileMode.Create));
            Console.WriteLine();
            LabColorDistanceCalculator c = new LabColorDistanceCalculator();
            Console.WriteLine(c.calcDistance(Color.FromArgb(219, 86, 86), Color.FromArgb(232, 39, 143)));
            NbtBase res = mystuct.GetNBT();
            //Console.WriteLine(res);
            //Console.WriteLine(res);
            res.print(bw, true);
            bw.Close();
        }
    }
}
