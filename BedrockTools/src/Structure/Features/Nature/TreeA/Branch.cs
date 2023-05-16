using BedrockTools.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BedrockTools.Structure.Features.Nature.TreeA
{
    public class Branch
    {
        public Vector3 pointA;
        public Vector3 pointB;

        public float widthA;
        public float widthB;
        public float toleranceEnd;

        public Branch(Vector3 pointA, Vector3 pointB, float widthA, float widthB, float toleranceEnd) {
            this.pointA = pointA;
            this.pointB = pointB;
            this.widthA = widthA;
            this.widthB = widthB;
            this.toleranceEnd = toleranceEnd;
        }

        public bool TestPoint(Vector3 point)
        {
            Vector3 u = pointB - pointA;
            Vector3 v = point - pointA;
            float t = Vector3.Dot(u, v) / u.LengthSquared();
            if (t < 0 && -t * u.Length() > toleranceEnd) return false;
            if (t > 0 && (t-1) * u.Length() > toleranceEnd) return false;
            if (t < 0.0f) { t = 0.0f; }
            if (t > 1f) { t = 1f; }

            float distance = ((u * t + pointA) - point).Length();
            float margin = (widthB-widthA)*t+widthA;
            
            return distance <= margin;
        }

        public void CreekBranch(float minAngle, float maxAngle) {
            Vector3 v;
            Random r = new Random();
            float angle = (float)(r.NextDouble()*(maxAngle-minAngle) + minAngle);
            do {
                v = pointB - pointA;
                v = GeometryUtil.RotateVector(v, GeometryUtil.GenerateRandomUnitVector(), angle);
            } while (Vector3.Dot(v, Vector3.UnitY) < 0);
            pointB = v + pointA;
        }
    }
}
