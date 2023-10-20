
using System.Numerics;

namespace BedrockTools.Geometry.Path {
    public abstract class ParametricPath {
        public Vector3 this[float t] => GetPoint(t);



        public float Start {
            get;
            protected set;
        }
        public float End {
            get;
            protected set;
        }

        public ParametricPath(float start, float end) {
            this.Start = start;
            this.End = end;
        }

        public abstract Vector3 GetPoint(float t);
        public abstract Vector3 GetDirection(float t);

    }
}
