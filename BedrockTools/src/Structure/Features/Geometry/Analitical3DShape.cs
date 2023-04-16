using BedrockTools.Objects;
using BedrockTools.Objects.Blocks;
using System;
using System.Linq;

namespace BedrockTools.Structure.Features.Geometry {
    public class Analitical3DShape : Feature{
        public FillMode FillingMode { get; }
        public Block FillingBlock { get; }
      
        protected Func<int, int, int, bool> IsPointInsideRegion;

        public Analitical3DShape(Dimensions Size, FillMode fillMode, Block fillBlock, Func<int, int, int, bool> regionTestFunction) : base(Size) {
            FillingMode = fillMode;
            FillingBlock = fillBlock;
            IsPointInsideRegion = regionTestFunction;
        }
        protected Analitical3DShape(Dimensions Size, FillMode fillMode, Block fillBlock) : base(Size) {
            FillingMode = fillMode;
            FillingBlock = fillBlock;
        }


        public override Block GetBlock(int x, int y, int z) {            
            if (!IsPointInsideRegion(x, y, z) ) {
                return null;
            }
            if (FillingMode == FillMode.Solid)
                return FillingBlock;

            bool AreAllOrthogonalInside = (
                IsPointInsideRegion(x - 1, y, z)
                && IsPointInsideRegion(x + 1, y, z)
                && IsPointInsideRegion(x, y - 1, z)
                && IsPointInsideRegion(x, y + 1, z)
                && IsPointInsideRegion(x, y, z - 1)
                && IsPointInsideRegion(x, y, z + 1)
            );
            if (!AreAllOrthogonalInside) {
                return FillingBlock;
            }
            if (FillingMode == FillMode.BorderThin) {
                return null;
            }
            bool AreAllDiagonalInside = (
                IsPointInsideRegion(x - 1, y - 1, z -1)
                && IsPointInsideRegion(x - 1, y - 1, z + 1)
                && IsPointInsideRegion(x - 1, y + 1, z - 1)
                && IsPointInsideRegion(x - 1, y + 1, z + 1)
                && IsPointInsideRegion(x + 1, y - 1, z - 1)
                && IsPointInsideRegion(x + 1, y - 1, z + 1)
                && IsPointInsideRegion(x + 1, y + 1, z - 1)
                && IsPointInsideRegion(x + 1, y + 1, z + 1)
            );
            if (!AreAllDiagonalInside) {
                return FillingBlock;
            }
            return null;


        }
    }
}
