using System;
using System.IO;
using BedrockTools_Build.Operations.Objects.Blocks;
namespace BedrockTools_Build {
    class Program {
        static void Main(string[] args) {

            new BlockFactoryGeneration().Run();
            new StairsBlockFactoryGeneration().Run();
            return;
        }
    }
}
