using System;
using System.Collections.Generic;
using System.Text;

namespace BedrockTools_Build.Generator.CodeCs {
    public class CsParameter : ICodeGenerator {
        public string Name { get; set; }
        public string Type { get; set; }

        public bool IsOut { get; set; } = false;
        public string DefaultValue { get; set; } = "";
        public string GetCode(int tabulation = 0) {
            StringBuilder builder = new StringBuilder();
            if (IsOut) {
                builder.Append("out ");
            }
            builder.Append(Type+" "+Name);
            return builder.ToString();
        }

        public string GetSignature() {
            string answer = "";
            if (IsOut) {
                answer+="out ";
            }
            answer+=$"{Type} {Name}";
            if (DefaultValue!="") {
                answer+=$" = {DefaultValue}";
            }
            return answer;
        }
    }
}
