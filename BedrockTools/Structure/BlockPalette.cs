﻿using System;
using System.Collections.Generic;
using System.Text;
using BedrockTools.Objects;
using BedrockTools.Nbt.Elements;
using BedrockTools.Nbt.Extension;
namespace BedrockTools.Structure {
    public class BlockPalette :INbtParsable<NbtCompoundOrdered> {
        Dictionary<Block, int> blocks;

        public BlockPalette() {
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
                list.Add((NbtCompound)blockData.ToNbt());
            }
            return list;
        }

        NbtCompound GetEntityData() {
            return new NbtCompoundSorted();
        }
        public NbtCompoundOrdered ToNbt() {
            return new NbtCompoundOrdered(){
                {"block_palette", NbtList.Compounds(GetNbtCompounds())},
                {"block_position_data", GetEntityData()}
            };
        }
    }
}