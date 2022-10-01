using System;
using System.IO;
using System.Drawing;
//using MinecraftBedrockStructureBlock.structure;
//using MinecraftBedrockStructureBlock.types;
//using MinecraftBedrockStructureBlock.image;
using BedrockTools.Nbt.Elements;
using BedrockTools.Objects.Minecraft.Blocks;
namespace TestConsole {
    class Program {
        static void Main(string[] args) {
            NbtCompound t = new NbtCompoundOrdered() {
                { "name", (NbtString)"Hey" },
                { "b", (NbtByte)3 },
                { "yo", 
                    new NbtCompoundSorted(){
                        {"version", NbtList.FromInts(1,19,3,0) }
                    }
                },
            };
            MinecraftBlockPrefabs p = MinecraftBlockPrefabs.Instance;
            var properties = p.GetType().GetProperties();
            var fields = p.GetType().GetFields();
            var members = p.GetType().GetMembers();

            Console.WriteLine("====Properties====");
            foreach ( var prop in properties)
                Console.WriteLine(prop);
            Console.WriteLine("====Fields====");
            foreach (var field in fields)
                Console.WriteLine(field);

            Console.WriteLine("====Members====");
            foreach (var mem in members)
                Console.WriteLine(mem);
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
