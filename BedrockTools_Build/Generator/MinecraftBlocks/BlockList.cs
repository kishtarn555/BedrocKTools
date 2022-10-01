using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace BedrockTools_Build.Generator.MinecraftBlocks {
    class BlockList {
        public class BlockItems {
            public string nameclass;
            public string identifier;
            public string nbt;

            public BlockItems(string nameclass, string identifier, string nbt) {
                this.nameclass = nameclass;
                this.identifier = identifier;
                this.nbt = nbt;
            }
        }
        public List<BlockItems> blocks;

        public BlockList() {
            blocks = new List<BlockItems>();
        }
        public static BlockList FromFile(string filepath) {
            BlockList b = new BlockList();
            string[] fileContent = File.ReadAllLines(filepath);
            foreach(string line in fileContent) {
                string tline = Regex.Replace(line, @"//.*", "");
                tline = line.Trim();
                if (tline.Length == 0) 
                    continue;
                if (Regex.IsMatch(tline, "//.*"))
                    continue;
                string[] components = Regex.Split(tline, @"\s");
                if (components.Length==2) {
                    b.blocks.Add(new BlockItems(components[0], components[1], "{}"));
                } else {
                    b.blocks.Add(new BlockItems(components[0], components[1], "{}"));
                }
            }
            return b;
        }
    }
}
