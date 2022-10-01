using System;
using System.Collections.Generic;
using System.Text;


namespace MinecraftBedrockStructureBlock.structure.block.prefabs {
    public static class MinecraftPrefabs {
        public static Block Air => new Block("minecraft:air", "{}");
        public static Block AncientDebris => new Block("minecraft:ancient_debris", "{}");
        public static Block Clay => new Block("minecraft:clay", "{}");
        public static class Concrete {
            public static Block White => new Block("minecraft:concrete", "{color:\"white\"}");
            public static Block Orange => new Block("minecraft:concrete", "{color:\"orange\"}");
            public static Block Magenta => new Block("minecraft:concrete", "{color:\"magenta\"}");
            public static Block LightBlue => new Block("minecraft:concrete", "{color:\"light_blue\"}");
            public static Block Yellow => new Block("minecraft:concrete", "{color:\"yellow\"}");
            public static Block Lime => new Block("minecraft:concrete", "{color:\"lime\"}");
            public static Block Pink => new Block("minecraft:concrete", "{color:\"pink\"}");
            public static Block Gray => new Block("minecraft:concrete", "{color:\"gray\"}");
            public static Block LightGray => new Block("minecraft:concrete", "{color:\"silver\"}");
            public static Block Cyan => new Block("minecraft:concrete", "{color:\"cyan\"}");
            public static Block Purple => new Block("minecraft:concrete", "{color:\"purple\"}");
            public static Block Blue => new Block("minecraft:concrete", "{color:\"blue\"}");
            public static Block Brown => new Block("minecraft:concrete", "{color:\"brown\"}");
            public static Block Green => new Block("minecraft:concrete", "{color:\"green\"}");
            public static Block Red => new Block("minecraft:concrete", "{color:\"red\"}");
            public static Block Black => new Block("minecraft:concrete", "{color:\"black\"}");
        }
        public static class Dirt {
            public static Block Normal => new Block("minecraft:dirt", "{dirt_type:\"normal\"}");
            public static Block Coarset => new Block("minecraft:dirt", "{dirt_type:\"coarse\"}");
        }
        public static Block EndStone => new Block("minecraft:end_stone", "{}");
        public static Block Grass => new Block("minecraft:grass", "{}");
        public static class Ice {
            public static Block Normal => new Block("minecraft:ice", "{}");
            public static Block Packed => new Block("minecraft:packed_ice", "{}");
        }
        public static class OakLog {
            
        }
        public static class Planks {
            public static Block Oak => new Block("minecraft:planks", "{wood_type:\"oak\"}");
            public static Block Spruce => new Block("minecraft:planks", "{wood_type:\"spruce\"}");
            public static Block Birch => new Block("minecraft:planks", "{wood_type:\"birch\"}");
            public static Block Acacia => new Block("minecraft:planks", "{wood_type:\"acacia\"}");
            public static Block DarkOark => new Block("minecraft:planks", "{wood_type:\"dark_oak\"}");
            public static Block Jungle => new Block("minecraft:planks", "{wood_type:\"jungle\"}");
            public static Block Mangrove => new Block("minecraft:mangrove_planks", "{}");
            public static Block Crimson => new Block("minecraft:crimson_planks", "{}");
            public static Block Warped => new Block("minecraft:warped_planks", "{}");
        }
        public static class Prismarine {
            public static Block Bricks => new Block("minecraft:prismarine", "{prismarine_block_type:\"bricks\"}");
            public static Block Dark => new Block("minecraft:prismarine", "{prismarine_block_type:\"dark\"}");
            public static Block Default => new Block("minecraft:prismarine", "{prismarine_block_type:\"default\"}");
        }
        public static Block RawIronBlock => new Block("minecraft:raw_iron_block", "{}");
        public static Block RedstoneBlock => new Block("minecraft:redstone_block", "{}");
        public static class Stone {
            public static Block Normal => new Block("minecraft:stone", "{stone_type:\"stone\"}");
            public static Block Granite => new Block("minecraft:stone", "{stone_type:\"granite\"}");
        }
        //crimson, warped and mangrove are <prefix>_planks.

    }
}
