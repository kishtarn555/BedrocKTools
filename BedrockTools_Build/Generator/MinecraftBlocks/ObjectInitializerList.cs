using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace BedrockTools_Build.Generator.MinecraftBlocks {
    class ObjectInitializerList {
        List<ObjectInitializer> objects;
        public ObjectInitializerList(string code) {
            string[] lines = code.Split("\n");
        }
    }
}
