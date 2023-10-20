using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BedrockTools.Geometry.Path {
    public class ConstantPath : ParametricPath {
        Vector3 ConstantValue;
        public ConstantPath(Vector3 constantValue, float start, float end) : base(start, end) {
            this.ConstantValue = constantValue;
        }

        public override Vector3 GetPoint(float t) => ConstantValue;
        public override Vector3 GetDirection(float t) => Vector3.Zero;
    }
}
