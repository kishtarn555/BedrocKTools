using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BedrockTools_Build.Generator.MinecraftBlocks {
    class MinecraftBlockPrefabsGenerator : Generator {
        BlockList blocks;
        
        public MinecraftBlockPrefabsGenerator(string target, BlockList blockList) : base(target) {
            blocks = blockList;
        }


        public override string GetCode() {
            return
 $@"
{base.GetCode()}
using System;
using System.Collections.Generic;

namespace BedrockTools.Objects {{

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
        }}
    }}
}}
";      
        }
        private string GetDictionary() {
            StringBuilder builder = new StringBuilder();
            foreach(var item in blocks.blocks) {
                builder.Append(
$@"            blockDictionary.Add(""{item.nameclass}"", new BedrockTools.Objects.Minecraft.Blocks.{item.nameclass}());
");
            }
            return builder.ToString();
        }
    }
}
