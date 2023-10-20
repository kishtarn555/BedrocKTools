using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BedrockTools.Geometry.Path {
    public abstract class Spline : ParametricPath {

        protected Matrix4x4 CharacteristicMatrix;
        protected Vector3[] Points;
        
        public Spline (Matrix4x4 characteristicMatrix, Vector3[] points, int segments): base(0, segments) {
            CharacteristicMatrix = characteristicMatrix;
            Points = new Vector3[points.Length];
            Array.Copy(points, Points, points.Length);
        }

        protected abstract Vector3 GetA(int t);
        protected abstract Vector3 GetB(int t);
        protected abstract Vector3 GetC(int t);
        protected abstract Vector3 GetD(int t);

        public override Vector3 GetPoint(float t) {

            int index = (int)MathF.Floor(t);
            float frac  = t- (float)Math.Floor(t);
            Vector3 A = GetA(index);
            Vector3 B = GetB(index);
            Vector3 C = GetC(index);
            Vector3 D = GetD(index);
            Vector4 vt = new Vector4(1, frac, frac * frac, frac * frac * frac);
            Vector4 temporal = Vector4.Zero;
            for (int j = 0; j < 4; j++) {
                for (int k = 0; k < 4; k++) {
                    temporal[j] += vt[k] * CharacteristicMatrix[k, j];
                }
            }
            Vector3 result = (
                A * temporal.X +
                B * temporal.Y +
                C * temporal.Z +
                D * temporal.W
                );
            return result;

        }
        public override Vector3 GetDirection(float t) {

            int index = (int)t;
            float frac  = t-(float)Math.Floor(t);
            Vector3 A = GetA(index);
            Vector3 B = GetB(index);
            Vector3 C = GetC(index);
            Vector3 D = GetD(index);
            Vector4 vt = new Vector4(0, 1, 2f*frac, 3* frac * frac);
            Vector4 temporal = Vector4.Zero;
            for (int j = 0; j < 4; j++) {
                for (int k = 0; k < 4; k++) {
                    temporal[j] += vt[k] * CharacteristicMatrix[k, j];
                }
            }
            Vector3 result = (
                A * temporal.X +
                B * temporal.Y +
                C * temporal.Z +
                D * temporal.W
                );
            return result;

        }

    }
}
