using System;
using System.IO;
using MinecraftBedrockStructureBlock.structure.block;
using MinecraftBedrockStructureBlock.types;
namespace TestConsole {
    class Program {
        static void Main(string[] args) {
            NbtSortedCompound dummy = new NbtSortedCompound("name");
            Block blockOne = new Block("minecraft:identifier", dummy);
            Block blockTwo = new Block("minecraft:identifier", dummy);
            Console.WriteLine(blockOne.GetHashCode());
            Console.WriteLine(blockTwo.GetHashCode());
            Console.WriteLine(blockTwo.Equals(blockTwo));
        }
    }
}
