using System;
using System.Linq;
using BedrockTools.Nbt;
using BedrockTools.Nbt.Elements;
using BedrockTools.Objects;
using BedrockTools.Objects.Blocks;
using BedrockTools.Objects.Entities;

namespace BedrockTools.Structure {
    public class McStructureDeserializer {
        NbtCompound target;
        public McStructureDeserializer(NbtCompound target) {
            this.target = target;
        }

        public McStructure Deserialize() {
            NbtList size = (NbtList)target["size"];
            int sx = ((NbtInt)size[0]).Value;
            int sy = ((NbtInt)size[1]).Value;
            int sz = ((NbtInt)size[2]).Value;
            McStructure mcstructure = new McStructure(new Dimensions(sx, sy, sz));

            NbtCompound structure = (NbtCompound)target["structure"];
            int[] firstLayer = ((NbtList)(((NbtList)structure["block_indices"])[0])).Select(x => ((NbtInt)x).Value).ToArray();
            int[] secondLayer = ((NbtList)(((NbtList)structure["block_indices"])[1])).Select(x => ((NbtInt)x).Value).ToArray();

            NbtCompound defaultPalette = (NbtCompound)structure.Extract("palette", "default");
            NbtList blockPalette = (NbtList)defaultPalette["block_palette"];
            NbtList blockIndices = (NbtList)structure["block_indices"];
            NbtList baseLayer = (NbtList)blockIndices[0];
            

            int ind = 0;
            for (int x = 0; x < mcstructure.Size.X; x++) {
                for (int y =0; y < mcstructure.Size.Y; y++) {
                    for (int z=0;  z < mcstructure.Size.Z; z++) {
                        int pind = ((NbtInt)baseLayer[ind]).Value;
                        ind++;
                        if (pind == -1) continue;
                        NbtCompound blockData = (NbtCompound)blockPalette[pind];
                        NbtString identifier = (NbtString)blockData["name"];
                        NbtCompoundSorted blockState = (NbtCompoundSorted)blockData["states"];
                        Block block = new UnregisteredBlock(identifier.Value, blockState);

                        mcstructure.SetBlock(x, y, z, block);
                    }
                }
            }
            //TODO: add support for block entity data
            



            return mcstructure;
        }
        
    }
}
