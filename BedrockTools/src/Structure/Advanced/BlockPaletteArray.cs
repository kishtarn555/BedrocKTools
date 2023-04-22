using BedrockTools.Objects.Blocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BedrockTools.Structure.Advanced {
    public class BlockPaletteArray : IBlockPalette {
        Block[] palette;

        public Block this[int index] => palette[index % palette.Length];

        public BlockPaletteArray(Block[] palette) {
            this.palette = palette;
        }

    }
}
