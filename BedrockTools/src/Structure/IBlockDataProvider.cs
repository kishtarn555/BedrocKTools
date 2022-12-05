using System;
using BedrockTools.Objects;
using BedrockTools.Objects.Blocks;
namespace BedrockTools.Structure {
    public interface IBlockDataProvider : IRegion {
        public Block GetBlock(IntCoords coordinates);
    }
}
