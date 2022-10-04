using System;
using System.Collections.Generic;
using BedrockTools.Nbt.Elements;

namespace BedrockTools.Objects.Blocks {
    /// <summary>
    /// Represents a Block with Entity Data such as a chest
    /// You might read: https://minecraft.fandom.com/wiki/Bedrock_Edition_level_format/Block_entity_format
    /// </summary>
    public class BlockEntity : Block {
        public NbtCompoundSorted EntityData { get; }
        public BlockEntity(string identifier, NbtCompoundSorted blockstates, NbtCompoundSorted entityData)
            : base(identifier, blockstates) {
            this.EntityData = entityData;
        }
        public BlockEntity(string identifier, NbtCompoundSorted entityData): base(identifier) {
            this.EntityData = entityData;
        }

        /// <summary>
        /// EntityObjects are Unique, due to how Minecraft sturcture implements it.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj) {
            return false;
        }

        public override int GetHashCode() {
            return HashCode.Combine(Identifier, BlockStates, EntityData);
        }
    }
}
