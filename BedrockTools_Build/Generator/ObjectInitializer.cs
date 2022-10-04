using System;
using System.Collections.Generic;
using System.Linq;

namespace BedrockTools_Build.Generator {
    class ObjectInitializer : ICodeGenerator {
        string classname;
        Dictionary<string, string> parameters;
        Dictionary<string, string> values;
        public bool UseObjectInitializer { get; }
        bool minify;
        public ObjectInitializer(string classname, bool useObjectInitializer, bool minify=true) {
            this.classname = classname;
            this.UseObjectInitializer = useObjectInitializer;
            parameters = new Dictionary<string, string>();
            values = new Dictionary<string, string>();
            this.minify = minify;
        }
        public void AddConstructorParameter(string name, string value) {
            parameters.Add(name, value);
        }
        public void AddObjectValue(string name, string value) {
            values.Add(name, value);
        }
        public string GetCode(int tabulation = 0) {
            CodeBuilder builder = new CodeBuilder(tabulation);
            if (!minify)
                builder.StartLine();
            builder
                .Write($"new {classname}(")
                .Write(
                    string.Join(", ", parameters.Select(p => p.Key + ":" + p.Value))
                )
                .Write(")");
            if (!UseObjectInitializer || values.Count==0) {
                return builder.ToString();
            }
            builder
                .Write(" {");
            if (!minify) {
                
                builder
                    .NewLine()
                    .Ident()
                    .StartLine();
                builder.Write(
                    string.Join(",\n"+builder.GetTabString(),values.Select(value => value.Key+" = "+value.Value) )
                    );

                builder
                    .NewLine()
                    .Deident()
                    .StartLine()
                    .Write("}");
            } else {
                builder
                    .Write(
                        string.Join(", ", values.Select(value => value.Key + "=" + value.Value))
                    )
                    .Write("}");
            }
            return builder.ToString();            
        }
        
    }
}
