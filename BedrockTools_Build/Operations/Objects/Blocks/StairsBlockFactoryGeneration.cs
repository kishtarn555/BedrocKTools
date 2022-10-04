using System;
using System.Collections.Generic;
using System.IO;
using BedrockTools_Build.Generator;
using BedrockTools_Build.Generator.MinecraftBlocks;
using BedrockTools_Build.OilInit;
using BedrockTools_Build.OilInit.Minecraft;

namespace BedrockTools_Build.Operations.Objects.Blocks {
    class StairsBlockFactoryGeneration : Operation {
        public override void Run() {
            ObjectInitializerList stairsBlocks = new OilParser(
                   code: File.ReadAllText("OilFiles/Block/minecraft_stairs_blocks.oil"),
                   settings: OilSettings.GetSettings()
               ).Parse();
            MinecraftStairsFactoryGenerator stairsFactoryGenerator = new MinecraftStairsFactoryGenerator(stairsBlocks);


            SourceGenerator sourceGen = new SourceGenerator(
                "Objects/Blocks/Factory/StairsFactory.cs",
                stairsFactoryGenerator,
                new Dictionary<string, string>() {
                    { "version", MinecraftStairsFactoryGenerator.VERSION}
                }

            );
            sourceGen.WriteCode();
        }
    }
}
