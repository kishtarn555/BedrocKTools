using BedrockTools.Objects;
using BedrockTools.Objects.Blocks;
using System;
using System.Collections.Generic;
using System.Text;

namespace BedrockTools.Structure.Features.Geometry {
    public abstract class Analitical2DShape : Feature{
        public FillMode FillingMode { get; }
        public Block FillingBlock { get; }
        public PlaneType Plane { get; }
        public Analitical2DShape (Dimensions Size, FillMode fillMode, Block fillBlock): base(Size) {
            FillingMode = fillMode;
            FillingBlock = fillBlock;
            Plane = PlaneType.XY;
        }

        public Analitical2DShape(Dimensions Size, FillMode fillMode, PlaneType plane, Block fillBlock) : base(Size) {
            FillingMode = fillMode;
            FillingBlock = fillBlock;
            Plane = plane;
        }
        protected abstract bool IsPointInsideRegion(int a, int b);
        
        
        public override Block GetBlock(int x, int y, int z) {
            (int a, int b) = Plane.GetABCoords(x,y,z);
            if (!Plane.IsPointInPlane(x,y,z) || !IsPointInsideRegion(a, b) ) {
                return null;
            }
            if (FillingMode == FillMode.Solid)
                return FillingBlock;

            bool AreAllOrthogonalInside = (
                IsPointInsideRegion(a + 1, b)
                && IsPointInsideRegion(a - 1, b)
                && IsPointInsideRegion(a, b + 1)
                && IsPointInsideRegion(a, b - 1)
            );
            if (!AreAllOrthogonalInside) {
                return FillingBlock;
            }
            if (FillingMode == FillMode.BorderThin) {
                return null;
            }
            bool AreAllDiagonalInside = (
                IsPointInsideRegion(a + 1, b + 1)
                && IsPointInsideRegion(a + 1, b - 1)
                && IsPointInsideRegion(a - 1, b + 1)
                && IsPointInsideRegion(a - 1, b - 1)
            );
            if (!AreAllDiagonalInside) {
                return FillingBlock;
            }
            return null;


        }
    }
}
