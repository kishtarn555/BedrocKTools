using System;
using System.Collections.Generic;
using System.Drawing;
using MinecraftBedrockStructureBlock.structure.block;
using MinecraftBedrockStructureBlock.structure;
using MinecraftBedrockStructureBlock.image.util;
namespace MinecraftBedrockStructureBlock.image {
    public class ImageStructureConverter {
        public enum DistanceMode {
            LabSolid,
            RGBEulerSolid,
            RGBredmean
        }
        static ColorDistanceCalculator GetCalculator(DistanceMode mode) {
            switch(mode) {
                case DistanceMode.LabSolid:
                    return new LabColorDistanceCalculator();
                case DistanceMode.RGBEulerSolid:
                    return new EulerColorDistanceCalculator();
                case DistanceMode.RGBredmean:
                    return new RedmeanColorDistanceCalculator();
                default:
                    throw new Exception();
            }
        }
        
        static double[,] getDitheringMatrix(int size) {
            if (size != (size & (-size))) {
                throw new ArgumentException() ;
            }
            double[,] m= new double[size, size];
            if (size == 2) {
                return new double[2, 2]{
                { 0,2},
                { 3,1}
                };
            }
            double[,] m2 = getDitheringMatrix(size / 2);
            int ss = size / 2;
            for (int i =0; i < ss; i++) {
                for (int j = 0; j < ss; j++) {
                    m[i, j] = size * size * m2[i, j];
                    m[i, j+ss] = size * size * m2[i, j]+2;
                    m[i+ss, j] = size * size * m2[i, j]+3;
                    m[i+ss, j+ss] = size * size * m2[i, j]+1;
                }
            }
            return m;
        }
        public static McStructure StructFromImage(string path, DistanceMode distanceMode=DistanceMode.LabSolid, int ditherSize=1, int upscale=1) {
            Bitmap image = new Bitmap(path);
            int width = image.Width;
            int height = image.Height;
            if (ditherSize == 1)
                return solid(image, width, height, GetCalculator(distanceMode), upscale);
            return dithering(image, width, height, ditherSize, GetCalculator(distanceMode), upscale);
            
        }

        static double ContrastFactor(Color a, Color b) {
            return 1.0;
            if (new LabColorDistanceCalculator().calcDistance(a, b) > 37)
                return 0.05;
            return 1.0;
        } 
        public static McStructure DiffDithering(string path, DistanceMode mode=DistanceMode.LabSolid, int ditherSize=2) {
            Bitmap image = new Bitmap(path);
            int width = image.Width;
            int height = image.Height;
            Color3D[,] img = new Color3D[width, height];
            Color[,] orig = new Color[width, height];
            for (int i =0; i < width; i++) {
                for (int j = 0; j < height; j++) {
                    img[i, j] = new Color3D(image.GetPixel(i, j));
                    orig[i, j] = image.GetPixel(i, j);
                }
            }
            McStructure mc = new McStructure(width, 1, height);
            for (int i =0; i < width; i++) {
                for (int j=0; j < height; j++) {
                    img[i, j].Clamp();  // FIXME: This is a fix, but it probably should be fixed by Garnut clamping.
                    (double distance, Block block) info= MapColorPalette.GetColorDistanceVector(img[i, j].getColor(), GetCalculator(mode)).GetSortedList()[0];
                    mc.setBlock(i, 0, j, info.block);
                    Color3D c = new Color3D(MapColorPalette.GetColor(info.block));
                    Color3D diff = img[i, j] - c;
                    if (j + 1 < width)
                        img[i, j + 1] = img[i, j + 1] + (diff * 7.0 / 16.0) * ContrastFactor(orig[i, j], orig[i, j + 1]);
                    if (i + 1 < height) {
                        if (j -1 >=0)
                            img[i + 1, j-1] = img[i + 1, j-1] + (diff * 3.0 / 16.0) * ContrastFactor(orig[i, j], orig[i+1, j - 1]);
                        img[i + 1, j ] = img[i + 1, j ] + (diff * 5.0 / 16.0) * ContrastFactor(orig[i, j], orig[i+1, j ]);
                        if (j + 1 < width)
                            img[i+1, j + 1] = img[i+1, j + 1] + (diff * 1.0 / 16.0) * ContrastFactor(orig[i, j], orig[i+1, j + 1]);
                    }

                }
            }
            return mc;
        }

        public static McStructure solid(Bitmap image, int width, int height, ColorDistanceCalculator calculator, int upscale) {
            McStructure result = new McStructure(width*upscale, 1, height*upscale);
            for (int i = 0; i < width*upscale; i++) {
                for (int j = 0; j < height*upscale; j++) {
                    result.setBlock(i, 0, j,
                        MapColorPalette.GetClosest(image.GetPixel(i/upscale, j/upscale), calculator)
                    );
                }
            }
            return result;
        }
        public static McStructure dithering(Bitmap image, int width, int height, int dotSize, ColorDistanceCalculator calculator, int upscale) {
            McStructure result = new McStructure(width*upscale, 1, height*upscale);
            double[,] m = getDitheringMatrix(dotSize);
            for (int i = 0; i < dotSize; i++) {
                for (int j = 0; j < dotSize; j++) {
                    Console.Write(m[i, j] + " ");
                    m[i, j] /= Math.Pow(2, dotSize);
                }
                Console.WriteLine();
            }
            for (int i = 0; i < width*upscale; i++) {
                for (int j = 0; j < height*upscale; j++) {
                    (double distace, Block block) a = MapColorPalette.GetColorDistanceVector(image.GetPixel(i/upscale, j/upscale), calculator).GetSortedList()[0];
                    (double distace, Block block) b = MapColorPalette.GetColorDistanceVector(image.GetPixel(i/upscale, j/upscale), calculator).GetSortedList()[1];
                    double lerp = a.distace / (a.distace + b.distace);
                    if (lerp <= m[i%dotSize, j%dotSize]) {
                        result.setBlock(i, 0, j, a.block);
                    }
                    else {
                        result.setBlock(i, 0, j, b.block);
                    }
                }
            }
            return result;      
            
        }

        public static McStructure DiffusionDithering(Bitmap image, int width, int height, int dotSize, ColorDistanceCalculator calculator, int upscale) {
            McStructure result = new McStructure(width, 1, height);
            double[,] m = getDitheringMatrix(dotSize);
            for (int i = 0; i < dotSize; i++) {
                for (int j = 0; j < dotSize; j++) {
                    Console.Write(m[i, j] + " ");
                    m[i, j] /= Math.Pow(2, dotSize);
                }
                Console.WriteLine();
            }
            for (int i = 0; i < width; i++) {
                for (int j = 0; j < height; j++) {
                    (double distace, Block block) a = MapColorPalette.GetColorDistanceVector(image.GetPixel(i, j), calculator).GetSortedList()[0];
                    (double distace, Block block) b = MapColorPalette.GetColorDistanceVector(image.GetPixel(i, j), calculator).GetSortedList()[1];
                    double lerp = a.distace / (a.distace + b.distace);
                    if (lerp <= m[i % dotSize, j % dotSize]) {
                        result.setBlock(i, 0, j, a.block);
                    }
                    else {
                        result.setBlock(i, 0, j, b.block);
                    }
                }
            }
            return result;

        }
    }
}
