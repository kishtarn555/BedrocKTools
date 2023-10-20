using BedrockTools.Geometry.Path;
using BedrockTools.Geometry;
using BedrockTools.Objects.Blocks;
using BedrockTools.Objects;
using BedrockTools.Structure.Advanced.Obj;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BedrockTools.Structure.Features.Geometry.Splines {
    public class PathBlockAdderFeature : Feature{
        ParametricPath Path;
        Vector3 Origin;
        float t_step;
        Block material;
        public PathBlockAdderFeature(Dimensions size, ParametricPath path, Vector3 origin,  float t_step, Block material) : base(size) {
            this.Path = path;
            this.Origin = origin;
            this.t_step = t_step;
            this.material = material;
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
            IntCoords previous = new IntCoords(0, 0, 0);
            bool first=true;
            for (float t = Path.Start; t < Path.End; t+= t_step) {
                Vector3 point = Path.GetPoint(t)-Origin;
                IntCoords coords = new IntCoords(
                    (int)MathF.Round(point.X),
                    (int)MathF.Round(point.Y),
                    (int)MathF.Round(point.Z)
                );
                if (!coords.InRegion(IntCoords.Zero, Size)) continue;
                if (!coords.Equals(previous) || first) {
                    yield return new CoordBlockPair(coords, material);
                }
                first = false;
                previous = coords;
            }
        }
    }
}
