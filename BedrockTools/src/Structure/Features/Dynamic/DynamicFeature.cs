using System.Collections.Generic;

using BedrockTools.Objects;
using BedrockTools.Objects.Blocks;

namespace BedrockTools.Structure.Features.Dynamic {
    public class DynamicFeature : Feature {
        Dictionary<IntCoords, Block> Cache;
        public DynamicFeature() :base(1,1,1) {
            Cache = new Dictionary<IntCoords, Block>();
        }

        public override IEnumerable<CoordBlockPair> AllBlocks() {
            foreach (var cache in Cache) {
                yield return new CoordBlockPair(cache.Key, cache.Value);
            }
        }


        public override Block GetBlock(int x, int y, int z) {
            IntCoords coords = new IntCoords(x, y, z);
            if (!Cache.ContainsKey(coords))
                return null;
            return Cache[coords];
        }
    }
}
