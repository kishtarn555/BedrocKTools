using System;
using System.IO;
//using MinecraftBedrockStructureBlock.structure;
//using MinecraftBedrockStructureBlock.types;
//using MinecraftBedrockStructureBlock.image;
using BedrockTools.Objects;
using BedrockTools.Structure;
using BedrockTools.Nbt.IO;
using BedrockTools.Structure.Features.Util;
using BedrockTools.Structure.Features;
using System.Numerics;
using BedrockTools.Structure.Features.Geometry.Splines;
using System.Linq;
using System.Drawing;
using System.Threading;
using System.ComponentModel;
using BedrockTools.Objects.Blocks;
using BedrockTools.Geometry.Path;
using BedrockTools.Structure.Features.Geometry;
using BedrockTools.Nbt.Elements;

namespace TestConsole {
    class Program {
        public static void WriteStructureTest(McStructure mcstructure, string name) {
            //BinaryWriter bw = new BinaryWriter(new FileStream(
            //   Path.Combine(MojangCom, @"development_behavior_packs\moveit\structures\moveit\hf.mcstructure"), FileMode.Create));
            string append = DateTime.Now.ToString("yyyy MM dd HH mm");

            BinaryWriter bw = new BinaryWriter(new FileStream(
               "C:\\Users\\hecto\\source\\repos\\MC\\" + name + append + ".mcstructure", FileMode.Create));
            McStructureSerializer serializer = new McStructureSerializer(mcstructure);
            new NbtBinaryWriter(bw).WriteRoot(serializer.GetStructureAsNbt());
            bw.Close();
        }
        static void Main(string[] args) {
            //McStructure mcstructure = CityCrafter.StreetTest(
            //    new Vector3(5200, -35, 5460),
            //    CityCrafter.RiverMarketSide
            //    );

            //string MojangCom =
            //    Path.Combine(
            //        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            //        @"Packages\Microsoft.MinecraftUWP_8wekyb3d8bbwe\LocalState\games\com.mojang"
            //        );
            ////BinaryWriter bw = new BinaryWriter(new FileStream(
            ////   Path.Combine(MojangCom, @"development_behavior_packs\moveit\structures\moveit\hf.mcstructure"), FileMode.Create));
            //string append = DateTime.Now.ToString("yyyy MM dd HH mm");

            //BinaryWriter bw = new BinaryWriter(new FileStream(
            //   "C:\\Users\\hecto\\source\\repos\\MC\\deserialize" + append + ".mcstructure", FileMode.Create));
            //McStructureSerializer serializer = new McStructureSerializer(mcstructure);
            //new NbtBinaryWriter(bw).WriteRoot(serializer.GetStructureAsNbt());
            //bw.Close();
            McStructure str = Test3D.TeapotTest2();
            WriteStructureTest(str, "teapot_again");
            return;
        }
    }
}
