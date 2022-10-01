using MinecraftBedrockStructureBlock.types;
using System;
using System.Collections.Generic;
using System.Text;

namespace MinecraftBedrockStructureBlock.structure.block {
    class Palette : NbtRepresentableObject {
        Dictionary<Block, int> blocks;

        public Palette() {
            blocks = new Dictionary<Block, int>();
        }
        public int getIndex(Block block) {
            if (block == null) 
                return -1;
            if (blocks.ContainsKey(block)) 
                return blocks[block];
            int val = blocks.Count;
            blocks[block] = blocks.Count;
            return val;
        }

        public List<NbtCompound> GetNbtCompounds() {
            List<NbtCompound> list = new List<NbtCompound>();
            foreach (Block blockData in blocks.Keys) {
                list.Add((NbtCompound)blockData.GetNBT());
            }
            return list;
        }
        public override NbtBase GetNBT() {
            throw new NotImplementedException();
        }
    }
}
