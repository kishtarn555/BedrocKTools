using System;
using System.Collections.Generic;
using BedrockTools_Build.Operations.Objects.Blocks;
namespace BedrockTools_Build {
    internal class Program {
        private static void Main(string[] args) {
            new BlockClassGeneration().Run();
            new BlockFactoryGeneration().Run();
            return;
        }
    }
}
