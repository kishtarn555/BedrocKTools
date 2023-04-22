using BedrockTools.Objects.Blocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BedrockTools.Structure.Advanced {
    public class BlockPaletteDictionary : IBlockPalette {
        IDictionary<int, Block> palette;
        Block missingBlock;
        public Block this[int index]  {
            get{
                if (palette.ContainsKey(index))
                    return palette[index];
                return missingBlock;
            }
        }

        public BlockPaletteDictionary(IDictionary<int, Block> palette) {
            this.palette = palette;
            missingBlock = VanillaBlockFactory.Purpur();
        }
        public BlockPaletteDictionary(IDictionary<int, Block> palette, Block missingBlock) {
            this.palette = palette;
            this.missingBlock = missingBlock;
        }

    }
}
