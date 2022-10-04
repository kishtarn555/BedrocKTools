using System;
using BedrockTools.Objects.Blocks.Util;
using BedrockTools.Nbt.Elements;

namespace BedrockTools.Objects.Blocks {
    public class StairsBlock : Block {
        public BlockOrientation Orientation { get; protected set; }
        public bool IsUpsideDown { get; protected set; }
        
        public StairsBlock(string identifier, BlockOrientation orientation, bool isUpsideDown)  {
            Identifier = identifier;
            int weirdo_direction = -1;
            switch (orientation) {
                case BlockOrientation.East:
                    weirdo_direction = 0;
                    break;
                case BlockOrientation.West:
                    weirdo_direction = 1;
                    break;
                case BlockOrientation.South:
                    weirdo_direction = 2;
                    break;
                case BlockOrientation.North:
                    weirdo_direction = 3;
                    break;
                default:
                    throw new ArgumentException("Orientation for a stair can only be North, East, South or West. But got: " + orientation);
            }
            NbtCompoundSorted blockStates = new NbtCompoundSorted() {
                { "weirdo_direction", (NbtInt)weirdo_direction },
                { "upside_down_bit", (NbtInt)(isUpsideDown? 0:1) }
            };
            BlockStates = blockStates;
        }

        



    }
}
