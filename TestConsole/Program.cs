using System;
using System.IO;
using MinecraftBedrockStructureBlock.structure;
using MinecraftBedrockStructureBlock.structure.block;
using MinecraftBedrockStructureBlock.structure.block.prefabs;
using MinecraftBedrockStructureBlock.types;
using MinecraftBedrockStructureBlock.types.util;
namespace TestConsole {
    class Program {
        static void Main(string[] args) {
            McStructure mystuct = new McStructure(4, 1, 4);
            BinaryWriter bw = new BinaryWriter(new FileStream("output.mcstructure", FileMode.Create));
            mystuct.setBlock(0, 0, 0, MinecraftPrefabs.ConcreteBlack);
            mystuct.setBlock(1, 0, 0, MinecraftPrefabs.ConcreteGray);
            mystuct.setBlock(2, 0, 0, MinecraftPrefabs.ConcreteLightGray);
            mystuct.setBlock(3, 0, 0, MinecraftPrefabs.ConcreteWhite);
            mystuct.setBlock(0, 0, 1, MinecraftPrefabs.ConcreteBrown);
            mystuct.setBlock(1, 0, 1, MinecraftPrefabs.ConcreteRed);
            mystuct.setBlock(2, 0, 1, MinecraftPrefabs.ConcreteOrange);
            mystuct.setBlock(3, 0, 1, MinecraftPrefabs.ConcreteYellow);
            mystuct.setBlock(0, 0, 2, MinecraftPrefabs.ConcreteLightBlue);
            mystuct.setBlock(1, 0, 2, MinecraftPrefabs.ConcreteCyan);
            mystuct.setBlock(2, 0, 2, MinecraftPrefabs.ConcreteGreen);
            mystuct.setBlock(3, 0, 2, MinecraftPrefabs.ConcreteLime);
            mystuct.setBlock(0, 0, 3, MinecraftPrefabs.ConcretePink);
            mystuct.setBlock(1, 0, 3, MinecraftPrefabs.ConcreteMagenta);
            mystuct.setBlock(2, 0, 3, MinecraftPrefabs.ConcretePurple);
            mystuct.setBlock(3, 0, 3, MinecraftPrefabs.ConcreteBlue);
            NbtBase res = mystuct.GetNBT();
            Console.WriteLine(res);
            res.print(bw, true);
            bw.Close();
        }
    }
}
