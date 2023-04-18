using BedrockTools.Geometry;
using BedrockTools.Objects;
using BedrockTools.Objects.Blocks;
using BedrockTools.Structure.Features.Geometry;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;

namespace BedrockTools.Structure.Features.Advance {
    public class TrianglesFromObjParser {

        public static List<Triangle3DFeature> ParseObjFileTo3DTriangles(string filePath, Dimensions size, FillMode fillMode, Block block, float width, float scale) {


            // Create a list to store the triangles
            List<Triangle3DFeature> triangles = new List<Triangle3DFeature>();
            List<Vector3> vertices = new List<Vector3>();
            float mx = float.PositiveInfinity;
            float my = float.PositiveInfinity;
            float mz = float.PositiveInfinity;
            // Read the file line by line
            foreach (string line in File.ReadLines(filePath)) {
                string[] parts = line.Split(new char[] { ' ' }, System.StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length == 0 || parts[0].StartsWith("#")) {
                    // Skip comments and empty lines
                    continue;
                }
                else if (parts[0] == "v") {
                    // Parse vertex positions
                    float x = float.Parse(parts[1]) * scale;
                    float y = float.Parse(parts[2]) * scale;
                    float z = float.Parse(parts[3]) * scale;
                    mx = float.Min(mx, x);
                    my = float.Min(my, y);
                    mz = float.Min(mz, z);

                    vertices.Add(new Vector3(x, y, z));
                }
                else if (parts[0] == "f") {
                    // Parse face indices
                    int[] indices = parts.Skip(1).Select(s => int.Parse(s.Split('/')[0])).ToArray();
                    Vector3 A = new Vector3(vertices[indices[0] - 1].X, vertices[indices[0] - 1].Y, vertices[indices[0] - 1].Z);
                    Vector3 B = new Vector3(vertices[indices[1] - 1].X, vertices[indices[1] - 1].Y, vertices[indices[1] - 1].Z);
                    Vector3 C = new Vector3(vertices[indices[2] - 1].X, vertices[indices[2] - 1].Y, vertices[indices[2] - 1].Z);
                    Triangle3DFeature triangle = new Triangle3DFeature(size, fillMode, block, A, B, C, width);
                    triangles.Add(triangle);
                }
            }

            Vector3 low = new Vector3(mx, my, mz);
            Vector3 lowTarget = new Vector3(0, 0, 0);

            triangles.ForEach(triangle => {
                triangle.SetTransformation(Matrix4x4.CreateTranslation(low - lowTarget));
            });



            return triangles;
        }


      

            
        

        

        static bool TestCubeTriangleIntersection(Triangle triangle, Vector3 minima, Vector3 maxima) {
            //Bounding box optimization
            if (maxima.X < triangle.low.X || triangle.hi.X < minima.X) return false;
            if (maxima.Y < triangle.low.Y || triangle.hi.Y < minima.Y) return false;
            if (maxima.Z < triangle.low.Z || triangle.hi.Z < minima.Z) return false;
            
            Vector3 u = Vector3.Normalize(triangle.B - triangle.A);
            Vector3 v = Vector3.Normalize(triangle.C - triangle.A);
            Vector3 n = triangle.normal;

            Vector3 maxX = new Vector3(maxima.X, minima.Y, minima.Z);
            Vector3 maxY = new Vector3(minima.X, maxima.Y, minima.Z);
            Vector3 maxZ = new Vector3(minima.X, minima.Y, maxima.Z);



            Vector3 minX = new Vector3(minima.X, maxima.Y, maxima.Z);
            Vector3 minY = new Vector3(maxima.X, minima.Y, maxima.Z);
            Vector3 minZ = new Vector3(maxima.X, maxima.Y, minima.Z);

            Line[] segments = new Line[]{
                new Line(minima, maxX ),
                new Line(minima, maxY ),
                new Line(minima, maxZ ),

                new Line(maxima, minX ),
                new Line(maxima, minY ),
                new Line(maxima, minZ ),

                new Line(minX, maxY),
                new Line(minX, maxZ),


                new Line(minY, maxX),
                new Line(minY, maxZ),


                new Line(minZ, maxX),
                new Line(minZ, maxY),
            };


            HashSet<Vector3> intersetcion = new HashSet<Vector3>();
            for (int i =0; i < 12; i++) {
                Vector3 dir = segments[i].B - segments[i].A;
                float denom = Vector3.Dot(dir, n);
                if (denom == 0) continue;
                float d = Vector3.Dot((triangle.A - segments[i].A), n)/denom;
                if (d < 0 || d > 1) continue;
                intersetcion.Add(segments[i].A + dir * d);
            }            

            if (intersetcion.Count == 0) return false;
            Vector3[] polygon = intersetcion.ToArray();
            if (polygon.Length == 1) {
                return GeometryUtil.PointInTrianlge(triangle, polygon[0]);
            }
            ////Sort Polygon points
            //for (int i = 0; i < Polygon.Length-2; i++) {
            //    for (int j = i+2; j < Polygon.Length; j++) {
            //        Vector3 a = Polygon[i + 1] - Polygon[i];
            //        Vector3 b = Polygon[j] - Polygon[i];
            //        float dir = GeometryUtil.GetCrossSignedMagnitude(a, b, n);
            //        if (dir < 0) {
            //            Vector3 tmp = Polygon[i + 1];
            //            Polygon[i+1] = Polygon[j];
            //            Polygon[j] = tmp;
            //        }
            //    }
            //}

            int insideSgn = Math.Sign(
                GeometryUtil.GetCrossSignedMagnitude(
                    triangle.B - triangle.A,
                    triangle.C - triangle.A,
                    n
                )
            );
            Vector3[] tri = new Vector3[] {
                triangle.A,
                triangle.B,
                triangle.C,
                triangle.A,
            };
            for (int t = 0; t < 3; t++) {
                bool anyInside = false;
                Vector3 a, b;
                a = tri[t+1] - tri[t];
                for (int i = 0; i < polygon.Length; i++) {
                    b = polygon[i] - tri[t];
                    if (insideSgn == Math.Sign(
                        GeometryUtil.GetCrossSignedMagnitude(a, b, n)
                        )) {
                        anyInside = true;
                        break;
                    }
                }

                if (!anyInside) return false;
            }
            return true;
        }

        //FIXE: This do be too slow
        public static McStructure IntersectionTriangleObjToStruct(string filePath, Dimensions size, Block block, float scale) {
            

            // Create a list to store the triangles
            List<Triangle> triangles = new List<Triangle>();
            List<Vector3> vertices = new List<Vector3>();
            float mx = float.PositiveInfinity;
            float my = float.PositiveInfinity;
            float mz = float.PositiveInfinity;
            // Read the file line by line
            foreach (string line in File.ReadLines(filePath)) {
                string[] parts = line.Split(new char[] { ' ' }, System.StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length == 0 || parts[0].StartsWith("#")) {
                    // Skip comments and empty lines
                    continue;
                }
                else if (parts[0] == "v") {
                    // Parse vertex positions
                    float x = float.Parse(parts[1]) * scale;
                    float y = float.Parse(parts[2]) * scale;
                    float z = float.Parse(parts[3]) * scale;
                    mx = float.Min(mx, x);
                    my = float.Min(my, y);
                    mz = float.Min(mz, z);

                    vertices.Add(new Vector3(x, y, z));
                }
                else if (parts[0] == "f") {
                    // Parse face indices
                    int[] indices = parts.Skip(1).Select(s => int.Parse(s.Split('/')[0])).ToArray();
                    Vector3 A = new Vector3(vertices[indices[0] - 1].X, vertices[indices[0] - 1].Y, vertices[indices[0] - 1].Z);
                    Vector3 B = new Vector3(vertices[indices[1] - 1].X, vertices[indices[1] - 1].Y, vertices[indices[1] - 1].Z);
                    Vector3 C = new Vector3(vertices[indices[2] - 1].X, vertices[indices[2] - 1].Y, vertices[indices[2] - 1].Z);
                    Triangle triangle = new Triangle(A, B, C);
                    triangles.Add(triangle);
                }
            }

            Vector3 low = new Vector3(mx, my, mz);
            Vector3 lowTarget = new Vector3(0, 0, 0);
            Vector3 translation = lowTarget - low;
            for (int i =0; i < triangles.Count; i++) {
                triangles[i] = new Triangle(
                    triangles[i].A + translation,
                    triangles[i].B + translation,
                    triangles[i].C + translation
                );
            }
            McStructure structure = new McStructure(size);
            Random rand = new Random();
            foreach (Triangle triangle in triangles) {
                Vector3 lo = triangle.low;
                Vector3 hi = triangle.hi;
                for (int x = int.Max(0, (int)lo.X-1); x < int.Min(size.X, (int)hi.X+1); x++) {
                    for (int y = int.Max(0, (int)lo.Y-1); y < int.Min(size.Y, (int)hi.Y+1); y++) {
                        for (int z = int.Max(0, ( int)lo.Z-1); z < int.Min(size.Z, (int)hi.Z+1); z++) {
                            if (TestCubeTriangleIntersection(triangle, new Vector3(x,y,z), new Vector3(x, y, z)+Vector3.One)) {
                                structure.SetBlock(x, y, z, block);
                            }
                        }
                    }
                }
            }

            return structure;
        }

    }
}
