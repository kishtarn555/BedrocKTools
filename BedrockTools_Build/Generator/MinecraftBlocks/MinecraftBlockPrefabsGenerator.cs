using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace BedrockTools_Build.Generator.MinecraftBlocks {
    class MinecraftBlockPrefabsGenerator : Generator {
        BlockList blocks;
        abstract class Node {
            public abstract string getcode(string name, int ident);
            protected string tab(int tabs) {
                string space = "";
                for (int i = 0; i < tabs * 4; i++)
                    space += " ";
                return space;
            }
        }
        class Cls:Node {
            public Dictionary<string, Node> objects = new Dictionary<string, Node>();
            public Cls() {
                objects = new Dictionary<string, Node>();
            }
            public override string getcode(string name, int ident) {
                StringBuilder str = new StringBuilder($"{tab(ident)}public static class {name} {{\r\n");
                foreach (var n in objects) {
                    str.Append(n.Value.getcode(n.Key, ident + 1));
                }
                str.Append($"{tab(ident)}}}\r\n");
                return str.ToString();

            }
        }

        class Fact : Node {
            public string dictname;
            public string classname;

            public Fact(string dictname, string classname) {
                this.dictname = dictname;
                this.classname = classname;
            }

            public override string getcode(string name, int ident) {                

                return 
                    $"{tab(ident)}public static {classname} Get{name}\r\n"
                        +$"{tab(ident+1)}=> ({classname})MinecraftBlockPrefabs.Instance.GetPrefabByName(\"{dictname}\");\r\n";
            }
        }
        Dictionary<string, Node> blocktree;

        Cls GetClass(Dictionary<string, Node> dict, string key) {
            if (!dict.ContainsKey(key)) {
                dict.Add(key, new Cls());
            } 
                return (Cls)dict[key];
        }
        
        public MinecraftBlockPrefabsGenerator(string target, BlockList blockList) : base(target) {
            blocks = blockList;
            blocktree = new Dictionary<string, Node>();
            foreach (var block in blocks.blocks) {
                string[] path = block.nameclass.Split('.');
                string classname = Regex.Replace(block.nameclass, @"\.", "");
                if (path.Length==1) {
                    blocktree.Add(path[0], new Fact(path[0], classname));
                    continue;
                }
                Cls cur = GetClass(blocktree, path[0]);
                for (int i=1; i <path.Length-1; i++) {
                    cur = GetClass(cur.objects, path[i]);
                }
                cur.objects.Add(path[path.Length - 1], new Fact(block.nameclass,classname));
            }

        }

       
        public override string GetCode() {
            return
 $@"
{base.GetCode()}
using System;
using System.Collections.Generic;
using BedrockTools.Objects.Minecraft.Blocks;

namespace BedrockTools.Objects.Minecraft {{

    public sealed class MinecraftBlockPrefabs {{
        static MinecraftBlockPrefabs _instance = null;
        Dictionary<string, Block> blockDictionary;

        public static MinecraftBlockPrefabs Instance {{
            get {{
                if (_instance == null)
                    _instance = new MinecraftBlockPrefabs();
                return _instance;
            }}
        }}

        public Block GetPrefabByName(string name) {{
            return blockDictionary[name];
        }}

        private MinecraftBlockPrefabs() {{
            this.blockDictionary = new Dictionary<string, Block>();
{GetDictionary()}


{GetFactory()}
    }}
}}
";      
        }
        private string GetDictionary() {
            StringBuilder builder = new StringBuilder();
            foreach(var item in blocks.blocks) {
                builder.Append(
$@"            blockDictionary.Add(""{item.nameclass}"", new BedrockTools.Objects.Minecraft.Blocks.{Regex.Replace(item.nameclass,@"\.","")}());
");
            }
            builder.Append("         }");
            return builder.ToString();
        }
        string GetFactory() {
            StringBuilder str = new StringBuilder();
            foreach (var nt in blocktree) {
                str.Append(nt.Value.getcode(nt.Key, 2));
            }
            return str.ToString();
        }
    }
}
