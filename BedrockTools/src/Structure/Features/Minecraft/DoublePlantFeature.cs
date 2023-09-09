using BedrockTools.Objects.Blocks;
using BedrockTools.Objects.Blocks.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BedrockTools.Structure.Features.Minecraft {
    public class DoublePlantFeature : Feature {
        public DoublePlantType PlantType { get; protected set; }
        public DoublePlantFeature(DoublePlantType plantType) : base(1,2,1) {
            PlantType = plantType;
        }

        public override Block GetBlock(int x, int y, int z) => VanillaBlockFactory.DoublePlant(PlantType,y==1);
    }
}
