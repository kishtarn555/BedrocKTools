using System;
using System.IO;
using MinecraftBedrockStructureBlock.structure.block;
using MinecraftBedrockStructureBlock.types;
namespace TestConsole {
    class Program {
        static void Main(string[] args) {
            NbtSortedCompound dummy = new NbtSortedCompound("name");
            NbtSortedCompound dummy2 = new NbtSortedCompound("name");
            dummy.Add(new NbtInt("a", 4));
            dummy.Add(new NbtInt("lol", 4));
            dummy2.Add(new NbtInt("lol", 4));
            dummy2.Add(new NbtInt("a", 4));
            NbtList lst = new NbtList("zo", MinecraftBedrockStructureBlock.enums.NbtTypes.TAG_Int);
            lst.Add(new NbtInt("", 33));
            lst.Add(new NbtInt("", 10));
            dummy.Add(lst);
            dummy2.Add(lst);
            Block blockOne = new Block("minecraft:identifier", dummy);
            Block blockTwo = new Block("minecraft:identifier", dummy2);

            Console.WriteLine(blockOne.GetHashCode());
            Console.WriteLine(blockTwo.GetHashCode());
            Console.WriteLine(blockOne.ToString());
            Console.WriteLine(blockTwo.ToString());
            Console.WriteLine(blockTwo.Equals(blockTwo));
        }
    }
}
