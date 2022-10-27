using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using BedrockTools.Nbt.Elements;
using BedrockTools.Objects.Blocks.Util;

namespace BedrockTools.Objects.Blocks {
    public class StairsBlock : Block , IEquatable<StairsBlock> {
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

        public override Block Transform(McTransform transformation) {
            if (transformation.Equals(McTransform.Identity))
                return this;
            List<BlockOrientation> orientations = new List<BlockOrientation> {
                BlockOrientation.North,
                BlockOrientation.West,
                BlockOrientation.South,
                BlockOrientation.East
            };
            int direction = orientations.IndexOf(Orientation);
            direction += (int)transformation.Rotation;
            direction &= 3; //Modulo 4.
            if (transformation.FlipZ && (direction & 1) == 0) {
                direction += 2;
                direction &= 3;
            }
            if (transformation.FlipX && (direction & 1) == 1) {
                direction += 2;
                direction &= 3;
            }
            return new StairsBlock(Identifier, orientations[direction], IsUpsideDown);          
        }
        public bool Equals(StairsBlock other) {
            return false;
            return Identifier == other.Identifier
            && Orientation == other.Orientation
            && IsUpsideDown == other.IsUpsideDown;
        }

    }
}
