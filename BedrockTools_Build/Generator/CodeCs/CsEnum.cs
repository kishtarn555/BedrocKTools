using System;
using System.Collections.Generic;
using System.Text;

namespace BedrockTools_Build.Generator.CodeCs {
    public class CsEnum : ICodeGenerator {
        public AccessModifier Access { get; set; }
        public string Name { get; set; }
        public List<string> Elements { get; set; } = new List<string>();

        public string GetCode(int tabulation = 0) {
            if (Elements.Count==0)
                throw new Exception();
            CodeBuilder builder = new CodeBuilder(tabulation);
            builder.StartLine();
            builder.Write(Access.ToString().ToLower());
            builder.Write($" enum {Name} {{");
            builder.EndLine().Ident();
            for (int i = 0; i <Elements.Count-1; i++) {
                builder.WriteLine($"{Elements[i]},");
            }
            builder.WriteLine($"{Elements[Elements.Count-1]}");
            builder.Deident().WriteLine("}");
            return builder.ToString();
        }
    }
}
