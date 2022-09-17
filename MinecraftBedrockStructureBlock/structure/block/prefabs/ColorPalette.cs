using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace MinecraftBedrockStructureBlock.structure.block.prefabs {
    class ColorPalette {
        static ColorPalette _instance = null;
        public static ColorPalette Instance {
            get {
                if (_instance==null) {
                    _instance = new ColorPalette();
                }
                return _instance;
            }
        }
        Dictionary<Color, Block> colorMap;
        public ColorPalette() {
            colorMap = new Dictionary<Color, Block>();
        }
    }
}
