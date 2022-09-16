using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using MinecraftBedrockStructureBlock.enums;
using MinecraftBedrockStructureBlock.types;

namespace MinecraftBedrockStructureBlock.structure {
    class McStructure : NbtRepresentableObject {
        public int sizeX;
        public int sizeY;
        public int sizeZ;


        public McStructure(int sizeX, int sizeY, int sizeZ) {
            this.sizeX = sizeX;
            this.sizeY = sizeY;
            this.sizeZ = sizeZ;
        }
        //TODO Palette:
        void buildPalette() { }
        
        int getBaseBlock(int x, int y, int z) {
            return -1;
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
            block_indices.Add(baseLayer);
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
        public NbtList parseEntitiesNBT() {
            NbtList entities = new NbtList("entities", NbtTypes.TAG_End);
            return entities;
        }
        NbtCompound parsePaletteNBT() {
            NbtCompound palette = new NbtCompound("palette");
            NbtCompound defaultPalette = new NbtCompound("default");
            palette.Add(defaultPalette);
            return palette;
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
            return root;
           
        }
    }
}
