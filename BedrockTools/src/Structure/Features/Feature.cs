using System.Collections.Generic;
using BedrockTools.Objects.Blocks;
using BedrockTools.Objects;

namespace BedrockTools.Structure.Features {
    public abstract class Feature {
        public Dimensions Size { get; protected set; }
        protected List<PairTransformFeature> SubFeatures;
        public Feature(int x,int y, int z) {
            Size = new Dimensions(x, y, z);
            SubFeatures = new List<PairTransformFeature>();
        }

        public Feature(Dimensions size) {
            Size = size;
            SubFeatures = new List<PairTransformFeature>();
        }
        public abstract Block GetBlock(int x, int y, int z);

        public Block GetBlock(IntCoords coords) => GetBlock(coords.X, coords.Y, coords.Z); 

        public void PlaceInStructure(McTransform transform, IMcStructure target) {
            for (int x = 0; x < Size.X; x++) {
                for (int y = 0; y < Size.Y; y++) {
                    for (int z = 0; z < Size.Z; z++) {
                        if (GetBlock(x, y, z) == null) continue;
                        target.SetBlock(
                            transform.GetCoords(Size,x,y,z),
                            GetBlock(x, y, z).Transform(transform)
                        );
                    }
                }
            }
            foreach (PairTransformFeature subFeature in SubFeatures) {
                subFeature.Feature.PlaceInStructure(McTransform.NestedCalculation(transform, Size, subFeature.Transform, subFeature.Feature.Size), target);
            }
        }

        public void AddSubfeature(int x, int y, int z, Feature feature) => SubFeatures.Add(new PairTransformFeature(x, y, z, feature));
        public void AddSubfeature(McTransform transform, Feature feature) => SubFeatures.Add(new PairTransformFeature(transform, feature));
    }
}
