using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BedrockTools.Structure.Features.Nature.TreeA {
    public class SuperBranch : IEnumerable<Branch> {
        public Vector3 pointA;
        public Vector3 pointB;
        public float widthA;
        public float widthB;
        public int segments;
        public float deviation;
        public float tolerance;

        public IEnumerator<Branch> GetEnumerator() {
            Random random = new Random();
            Vector3[] points = Enumerable.Range(0, segments + 1).Select((x) => {
                float p = x;
                p = p / segments;
                Vector3 res = Vector3.Lerp(pointA, pointB, p);
                if (x !=0 && x != segments) {
                    res +=  new Vector3((float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble()) * deviation;
                }
                return res;
            }).ToArray();

            float[] widths = Enumerable.Range(0, segments + 1).Select((x) => {
                float p = x;
                p = p / segments;
                return (widthB - widthA) * p + widthA;
            }).ToArray();

            for (int i = 0; i < segments; i++) {

                yield return new Branch(
                    points[i],
                    points[i + 1],
                    widths[0],
                    widths[1],
                    tolerance
                ) ;
                
            }
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
