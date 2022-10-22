using System;
using System.IO;
using System.Collections.Generic;
using BedrockTools_Build.Generator;
using BedrockTools_Build.Generator.MinecraftBlocks;
using BedrockTools_Build.Oil2;
using BedrockTools_Build.OilInit;

namespace BedrockTools_Build.Operations.Objects.Blocks {
    public class BlockClassGeneration : Operation {
        public override void Run() {
            ObjectInitializerList list = new MinecraftOil2Parser(
                code: File.ReadAllText("OilFiles/Block/Block2.oiltwo"),
                settings: OilSettings.GetSettings()
            ).Parse();
            foreach(KeyValuePair<string, ObjectInitializer> kvp in list.GetInitializers()) {
                if (kvp.Value.ObjectType=="Variant") {
                    VariationGenerator codegen = new VariationGenerator() {
                        ClassName=kvp.Key,
                        Values=new List<string>(kvp.Value.GetObjectValue("values").Split(",")),
                        Variation= kvp.Value.GetObjectValue("variant")
                    };
                    SourceGenerator sourceGenerator = new SourceGenerator(
                         $"Objects/Blocks/Minecraft/{kvp.Key}Block.cs",
                         codegen,
                         new Dictionary<string, string> { { "version", VariationGenerator.VERSION } }
                        );
                    sourceGenerator.WriteCode();
                }
            }
        }
    }
}
