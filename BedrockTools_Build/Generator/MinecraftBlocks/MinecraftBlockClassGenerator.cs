using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;

namespace BedrockTools_Build.Generator.MinecraftBlocks {
    class MinecraftBlockClassGenerator : Generator {
        string identifier;
        string classname;
        string blockstates;

        public MinecraftBlockClassGenerator(string target, string identifier, string classname, string blockstates="{}") :base(target){
            this.identifier = identifier;
            this.classname = classname;
            this.blockstates = blockstates;
        }

     

        public override string GetCode() {
            string nspace = Regex.Match(classname, @".*(?=\.)").Value;
            string singlename= Regex.Match(classname, @"\w+$").Value;
            return
$@"{base.GetCode()}

namespace BlockTools.Objects.Minecraft.Blocks.{nspace} {{    
    public class {singlename} : VanillaBlock {{
        public {singlename} () : base(""{identifier}"", ""{blockstates}"") {{}}
    }}
}}
";

        }
    }
}
