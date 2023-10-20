using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BedrockTools.Geometry {
    public static class BresenhamLine {



        public static IEnumerable<(int x, int y)> GetPoints(int x0, int y0, int x1, int y1) {
            if (Math.Abs(y1 - y0) <= Math.Abs(x1 - x0)) {
                if (x0 > x1)
                    return plotLineLow(x1, y1, x0, y0).Reverse();
                else
                    return plotLineLow(x0, y0, x1, y1);
            }
            else {
                if (y0 > y1)
                    return plotLineHigh(x1, y1, x0, y0).Reverse();
                else
                    return plotLineHigh(x0, y0, x1, y1);
            
            }

        }

        public static IEnumerable<(int x, int y, int z)> GetPoints(int x0, int y0, int z0, int x1, int y1, int z1) {
            int wX = Math.Abs(x0- x1);
            int wY = Math.Abs(y0- y1);
            int wZ = Math.Abs(z0- z1);

            if (wX >= wY && wX >= wZ) {
                var xy = GetPoints(x0, y0, x1, y1);
                var xz = GetPoints(x0, z0, x1, z1);

                return xy.Zip(xz, (a, b) => (a.x, a.y, b.y));
            }
            if (wY >= wX && wY >= wZ) {
                var yx = GetPoints(y0, x0, y1, x1);
                var yz = GetPoints(y0, z0, y1, z1);

                return yx.Zip(yz, (a, b) => (a.y, a.x, b.y));
            }
            var zx = GetPoints(z0, x0, z1, x1);
            var zy = GetPoints(z0, y0, z1, y1);
            return zx.Zip(zy, (a, b) => (a.y, b.y, a.x));
            

        }


        static IEnumerable<(int x, int y)> plotLineLow(int x0, int y0, int x1, int y1) {
            int dx = x1 - x0;
            int dy = y1 - y0;
            int yi = 1;
            if (dy < 0) {
                yi = -1;
                dy = -dy;
            }
            int D = 2 * dy - dx;
            int y = y0;

            for (int x = x0; x <= x1; x++) {
                yield return(x, y);
                if (D > 0) {
                    y = y + yi;
                    D = D - 2 * dx;
                }
                D = D + 2 * dy;
            }
        }

        static IEnumerable<(int x, int y)> plotLineHigh(int x0, int y0, int x1, int y1) {
            int dx = x1 - x0;
            int dy = y1 - y0;
            int xi = 1;
            if (dx < 0) {
                xi = -1;
                dx = -dx;
            }
            int D = 2 * dx - dy;
            int x = x0;

            for (int y = y0; y <= y1; y++) {
                yield return (x, y);
                if (D > 0) {
                    x = x + xi;
                    D = D - 2 * dy;
                }
                D = D + 2 * dx;
            }
        }


    }
}
