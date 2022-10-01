using System;
using System.IO;
using System.Drawing;
//using MinecraftBedrockStructureBlock.structure;
//using MinecraftBedrockStructureBlock.types;
//using MinecraftBedrockStructureBlock.image;
using BedrockTools.Nbt.Elements;
using BedrockTools.Nbt.Sugar.Implicit;
namespace TestConsole {
    class Program {
        static void Main(string[] args) {
            NbtCompoundBase t = new NbtCompoundOrdered() {
                { "name", "Hey" },
                { "b", (byte)3 },
                { "yo", 
                    new NbtCompoundSorted(){
                        {"version", NbtList.FromInts(1,19,3,0) }
                    }
                },
            };
            Console.WriteLine(t.ToString());
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
