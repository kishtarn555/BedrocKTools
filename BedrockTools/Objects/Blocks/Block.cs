﻿using System;
using BedrockTools.Nbt.Extension;
using BedrockTools.Nbt.Elements;
using BedrockTools.Nbt.Util;

namespace BedrockTools.Objects.Blocks {
    public class Block : INbtParsable<NbtCompoundOrdered> {
        public const int STRUCTURE_VERSION = 17959425;
        public string Identifier { get; }
        public NbtCompoundSorted BlockStates { get; }
        public Block(string identifier) {
            this.Identifier = identifier;
            this.BlockStates = new NbtCompoundSorted();
        }
        public Block (string identifier, NbtCompoundSorted blockStates) {
            Identifier = identifier;
            BlockStates = blockStates;
        }
        public Block(string identifier, string blockStates) {
            this.Identifier = identifier;
            this.BlockStates = (NbtCompoundSorted)(new SNbtParser<NbtCompoundSorted>(blockStates).Parse());
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

    }
}
