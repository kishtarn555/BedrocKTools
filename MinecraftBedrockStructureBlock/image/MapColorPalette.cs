using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using MinecraftBedrockStructureBlock.structure.block;
using MinecraftBedrockStructureBlock.structure.block.prefabs;

namespace MinecraftBedrockStructureBlock.image {
    class MapColorPalette {
        static MapColorPalette _instance = null;
        public static MapColorPalette Instance {
            get {
                if (_instance==null) {
                    _instance = new MapColorPalette();
                }
                return _instance;
            }
        }
        Dictionary<Color, Block> colorMap;

        public static Block GetClosest(Color target, ColorDistanceCalculator distancer) {
            KeyValuePair<double, Block> res = new KeyValuePair<double, Block>(1e9, null);
            foreach (KeyValuePair<Color, Block> kv in Instance.colorMap) {

                Color col = kv.Key;
                double distance = distancer.calcDistance(col, target);
                
                if (distance < res.Key) { 
                    res = new KeyValuePair<double, Block>(distance, kv.Value);
                }
            }
            return res.Value;
        }

        public static ColorDistanceVector GetColorDistanceVector(Color target, ColorDistanceCalculator distancer) {
            ColorDistanceVector result = new ColorDistanceVector();
            foreach (KeyValuePair<Color, Block> kv in Instance.colorMap) {
                Color col = kv.Key;
                result.Add(distancer.calcDistance(col, target), kv.Value);
            }
            return result;
        }
        public static Color GetColor(Block block) { 
            foreach( KeyValuePair<Color, Block> kv in Instance.colorMap) {
                if (kv.Value.Equals(block)) {
                    return kv.Key;
                }
            }
            throw new Exception();
        }
        public MapColorPalette() {
            colorMap = new Dictionary<Color, Block>();
            colorMap[ColorTranslator.FromHtml("#B5B5B5")] = MinecraftPrefabs.Concrete.White;
            colorMap[ColorTranslator.FromHtml("#6D6D6D")] = MinecraftPrefabs.Concrete.LightGray;
            colorMap[ColorTranslator.FromHtml("#292929")] = MinecraftPrefabs.Concrete.Gray;
            colorMap[ColorTranslator.FromHtml("#0D0D0D")] = MinecraftPrefabs.Concrete.Black;
            colorMap[ColorTranslator.FromHtml("#A2A224")] = MinecraftPrefabs.Concrete.Yellow;
            colorMap[ColorTranslator.FromHtml("#74441B")] = MinecraftPrefabs.Concrete.Orange;
            colorMap[ColorTranslator.FromHtml("#521B1B")] = MinecraftPrefabs.Concrete.Red;
            colorMap[ColorTranslator.FromHtml("#37291B")] = MinecraftPrefabs.Concrete.Brown;
            colorMap[ColorTranslator.FromHtml("#446E0D")] = MinecraftPrefabs.Concrete.Lime;
            colorMap[ColorTranslator.FromHtml("#37441B")] = MinecraftPrefabs.Concrete.Green;
            colorMap[ColorTranslator.FromHtml("#294452")] = MinecraftPrefabs.Concrete.Cyan;
            colorMap[ColorTranslator.FromHtml("#496D99")] = MinecraftPrefabs.Concrete.LightBlue;
            colorMap[ColorTranslator.FromHtml("#1B2960")] = MinecraftPrefabs.Concrete.Blue;
            colorMap[ColorTranslator.FromHtml("#442260")] = MinecraftPrefabs.Concrete.Purple;
            colorMap[ColorTranslator.FromHtml("#602974")] = MinecraftPrefabs.Concrete.Magenta;
            colorMap[ColorTranslator.FromHtml("#AC5A75")] = MinecraftPrefabs.Concrete.Pink;
            colorMap[ColorTranslator.FromHtml("#513B29")] = MinecraftPrefabs.Dirt.Normal;
            colorMap[ColorTranslator.FromHtml("#655433")] = MinecraftPrefabs.Planks.Oak;
            colorMap[ColorTranslator.FromHtml("#5C3D23")] = MinecraftPrefabs.Planks.Spruce;
            colorMap[ColorTranslator.FromHtml("#6B4E36")] = MinecraftPrefabs.Planks.Jungle;
            colorMap[ColorTranslator.FromHtml("#292929")] = MinecraftPrefabs.Planks.Acacia;
            colorMap[ColorTranslator.FromHtml("#37291B")] = MinecraftPrefabs.Planks.DarkOark;
            colorMap[ColorTranslator.FromHtml("#4F2234")] = MinecraftPrefabs.Planks.Crimson;
            colorMap[ColorTranslator.FromHtml("#1F4C4B")] = MinecraftPrefabs.Planks.Warped;
            colorMap[ColorTranslator.FromHtml("#745E4F")] = MinecraftPrefabs.RawIronBlock;
            colorMap[ColorTranslator.FromHtml("#67863E")] = MinecraftPrefabs.Grass;
            colorMap[ColorTranslator.FromHtml("#B0A673")] = MinecraftPrefabs.EndStone;
            colorMap[ColorTranslator.FromHtml("#4F4F4F")] = MinecraftPrefabs.Stone.Normal;
            colorMap[ColorTranslator.FromHtml("#4F4F4F")] = MinecraftPrefabs.Stone.Granite;
            colorMap[ColorTranslator.FromHtml("#747782")] = MinecraftPrefabs.Clay;
            colorMap[ColorTranslator.FromHtml("#7272B5")] = MinecraftPrefabs.Ice.Packed;
            colorMap[ColorTranslator.FromHtml("#B50000")] = MinecraftPrefabs.RedstoneBlock;
            colorMap[ColorTranslator.FromHtml("#419B97")] = MinecraftPrefabs.Prismarine.Bricks;
        }
        
    }
    
}
