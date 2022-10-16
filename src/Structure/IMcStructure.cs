using System;
using BedrockTools.Nbt.Elements;
using BedrockTools.Objects;
using BedrockTools.Objects.Blocks;
using BedrockTools.Objects.Entities;
namespace BedrockTools.Structure {
    public interface IMcStructure {
        public Dimensions Size { get; }
        public IntCoords Origin { get; }

        public void SetBlock(int x, int y, int z, Block block);
        public void SetBlock(IntCoords coords, Block block);
        public void AddEntity(Entity entity);
        public Block GetBlock(int x, int y, int z);
        public Block GetBlock(IntCoords coords);
    }
}
