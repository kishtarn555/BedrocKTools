using BedrockTools.Objects;
using BedrockTools.Objects.Blocks;
using System;
using System.Collections.Generic;
using System.Numerics;
using BedrockTools.Structure.Advanced.Obj;
using BedrockTools.Geometry;
using BedrockTools.Geometry.Path;

namespace BedrockTools.Structure.Features.Geometry.Splines {
    public class FlatPathFeature : Feature {

        ParametricPath Path;
        ParametricPath Up;
        Vector3 Origin;
        float radious;
        float t_step;
        Block material;
        Block material2;
        public FlatPathFeature(Dimensions size, ParametricPath path, ParametricPath up, Vector3 origin, float radious, float t_step,Block material, Block material2) : base (size) { 
            this.Path = path;
            this.Up = up;
            this.Origin = origin;
            this.radious = radious;
            this.t_step = t_step;      
            this.material = material;
            this.material2 = material2;
        }

        // FIXME: This is slow!!!
        public override Block GetBlock(int x, int y, int z) {
            var coords = new IntCoords(x, y, z);
            foreach (CoordBlockPair pair in AllBlocks()) { 
                if (pair.Coords.Equals(coords)) {
                    return pair.Block;
                }
            }
            return null;

        }

        
        public override IEnumerable<CoordBlockPair> AllBlocks() {
            (Vector3 left, Vector3 point, Vector3 right) previous = (Vector3.Zero, Vector3.Zero, Vector3.Zero);
            List<Triangle> triangles = new List<Triangle>();
            bool first_step = true;
            for (float t =Path.Start; t < Path.End; t+= t_step) {
                Vector3 point = Path.GetPoint(t);
                Vector3 tangent = Vector3.Normalize(Path.GetDirection(t));
                Vector3 right = Vector3.Cross(tangent, Vector3.Normalize(Up.GetPoint(t)));
                (Vector3 left, Vector3 point, Vector3 right) cur = (
                    point - radious * right - Origin,
                    point- Origin,
                    point + radious * right - Origin
                );
                if (!first_step) {
                    Triangle A = new Triangle(previous.left, previous.point, cur.left, Vector2.Zero);
                    Triangle B = new Triangle(cur.left, cur.point, previous.point, Vector2.Zero);
                    triangles.Add(A);
                    triangles.Add(B);

                    Triangle C = new Triangle(previous.point, previous.right, cur.point, Vector2.One*0.9f);
                    Triangle D = new Triangle(cur.point, cur.right, previous.right, Vector2.One*0.9f);
                    triangles.Add(C);
                    triangles.Add(D);

                }
                first_step  = false;
                previous = cur;
            }
            Block[,] palette = new Block[1, 2] { {material, material2 } }; //FIXME
            McStructure structure = TrianglesFromObjParser.IntersectionTrianglesToStructure(triangles, Size, new UVBlockPalette(palette,1,2));
            StructureFeature realfeature = new StructureFeature(structure);
            return realfeature.AllBlocks();
        }

       

    }
}
