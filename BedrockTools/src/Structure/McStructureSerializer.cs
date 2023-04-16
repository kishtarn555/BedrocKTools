using System;
using BedrockTools.Nbt;
using BedrockTools.Nbt.Elements;
using BedrockTools.Objects;
using BedrockTools.Objects.Blocks;
using BedrockTools.Objects.Entities;

namespace BedrockTools.Structure {
    public class McStructureSerializer{
        IMcStructure target;
        public McStructureSerializer (IMcStructure target) {
            this.target = target;
        }


        public NbtCompound GetStructureAsNbt() {
            return new NbtCompoundOrdered() {
                { "format_version", (NbtInt) 1},
                { "size", NbtList.FromInts(target.Size.X, target.Size.Y, target.Size.Z)},
                { "structure", ParseStructure()},
                { "structure_world_origin", target.Origin.ToNbt()}
            };
        }
        private BlockPalette BuildPalette() {
            BlockPalette palette = new BlockPalette();
            int index = 0;
            Dimensions Size = target.Size;
            for (int i = 0; i < Size.X; i++) {
                for (int j = 0; j < Size.Y; j++) {
                    for (int k = 0; k < Size.Z; k++) {
                        palette.getIndex(target.GetBlock(i, j, k));
                       
                        index++;
                    }
                }
            }
            return palette;
        }

        private NbtList ParseBlockIndices(BlockPalette palette) {
            NbtList block_indices = new NbtList(NbtTag.TAG_List);
            NbtList baseLayer = new NbtList(NbtTag.TAG_Int);
            Dimensions Size = target.Size;
            for (int i = 0; i < Size.X; i++) {
                for (int j = 0; j < Size.Y; j++) {
                    for (int k = 0; k < Size.Z; k++) {
                        baseLayer.Add((NbtInt)palette.getIndex(
                            target.GetBlock(i, j, k)
                        ));
                    }
                }
            }
            NbtList upperLayer = new NbtList(NbtTag.TAG_Int);
            for (int i = 0; i < Size.X; i++) {
                for (int j = 0; j < Size.Y; j++) {
                    for (int k = 0; k < Size.Z; k++) {
                        upperLayer.Add((NbtInt)(-1));
                    }
                }
            }
            block_indices.Add(baseLayer);
            block_indices.Add(upperLayer);
            return block_indices;

        }

        //TODO: Support entities
        private NbtList ParseEntities() {
            return NbtList.Empty();
        }

        private NbtCompound ParseStructure() {
            BlockPalette palette = BuildPalette(); 
            NbtCompound structure = new NbtCompoundOrdered() {
                {"block_indices", ParseBlockIndices(palette)},
                {"entities", ParseEntities()},
                {"palette",  new NbtCompoundOrdered(){
                    {"default", palette.ToNbt() }
                }}
            };
            return structure;
        }
    }
}
