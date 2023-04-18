using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BedrockTools.Geometry {
    public struct Line {
        public Vector3 A, B;

        public Line(Vector3 A, Vector3 B) {
            this.A = A;
            this.B = B;
        }
    }
}
