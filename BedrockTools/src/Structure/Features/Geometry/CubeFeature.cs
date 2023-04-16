using System;
using BedrockTools.Objects;
using BedrockTools.Objects.Blocks;

namespace BedrockTools.Structure.Features.Geometry {
    public class CubeFeature : Analitical3DShape {
        public CubeFeature(int x,int y, int z, Block fillerBlock) : base(new Dimensions(x,y,z), FillMode.Solid, fillerBlock) {
            IsPointInsideRegion = TestRegion;
        }
        public CubeFeature(Dimensions size, Block fillerBlock):base(size, FillMode.Solid, fillerBlock) {            
            IsPointInsideRegion = TestRegion;
        }

        public CubeFeature(Dimensions size, FillMode fillMode, Block fillerBlock) : base(size, fillMode, fillerBlock) {
            IsPointInsideRegion = TestRegion;
        }

        protected bool TestRegion(int a, int b, int c) => (
                0 <= a && a < Size.X &&
                0 <= b && b < Size.Y &&
                0 <= c && c < Size.Z 
            );
    }
}
