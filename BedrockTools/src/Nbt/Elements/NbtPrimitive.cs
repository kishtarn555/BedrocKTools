using System;

namespace BedrockTools.Nbt.Elements {
    public abstract class NbtPrimitive<T>: NbtElement, INbtValue {
        public abstract T Value { get; protected set; }

        object INbtValue.Value => this.Value;

        public NbtPrimitive(T value) {
            this.Value = value;
        }

        public override bool Equals(object obj) {
            if (obj is INbtValue prim) {
                return prim.Tag == this.Tag && this.Value.Equals(prim.Value);
            }
            return base.Equals(obj);
        }

        public override int GetHashCode() => HashCode.Combine(Tag, Value);
    }
}
