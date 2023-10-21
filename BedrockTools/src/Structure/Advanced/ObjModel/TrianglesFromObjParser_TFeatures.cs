using BedrockTools.Geometry;
using BedrockTools.Objects;
using BedrockTools.Objects.Blocks;
using BedrockTools.Structure;
using BedrockTools.Structure.Features.Geometry;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;

namespace BedrockTools.Structure.Advanced.Obj
{
    public partial class TrianglesFromObjParser
    {

        public static List<Triangle3DFeature> ParseObjFileTo3DTriangles(IEnumerable<string> objLines, Dimensions size, FillMode fillMode, Block block, float width, float scale) {
            List<Triangle> triangles3D = GetTrianglesFromObj(objLines, scale);

            // Create a list to store the triangles
            List<Triangle3DFeature> triangles = triangles3D.Select(triangle => new Triangle3DFeature(
                size, fillMode, block, triangle.A, triangle.B, triangle.C, width
                )).ToList();



            return triangles;
        }

    }
}
