using BedrockTools.Nbt.Elements;
using BedrockTools.Objects.Blocks.Util;


namespace BedrockTools.Objects.Blocks {
    public class DoublePlantBlock : Block {
        DoublePlantType plantType;
        bool upperBlockBit;

        public DoublePlantBlock(string identifier, DoublePlantType plantType, bool upperBlockBit) : base(identifier) {
            this.plantType = plantType;
            this.upperBlockBit = upperBlockBit;
        }

        public override NbtCompoundSorted GetBlockState() =>
            new NbtCompoundSorted() {
                { "double_plant_type", (NbtString)plantType },
                { "upper_block_bit", (NbtByte)upperBlockBit }
            };
    }
}
