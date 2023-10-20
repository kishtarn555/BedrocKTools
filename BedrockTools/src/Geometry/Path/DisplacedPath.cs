using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BedrockTools.Geometry.Path {
    public class DisplacedPath : ParametricPath {
        ParametricPath original;
        ParametricPath displacementPath;
        public DisplacedPath(float start, float end, ParametricPath original, ParametricPath displacementPath) : base(start, end) {
            this.original = original;
            this.displacementPath = displacementPath;
        }

        public override Vector3 GetDirection(float t) => original.GetDirection(t)+displacementPath.GetDirection(t);
        public override Vector3 GetPoint(float t) => original.GetPoint(t)+displacementPath.GetPoint(t);
    }
}
