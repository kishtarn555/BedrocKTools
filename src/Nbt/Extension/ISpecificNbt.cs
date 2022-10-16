using System;
using BedrockTools.Nbt.Elements;

namespace BedrockTools.Nbt.Extension {
    interface INbtParsable<T> where T: NbtElement {
        public T ToNbt();
    }
}
