using System;
using System.Collections.Generic;
using System.Text;

namespace BedrockTools.Objects.Minecraft.Blocks {
    public sealed class MinecraftBlockPrefabs {

        static MinecraftBlockPrefabs _instance = null;
        public static MinecraftBlockPrefabs Instance {
            get {
                if (_instance == null)
                    _instance = new MinecraftBlockPrefabs();
                return _instance;
            }
        }

        public Block GetPrefabByName(string name) {
            return Instance.GetType().GetField(name).GetValue(null) as Block;
        }
        public static readonly Block TestBlock = new Block("lol");
    }
}
