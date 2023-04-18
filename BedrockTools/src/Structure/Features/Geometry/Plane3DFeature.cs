using BedrockTools.Objects;
using BedrockTools.Objects.Blocks;
using System;
using System.Diagnostics;
using System.Numerics;

namespace BedrockTools.Structure.Features.Geometry {
    public class Plane3DFeature : Analitical3DShape {
        Vector3 A;
        Vector3 B;
        Vector3 C;
        float width;
        public Plane3DFeature(Dimensions Size, FillMode fillMode, Block fillBlock, Vector3 a, Vector3 b, Vector3 c, float width) : base(Size, fillMode, fillBlock) {
            A = a;
            B = b;
            C = c;
            this.width = width;
            IsPointInsideRegion = TestPoint;
        }


        bool TestPoint(float x, float y, float z) {
            Vector3 p = new Vector3(x,y,z)-A;
            Vector3 u = Vector3.Normalize(B - A);
            Vector3 v = Vector3.Normalize(C - A);

            Vector3 normal = Vector3.Cross(u, v);
            float distance = Vector3.Dot(normal, p);            
            return Math.Abs(distance) <= width/2f;

        }




    }
}
