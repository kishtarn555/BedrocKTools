namespace BedrockTools.Nbt.Elements {
    public abstract class NbtPrimitive<T>: NbtElement {
        public abstract T Value { get; protected set; }

        public NbtPrimitive(T value) {
            this.Value = value;
        }
    }
}
