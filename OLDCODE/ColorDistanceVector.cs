using System;
using System.Collections.Generic;
using MinecraftBedrockStructureBlock.structure.block;

namespace MinecraftBedrockStructureBlock.image {
    public class ColorDistanceVector {
       
        List<(double distance, Block block)> results;
        public ColorDistanceVector() {
            results = new List<(double distance, Block block)>();
        }
        public void Add(double distance, Block block) {
            results.Add((distance, block));
        }

        public List<(double distance, Block block)> GetSortedList() {
            results.Sort(
                delegate((double distance, Block block)a, (double distance, Block block) b  ) {
                    return a.distance.CompareTo(b.distance);
                }
            );
            return results;
        }
    }
}
