using System;
using System.IO;

using BedrockTools_Build.Generator.MinecraftBlocks;
using BedrockTools_Build.Generator;
using BedrockTools_Build.OilInit.Minecraft;
using BedrockTools_Build.OilInit;
namespace BedrockTools_Build {
    class Program {
        static void Main(string[] args) {
            ObjectInitializerList simpleBlocks =
                new OilBlockParser(
                    code: File.ReadAllText("OilFiles/Block/minecraft_simple_blocks.blockoil"), 
                    objectType: "Block", 
                    settings: OilSettings.GetSettings()
                ).Parse();
            ObjectInitializerList stairsBlocks = new OilParser(
                    code: File.ReadAllText("OilFiles/Block/minecraft_stairs_blocks.oil"),
                    settings: OilSettings.GetSettings()
                ).Parse();
            MinecraftBlockFactoryGenerator blockCodeGenerator = new MinecraftBlockFactoryGenerator(simpleBlocks);
            MinecraftStairsFactoryGenerator stairsFactoryGenerator = new MinecraftStairsFactoryGenerator(stairsBlocks);
            SourceGenerator simpeBlockGenerator = new SourceGenerator("Objects/Blocks/Factory/BlockFactory.cs", blockCodeGenerator);
            SourceGenerator stairsBlockGenerator = new SourceGenerator("Objects/Blocks/Factory/StairsFactory.cs", stairsFactoryGenerator);
            simpeBlockGenerator.WriteCode();
            stairsBlockGenerator.WriteCode();
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
