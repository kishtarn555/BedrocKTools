using System;
using System.Collections.Generic;
using System.Text;

namespace BedrockTools_Build.Generator {
    public interface ICodeGenerator {
        public string GetCode(int tabulation=0);
    }
}
