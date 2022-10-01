using System;

using BedrockTools_Build.Generator.MinecraftBlocks;
namespace BedrockTools_Build {
    class Program {
        static void Main(string[] args) {
            MinecraftBlockPrefabsGenerator generator = new MinecraftBlockPrefabsGenerator(
                "Objects/Minecraft/MinecraftBlockPrefab.cs", 
                BlockList.FromFile("Config/minecraft_simple_blocks.ldcon")
            );
        }
    }
}
