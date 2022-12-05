using System;
using BedrockTools.Objects;
using BedrockTools.Objects.Blocks;
namespace BedrockTools.Structure {
    public interface IBlockDataConsumer : IBlockDataProvider {
        public void SetBlock(IntCoords coordinates, Block block);
    }
}
