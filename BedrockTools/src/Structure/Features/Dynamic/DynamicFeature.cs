using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using BedrockTools.Objects;
using BedrockTools.Objects.Blocks;

namespace BedrockTools.Structure.Features.Dynamic {
    public abstract class DynamicFeature : Feature {
        protected Dictionary<IntCoords, Block> Cache;
        bool cached = false;
        IntCoords origin;
        public DynamicFeature() : base(1, 1, 1) {
            Cache = new Dictionary<IntCoords, Block>();
        }


        public void CalculateDimensions() {
            origin = Cache.Keys.Aggregate(IntCoords.Zero, (or, cur) => {
                return new IntCoords(int.Min(or.X, cur.X), int.Min(or.Y, cur.Y), int.Min(or.Z, cur.Z));
            });
            IntCoords temporal = Cache.Keys.Aggregate(IntCoords.Zero, (or, cur) => {
                return new IntCoords(int.Max(or.X, cur.X - origin.X + 1), int.Max(or.Y, cur.Y - origin.Y + 1), int.Max(or.Z, cur.Z - origin.Z + 1));
            });
            Size = new Dimensions(temporal.X, temporal.Y, temporal.Z);
        }

        public IntCoords GetOrigin() {
            if (!cached) {
                CalculateFeature();
            }
            return origin;
        }

        protected void BuildCache() {
            CalculateFeature();
            CalculateDimensions();
            cached = true;
        }

        protected abstract void CalculateFeature();

        public override IEnumerable<CoordBlockPair> AllBlocks() {

            if (!cached) {
                CalculateFeature();
            }
            foreach (var cache in Cache) {
                yield return new CoordBlockPair(cache.Key, cache.Value);
            }
        }


        public override Block GetBlock(int x, int y, int z) {
            if (!cached) {
                CalculateFeature();
            }
            IntCoords coords = new IntCoords(x, y, z);
            if (!Cache.ContainsKey(coords))
                return null;
            return Cache[coords];
        }
    }
}
