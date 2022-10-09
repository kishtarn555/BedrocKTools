using System.Collections.Generic;
using System.Linq;
using BedrockTools_Build.OilInit;


namespace BedrockTools_Build.Generator.MinecraftBlocks {
    public class MinecraftBlockFactoryGenerator : ICodeGenerator {
        public const string VERSION = "1.0.0";
        private ObjectInitializerList InitializerList;
        
        public MinecraftBlockFactoryGenerator(ObjectInitializerList initializerList) {
            InitializerList=initializerList;
        
        }
        
        
        public string GetCode(int tabulation = 0) {
            CodeBuilder builder = new CodeBuilder(0);
            builder
                .WriteLine("using System.Collections.Generic;")
                .WriteLine("using BedrockTools.Objects.Blocks.Minecraft;")
                .WriteLine("using BedrockTools.Objects.Blocks.Util;")
                .EndLine()
                .WriteLine("namespace BedrockTools.Objects.Blocks {")
                .Ident()
                    .WriteLine("public static partial class VanillaBlockFactory {");
                        BuildFactories(builder);
                    builder.WriteLine("}")
                .Deident()
                .WriteLine("}");
                
            return builder.ToString();
        }
        private List<CodeCs.CsParameter> GetParameters(KeyValuePair<string, ObjectInitializer> oilObject) {
            List<CodeCs.CsParameter> result = new List<CodeCs.CsParameter>();
            switch(oilObject.Value.ObjectType) {
                case "UnitBlock":
                    break;
                case "ColorBlock":
                    result.Add(new CodeCs.CsParameter { Type="BlockColorValue", Name="color" });
                    break;
                case "StairsBlock":
                    result.Add(new CodeCs.CsParameter { Type="BlockOrientation", Name="orientation" });
                    result.Add(new CodeCs.CsParameter { Type="bool", Name="isUpsideDown", DefaultValue="false" });
                    break;
                case "Variant":
                    result.Add(new CodeCs.CsParameter { 
                        Type=oilObject.Key+"Block."+ oilObject.Key+"Type",
                        Name="variation" 
                    });
                    break;
            }
            return result;
        }
        private void BuildFactories(CodeBuilder builder) {
            builder.Ident();
            foreach(KeyValuePair<string, ObjectInitializer> decs in InitializerList.GetInitializers()) {
                string returnType = decs.Value.ObjectType;
                if (returnType=="Variant") {
                    returnType = decs.Key+"Block";
                }
                CodeCs.CsMethod method = new CodeCs.CsMethod {
                    Access=CodeCs.AccessModifier.Public,
                    IsStatic=true,
                    Name = decs.Key,
                    ReturnType = returnType,
                    Parameters=GetParameters(decs)
                };
                List<string> callParameters = new List<string>() { $@"""{decs.Value.GetParameter("identifier")}""" };
                callParameters.AddRange(method.Parameters.Select(parameter => parameter.Name));
                string argList = string.Join(", ", callParameters);
                method.MethodGenerator = new LineGenerator($"return new {method.ReturnType} ({argList});");
                decs.Value.settings= OilSettings.GetSettings();
                builder.Write(method.GetCode(builder.StateTab));
            }

            builder.Deident();
                
        }
    }
}
