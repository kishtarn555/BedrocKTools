using BedrockTools.Objects;
using BedrockTools.Objects.Blocks;
using BedrockTools.Objects.Entities;
using BedrockTools.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BedrockTools.src.Structure {
    public class DynamicMcStructure : IMcStructure {
        Dictionary<IntCoords, Block> blocks;
        IntCoords mininamlBoundingBox;
        IntCoords maximalBoundingBox;


        public Dimensions Size => new Dimensions(
                maximalBoundingBox.X - mininamlBoundingBox.X + 1,
                maximalBoundingBox.Y - mininamlBoundingBox.Y + 1,
                maximalBoundingBox.Z - mininamlBoundingBox.Z + 1
            );

        public IntCoords Origin { get; protected set; }

        public DynamicMcStructure() {
            Origin = IntCoords.Zero;
            mininamlBoundingBox = maximalBoundingBox = IntCoords.Zero;
            blocks = new Dictionary<IntCoords, Block>();
        }
        public DynamicMcStructure(IntCoords origin) {
            Origin = origin;
            mininamlBoundingBox = maximalBoundingBox = IntCoords.Zero;
            blocks = new Dictionary<IntCoords, Block>();
        }

        void addPointToBoundingBox(IntCoords coords) {
            mininamlBoundingBox.X = int.Min(mininamlBoundingBox.X, coords.X);
            mininamlBoundingBox.Y = int.Min(mininamlBoundingBox.Y, coords.Y);
            mininamlBoundingBox.Z = int.Min(mininamlBoundingBox.Z, coords.Z);

            maximalBoundingBox.X = int.Max(maximalBoundingBox.X, coords.X);
            maximalBoundingBox.Y = int.Max(maximalBoundingBox.Y, coords.Y);
            maximalBoundingBox.Z = int.Max(maximalBoundingBox.Z, coords.Z);
        }

        public void SetLocalBlock(int x, int y, int z, Block block) => SetLocalBlock(new IntCoords(x, y, z), block);

        public void SetLocalBlock(IntCoords coords, Block block) {
            addPointToBoundingBox(coords);
            blocks[coords] = block;
        }
        public Block GetLocalBlock(int x, int y, int z) => GetLocalBlock(new IntCoords(x, y, z));
        public Block GetLocalBlock(IntCoords coords) {
            if (!blocks.ContainsKey(coords)) 
                return null;
            return blocks[coords];
        }


        public void AddEntity(Entity entity) => throw new NotImplementedException();
        public Block GetBlock(int x, int y, int z) => GetBlock(new IntCoords(x, y, z));
        public Block GetBlock(IntCoords coords) => GetLocalBlock(coords + mininamlBoundingBox);
        public Block[,,] GetBlocks() {
            Dimensions size = Size;
            Block[,,] result = new Block[size.X, size.Y, size.Z];
            foreach ((IntCoords localCoords, Block block) in blocks) {
                IntCoords coords = localCoords - mininamlBoundingBox;
                result[coords.X, coords.Y, coords.Z] = block;
            }
            return result;
        }
        public void SetBlock(int x, int y, int z, Block block) => SetBlock(new IntCoords(x, y, z), block);
        public void SetBlock(IntCoords coords, Block block) => SetLocalBlock(coords, block);

        public void SetBlocks(Block[,,] blocks) {
            mininamlBoundingBox = IntCoords.Zero;
            maximalBoundingBox = IntCoords.Zero;
            this.blocks.Clear();
            for (int x = 0; x < blocks.GetLength(0); x++) {
                for (int y = 0; y < blocks.GetLength(1); y++) {
                    for(int z=0; z < blocks.GetLength(2); z++) {
                        SetLocalBlock(x, y, z, blocks[x, y, z]);
                    }
                }
            }
        }

    }
}
