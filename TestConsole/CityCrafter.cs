using BedrockTools.Geometry.Path;
using BedrockTools.Objects.Blocks;
using BedrockTools.Objects;
using BedrockTools.Structure.Features.Geometry.Splines;
using BedrockTools.Structure.Features;
using BedrockTools.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.ExceptionServices;

namespace TestConsole {
    public static class CityCrafter {
        public static Vector3[] StreetPoints = new Vector3[]
        {
            new Vector3(3496, -30, 5386) + new Vector3(0.5f, 0f, 0.5f),
            new Vector3(3412, -30, 5318) + new Vector3(0.5f, 0f, 0.5f),
            new Vector3(3351, -30, 5256) + new Vector3(0.5f, 0f, 0.5f),
            new Vector3(3174, -29, 5011) + new Vector3(0.5f, 0f, 0.5f),
            new Vector3(3014, -28, 4730) + new Vector3(0.5f, 0f, 0.5f),
            new Vector3(2944, -29, 4529) + new Vector3(0.5f, 0f, 0.5f),
            new Vector3(2928, -30, 4426) + new Vector3(0.5f, 0f, 0.5f),
            new Vector3(2928, -31, 4286) + new Vector3(0.5f, 0f, 0.5f),
            new Vector3(2928, -33, 4140) + new Vector3(0.5f, 0f, 0.5f),

        };


        public static Vector3[] RiverMarketSide = new Vector3[]
        {
            new Vector3(3174, -29, 5011) + new Vector3(0.5f, 0f, 0.5f),
            new Vector3(3351, -30, 5256) + new Vector3(0.5f, 0f, 0.5f),
            new Vector3(3412, -30, 5318) + new Vector3(0.5f, 0f, 0.5f),
            new Vector3(3443, -30, 5344) + new Vector3(0.5f, 0f, 0.5f),
            new Vector3(3547, -30, 5411) + new Vector3(0.5f, 0f, 0.5f),
            new Vector3(3639, -27, 5428) + new Vector3(0.5f, 0f, 0.5f),
            new Vector3(3745, -28, 5434) + new Vector3(0.5f, 0f, 0.5f),
            new Vector3(3786, -30, 5435) + new Vector3(0.5f, 0f, 0.5f),
            new Vector3(3866, -29, 5439) + new Vector3(0.5f, 0f, 0.5f),
            new Vector3(4023, -27, 5440) + new Vector3(0.5f, 0f, 0.5f),
            new Vector3(4122, -27, 5429) + new Vector3(0.5f, 0f, 0.5f),
            new Vector3(4170, -30, 5379) + new Vector3(0.5f, 0f, 0.5f),
            new Vector3(4314, -34, 5370) + new Vector3(0.5f, 0f, 0.5f),
            new Vector3(4397, -33, 5352) + new Vector3(0.5f, 0f, 0.5f),
            new Vector3(4578, -33, 5313) + new Vector3(0.5f, 0f, 0.5f),
            new Vector3(4718, -32, 5335) + new Vector3(0.5f, 0f, 0.5f),
            new Vector3(4928, -31, 5373) + new Vector3(0.5f, 0f, 0.5f),
            new Vector3(5059, -33, 5429) + new Vector3(0.5f, 0f, 0.5f),
            new Vector3(5216, -33, 5487) + new Vector3(0.5f, 0f, 0.5f),
            new Vector3(5278, -33, 5516) + new Vector3(0.5f, 0f, 0.5f),
            
            
            
            
            //new Vector3(4308, -30, 5411) + new Vector3(0.5f, 0f, 0.5f),
            //new Vector3(4308, -30, 5411) + new Vector3(0.5f, 0f, 0.5f),
            //new Vector3(4470, -29, 5381) + new Vector3(0.5f, 0f, 0.5f),
            //new Vector3(4642, -29, 5359) + new Vector3(0.5f, 0f, 0.5f),
            //new Vector3(4785, -30, 5384) + new Vector3(0.5f, 0f, 0.5f),
            //new Vector3(4943, -32, 5416) + new Vector3(0.5f, 0f, 0.5f),
            //new Vector3(5117, -31, 5492) + new Vector3(0.5f, 0f, 0.5f),
            //new Vector3(5171, -31, 5522) + new Vector3(0.5f, 0f, 0.5f),
            //new Vector3(5171, -31, 5522) + new Vector3(0.5f, 0f, 0.5f),
            //new Vector3(5293, -28, 5657) + new Vector3(0.5f, 0f, 0.5f),
            //new Vector3(5478, -30, 5957) + new Vector3(0.5f, 0f, 0.5f),

        };



        public static McStructure StreetTest(Vector3 Origin, Vector3[] StreetPoints) {
            McStructure mcstructure = new McStructure(new Dimensions(210, 18, 200));
            var points = StreetPoints;
            var splines = new (Block mat, Block mat2, float rad)[] {
                    (VanillaBlockFactory.SmoothStone(),VanillaBlockFactory.SmoothStone(), 11.9f),
                    (VanillaBlockFactory.Stone(BedrockTools.Objects.Blocks.Minecraft.StoneBlock.StoneType.Stone),VanillaBlockFactory.Stone(BedrockTools.Objects.Blocks.Minecraft.StoneBlock.StoneType.Stone), 6.9f),
                    (VanillaBlockFactory.Grass(),VanillaBlockFactory.Grass(), 0.4f),
            }.SelectMany(x => {
                var catmull = new CatmullRomSpline(points);
                var left = new ParametricFunction(catmull.Start, catmull.End,
                    (float t) => Vector3.Normalize(Vector3.Cross(catmull.GetDirection(t), Vector3.UnitY)) * x.rad + catmull.GetPoint(t),
                    catmull.GetDirection
                    );
                var right = new ParametricFunction(catmull.Start, catmull.End,
                    (float t) => -Vector3.Normalize(Vector3.Cross(catmull.GetDirection(t), Vector3.UnitY)) * x.rad + catmull.GetPoint(t),
                    catmull.GetDirection
                    );
                if (x.rad < 1f) {
                    return new Feature[] {
                        new PathBlockAdderFeature(
                            size: mcstructure.Size,
                            path: left,
                            origin: Origin,
                            t_step: 0.001f,
                            material: x.mat
                        ),
                        new PathBlockAdderFeature(
                            size: mcstructure.Size,
                            path: right,
                            origin: Origin,
                            t_step: 0.001f,
                            material: x.mat
                        ),
                    };
                }
                else {
                    return new Feature[]
                    {
                        new FlatPathFeature(
                            size: mcstructure.Size,
                            path: catmull,
                            up: new ConstantPath(Vector3.UnitY, 0, 1e9f),
                            origin: Origin,
                            radious: x.rad,
                            t_step:0.001f,
                            material: x.mat,
                            material2: x.mat


                            )
                    };
                }
            });

            foreach (var spline in splines) {
                spline.PlaceInStructure(McTransform.Identity, mcstructure);
            }

            for (int x =0; x < mcstructure.Size.X; x++) {
                for (int z =0; z < mcstructure.Size.Z; z++) {
                    bool hasFloor = false;
                    for (int y =0; y < mcstructure.Size.Y; y++) {
                        if (hasFloor) {
                            mcstructure.SetBlock(x, y, z, VanillaBlockFactory.Air());
                        }
                        if (mcstructure.GetBlock(x,y,z)!=null) {
                            hasFloor = true;
                        }
                    }
                }
            }
            return mcstructure;
        }
    }
    
}
