using System;
using System.Collections.Generic;
using System.Text;
using BedrockTools.Objects.Blocks;
using BedrockTools.Nbt.Elements;
using BedrockTools.Nbt.Extension;
namespace BedrockTools.Structure {
    public class BlockPalette :INbtParsable<NbtCompoundOrdered> {
        Dictionary<Block, int> blocks;
        Dictionary<int, NbtCompound> entityData = new Dictionary<int, NbtCompound>();

        public BlockPalette() {
            blocks = new Dictionary<Block, int>();
        }
        public int getIndex(Block block) {
            if (block == null) {
                return -1;
            }
            if (blocks.ContainsKey(block)) {
                return blocks[block];
            }
            int val = blocks.Count;
            blocks[block] = blocks.Count;
            return val;
        }

        public List<NbtCompound> GetNbtCompounds() {
            List<NbtCompound> list = new List<NbtCompound>();
            foreach (Block blockData in blocks.Keys) {
                list.Add(blockData.GetStructureBlock());
            }
            return list;
        }

        NbtCompound GetEntityData() {
            
            return new NbtCompoundSorted();
        }
        public NbtCompoundOrdered ToNbt() => new NbtCompoundOrdered() {
                {"block_palette", NbtList.Compounds(GetNbtCompounds())},
                {"block_position_data", GetEntityData()}
            };
    }
}
