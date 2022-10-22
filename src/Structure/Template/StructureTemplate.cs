using System;
using System.Collections.Generic;
using BedrockTools.Objects.Blocks;
using BedrockTools.Objects;
using BedrockTools.Objects.Entities;

namespace BedrockTools.Structure.Template {
    public class StructureTemplate : BaseStructure {
       
        public StructureTemplate(Dimensions size):base(size) {}

        public McStructure RenderStructure(Func<int, Block> BlockMap) {
            McStructure response = new McStructure(Size);
            //foreach(Entity entity in entities) {
            //    response.AddEntity(entity);
            //}
            for (int x = 0; x < Size.X; x++) {
                for (int y = 0; y < Size.Y; y++) {
                    for (int z = 0; z < Size.Z; z++) {
                        Block block = blocks[x, y, z];
                        if (block is TemplateBlock tblock) {
                            block = BlockMap(tblock.TemplateIndex);
                        }
                        response.SetBlock(x, y, z, block);
                    }
                }
            }
            return response;
        }

        public McStructure RenderStructure(Block[] palette) => RenderStructure((int x) => palette[x]);

        public void SetBlock(int x, int y, int z, Block block) => blocks[x, y, z] = block;

        public void SetBlock(IntCoords coords, Block block) => SetBlock(coords.X, coords.Y, coords.Z, block);

        public void SetTemplateBlock(int x, int y, int z, int index) => blocks[x,y,z] = new TemplateBlock(index);

        public void SetTemplateBlock(IntCoords coords, int index) => SetTemplateBlock(coords.X, coords.Y, coords.Z, index);
    }
}
