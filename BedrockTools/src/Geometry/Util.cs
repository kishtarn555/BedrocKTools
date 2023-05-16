using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace BedrockTools.Geometry {
    public class GeometryUtil {
        public static float GetCrossSignedMagnitude(Vector3 v1, Vector3 v2, Vector3 normal) {
            return Vector3.Dot(Vector3.Cross(v1, v2), normal);
        }


        public static bool TestLinealConvexPointInPolygon(Vector3[] points,Vector3 point, bool inclusive) {
            if (points.Length  < 3) throw new Exception();
            int sign = 0;
            Vector3 normal = Vector3.Normalize(Vector3.Cross(points[1] - points[0], points[2] - points[0]));
            for (int i=0; i < points.Length; i++) {
                int ni = (i+1)%points.Length;
                Vector3 u = points[ni] - points[i];
                Vector3 v = point - points[i];
                int res = Math.Sign(GetCrossSignedMagnitude(u, v, normal));
                if (inclusive && res == 0) continue;
                if (sign == 0) sign = res;
                if (res != sign) return false;
            }
            return true;
        }

        internal static bool PointInTrianlge(Triangle triangle, Vector3 point) => TestLinealConvexPointInPolygon(new Vector3[] {triangle.A, triangle.B, triangle.C}, point, true);

        public static Vector3 GenerateRandomUnitVector() {
            Random random = new Random();
            float x = (float)(random.NextDouble() * 2 - 1);
            float y = (float)(random.NextDouble() * 2 - 1);
            float z = (float)(random.NextDouble() * 2 - 1);

            Vector3 vector = new Vector3(x, y, z);
            vector = Vector3.Normalize(vector);
            return vector;
        }
        public  static Vector3 RotateVector(Vector3 vector, Vector3 axis, float angle) {
            float cosTheta = (float)Math.Cos(angle);
            float sinTheta = (float)Math.Sin(angle);

            Vector3 u = axis;

            float x = vector.X * (cosTheta + u.X * u.X * (1 - cosTheta)) +
                      vector.Y * (u.X * u.Y * (1 - cosTheta) - u.Z * sinTheta) +
                      vector.Z * (u.X * u.Z * (1 - cosTheta) + u.Y * sinTheta);

            float y = vector.X * (u.Y * u.X * (1 - cosTheta) + u.Z * sinTheta) +
                      vector.Y * (cosTheta + u.Y * u.Y * (1 - cosTheta)) +
                      vector.Z * (u.Y * u.Z * (1 - cosTheta) - u.X * sinTheta);

            float z = vector.X * (u.Z * u.X * (1 - cosTheta) - u.Y * sinTheta) +
                      vector.Y * (u.Z * u.Y * (1 - cosTheta) + u.X * sinTheta) +
                      vector.Z * (cosTheta + u.Z * u.Z * (1 - cosTheta));

            return new Vector3(x, y, z);
        }

    }
}
