using System;
using System.IO;
//using MinecraftBedrockStructureBlock.structure;
//using MinecraftBedrockStructureBlock.types;
//using MinecraftBedrockStructureBlock.image;
using BedrockTools.Objects;
using BedrockTools.Structure;
using BedrockTools.Nbt.IO;
using BedrockTools.Structure.Features.Util;
using BedrockTools.Structure.Features;
using System.Numerics;
using BedrockTools.Structure.Features.Geometry.Splines;
using System.Linq;
using System.Drawing;
using System.Threading;
using System.ComponentModel;
using BedrockTools.Objects.Blocks;
using BedrockTools.Geometry.Path;
using BedrockTools.Structure.Features.Geometry;
using BedrockTools.Nbt.Elements;

namespace TestConsole {
    class Program {

        static McStructure AxisTest() {
            McStructure mcstructure = new McStructure(new Dimensions(20, 25, 20));
            AxisFeature ax1 = new AxisFeature(8, 10, 10);
            AxisFeature ax2 = new AxisFeature(3, 2, 5);
            ax1.AddSubfeature(McTransform.Identity.Translate(3, 6, 1).Rotate(McRotation.n90), ax2);

            ax1.PlaceInStructure(McTransform.Identity.Translate(1, 2, 3).Rotate(McRotation.n90), mcstructure);
            return mcstructure;
        }
        
        public static McStructure SplineTest1() {
            McStructure mcstructure = new McStructure(new Dimensions(100, 100, 100));
            var points = Enumerable.Range(0, 100 + 1).Select(x => {
                float t = x;
                t /= 100f;
                float theta = t * 6 * 3.1415f;
                float r = (1f - MathF.Abs((t - 0.5f) * 2f));
                var result = new Vector3(50, t * 100, 50) + 40f* r * new Vector3(MathF.Cos(theta), 0, MathF.Sin(theta));
                Console.WriteLine(result);
                return result;
            }).ToArray();
            FlatPathFeature spline = new FlatPathFeature(
                size:    new Dimensions(100, 100, 100),
                path: new CatmullRomSpline(points),
                up: new CatmullRomSpline(points),
                origin:  Vector3.Zero,
                radious: 2,
                t_step:  0.01f,
                material: VanillaBlockFactory.BrickBlock(),
                material2: VanillaBlockFactory.BrickBlock()
            );

            spline.PlaceInStructure(McTransform.Identity, mcstructure);
            //spline2.PlaceInStructure(McTransform.Identity, mcstructure);
            return mcstructure;
        }


        public static McStructure StreetTest() {
            McStructure mcstructure = new McStructure(new Dimensions(200, 10, 200));
            var points = new Vector3[]
            {

                new Vector3(50f, 0, 250f),
                new Vector3(50f, 0, 200f),
                new Vector3(50f, 0, 150f),
                new Vector3(50f, 0, 100f),
                new Vector3(63.0f, 0, 63),
                new Vector3(100f, 0, 50f),
                new Vector3(150f, 0, 50f),
                new Vector3(200f, 0, 50f),
                new Vector3(250f, 0, 50f),
            };
            var splines = new (Block mat, Block mat2, float rad)[] {
                (VanillaBlockFactory.SmoothStone(),VanillaBlockFactory.SmoothStone(), 10.99f),
                (VanillaBlockFactory.Stone(BedrockTools.Objects.Blocks.Minecraft.StoneBlock.StoneType.Stone),VanillaBlockFactory.Stone(BedrockTools.Objects.Blocks.Minecraft.StoneBlock.StoneType.Stone), 6.2f),
                (VanillaBlockFactory.Grass(),VanillaBlockFactory.Grass(), 0.1f),
            }.Select(x =>
                new FlatPathFeature(
                size: new Dimensions(200, 10, 200),
                path: new CatmullRomSpline(points),
                up: new ConstantPath(Vector3.UnitY, 0, 1e9f),
                origin: Vector3.Zero,
                radious: x.rad,
                t_step: 0.01f,
                material: x.mat,
                material2: x.mat2
            ));
            foreach (var spline in splines) {
                spline.PlaceInStructure(McTransform.Identity, mcstructure);
            }
            //spline2.PlaceInStructure(McTransform.Identity, mcstructure);
            return mcstructure;
        }


        public static McStructure SinglePathTest() {
            McStructure mcstructure = new McStructure(new Dimensions(100, 100, 100));
            var points = Enumerable.Range(0, 100 + 1).Select(x => {
                float t = x;
                t /= 100f;
                float theta = t * 6 * 3.1415f;
                float r = (1f - MathF.Abs((t - 0.5f) * 2f));
                var result = new Vector3(50, t * 100, 50) + 40f * r * new Vector3(MathF.Cos(theta), 0, MathF.Sin(theta));
                Console.WriteLine(result);
                return result;
            }).ToArray();
            PathBlockAdderFeature spline = new PathBlockAdderFeature(
                size: new Dimensions(100, 100, 100),
                path: new CatmullRomSpline(points),
                origin: Vector3.Zero,
                t_step: 0.01f,
                material: VanillaBlockFactory.BrickBlock()
            );

            spline.PlaceInStructure(McTransform.Identity, mcstructure);
            //spline2.PlaceInStructure(McTransform.Identity, mcstructure);
            return mcstructure;
        }

        public static McStructure StreetTest2() {
            McStructure mcstructure = new McStructure(new Dimensions(200, 10, 200));
            var points = new Vector3[]
            {

                new Vector3(50.5f,  0, 250.5f),
                new Vector3(50.5f,  0, 200.5f),
                new Vector3(50.5f,  0, 150.5f),
                new Vector3(50.5f,  0, 100.5f),
                new Vector3(63.5f,  0, 63.5f),
                new Vector3(100.5f, 0, 50.5f),
                new Vector3(150.5f, 0, 50.5f),
                new Vector3(200.5f, 0, 50.5f),
                new Vector3(250.5f, 0, 50.5f),
            };
            var splines = new (Block mat, Block mat2, float rad)[] {
                (VanillaBlockFactory.SmoothStone(),VanillaBlockFactory.SmoothStone(), 11.9f),
                (VanillaBlockFactory.Stone(BedrockTools.Objects.Blocks.Minecraft.StoneBlock.StoneType.Stone),VanillaBlockFactory.Stone(BedrockTools.Objects.Blocks.Minecraft.StoneBlock.StoneType.Stone), 6.9f),
                (VanillaBlockFactory.Grass(),VanillaBlockFactory.Grass(), 0.4f),
            }.SelectMany(x => {
                var catmull = new CatmullRomSpline(points);
                var left = new ParametricFunction(catmull.Start, catmull.End,
                    (float t) => Vector3.Normalize(Vector3.Cross(catmull.GetDirection(t), Vector3.UnitY))*x.rad+catmull.GetPoint(t),
                    catmull.GetDirection
                    );
                var right = new ParametricFunction(catmull.Start, catmull.End,
                    (float t) => -Vector3.Normalize(Vector3.Cross(catmull.GetDirection(t), Vector3.UnitY)) * x.rad + catmull.GetPoint(t),
                    catmull.GetDirection
                    );
                if (x.rad < 1f) {
                    return new Feature[] {
                        new PathBlockAdderFeature(
                            size: new Dimensions(200, 10, 200),
                            path: left,
                            origin: Vector3.Zero,
                            t_step: 0.01f,
                            material: x.mat
                        ),
                        new PathBlockAdderFeature(
                            size: new Dimensions(200, 10, 200),
                            path: right,
                            origin: Vector3.Zero,
                            t_step: 0.01f,
                            material: x.mat
                        ),
                    };
                } else {
                    return new Feature[]
                    {
                        new FlatPathFeature(
                            size: new Dimensions(210, 10, 110),
                            path: catmull,
                            up: new ConstantPath(Vector3.UnitY, 0, 1e9f),
                            origin: Vector3.Zero,
                            radious: x.rad,
                            t_step:0.01f,
                            material: x.mat,
                            material2: x.mat


                            )
                    };
                }
            });
        
            foreach (var spline in splines) {
                spline.PlaceInStructure(McTransform.Identity, mcstructure);
            }
            return mcstructure;
        }


        static (McStructure,McStructure) DeserializerTest() {
            McStructure start = new McStructure(new Dimensions(50, 50, 50));
            SphereFeature sphere = new SphereFeature(start.Size, FillMode.Solid, VanillaBlockFactory.Purpur());
            sphere.PlaceInStructure(McTransform.Identity, start);

            NbtCompound NBTStructure = new McStructureSerializer(start).GetStructureAsNbt();

            McStructure end = new McStructureDeserializer(NBTStructure).Deserialize();

            return (start,end);
        }


        static void WriteStructureTest(McStructure mcstructure, string name) {
            //BinaryWriter bw = new BinaryWriter(new FileStream(
            //   Path.Combine(MojangCom, @"development_behavior_packs\moveit\structures\moveit\hf.mcstructure"), FileMode.Create));
            string append = DateTime.Now.ToString("yyyy MM dd HH mm");

            BinaryWriter bw = new BinaryWriter(new FileStream(
               "C:\\Users\\hecto\\source\\repos\\MC\\"+name + append + ".mcstructure", FileMode.Create));
            McStructureSerializer serializer = new McStructureSerializer(mcstructure);
            new NbtBinaryWriter(bw).WriteRoot(serializer.GetStructureAsNbt());
            bw.Close();
        }

        static void Main(string[] args) {
            //McStructure mcstructure = CityCrafter.StreetTest(
            //    new Vector3(5200, -35, 5460),
            //    CityCrafter.RiverMarketSide
            //    );

            //string MojangCom =
            //    Path.Combine(
            //        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            //        @"Packages\Microsoft.MinecraftUWP_8wekyb3d8bbwe\LocalState\games\com.mojang"
            //        );
            ////BinaryWriter bw = new BinaryWriter(new FileStream(
            ////   Path.Combine(MojangCom, @"development_behavior_packs\moveit\structures\moveit\hf.mcstructure"), FileMode.Create));
            //string append = DateTime.Now.ToString("yyyy MM dd HH mm");

            //BinaryWriter bw = new BinaryWriter(new FileStream(
            //   "C:\\Users\\hecto\\source\\repos\\MC\\deserialize" + append + ".mcstructure", FileMode.Create));
            //McStructureSerializer serializer = new McStructureSerializer(mcstructure);
            //new NbtBinaryWriter(bw).WriteRoot(serializer.GetStructureAsNbt());
            //bw.Close();
            (McStructure sta,McStructure stb) = DeserializerTest();
            WriteStructureTest(sta, "start");
            WriteStructureTest(stb, "end");
            return;
        }
    }
}
