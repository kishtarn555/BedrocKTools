using BedrockTools.Objects;
using BedrockTools.Objects.Blocks;
using BedrockTools.Structure.Features.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BedrockTools.Structure.Features.Geometry {
    public class Region3DFeature : Analitical3DShape {
        Vector3 minBounding;
        Vector3 maxBounding;

        public Region3DFeature(
            Dimensions Size,
            FillMode fillMode,
            Block fillBlock,
            Vector3 minBounding,
            Vector3 maxBounding
        ) : base(Size, fillMode, fillBlock) {
            this.minBounding = minBounding;
            this.maxBounding = maxBounding;

            IsPointInsideRegion = BlockTest;
        }


        private bool BlockTest(float x, float y, float z) { 
            if (x < minBounding.X || y < minBounding.Y || z < minBounding.Z) return false;
            if (x > maxBounding.X || y > maxBounding.Y || z > maxBounding.Z) return false;
            return true;
        }
    }
}
