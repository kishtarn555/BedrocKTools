using BedrockTools.Objects.Blocks;

namespace BedrockTools.Structure.Advanced {
    public interface IBlockPalette {
        public Block this[int index] { get; }
    }
}
