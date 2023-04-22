using BedrockTools.Objects.Blocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BedrockTools.Structure.Advanced.Obj
{
    public class UVBlockPalette
    {
        Block[,] palette;
        float W, H;

        public UVBlockPalette(Block[,] palette, int W, int H)
        {
            this.palette = palette;
            this.W = W;
            this.H = H;
        }

        public Block this[float u, float v]
        {
            get
            {
                u = u - (float)Math.Truncate(u);
                v = v - (float)Math.Truncate(v);
                u *= W;
                v *= H;
                return palette[(int)u, (int)v];
            }
        }
    }
}
