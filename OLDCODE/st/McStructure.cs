using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using MinecraftBedrockStructureBlock.enums;
using MinecraftBedrockStructureBlock.types;
using MinecraftBedrockStructureBlock.structure.block;

namespace MinecraftBedrockStructureBlock.structure {
    public class McStructure : NbtRepresentableObject {
        public int sizeX;
        public int sizeY;
        public int sizeZ;

        Block[,,] blocks;
        Palette palette;
        Dictionary<int, NbtCompound> entityData;
        public McStructure(int sizeX, int sizeY, int sizeZ) {
            this.sizeX = sizeX;
            this.sizeY = sizeY;
            this.sizeZ = sizeZ;
            blocks = new Block[sizeX,sizeY,sizeZ];
            palette = null;
        }

        public void setBlock(int x, int y, int z, Block block) {
            blocks[x, y, z] = block;
        }

        void buildPalette() {
            palette = new Palette();
            entityData = new Dictionary<int, NbtCompound>();
            int index = 0;
            for (int i =0;i < sizeX; i++) {
                for (int j =0; j <sizeY; j++) {
                    for (int k =0; k <sizeZ; k++) {
                        palette.getIndex(blocks[i, j, k]);
                        if (blocks[i,j,k] is BlockEntity) {
                            entityData[index] =(blocks[i, j, k] as BlockEntity).entityData;
                        }
                        index++;
                    }
                }
            }
        }
    
        
        int getBaseBlock(int x, int y, int z) {
            return palette.getIndex(blocks[x,y,z]);
        }
        NbtList parseSizeNBT() {
            NbtList size = new NbtList("size", NbtTypes.TAG_Int);
            size.Add(new NbtInt("", sizeX));
            size.Add(new NbtInt("", sizeY));
            size.Add(new NbtInt("", sizeZ));            
            return size;
        }


        NbtList parseBlockIndicesNBT() {
            NbtList block_indices = new NbtList("block_indices", NbtTypes.TAG_List);
            NbtList baseLayer = new NbtList("", NbtTypes.TAG_Int);
            for (int i = 0; i < sizeX; i++) {
                for (int j = 0; j < sizeY; j++) {
                    for (int k = 0; k < sizeZ; k++) {
                        baseLayer.Add(new NbtInt("", getBaseBlock(i, j, k)));
                    }
                }
            }
            NbtList upperLayer = new NbtList("", NbtTypes.TAG_Int);
            for (int i = 0; i < sizeX; i++) {
                for (int j = 0; j < sizeY; j++) {
                    for (int k = 0; k < sizeZ; k++) {
                        upperLayer.Add(new NbtInt("", -1));
                    }
                }
            }
            block_indices.Add(baseLayer);
            block_indices.Add(upperLayer);
            return block_indices;

        }
        //TODO: Support entities
        NbtList parseEntitiesNBT() {
            NbtList entities = new NbtList("entities", NbtTypes.TAG_End);
            return entities;
        }
        NbtCompound parsePaletteNBT() {
            buildPalette();
            NbtCompound paletteNbt = new NbtCompound("palette");
            NbtCompound defaultPalette = new NbtCompound("default");
            paletteNbt.Add(defaultPalette);
            List<NbtCompound> paletteList = this.palette.GetNbtCompounds();
            NbtList blockPalette = new NbtList("block_palette", NbtTypes.TAG_Compound);
            foreach (NbtCompound pl in paletteList) {
                blockPalette.Add(pl);
            }
            defaultPalette.Add(blockPalette);
            NbtCompound blockPositionData = new NbtCompound("block_position_data");
            defaultPalette.Add(blockPositionData);

            return paletteNbt;
        }

        NbtCompound parseStructureNBT() {
            NbtCompound structure = new NbtCompound("structure");
            structure.Add(parseBlockIndicesNBT());
            structure.Add(parseEntitiesNBT());
            structure.Add(parsePaletteNBT());
            return structure;
        }

        public override NbtBase GetNBT() {
            buildPalette();
            NbtCompound root = new NbtCompound("");
            {
            NbtInt format_version = new NbtInt("format_version", 1);
            root.Add(format_version);            
            root.Add(parseSizeNBT());
            root.Add(parseStructureNBT());

                // TODO: Add entitiy support
            }
            NbtList origin = new NbtList("structure_world_origin", NbtTypes.TAG_Int);
            origin.Add(new NbtInt("", 0));
            origin.Add(new NbtInt("", 0));
            origin.Add(new NbtInt("", 0));
            root.Add(origin);
            return root;
           
        }
    }
}
