﻿using BedrockTools.Objects;
using BedrockTools.Objects.Blocks;
using System;

namespace BedrockTools.Structure.Features.Modifier {
    public class SubstractModifier : Feature {
        readonly Feature target;
        readonly Feature mask;
        readonly McTransform maskInverseTransform;
        public SubstractModifier(Feature target, Feature mask) : base(target.Size) {
            this.target = target;
            this.mask = mask;
            maskInverseTransform = McTransform.Identity;
        }
        public SubstractModifier(Feature target, Feature mask, McTransform maskTransform) : base(target.Size) {
            this.target = target;
            this.mask = mask;
            maskInverseTransform = maskTransform.GetInverse();
        }


        public override Block GetBlock(int x, int y, int z) {
            IntCoords maskCoords = maskInverseTransform.GetCoords(mask.Size, x, y, z);
            if (!maskCoords.InRegion(IntCoords.Zero, mask.Size) ) {
                return target.GetBlock(x, y, z);
            }
            if (mask.GetBlock(maskCoords) == null) {
                return target.GetBlock(x, y, z);
            }else {
                return null;
            }
        }
    }
}
