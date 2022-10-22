using System;
using BedrockTools.Objects.Blocks;
using BedrockTools.Objects;
using BedrockTools.Objects.Entities;

namespace BedrockTools.Structure.Features {
    public class StructureFeature: BaseStructure {
        
        public StructureFeature(Dimensions size) : base(size) { }
        public StructureFeature(Block[,,] blocks) : base(
            new Dimensions(blocks.GetLength(0), blocks.GetLength(1), blocks.GetLength(2))
        ) {
            this.blocks = (Block[,,])blocks.Clone();
            Size = new Dimensions(blocks.GetLength(0), blocks.GetLength(1), blocks.GetLength(2));
        }      

        //TODO: Rotation and mirroring
        public void PlaceInStructure(IMcStructure structure, IntCoords offset) {
            for (int x=0; x < Size.X; x++) {
                for (int y = 0; y < Size.Y; y++) {
                    for (int z=0; z < Size.Z; z++) {
                        Block block = GetBlock(x, y, z);
                        if (block!=null)
                            structure.SetBlock(new IntCoords(x, y, z) + offset, blocks[x, y, z]);
                    }

                }
            }
        }       
    }
}
