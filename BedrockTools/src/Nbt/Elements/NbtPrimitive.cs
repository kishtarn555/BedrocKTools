namespace BedrockTools.Nbt.Elements {
    public abstract class NbtPrimitive<T>: NbtElement, INbtValue {
        public abstract T Value { get; protected set; }

        object INbtValue.Value => this.Value;

        public NbtPrimitive(T value) {
            this.Value = value;
        }



    }
}
