using System;
using System.IO;

using BedrockTools_Build.Generator.MinecraftBlocks;
using BedrockTools_Build.Generator;
using BedrockTools_Build.OilInit.Minecraft;
using BedrockTools_Build.OilInit;
namespace BedrockTools_Build {
    class Program {
        static void Main(string[] args) {
            ObjectInitializerList lst =
                new OilBlockParser(File.ReadAllText("OilFiles/minecraft_simple_blocks.blockoil"), "Block", OilSettings.GetSettings()).Parse();

            MinecraftBlockRegistryGenerator registryGenerator = new MinecraftBlockRegistryGenerator(lst);
            SourceGenerator generator = new SourceGenerator("Objects/Blocks/BlocksRegistry.cs", registryGenerator);
            generator.WriteCode();
            return;
            /*
            BlockList blockList = BlockList.FromFile("Config/minecraft_simple_blocks.ldcon");
            MinecraftBlockRegistryGenerator generator = new MinecraftBlockPrefabsGenerator(
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
            */
        }
    }
}
