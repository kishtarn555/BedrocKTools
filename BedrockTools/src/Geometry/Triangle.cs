using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BedrockTools.Geometry {
    public struct Triangle {
        public Vector3 A, B, C;
        public Vector3 normalA, normalB, normalC;
        public Vector2 TextCoords;

        public Vector3 normal {
            get {
                Vector3 u = Vector3.Normalize(B - A);
                Vector3 v = Vector3.Normalize(C - A);
                return Vector3.Cross(u, v);
            }
        }

        public Vector3 low {
            get {
                return Vector3.Min(A, Vector3.Min(B, C)); ;
            }
        }
        public Vector3 hi {
            get {
                return Vector3.Max(A, Vector3.Max(B, C)); ;
            }
        }

        public Triangle(Vector3 A, Vector3 B, Vector3 C) {
            this.A = A;
            this.B = B;
            this.C = C;
            normalA = normalB = normalC = Vector3.Zero;
            this.TextCoords = Vector2.Zero;
        }
        public Triangle(Vector3 A, Vector3 B, Vector3 C, Vector2 TextCoords) {
            this.A = A;
            this.B = B;
            this.C = C;

            normalA = normalB = normalC = Vector3.Zero;
            this.TextCoords = TextCoords;
        }
        public Triangle(Vector3 A, Vector3 B, Vector3 C, Vector2 TextCoords, Vector3 normalA, Vector3 normalB, Vector3 normalC) {
            this.A = A;
            this.B = B;
            this.C = C;

            this.normalA = normalA;
            this.normalB = normalB;
            this.normalC = normalC;
            
            this.TextCoords = TextCoords;
        }

        internal static Triangle Shrink(Triangle triangle) {
            Vector3 center = (triangle.A + triangle.B + triangle.C) / 3f;
            Vector3 vA = center-triangle.A;
            Vector3 vB = center-triangle.B;
            Vector3 vC = center-triangle.C;

            vA = vA.Length() < 0.01f ? vA/2f : vA / vA.Length() * 0.01f;
            vB = vB.Length() < 0.01f ? vB/2f : vB / vB.Length() * 0.01f;
            vC = vC.Length() < 0.01f ? vC/2f : vC / vC.Length() * 0.01f;

            Vector3 A = triangle.A + vA;
            Vector3 B = triangle.B + vB;
            Vector3 C = triangle.C + vC;
            if (triangle.normalA != Vector3.Zero) {
                A -= Vector3.Normalize(triangle.normalA) * 0.01f;
            }
            if (triangle.normalB != Vector3.Zero) {
                B -= Vector3.Normalize(triangle.normalB) * 0.01f;
            }
            if (triangle.normalC != Vector3.Zero) {
                C -= Vector3.Normalize(triangle.normalC) * 0.01f;
            }
            Triangle result = new Triangle(A,B, C, triangle.TextCoords, triangle.normalA, triangle.normalB, triangle.normalC);
            return result;
        }
    }
}
