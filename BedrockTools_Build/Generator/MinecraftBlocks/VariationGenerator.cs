using System.Collections.Generic;
using BedrockTools_Build.Generator.CodeCs;

namespace BedrockTools_Build.Generator.MinecraftBlocks {
    public class VariationGenerator : ICodeGenerator {
        internal const string VERSION = "0.1.0";
        public string ClassName { get; set; }
        public string Variation { get; set; }
        public List<string> Values { get; set; }
        public VariationGenerator() {

        }
        public string GetCode(int tabulation = 0) {
            ClassBuilder builder = new ClassBuilder("BedrockTools.Objects.Blocks.Minecraft", ClassName+"Block");
            builder.SetInheritance("Block");
            builder.AddDependency("BedrockTools.Nbt.Elements");
            builder.AddField(
                new CsField {
                    MemberType = ClassName+"Type",
                    Name = "Variation",
                    Access = AccessModifier.Public,
                    IsReadonly=true,
                }
            );
            CsConstructor constructor = new CsConstructor {
                ClassName= builder.ClassName,
                Access=AccessModifier.Public,
                Parameters= new List<CsParameter> {
                    new CsParameter {
                        Type="string",
                        Name="identifier"
                    },
                    new CsParameter {
                        Type=ClassName+"Type",
                        Name="variation"
                    }
                },
                Generator = new LineGenerator("Variation = variation;")
            };
            constructor.AddBaseParameter("identifier");
            CsEnum variationEnum = new CsEnum {
                Access=AccessModifier.Public,
                Name= ClassName+"Type"
            };
            foreach (string value in Values) {
                variationEnum.Elements.Add(value);
            }
            builder.AddConstructor(constructor);
            builder.AddEnum(variationEnum);
            CsMethod GetBlockState = new CsMethod {
                Name="GetBlockState",
                ReturnType="NbtCompoundSorted",
                IsOverriding=true,
                Access=AccessModifier.Public,
                MethodGenerator=new GetNbtCodeGenerator { Parent=this}
            };
            builder.AddMethod(GetBlockState);
            
            return builder.GetCode();
        }

        private class GetNbtCodeGenerator : ICodeGenerator {
            public VariationGenerator Parent { get; set; }
            public string GetCode(int tabulation = 0) {
                CodeBuilder builder = new CodeBuilder(tabulation);
                builder.WriteLine("return new NbtCompoundSorted() {").Ident();
                builder.WriteLine(@$"{{""{Parent.Variation.ToLower()}"", (NbtString)Variation.ToString().ToLower()}}");
                builder.Deident().WriteLine("};");
                return builder.ToString();
            }
        }
    }
}
