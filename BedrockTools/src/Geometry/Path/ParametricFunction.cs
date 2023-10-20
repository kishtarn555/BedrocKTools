using System;
using System.Numerics;

namespace BedrockTools.Geometry.Path {
    public class ParametricFunction : ParametricPath {
        Func<float, Vector3> Function;
        Func<float, Vector3> Derivate;
        public ParametricFunction(float start, float end, Func<float, Vector3> function, Func<float, Vector3> derivate) : base(start, end) {
            Function = function;
            Derivate = derivate;
        }

        public override Vector3 GetDirection(float t) => Derivate(t);
        public override Vector3 GetPoint(float t) => Function(t);
    }
}
