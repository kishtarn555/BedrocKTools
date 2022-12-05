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

        public static IntCoords operator + (IntCoords a, IntCoords b) {
            return new IntCoords(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }

        public bool InRegion(IntCoords origin, Dimensions size) {
            int nx = X - origin.X;
            int ny = Y - origin.Y;
            int nz = Z - origin.Z;
            if (nx < 0 || ny < 0 || nz < 0) {
                return false;
            }
            if (nx >= size.X || ny >= size.Y || nz >= size.Z ) {
                return false;
            }
            return true;
        }
    }
}
