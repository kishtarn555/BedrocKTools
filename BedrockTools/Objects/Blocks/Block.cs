﻿using System;
using BedrockTools.Nbt.Extension;
using BedrockTools.Nbt.Elements;
using BedrockTools.Nbt.Util;

namespace BedrockTools.Objects.Blocks {
    public class Block : INbtParsable<NbtCompoundOrdered> {
        public const int STRUCTURE_VERSION = 17959425;
        public string Identifier { get; protected set; }
        public virtual NbtCompoundSorted BlockStates { get; protected set; }
        protected Block() {

        }
        public Block(string identifier) {
            Identifier = identifier;
            BlockStates = new NbtCompoundSorted();
        }
        public Block (string identifier, NbtCompoundSorted blockStates) {
            Identifier = identifier;
            BlockStates = blockStates;
        }
        public Block(string identifier, string blockStates) {
            Identifier = identifier;
            BlockStates = (NbtCompoundSorted)(new SNbtParser<NbtCompoundSorted>(blockStates).Parse());
        }
        public NbtCompoundOrdered ToNbt() {
            return new NbtCompoundOrdered() {
                { "name", (NbtString)Identifier},
                { "states", BlockStates},
                { "version", (NbtInt)STRUCTURE_VERSION}
            };
        }
        public override string ToString() {
            return $"[Block] {Identifier} {BlockStates}";
        }

        public override bool Equals(object obj) {
            if (!(obj is Block)) return false;
            if (obj is BlockEntity) return false;
            return obj.ToString() == ToString();
        }

        public override int GetHashCode() {
            return HashCode.Combine(Identifier, BlockStates.ToString());
        }
    }
}
