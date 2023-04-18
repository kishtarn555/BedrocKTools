using BedrockTools.Objects;
using BedrockTools.Objects.Blocks;
using System;
using System.Collections.Generic;
using System.Text;

namespace BedrockTools.Structure.Features.Geometry {
    public class SphereFeature : Analitical3DShape {
        public SphereFeature(Dimensions Size, FillMode fillMode, Block fillBlock) : base(Size, fillMode, fillBlock) {
            IsPointInsideRegion = TestRegion;
        }

        protected bool TestRegion(float a, float b, float c) {
            double x = a,
                y = b,
                z = c,
                w = Size.X,
                h = Size.Y,
                l = Size.Z;
            x = (x + 0.5 - w / 2.0) / (w / 2.0);
            y = (y + 0.5 - h / 2.0) / (h / 2.0);
            z = (z + 0.5 - l / 2.0) / (l / 2.0);

            return x * x + y * y + z * z <= 1.0;
        }
    }
}
