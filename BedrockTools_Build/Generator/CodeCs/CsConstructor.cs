using System;
using System.Collections.Generic;
using System.Linq;

namespace BedrockTools_Build.Generator.CodeCs {
    public class CsConstructor : ICodeGenerator{
        public string ClassName { get; set; }
        public AccessModifier Access { get; set; }
        public List<CsParameter> Parameters { get; set; } = new List<CsParameter>();
        public bool UsesBase { get; set; } = false;
        public List<string> BaseParameters = new List<string>();
        public ICodeGenerator Generator { get; set; }
        public void AddBaseParameter(string parameter) {
            UsesBase=true;
            BaseParameters.Add(parameter);
        }
        public string GetCode(int tabulation = 0) {
            CodeBuilder builder = new CodeBuilder(tabulation);
            builder.StartLine();
            builder.Write(Access.ToString().ToLower());
            builder.Write($" {ClassName} (");
            builder.Write(string.Join(", ", Parameters.Select(parameter => parameter.GetCode(0))));
            builder.Write(")");
            if (UsesBase) {
                builder
                    .Write(" : base(")
                    .Write(string.Join(", ", BaseParameters))
                    .Write(")");
            }
            builder.Write(" {").Ident().EndLine();
            if (Generator!=null)
                builder.Write(Generator.GetCode(builder.StateTab));
            builder.Deident().WriteLine("}");
            return builder.ToString();
        }
    }
}
