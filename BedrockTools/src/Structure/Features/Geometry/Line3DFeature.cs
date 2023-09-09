using System.Numerics;

using BedrockTools.Objects;
using BedrockTools.Objects.Blocks;
using BedrockTools.Structure.Features.Geometry;


namespace BedrockTools.Structure.Features.Geometry {
    public class Line3DFeature : Analitical3DShape {
        Vector3 A;
        Vector3 B;
        float thickness;
        public Line3DFeature(Dimensions Size, FillMode fillMode, Block fillBlock, Vector3 A, Vector3 B, float thickness) : base(Size, fillMode, fillBlock) {
            this.A = A;
            this.B = B;
            this.thickness = thickness;
            IsPointInsideRegion = TestPoint;
        }


        bool TestPoint(float x, float y, float z) {
            Vector3 w = new Vector3(x, y, z) - A;
            Vector3 v = B - A;

            float area = Vector3.Cross(w, v).Length();
            float distance = area / (2f * v.Length());
            return distance < thickness;
        }
    }
}
