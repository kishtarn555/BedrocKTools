using BedrockTools.Nbt.Elements;

namespace BedrockTools.Nbt.Extension {
    public interface INbtParsable {
        public NbtElement ToNbt();
    }
}
