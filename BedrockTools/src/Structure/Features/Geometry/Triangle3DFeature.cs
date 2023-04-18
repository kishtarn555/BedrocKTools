using BedrockTools.Geometry;
using BedrockTools.Objects;
using BedrockTools.Objects.Blocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BedrockTools.Structure.Features.Geometry {
    public class Triangle3DFeature : Analitical3DShape {
        Vector3 A, B , C;
        float width;
        public Triangle3DFeature(
            Dimensions Size, 
            FillMode fillMode, 
            Block fillBlock,
            Vector3 A,
            Vector3 B,
            Vector3 C,
            float width
        ) : base(Size, fillMode, fillBlock) {
            this.A = A;
            this.B = B;
            this.C = C;
            this.width = width;

            IsPointInsideRegion = TestPoint;
        }


        public McTransform ResizeToBoundingBox() {
            Vector3 minV = new Vector3(Size.X,Size.Y,Size.Z);
            Vector3 maxV = new Vector3(0,0,0);


            McTransform mcTransform = McTransform.Identity;
            Matrix4x4 inverse;
            Matrix4x4.Invert(transformation, out inverse);
            Vector3 sa = Vector3.Transform(A, inverse);
            Vector3 sb = Vector3.Transform(B, inverse);
            Vector3 sc = Vector3.Transform(C, inverse);

            minV = Vector3.Min(minV, sa - Vector3.One * width);
            minV = Vector3.Min(minV, sb - Vector3.One * width);
            minV = Vector3.Min(minV, sc - Vector3.One * width);
            minV = Vector3.Max(minV, Vector3.Zero);

            maxV = Vector3.Max(maxV, sa +  Vector3.One * width);
            maxV = Vector3.Max(maxV, sb + Vector3.One * width);
            maxV = Vector3.Max(maxV, sc + Vector3.One * width);
            maxV = Vector3.Min(maxV, new Vector3(Size.X-1, Size.Y-1, Size.Z-1));

            Vector3 res = maxV - minV;
            Size = new Dimensions((int)res.X, (int)res.Y, (int)res.Z);
            mcTransform.Translate((int)minV.X, (int)minV.Y, ( int)minV.Z);

            transformation.Translation += new Vector3((int)minV.X, (int)minV.Y, (int)minV.Z);
            return mcTransform;
        }

        bool TestPoint(float x, float y, float z) {
            Vector3 o = new Vector3(x, y, z);
            Vector3 p = o - A;
            Vector3 u = Vector3.Normalize(B - A);
            Vector3 v = Vector3.Normalize(C - A);

            Vector3 normal = Vector3.Cross(u, v);
            float distance = Vector3.Dot(normal, p);
            if (Math.Abs(distance) > width / 2f) return false;

            o -= normal * distance;

            Vector3[] triangle = new Vector3[] {
                A, B, C,A
            };

            int sign = 0;
            for (int it=0; it < 3; it++) {
                float signedCross = GeometryUtil.GetCrossSignedMagnitude(o - triangle[it], triangle[it + 1] - triangle[it], normal);
                int cs = Math.Sign(signedCross);
                if (cs == 0) continue;
                if (sign == 0) {
                    sign = cs;
                }
                if (sign != cs) {
                    return false;
                }
            }
            return true;
        }



    }
}
