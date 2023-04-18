using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BedrockTools.Geometry {
    public struct Triangle {
        public Vector3 A, B, C;
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

        }

    }
}
