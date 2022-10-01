using System;
using System.Collections.Generic;
using System.Text;

namespace BedrockTools.Objects.Minecraft {
    public sealed class MinecraftBlockPrefabs {

        static MinecraftBlockPrefabs _instance = null;
        Dictionary<string, Block> blockDictionary;
        public static MinecraftBlockPrefabs Instance {
            get {
                if (_instance == null)
                    _instance = new MinecraftBlockPrefabs();
                return _instance;
            }
        }

        public Block GetPrefabByName(string name) {
            return blockDictionary[name];
        }
        public static readonly Block TestBlock = new Block("lol");

        private MinecraftBlockPrefabs() {
            this.blockDictionary = new Dictionary<string, Block>();
           /// blockDictionary.Add("name", )
        }
    }
}
