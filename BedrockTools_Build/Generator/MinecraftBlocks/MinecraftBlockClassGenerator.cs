using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;

namespace BedrockTools_Build.Generator.MinecraftBlocks {
    class MinecraftBlockClassGenerator : Generator {
        string identifier;
        string classname;
        string blockstates;

        public MinecraftBlockClassGenerator(string target, string classname, string identifier, string blockstates="{}") :base(target){
            this.identifier = identifier;
            this.classname = classname;
            this.blockstates = blockstates;
        }

     

        public override string GetCode() {
            string singlename = Regex.Replace(classname, @"\.", "");
            return
$@"{base.GetCode()}

using BedrockTools.Objects.Minecraft.Blocks;

namespace BedrockTools.Objects.Minecraft.Blocks {{    
    public class {singlename} : VanillaBlock {{
        public {singlename} () : base(""{identifier}"", ""{blockstates}"") {{}}
    }}
}}
";

        }
    }
}
