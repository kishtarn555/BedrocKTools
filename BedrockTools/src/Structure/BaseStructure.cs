using System;
using BedrockTools.Objects;
using BedrockTools.Objects.Blocks;
using BedrockTools.Objects.Entities;

namespace BedrockTools.Structure {
    public abstract class BaseStructure : IMcStructure {
        public Dimensions Size { get; protected set; }

        public IntCoords Origin { get; protected set; }
        protected Block[,,] blocks;

        public BaseStructure(Dimensions size) {
            Size = size;
            blocks = new Block[size.X, size.Y, size.Z];
            Origin = IntCoords.Zero;
        }
        public BaseStructure(Dimensions size, IntCoords origin) {
            Size = size;
            blocks = new Block[size.X, size.Y, size.Z];
            Origin = origin;
        }
        public BaseStructure(Dimensions size, Block[,,] blocks) {
            Size = size;
            this.blocks = (Block[,,])blocks.Clone();
            Origin = IntCoords.Zero;
        }

        public void AddEntity(Entity entity) => throw new NotImplementedException();

        public Block GetBlock(int x, int y, int z) => blocks[x, y, z];

        public Block GetBlock(IntCoords coords) => GetBlock(coords.X, coords.Y, coords.Z);

        public Block[,,] GetBlocks() => (Block[,,])blocks.Clone();

        public void SetBlock(int x, int y, int z, Block block) => blocks[x, y, z] = block;

        public void SetBlock(IntCoords coords, Block block) => SetBlock(coords.X, coords.Y, coords.Z, block);

        public void SetBlocks(Block[,,] blocks) => this.blocks = (Block[,,])blocks.Clone();
    }
}
