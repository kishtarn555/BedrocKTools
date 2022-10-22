using System;
using System.Collections.Generic;
using System.Text;

namespace BedrockTools_Build.Generator {
    public class LineGenerator: ICodeGenerator {
        public string[] Code { get; set; }
        public LineGenerator(params string[] lines) {
            Code = lines;
        }

        public string GetCode(int tabulation = 0) {
            CodeBuilder builder = new CodeBuilder(tabulation);
            foreach (string code in Code) {
                builder.WriteLine(code);
            }
            return builder.ToString();
        }
    }
}
