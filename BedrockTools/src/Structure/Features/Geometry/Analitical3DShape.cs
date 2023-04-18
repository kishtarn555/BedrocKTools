using BedrockTools.Objects;
using BedrockTools.Objects.Blocks;
using System;
using System.Linq;
using System.Numerics;

namespace BedrockTools.Structure.Features.Geometry {
    public class Analitical3DShape : Feature{
        public FillMode FillingMode { get; }
        public Block FillingBlock { get; }
      
        protected Func<float, float, float, bool> IsPointInsideRegion;

        protected Matrix4x4 transformation;

        public Analitical3DShape(Dimensions Size, FillMode fillMode, Block fillBlock, Func<float, float, float, bool> regionTestFunction) : base(Size) {
            FillingMode = fillMode;
            FillingBlock = fillBlock;
            IsPointInsideRegion = regionTestFunction;
            transformation = Matrix4x4.Identity;
        }
        protected Analitical3DShape(Dimensions Size, FillMode fillMode, Block fillBlock) : base(Size) {
            FillingMode = fillMode;
            FillingBlock = fillBlock;
            transformation = Matrix4x4.Identity;
        }

        public void SetTransformation (Matrix4x4 transformation) {
            this.transformation = transformation;
        }

        private bool LocalTest(float x, float y, float z) {
            var res = Vector4.Transform(new Vector4(x, y, z, 1), transformation);

            return IsPointInsideRegion(res.X, res.Y, res.Z);
            
        }

        public override Block GetBlock(int x, int y, int z) {

            
            if (!LocalTest(x, y, z) ) {
                return null;
            }
            if (FillingMode == FillMode.Solid)
                return FillingBlock;

            bool AreAllOrthogonalInside = (
                LocalTest(x - 1, y, z)
                && LocalTest(x + 1, y, z)
                && LocalTest(x ,y - 1, z)
                && LocalTest(x, y + 1, z)
                && LocalTest(x, y, z - 1)
                && LocalTest(x, y, z + 1)
            );
            if (!AreAllOrthogonalInside) {
                return FillingBlock;
            }
            if (FillingMode == FillMode.BorderThin) {
                return null;
            }
            bool AreAllDiagonalInside = (
                LocalTest(x - 1, y - 1, z -1)
                && LocalTest(x - 1, y - 1, z + 1)
                && LocalTest(x - 1, y + 1, z - 1)
                && LocalTest(x - 1, y + 1, z + 1)
                && LocalTest(x + 1, y - 1, z - 1)
                && LocalTest(x + 1, y - 1, z + 1)
                && LocalTest(x + 1, y + 1, z - 1)
                && LocalTest(x + 1, y + 1, z + 1)
            );
            if (!AreAllDiagonalInside) {
                return FillingBlock;
            }
            return null;


        }
    }
}
