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
namespace TestConsole {
    class Program {
        static void Main(string[] args) {
            McStructure mcstructure = new McStructure(new Dimensions(20,25,20));
            Axis ax1 = new Axis(8, 10, 10);
            Axis ax2 = new Axis(3, 2, 5);
            ax1.AddSubfeature(McTransform.Identity.Translate(3, 6, 1).Rotate(McRotation.n90), ax2);

            ax1.PlaceInStructure(McTransform.Identity.Translate(1, 2, 3).Rotate(McRotation.n90), mcstructure);
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
