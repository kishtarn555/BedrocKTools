using BedrockTools.Geometry;
using BedrockTools.Objects;
using BedrockTools.Objects.Blocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BedrockTools.Structure.Features.Raster {
    public class BesenhamLineFeature : Feature {
        IntCoords A, B;
        Block block;
        public BesenhamLineFeature(Dimensions size, IntCoords A, IntCoords B, Block block) : base(size) {
            this.A = A;
            this.B = B;
            this.block = block;
        }


        public override IEnumerable<CoordBlockPair> AllBlocks() => BresenhamLine.GetPoints(A.X, A.Y, A.Z, B.X, B.Y, B.Z).Select(
            v => new CoordBlockPair(new IntCoords(v.x,v.y,v.z), block));
        public override Block GetBlock(int x, int y, int z) => block;
    }
}
