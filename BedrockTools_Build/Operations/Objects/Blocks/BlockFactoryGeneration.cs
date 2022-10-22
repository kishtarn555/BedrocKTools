using System;
using System.Collections.Generic;
using System.IO;
using BedrockTools_Build.Generator;
using BedrockTools_Build.Generator.MinecraftBlocks;
using BedrockTools_Build.OilInit;
using BedrockTools_Build.Oil2;
using BedrockTools_Build.OilInit.Minecraft;
namespace BedrockTools_Build.Operations.Objects.Blocks {
    class BlockFactoryGeneration : Operation {        
        public override void Run() {
            ObjectInitializerList blockList =
                new MinecraftOil2Parser(
                    code: File.ReadAllText("OilFiles/Block/Block2.oiltwo"),
                    settings: OilSettings.GetSettings()
                ).Parse();
            MinecraftBlockFactoryGenerator codeGenerator = new MinecraftBlockFactoryGenerator(blockList);
            SourceGenerator sourceGenerator = new SourceGenerator(
               "Objects/Blocks/Factory/BlockFactory.cs",
               codeGenerator,
               new Dictionary<string, string>() {
                    { "version", MinecraftBlockFactoryGenerator.VERSION}
               }
            );
            sourceGenerator.WriteCode();
            
        }
    }
}
