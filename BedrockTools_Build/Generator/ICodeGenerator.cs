using System;
using System.Collections.Generic;
using System.Text;

namespace BedrockTools_Build.Generator {
    interface ICodeGenerator {
        public string GetCode(int tabulation=0);
    }
}
