using System;
using System.Collections.Generic;
using System.Linq;

namespace BedrockTools_Build.Generator.CodeCs {
    public class ClassBuilder : ICodeGenerator {
        public AccessModifier Accessiblity { get; set; }
        public string ClassName { get; set; }
        public EClassType ClassType { get; set; }
        public List<CsConstructor> Constructors { get; set; }
        public List<string> Dependencies { get; set; }
        public List<CsEnum> Enums { get; set; }
        public List<CsField> Fields { get; set; }
        public string Inheritance { get; set; }
        public List<CsMethod> Methods { get; set; }
        public string Namespace { get; set; }
        public List<CsProperty> Properties { get; set; }
        public bool UsesInheritance { get; set; }


        public ClassBuilder(string nameSpace, string className) {
            Constructors= new List<CsConstructor>();            
            Dependencies=new List<string>();
            Enums = new List<CsEnum>();
            Fields= new List<CsField>();
            Methods= new List<CsMethod>();
            Properties= new List<CsProperty>();            
            ClassName=className;
            Namespace=nameSpace;
            Accessiblity=AccessModifier.Public;
            ClassType=EClassType.Normal;
            Inheritance="";
            UsesInheritance=false;
        }
        public void AddDependency(string dependency) {
            Dependencies.Add(dependency);
        }
        public void SetInheritance(string baseClass) {
            Inheritance=baseClass;
            UsesInheritance=true;
        }

        public void AddField(CsField field) => Fields.Add(field);
        public void AddProperty(CsProperty property) => Properties.Add(property);
        public void AddMethod(CsMethod method) => Methods.Add(method);
        public void AddEnum(CsEnum csEnum) => Enums.Add(csEnum);
        internal void AddConstructor(CsConstructor constructor) => Constructors.Add(constructor);

        protected bool ListDependencies(CodeBuilder builder) {
            if (Dependencies.Count==0) {
                return false;
            }
            Dependencies.Sort((a, b) => {
                string[] aComponents = a.Split('.');
                string[] bComponents = b.Split('.');
                if (aComponents[0]=="System" && bComponents[0] != "System") {
                    return -1;
                }
                if (aComponents[0]!="System" && bComponents[0] == "System") {
                    return 1;
                }
                return a.CompareTo(b);
            });
            foreach (string dependency in Dependencies) {
                builder.WriteLine($"using {dependency};");
            }
            return true;
        }
        protected void WriteClassSignature(CodeBuilder builder) {
            builder.StartLine();
            builder.Write($"{Accessiblity.ToString().ToLower()} ");
            if (ClassType!=EClassType.Normal) {
                builder.Write($"{ClassType.ToString().ToLower()} ");
            }
            builder.Write($"class {ClassName} ");
            if (UsesInheritance) {
                builder.Write($": {Inheritance} ");
            }
        }
        protected bool ListFields(CodeBuilder builder) {
            if (Fields.Count==0) {
                return false;
            }
            Fields.Sort((a, b) => {
                if (
                    (a.IsStatic || a.IsReadonly || a.IsConstant) &&
                    !(b.IsStatic || b.IsReadonly || b.IsConstant)
                ) {
                    return -1;
                }
                if (
                    !(a.IsStatic || a.IsReadonly || a.IsConstant) &&
                    (b.IsStatic || b.IsReadonly || b.IsConstant)
                ) {
                    return 1;
                }
                if (a.Access!=b.Access) {
                    return a.Access.CompareTo(b.Access);
                }
                return a.Name.CompareTo(b.Name);
            });
            foreach (CsField field in Fields) {
                builder.StartLine();
                builder.Write(field.Access.ToString().ToLower());
                if (field.IsStatic) {
                    builder.Write(" static");
                }
                if (field.IsReadonly) {
                    builder.Write(" readonly");
                }
                if (field.IsConstant) {
                    builder.Write(" const");
                }
                builder.Write($" {field.MemberType}");
                builder.Write($" {field.Name};");
                builder.EndLine();
            }
            return true;
        }
        protected bool ListProperties(CodeBuilder builder) {
            if (Properties.Count==0) {
                return false;
            }
            Properties.Sort((a, b) => {

                if (a.Access!=b.Access) {
                    return a.Access.CompareTo(b.Access);
                }
                return a.Name.CompareTo(b.Name);
            });
            foreach (CsProperty property in Properties) {
                builder.StartLine();
                builder.Write(property.Access.ToString().ToLower());
                builder.Write($" {property.Type}");
                builder.Write($" {property.Name};");
                builder.EndLine();
            }
            return true;
        }
        public bool ListConstructors(CodeBuilder builder) {
            if (Constructors.Count==0) {
                return false;
            }
            builder.Write(
                string.Join(
                    "\n", 
                    Constructors.Select(constructor => constructor.GetCode(builder.StateTab))
                )
            );
            return true;
        }
        public bool ListMethods(CodeBuilder builder) {
            if (Methods.Count==0) {
                return false;
            }
            Methods.Sort((a, b) => {
                return a.Access.CompareTo(b.Access);
            });

            builder.Write(
                string.Join(
                    "\n",
                    Methods.Select(
                       method=> method.GetCode(builder.StateTab)
                    ).ToArray()
                )
            );
            
            return true;
        }

        public bool ListEnums(CodeBuilder builder) {
            if (Enums.Count==0) {
                return false;
            }
            Enums.Sort((a, b) =>a.Access.CompareTo(b.Access));
            foreach (CsEnum csEnum in Enums) {
                builder.Write(csEnum.GetCode(builder.StateTab));
            }
            return true;
        }
        public string GetCode(int tabulation = 0)  {
            CodeBuilder builder = new CodeBuilder(tabulation);
            ListDependencies(builder);
            
            builder.EndLine();            
            builder.WriteLine($"namespace {Namespace} {{").Ident(); 
            {
                WriteClassSignature(builder);
                builder.Write("{").Ident().EndLine();
                {
                    if (ListFields(builder)) {
                        builder.EndLine();
                    }
                    if (ListProperties(builder)) {
                        builder.EndLine();
                    }
                    if (ListConstructors(builder)) {
                        builder.EndLine();
                    }
                    if (ListMethods(builder)) {
                        builder.EndLine();
                    }
                    if (ListEnums(builder)) {
                        builder.EndLine();
                    }
                }
                builder.Deident().WriteLine("}");
            }
            builder.Deident().WriteLine("}");
            return builder.ToString();
        }

        public enum EClassType {
            Normal,
            Abstract,
            Static,
            Sealed
        }
    }
}
