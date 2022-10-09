using System;
using BedrockTools.Nbt.Elements;
using BedrockTools.Objects.Blocks.Util;

namespace BedrockTools.Objects.Blocks {
    public class StairsBlock : Block {
        public readonly bool IsUpsideDown;
        public readonly BlockOrientation Orientation;

        public StairsBlock(string identifier, BlockOrientation orientation, bool isUpsideDown) : base(identifier) {
            Orientation = orientation;
            IsUpsideDown = isUpsideDown;
        }

        public override NbtCompoundSorted GetBlockState() {
            int weirdo_direction;
            switch (Orientation) {
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
                    throw new ArgumentException("Orientation for a stair can only be North, East, South or West. But got: " + Orientation);
            }
            return new NbtCompoundSorted() {
                { "weirdo_direction", (NbtInt)weirdo_direction },
                { "upside_down_bit", (NbtByte)(IsUpsideDown? 1:0) }
            };
        }
    }
}
