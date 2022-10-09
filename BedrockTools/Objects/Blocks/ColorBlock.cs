using System;
using BedrockTools.Nbt.Elements;
using BedrockTools.Objects.Blocks.Util;

namespace BedrockTools.Objects.Blocks {
    public class ColorBlock : Block {
        public readonly BlockColorValue Color;
        public ColorBlock(string identifier, BlockColorValue color) :base(identifier){
            Color = color;            
        }
        public override NbtCompoundSorted GetBlockState() =>
            new NbtCompoundSorted() {
                {"color", (NbtString)Color.ToString().ToLower() }
            };        
    }
}
