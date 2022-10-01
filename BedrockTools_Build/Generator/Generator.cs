using System;
using System.IO;

namespace BedrockTools_Build.Generator {
    abstract class Generator  {
        public string Target;

        protected Generator(string target) {
            Target = target;
        }

        public abstract string GetCode();
        public abstract void Generate();
    }
}
