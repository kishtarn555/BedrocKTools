using System;
using System.Text.RegularExpressions;

using BedrockTools_Build.Generator.MinecraftBlocks;
namespace BedrockTools_Build {
    class Program {
        static void Main(string[] args) {
            BlockList blockList = BlockList.FromFile("Config/minecraft_simple_blocks.ldcon");
            MinecraftBlockPrefabsGenerator generator = new MinecraftBlockPrefabsGenerator(
                "Objects/Minecraft/MinecraftBlockPrefabs.cs", blockList
            ) ;
            foreach (var el in blockList.blocks) {
                MinecraftBlockClassGenerator gen = new MinecraftBlockClassGenerator(
                    $"Objects/Minecraft/Blocks/{Regex.Replace(el.nameclass,@"\.", "")}.cs",
                    el.nameclass,
                    el.identifier,
                    el.nbt
                );
                Console.WriteLine("Generating: "+el.nameclass+".cs");
                gen.Generate();
            }
            Console.WriteLine("Generating prefab");
            generator.Generate();
        }
    }
}
