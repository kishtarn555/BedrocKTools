using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace BedrockTools.Geometry.Path {
    public class CatmullRomSpline : Spline {
        public CatmullRomSpline(Vector3[] points) : base(Matrix4x4.Identity, points, points.Length-3) {
            float[,] characteristics = new float[,] {
                { 0.0f,  1.0f,  0.0f,  0.0f },
                {-0.5f,  0.0f,  0.5f,  0.0f },
                { 1.0f, -2.5f,  2.0f, -0.5f },
                {-0.5f,  1.5f, -1.5f,  0.5f }
            };
            for (int i = 0; i < 4; i++) {
                for (int j = 0; j < 4; j++) {
                    CharacteristicMatrix[i, j] = characteristics[i, j];
                }
            }
        }

        protected override Vector3 GetA(int t) => Points[t];
        protected override Vector3 GetB(int t) => Points[t+1];
        protected override Vector3 GetC(int t) => Points[t+2];
        protected override Vector3 GetD(int t) => Points[t+3];
    }
}
