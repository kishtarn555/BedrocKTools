using System;
using System.Collections.Generic;
using System.Linq;
using BedrockTools_Build.Generator;

namespace BedrockTools_Build.OilInit {
    public class ObjectInitializer : ICodeGenerator {
        Dictionary<string, string> parameters;
        Dictionary<string, string> values;
        public OilSettings settings;
        public string ObjectType { get; }
        public ObjectInitializer(string objectType, OilSettings settings) {
            parameters = new Dictionary<string, string>();
            values = new Dictionary<string, string>();
            this.settings=settings;
            ObjectType=objectType;
        }
        public bool HasParameter(string name) => parameters.ContainsKey(name);
        public bool HasObjectValue(string name) => values.ContainsKey(name);
        public string GetParameter(string name) => parameters[name];
        public string GetObjectValue(string name) => values[name];
        public void AddConstructorParameter(string name, string value) => parameters.Add(name, value);
        public void AddObjectValue(string name, string value) => values.Add(name, value);
        public string GetCode(int tabulation = 0) {
            CodeBuilder builder = new CodeBuilder(tabulation);
            if (!settings.Minify)
                builder.StartLine();
            builder
                .Write($"new {ObjectType}(");
            if (settings.UseConstructorArguments) {
                builder.Write(
                    string.Join(", ", parameters.Select(p => p.Key + ":" + p.Value))
                );
            }
            builder.Write(")");
            if (!settings.UseObjectInitialization || values.Count==0) {
                return builder.ToString();
            }
            builder
                .Write(" {");
            if (!settings.Minify) {
                
                builder
                    .EndLine()
                    .Ident()
                    .StartLine();
                builder.Write(
                    string.Join(",\n"+builder.GetTabString(),values.Select(value => value.Key+" = "+value.Value) )
                    );

                builder
                    .EndLine()
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

        public KeyValuePair<string, string>[] GetParameters() {
            return parameters.ToArray();
        }
        public KeyValuePair<string, string>[] GetObjectValues() {
            return values.ToArray();
        }
        public void Update(KeyValuePair<string, string>[] parameters, KeyValuePair<string, string>[] objectValues) {
            foreach (KeyValuePair<string, string> parameter in parameters) {
                this.parameters[parameter.Key] = parameter.Value;
            }
            foreach (KeyValuePair<string, string> objValue in objectValues) {
                values[objValue.Key] = objValue.Value;
            }
        }        
    }
}
