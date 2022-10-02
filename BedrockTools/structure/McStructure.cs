using System.Collections.Generic;
using BedrockTools.Nbt;
using BedrockTools.Nbt.Elements;
using BedrockTools.Nbt.Extension;
using BedrockTools.Objects;

namespace BedrockTools.Structure {
    public class McStructure : INbtParsable<NbtCompoundOrdered> {
        public int sizeX;
        public int sizeY;
        public int sizeZ;

        Block[,,] blocks;
        BlockPalette palette;
        Dictionary<int, NbtCompound> entityData;
        public McStructure(int sizeX, int sizeY, int sizeZ) {
            this.sizeX = sizeX;
            this.sizeY = sizeY;
            this.sizeZ = sizeZ;
            blocks = new Block[sizeX, sizeY, sizeZ];
            palette = null;
        }

        public void setBlock(int x, int y, int z, Block block) {
            blocks[x, y, z] = block;
        }

        void buildPalette() {
            palette = new BlockPalette();
            entityData = new Dictionary<int, NbtCompound>();
            int index = 0;
            for (int i = 0; i < sizeX; i++) {
                for (int j = 0; j < sizeY; j++) {
                    for (int k = 0; k < sizeZ; k++) {
                        palette.getIndex(blocks[i, j, k]);
                        if (blocks[i, j, k] is BlockEntity) {
                            entityData[index] = (blocks[i, j, k] as BlockEntity).EntityData;
                        }
                        index++;
                    }
                }
            }
        }


        int getBaseBlock(int x, int y, int z) {
            return palette.getIndex(blocks[x, y, z]);
        }


        NbtList parseBlockIndicesNBT() {
            NbtList block_indices = new NbtList(NbtTag.TAG_List);
            NbtList baseLayer = new NbtList(NbtTag.TAG_Int);
            for (int i = 0; i < sizeX; i++) {
                for (int j = 0; j < sizeY; j++) {
                    for (int k = 0; k < sizeZ; k++) {
                        baseLayer.Add((NbtInt)getBaseBlock(i, j, k));
                    }
                }
            }
            NbtList upperLayer = new NbtList(NbtTag.TAG_Int);
            for (int i = 0; i < sizeX; i++) {
                for (int j = 0; j < sizeY; j++) {
                    for (int k = 0; k < sizeZ; k++) {
                        upperLayer.Add((NbtInt)(-1));
                    }
                }
            }
            block_indices.Add(baseLayer);
            block_indices.Add(upperLayer);
            return block_indices;

        }
        //TODO: Support entities
        NbtList parseEntitiesNBT() {
            return NbtList.Empty();
        }
        NbtCompound parsePaletteNBT() {
            return new NbtCompoundOrdered(){
                { "default",palette.ToNbt() }
            };
        }

        NbtCompound parseStructureNBT() {
            NbtCompound structure = new NbtCompoundOrdered() {
                {"block_indices", parseBlockIndicesNBT()},
                {"entities", parseEntitiesNBT()},
                {"palette", parsePaletteNBT()}
            };
            return structure;
        }

        public NbtCompoundOrdered ToNbt() {
            buildPalette();

            return new NbtCompoundOrdered() {
                { "format_version", (NbtInt) 1},
                { "size", NbtList.FromInts(sizeX, sizeY, sizeZ)},
                { "structure", parseStructureNBT()},
                { "structure_world_origin", NbtList.FromInts(0, 0, 0)}
            };
        }
    }
}
