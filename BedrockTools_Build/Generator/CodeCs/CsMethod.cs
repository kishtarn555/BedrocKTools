using System;
using System.Collections.Generic;
using System.Linq;

namespace BedrockTools_Build.Generator.CodeCs {
    public class CsMethod : ICodeGenerator {
        public string Name { get; set; }
        public AccessModifier Access { get; set; }
        public string ReturnType { get; set; }
        public ICodeGenerator MethodGenerator;
        public List<CsParameter> Parameters { get; set; } = new List<CsParameter>();

        public bool IsOverriding = false;
        public bool IsStatic = false;

        public string GetCode(int tabulation= 0) {
            if (IsStatic && IsOverriding) {
                throw new ArgumentException(); //FIXME
            }
            CodeBuilder builder = new CodeBuilder(tabulation);
            builder.StartLine();
            builder.Write(Access.ToString().ToLower());
            if (IsOverriding) {
                builder.Write(" override");
            }
            if (IsStatic) {
                builder.Write(" static");
            }
            builder.Write($" {ReturnType}");
            builder.Write($" {Name} (");
            builder.Write(
                string.Join(", ", Parameters.Select(a => a.GetSignature()))
            );
            builder.Write(") {");
            builder.EndLine().Ident();
            builder.Write(MethodGenerator.GetCode(builder.StateTab));
            builder.Deident().WriteLine("}");
            return builder.ToString();
        }
    }
}
