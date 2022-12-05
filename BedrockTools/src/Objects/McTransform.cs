using System;
using System.Collections.Generic;
using System.Text;

namespace BedrockTools.Objects {
    public struct McTransform {
        public IntCoords Offset;
        public McRotation Rotation { get; set; }
        public bool FlipX { get; set; }
        public bool FlipZ { get; set; }

        public McTransform(IntCoords offset, McRotation rotation, bool flipX, bool flipZ) {
            Offset = offset;
            Rotation = rotation;
            FlipX = flipX;
            FlipZ = flipZ;
        }
        public McTransform(IntCoords offset) {
            Offset = offset;
            Rotation = McRotation.n0;
            FlipX = false;
            FlipZ= false;
        }

        public Dimensions GetDimensions(Dimensions original) {
            switch (Rotation) {
                case McRotation.n0:
                case McRotation.n180:
                    return original;
                case McRotation.n90:
                case McRotation.n270:
                    return new Dimensions(original.Z, original.Y, original.X);
                default:
                    return original;
            }
        } 
        //TODO: Support rotation
        public IntCoords GetCoords(Dimensions dim, int x, int y, int z) {
            Dimensions cur = GetDimensions(dim);
            if (Rotation == McRotation.n90 || Rotation== McRotation.n270) {
                int t = x;
                x = z;
                z = t;
            }
                    
            int nx = x;
            int ny = y;
            int nz = z;
            bool fx = FlipX;
            bool fz = FlipZ;

            DoFlipRotation(Rotation, ref fx, ref fz);
            if (fx) {
                nx = cur.X - nx - 1;
            }
            if (fz) {
                nz = cur.Z - nz - 1;
            }
            return new IntCoords(nx, ny, nz) + Offset;
        }
        public IntCoords GetCoords(Dimensions dim, IntCoords coords) => GetCoords(dim, coords.X, coords.Y, coords.Z);

        public McTransform Translate(int x, int y, int z) {
            Offset += new IntCoords(x, y, z);
            return this;
        }

        public McTransform Rotate(McRotation rotation) {
            int r = (int)rotation + (int)Rotation;
            r %= 4;
            Rotation = (McRotation)r;
            return this;
        }
        public McTransform MirrorX() {
            FlipX = !FlipX;
            return this;
        }
        public McTransform MirrorZ() {
            FlipZ = !FlipZ;
            return this;
        }
        /// <summary>
        /// Calculates the Inverse transform
        /// </summary>
        /// <returns>The inverse transform</returns>
        public McTransform GetInverse() {
            return new McTransform(
                new IntCoords(-Offset.X, -Offset.Y, -Offset.Z),
                (McRotation)((4-(int)Rotation)%4),
                FlipX,
                FlipZ
                ) ;
        }

        public static McTransform Identity => new McTransform(IntCoords.Zero, McRotation.n0, false, false);

        /// <summary>
        /// Calculates the transform after applying two of them
        /// </summary>
        /// <param name="first">First transform</param>
        /// <param name="second">Second transform</param>
        /// <returns>The result of both trasnforms</returns>
        public static McTransform Combine(McTransform first, McTransform second) {
            McTransform mc = new McTransform {
                Offset = first.Offset + second.Offset,
                Rotation = (McRotation)(((int)first.Rotation + (int)second.Rotation) % 4),
                FlipX = first.FlipX ^ second.FlipX,
                FlipZ = first.FlipZ ^ second.FlipZ
            };
            return mc;
        }
        /// <summary>
        /// Calculates the flip equivalent to a rotation and modifies flip x and flip z accordingly
        /// </summary>
        /// <param name="rotation">Which rotation we want</param>
        /// <param name="fx">flip x</param>
        /// <param name="fz">flip z</param>
        public static void DoFlipRotation(McRotation rotation, ref bool fx, ref bool fz) {
            if (rotation == McRotation.n270) 
                fz = !fz;
            else if (rotation == McRotation.n90) 
                fx = !fx;
            else if (rotation == McRotation.n180) {
                fx = !fx;
                fz = !fz;
            }
        }
        /// <summary>
        /// Calculates the global transform that needs to be applyied to an object that knows the local transform
        /// 
        /// </summary>
        /// <param name="parent">The transform of the parent object</param>
        /// <param name="parentDim">The size of the parent object</param>
        /// <param name="child">The local transform of the child object</param>
        /// <param name="childDim">The size of the child</param>
        /// <returns>The global tranform of the parent object</returns>
        public static McTransform NestedCalculation(McTransform parent, Dimensions parentDim, McTransform child, Dimensions childDim) {
            McTransform mc = new McTransform {
                Offset = parent.GetCoords(parentDim, child.Offset),
                Rotation = (McRotation)(((int)parent.Rotation + (int)child.Rotation) % 4),
                FlipX = parent.FlipX ^ child.FlipX,
                FlipZ = parent.FlipZ ^ child.FlipZ
            };
            int wx = mc.GetDimensions(childDim).X;
            int wz = mc.GetDimensions(childDim).Z;
            bool fx = parent.FlipX;
            bool fz = parent.FlipZ;
            DoFlipRotation(parent.Rotation, ref fx, ref fz);
            if (fx) {
                mc.Offset.X -= wx - 1;
            }
            if (fz) {
                mc.Offset.Z -= wz - 1;
            }
            return mc;
        }

        
    }
}
