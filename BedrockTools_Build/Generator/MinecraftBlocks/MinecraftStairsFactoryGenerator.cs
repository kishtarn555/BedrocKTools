using System.Collections.Generic;
using BedrockTools_Build.OilInit;
using BedrockTools_Build.Util.WordTrie;

namespace BedrockTools_Build.Generator.MinecraftBlocks {
    public class MinecraftStairsFactoryGenerator : ICodeGenerator {
        public const string VERSION = "0.1.0";
        private ObjectInitializerList InitializerList;
        WDir<ObjectInitializer> trie;
        public MinecraftStairsFactoryGenerator(ObjectInitializerList initializerList) {
            InitializerList=initializerList;
            foreach (var initializer in InitializerList.GetInitializers()) {
                initializer.Value.AddConstructorParameter("orientation", "orientation");
                initializer.Value.AddConstructorParameter("isUpsideDown", "isUpsideDown");
            }
            BuildTrie();
        }
        private void BuildTrie() {
            trie = new WDir<ObjectInitializer>();
            foreach (KeyValuePair<string, ObjectInitializer> kvp in InitializerList.GetInitializers()) {
                string[] comps = kvp.Key.Split(".");
                WDir<ObjectInitializer> cur = trie;
                for (int i=0; i < comps.Length-1; i++) {
                    cur = cur.MoveDir(comps[i]);
                }
                cur.AddItem(comps[comps.Length-1], kvp.Value);
            }
        }
        
        public string GetCode(int tabulation = 0) {
            CodeBuilder builder = new CodeBuilder(0);
            builder
                .WriteLine("using System.Collections.Generic;")
                .WriteLine("using BedrockTools.Objects.Blocks.Util;")
                .EndLine()
                .WriteLine("namespace BedrockTools.Objects.Blocks {")
                .Ident()
                    .WriteLine("public static partial class VanillaBlockFactory {");
                        BuildFactories(trie,builder);
                    builder.WriteLine("}")
                .Deident()
                .WriteLine("}");
                
            return builder.ToString();
        }

        private void BuildFactories(WDir<ObjectInitializer> cur,  CodeBuilder builder) {
            builder.Ident();
            foreach(KeyValuePair<string, WDir<ObjectInitializer> > subdirs in cur.subDirs) {
                builder.WriteLine($"public static class {subdirs.Key} {{");
                BuildFactories(subdirs.Value, builder);
                builder.WriteLine("}");
            }
            foreach(KeyValuePair<string, ObjectInitializer> decs in cur.items) {
                decs.Value.settings= OilSettings.GetSettings();
                builder.WriteLine($"public static StairsBlock {decs.Key} (BlockOrientation orientation, bool isUpsideDown = false)  => {decs.Value.GetCode()};");
            }
            builder.Deident();
                
        }
    }
}
