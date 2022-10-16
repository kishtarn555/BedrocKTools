using System;
using BedrockTools.Nbt.Elements;
using BedrockTools.Nbt.Extension;
using System.Text;

namespace BedrockTools.Objects {
    public struct IntCoords :INbtParsable<NbtList> {
        public int X;
        public int Y;
        public int Z;

        public IntCoords(int x, int y, int z) {
            X = x;
            Y = y;
            Z = z;
        }

        public NbtList ToNbt() {
            return NbtList.FromInts(X, Y, Z);
        }

        public static IntCoords Zero => new IntCoords(0, 0, 0);
    }
}
