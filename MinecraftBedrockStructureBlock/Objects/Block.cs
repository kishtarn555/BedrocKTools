﻿using System;
using BedrockTools.Nbt.Extension;
using BedrockTools.Nbt.Elements;
using System.Collections.Generic;

namespace BedrockTools.Objects {
    public class Block : INbtParsable<NbtCompoundOrdered> {
        public const int STRUCTURE_VERSION = 17959425;
        public string Identifier { get; }
        public NbtCompoundSorted BlockStates { get; }

        public Block(string identifier) {
            this.Identifier = identifier;
            this.BlockStates = new NbtCompoundSorted();
        }
        public Block (string identifier, NbtCompoundSorted blockstates) {
            this.Identifier = identifier;
            this.BlockStates = blockstates;
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
