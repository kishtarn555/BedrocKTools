using System;
using System.Collections.Generic;
using BedrockTools_Build.OilInit;

namespace BedrockTools_Build.Generator.MinecraftBlocks {
    public class MinecraftBlockRegistryGenerator : ICodeGenerator {
        private ObjectInitializerList InitializerList;

        public MinecraftBlockRegistryGenerator(ObjectInitializerList initializerList) {
            InitializerList=initializerList;

        }


        public string GetCode(int tabulation = 0) {
            CodeBuilder builder = new CodeBuilder(0);
            builder
                .WriteLine("using System.Collections.Generic;")
                .NewLine()
                .WriteLine("namespace BedrockTools.Objects.Blocks {")
                .Ident()
                    .WriteLine("public sealed class BlocksRegistry {")
                    .Ident()
                        .WriteLine("static BlocksRegistry _instance = null;")
                        .WriteLine("Dictionary<string, Block> blockDictionary;")
                        .NewLine()
                        .WriteLine("public static BlocksRegistry Instance {")
                        .Ident()
                            .WriteLine("get {")
                            .Ident()
                                .WriteLine("if (_instance == null) {")
                                .Ident()
                                    .WriteLine("_instance = new BlocksRegistry();")
                                .Deident()
                                .WriteLine("}")
                                .WriteLine("return _instance;")
                            .Deident()
                            .WriteLine("}")
                        .Deident()
                        .WriteLine("}")
                        .NewLine()
                        .WriteLine("public Block GetPrefabByName(string name) => blockDictionary[name];")
                        .NewLine()
                        .WriteLine("public void AddRegister(string key, Block block) {")
                        .Ident()
                            .WriteLine("blockDictionary.Add(key, block);")
                        .Deident()
                        .WriteLine("}")
                        .NewLine()
                        .WriteLine("private BlocksRegistry() {")
                        .Ident()
                            .WriteLine("blockDictionary = new Dictionary<string, Block>();");
                            BuildDictionaryCode(builder);
                        builder.Deident()
                        .WriteLine("}")
                    .Deident()
                    .WriteLine("}")
                .Deident()
                .WriteLine("}");
                
            return builder.ToString();
        }

        private void BuildDictionaryCode(CodeBuilder builder) {
            foreach (KeyValuePair<string, ObjectInitializer> iterator in InitializerList.GetInitializers()) {
                (string Key, ObjectInitializer initilizaer) item = (iterator.Key, iterator.Value);
                item.initilizaer.settings = OilSettings.GetSettings();
                builder.WriteLine($"blockDictionary.Add(\"{item.Key}\", {item.initilizaer.GetCode()});");                
            }            
                
        }
    }
}
