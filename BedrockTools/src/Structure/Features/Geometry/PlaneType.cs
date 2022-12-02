using System;
using System.Collections.Generic;
using System.Text;

namespace BedrockTools.Structure.Features.Geometry {
    public enum PlaneType {
        XY,
        XZ,
        YZ,
        YX,
        ZX,
        ZY
    }

    public static class PlaneTypeFunctionality {
        public static bool IsPointInPlane(this PlaneType plane, int x, int y, int z) {
            if (x != 0 && (plane == PlaneType.YZ || plane == PlaneType.ZY)) {
                return false;
            }
            if (y != 0 && (plane == PlaneType.XZ || plane == PlaneType.ZX)) {
                return false;
            }
            if (z != 0 && (plane == PlaneType.XY || plane == PlaneType.YX)) {
                return false;
            }
            return true;
        }
        public static (int, int) GetABCoords(this PlaneType plane, int x, int y, int z) {
            int a, b;
            switch (plane) {
                case PlaneType.XY:
                    a = x; b = y;
                    break;
                case PlaneType.XZ:
                    a = x; b = z;
                    break;
                case PlaneType.YZ:
                    a = y; b = z;
                    break;
                case PlaneType.YX:
                    a = y; b = x;
                    break;
                case PlaneType.ZX:
                    a = z; b = x;
                    break;
                case PlaneType.ZY:
                    a = z; b = y;
                    break;
                default:
                    throw new Exception("not a a supported plane" + plane);
            }
            return (a, b);
        }
    }
}
