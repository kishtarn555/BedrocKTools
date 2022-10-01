using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace BedrockTools_Build.Generator.MinecraftBlocks {
    class BlockList {
        public class BlockItem {
            public string nameclass;
            public string identifier;
            public string nbt;

            public BlockItem(string nameclass, string identifier, string nbt) {
                this.nameclass = nameclass;
                this.identifier = identifier;
                this.nbt = nbt;
            }
        }
        public List<BlockItem> blocks;

        public BlockList() {
            blocks = new List<BlockItem>();
        }
        public static BlockList FromFile(string filepath) {
            BlockList b = new BlockList();
            string[] fileContent = File.ReadAllLines(filepath);
            int lcode=-1;
            foreach(string line in fileContent) {
                lcode++;
                string tline = Regex.Replace(line, @"//.*", "");
                tline = line.Trim();
                if (tline.Length == 0) 
                    continue;
                if (Regex.IsMatch(tline, "//.*"))
                    continue;
                string[] components = Regex.Split(tline, @"\s");
                if (components.Length==2) {
                    b.blocks.Add(new BlockItem(components[0], components[1], "{}"));
                } else if (components.Length==3){
                    b.blocks.Add(new BlockItem(components[0], components[1], "{}"));
                } else {
                    throw new Exception($"Wrong format at line {lcode}:{line}.");
                }
            }
            return b;
        }
    }
}
