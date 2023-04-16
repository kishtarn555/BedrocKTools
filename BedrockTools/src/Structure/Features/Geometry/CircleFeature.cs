using System;
using BedrockTools.Objects;
using BedrockTools.Objects.Blocks;

namespace BedrockTools.Structure.Features.Geometry {
    public class CircleFeature : Analitical2DShape {

        public CircleFeature(Dimensions Size, FillMode fillMode, PlaneType plane, Block fillBlock) : base(Size, fillMode, plane, fillBlock) {
            IsPointInsideRegion = TestInside;
        }
        protected  bool TestInside(int a, int b) {
            (double w, double h) = Plane.GetABCoords(Size.X, Size.Y, Size.Z);
            w /= 2;
            h /= 2;
            (double da, double db) = (0.5+a, 0.5+b);
            da -= w;
            db -= h;
            bool output = ((da / w) * (da / w) + (db / h) * (db / h)) <= 1.0;
            
            return output;
        }
    }
}
