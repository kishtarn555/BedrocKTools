using System.Collections.Generic;
using BedrockTools.Nbt;
using BedrockTools.Nbt.Elements;
using BedrockTools.Nbt.Extension;
using BedrockTools.Objects.Blocks;
using BedrockTools.Objects;

namespace BedrockTools.Structure {
    public class McStructure : INbtParsable<NbtCompoundOrdered> {
        public readonly Dimensions Size;
        public readonly IntCoords Origin;

        Block[,,] blocks;
        BlockPalette palette; 
        Dictionary<int, NbtCompound> entityData;
        public McStructure(Dimensions size) {
            Size = size;
            blocks = new Block[Size.X, Size.Y, Size.Z];
            palette = null;
            Origin = new IntCoords(0, 0, 0);
        }

        public McStructure(Dimensions size, IntCoords origin) {
            Size = size;
            blocks = new Block[Size.X, Size.Y, Size.Z];
            palette = null;
            Origin = origin;
        }

        public void SetBlock(int x, int y, int z, Block block) {
            blocks[x, y, z] = block;
        }
        public void FillVoidWithAir() {
            FillVoid(VanillaBlockFactory.Air());
        }
        public void FillVoid(Block filler) {
            for (int x = 0; x < Size.X; x++) {
                for (int y = 0; y < Size.Y; y++) {
                    for (int z = 0; z < Size.Z; z++) {
                        if (blocks[x, y, z] == null) {
                            blocks[x, y, z] = filler;
                        }
                    }
                }
            }
        }

        public NbtCompoundOrdered ToNbt() {
            BuildPalette();

            return new NbtCompoundOrdered() {
                { "format_version", (NbtInt) 1},
                { "size", NbtList.FromInts(Size.X, Size.Y, Size.Z)},
                { "structure", ParseStructure()},
                { "structure_world_origin", Origin.ToNbt()}
            };
        }
        private void BuildPalette() {
            palette = new BlockPalette();
            entityData = new Dictionary<int, NbtCompound>();
            int index = 0;
            for (int i = 0; i < Size.X; i++) {
                for (int j = 0; j < Size.Y; j++) {
                    for (int k = 0; k < Size.Z; k++) {
                        palette.getIndex(blocks[i, j, k]);
                        if (blocks[i, j, k] is EntityBlock blockEntity) {
                            entityData[index] = blockEntity.GetEntityData();
                        }
                        index++;
                    }
                }
            }
        }


        private int GetBaseBlock(int x, int y, int z) {
            return palette.getIndex(blocks[x, y, z]);
        }

        private NbtList ParseBlockIndices() {
            NbtList block_indices = new NbtList(NbtTag.TAG_List);
            NbtList baseLayer = new NbtList(NbtTag.TAG_Int);
            for (int i = 0; i < Size.X; i++) {
                for (int j = 0; j < Size.Y; j++) {
                    for (int k = 0; k < Size.Z; k++) {
                        baseLayer.Add((NbtInt)GetBaseBlock(i, j, k));
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
        private NbtCompound ParsePalette() {
            return new NbtCompoundOrdered(){
                { "default",palette.ToNbt() }
            };
        }

        private NbtCompound ParseStructure() {
            NbtCompound structure = new NbtCompoundOrdered() {
                {"block_indices", ParseBlockIndices()},
                {"entities", ParseEntities()},
                {"palette", ParsePalette()}
            };
            return structure;
        }
    }
}
