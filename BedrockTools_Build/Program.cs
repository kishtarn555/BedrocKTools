using System;
using System.Text.RegularExpressions;

using BedrockTools_Build.Generator.MinecraftBlocks;
using BedrockTools_Build.Generator;
namespace BedrockTools_Build {
    class Program {
        static void Main(string[] args) {
            ObjectInitializer init = new ObjectInitializer("dummy", true, false);
            init.AddConstructorParameter("identifier", "\"minecraft:air\"");
            init.AddConstructorParameter("ido", "\"minecraft:air\"");

            init.AddObjectValue("keya", "3");
            init.AddObjectValue("summy", "true");
            Console.Write(init.GetCode(2));
            return;
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
