using BedrockTools.Geometry;
using BedrockTools.Objects;
using BedrockTools.Objects.Blocks;
using BedrockTools.Structure;
using BedrockTools.Structure.Features.Geometry;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Numerics;

namespace BedrockTools.Structure.Advanced.Obj
{
    public class TrianglesFromObjParser
    {

        public static List<Triangle3DFeature> ParseObjFileTo3DTriangles(string filePath, Dimensions size, FillMode fillMode, Block block, float width, float scale)
        {


            // Create a list to store the triangles
            List<Triangle3DFeature> triangles = new List<Triangle3DFeature>();
            List<Vector3> vertices = new List<Vector3>();
            float mx = float.PositiveInfinity;
            float my = float.PositiveInfinity;
            float mz = float.PositiveInfinity;
            // Read the file line by line
            foreach (string line in File.ReadLines(filePath))
            {
                string[] parts = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length == 0 || parts[0].StartsWith("#"))
                {
                    // Skip comments and empty lines
                    continue;
                }
                else if (parts[0] == "v")
                {
                    // Parse vertex positions
                    float x = float.Parse(parts[1]) * scale;
                    float y = float.Parse(parts[2]) * scale;
                    float z = float.Parse(parts[3]) * scale;
                    mx = float.Min(mx, x);
                    my = float.Min(my, y);
                    mz = float.Min(mz, z);

                    vertices.Add(new Vector3(x, y, z));
                }
                else if (parts[0] == "f")
                {
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

            triangles.ForEach(triangle =>
            {
                triangle.SetTransformation(Matrix4x4.CreateTranslation(low - lowTarget));
            });



            return triangles;
        }

        static bool TestAABBTriangleIntersection(Triangle triangle, Vector3 minima, Vector3 maxima) {
            Vector3 center = (minima + maxima) / 2f;
            Vector3 extends = (maxima - minima) / 2f;

            Vector3 v0 = triangle.A - center;
            Vector3 v1 = triangle.B - center;
            Vector3 v2 = triangle.C - center;

            Vector3 f0 = v1 - v0;
            Vector3 f1 = v2 - v1;
            Vector3 f2 = v0 - v2;

            Vector3 u0 = Vector3.UnitX;
            Vector3 u1 = Vector3.UnitY;
            Vector3 u2 = Vector3.UnitZ;

            Vector3[] Axes = new Vector3[]
            {
                Vector3.Cross(f0, u0),
                Vector3.Cross(f0, u1),
                Vector3.Cross(f0, u2),

                Vector3.Cross(f1, u0),
                Vector3.Cross(f1, u1),
                Vector3.Cross(f1, u2),

                Vector3.Cross(f2, u0),
                Vector3.Cross(f2, u1),
                Vector3.Cross(f2, u2),

                u0,
                u1,
                u2,

                triangle.normal
            };

            foreach (Vector3 axis in Axes) {
                float p0 = Vector3.Dot(v0, axis);
                float p1 = Vector3.Dot(v1, axis);
                float p2 = Vector3.Dot(v2, axis);

                float r = extends.X * Math.Abs(Vector3.Dot(u0, axis)) +
                    extends.Y * Math.Abs(Vector3.Dot(u1, axis)) +
                    extends.Z * Math.Abs(Vector3.Dot(u2, axis));

                float Max(float a, float b, float c) {
                    return Math.Max(a, Math.Max(b, c));
                }
                float Min(float a, float b, float c) {
                    return Math.Min(a, Math.Min(b, c));
                }

                if (Math.Max(-Max(p0,p1,p2),Min(p0,p1,p2)  ) > r) {
                    return false;
                }
            }
            return true;

        }

        static bool TestCubeTriangleIntersection(Triangle triangle, Vector3 minima, Vector3 maxima)
        {
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
            for (int i = 0; i < 12; i++)
            {
                Vector3 dir = segments[i].B - segments[i].A;
                float denom = Vector3.Dot(dir, n);
                if (denom == 0) continue;
                float d = Vector3.Dot(triangle.A - segments[i].A, n) / denom;
                if (d < 0 || d > 1) continue;
                intersetcion.Add(segments[i].A + dir * d);
            }

            if (intersetcion.Count == 0) return false;
            Vector3[] polygon = intersetcion.ToArray();
            if (polygon.Length == 1)
            {
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
            for (int t = 0; t < 3; t++)
            {
                bool anyInside = false;
                Vector3 a, b;
                a = tri[t + 1] - tri[t];
                for (int i = 0; i < polygon.Length; i++)
                {
                    b = polygon[i] - tri[t];
                    if (insideSgn == Math.Sign(
                        GeometryUtil.GetCrossSignedMagnitude(a, b, n)
                        ))
                    {
                        anyInside = true;
                        break;
                    }
                }

                if (!anyInside) return false;
            }
            return true;
        }

        public static McStructure IntersectionTriangleObjToStruct(string filePath, Dimensions size, Block block, float scale) => IntersectionTriangleObjToStruct(
            filePath, size, new UVBlockPalette(new Block[,] { { block } }, 1, 1), scale);
        
        

        
        //FIXE: This do be too slow
        public static McStructure IntersectionTriangleObjToStruct(string filePath, Dimensions size, UVBlockPalette palette, float scale)
        {


            // Create a list to store the triangles
            List<Triangle> triangles = new List<Triangle>();
            List<Vector3> vertices = new List<Vector3>();
            List<Vector2> UVcoords = new List<Vector2>();
            float mx = float.PositiveInfinity;
            float my = float.PositiveInfinity;
            float mz = float.PositiveInfinity;
            // Read the file line by line
            foreach (string line in File.ReadLines(filePath))
            {
                string[] parts = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length == 0 || parts[0].StartsWith("#"))
                {
                    // Skip comments and empty lines
                    continue;
                }
                else if (parts[0] == "v")
                {
                    // Parse vertex positions
                    float x = float.Parse(parts[1]) * scale;
                    float y = float.Parse(parts[2]) * scale;
                    float z = float.Parse(parts[3]) * scale;
                    mx = float.Min(mx, x);
                    my = float.Min(my, y);
                    mz = float.Min(mz, z);

                    vertices.Add(new Vector3(x, y, z));
                }
                else if (parts[0] == "vt")
                {
                    float u = float.Parse(parts[1]);
                    float v;
                    if (parts.Length > 2)
                    {
                        v = float.Parse(parts[2]);
                    }
                    else
                    {
                        v = 0;
                    }
                    UVcoords.Add(new Vector2(u, v));
                }
                else if (parts[0] == "f")
                {
                    // Parse face indices
                    int[] indices = parts.Skip(1).Select(s => int.Parse(s.Split('/')[0])).ToArray();
                    Vector3 A = new Vector3(vertices[indices[0] - 1].X, vertices[indices[0] - 1].Y, vertices[indices[0] - 1].Z);
                    Vector3 B = new Vector3(vertices[indices[1] - 1].X, vertices[indices[1] - 1].Y, vertices[indices[1] - 1].Z);
                    Vector3 C = new Vector3(vertices[indices[2] - 1].X, vertices[indices[2] - 1].Y, vertices[indices[2] - 1].Z);
                    Triangle triangle = new Triangle(A, B, C);
                    if (parts[1].Split("/").Length > 1)
                    {
                        triangle.TextCoords = UVcoords[
                            int.Parse(parts[1].Split("/")[1])
                        - 1];


                    }
                    triangles.Add(triangle);
                }
            }

            Vector3 low = new Vector3(mx, my, mz);
            Vector3 lowTarget = new Vector3(0, 0, 0);
            Vector3 translation = lowTarget - low;
            for (int i = 0; i < triangles.Count; i++)
            {
                triangles[i] = new Triangle(
                    triangles[i].A + translation,
                    triangles[i].B + translation,
                    triangles[i].C + translation,
                    triangles[i].TextCoords
                );
            }
            return IntersectionTrianglesToStructure(triangles, size, palette);
        }


        public static McStructure IntersectionTrianglesToStructure(List<Triangle> triangles, Dimensions size, UVBlockPalette palette) {
            McStructure structure = new McStructure(size);
            Random rand = new Random();
            foreach (Triangle triangle in triangles) {
                Vector3 lo = triangle.low;
                Vector3 hi = triangle.hi;
                for (int x = int.Max(0, (int)lo.X - 2); x < int.Min(size.X, (int)hi.X + 2); x++) {
                    for (int y = int.Max(0, (int)lo.Y - 2); y < int.Min(size.Y, (int)hi.Y + 2); y++) {
                        for (int z = int.Max(0, (int)lo.Z - 2); z < int.Min(size.Z, (int)hi.Z + 2); z++) {
                            if (TestAABBTriangleIntersection(triangle, new Vector3(x, y, z)- Vector3.One / 2f, new Vector3(x, y, z) + Vector3.One/2f)) {
                                structure.SetBlock(x, y, z, palette[triangle.TextCoords.X, triangle.TextCoords.Y]);
                            }
                        }
                    }
                }
            }
            return structure;

        }

    }
}
