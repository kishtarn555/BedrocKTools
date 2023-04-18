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
    }
}
