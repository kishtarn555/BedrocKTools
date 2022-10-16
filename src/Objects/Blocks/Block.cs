using System;
using BedrockTools.Nbt.Elements;

namespace BedrockTools.Objects.Blocks {
    public abstract class Block {
        public static readonly int STRUCTURE_VERSION = 17959425;
        public readonly string Identifier;

        public Block(string identifier) {
            Identifier = identifier;
        }
        public abstract NbtCompoundSorted GetBlockState();
        public virtual NbtCompoundOrdered GetStructureBlock() {
            return new NbtCompoundOrdered() {
                { "name", (NbtString)Identifier},
                { "states", GetBlockState()},
                { "version", (NbtInt)STRUCTURE_VERSION}
            };
        }
        
        public override string ToString() {            
            return $"{Identifier} {GetBlockState()}";
        }

        public override bool Equals(object obj) {
            if (obj is Block block) {
                return block.Identifier == Identifier 
                    && block.GetBlockState().ToString() == GetBlockState().ToString();
            }
            return false;
        }

        public override int GetHashCode() 
            => HashCode.Combine(Identifier, GetBlockState().ToString());
    }
}
