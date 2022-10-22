using BedrockTools.Objects;

namespace BedrockTools.Structure.Features {
    public class PairTransformFeature {
        public McTransform Transform { get; set; }
        public Feature Feature { get; set; }

        public PairTransformFeature(McTransform transform, Feature feature) {
            Transform = transform;
            Feature = feature;
        }

        public PairTransformFeature(int x, int y, int z, Feature feature) {
            Transform = new McTransform(new IntCoords(x, y, z));
            Feature = feature;
        }
    }
}
