using System;
using System.Collections.Generic;
using System.Text;

namespace BedrockTools_Build.Generator.CodeCs {
    public class CsField {
        public string Name { get; set; }
        public string MemberType { get; set; }
        public bool IsStatic { get; set; }
        public bool IsReadonly { get; set; }
        public bool IsConstant { get; set; }
        public AccessModifier Access { get; set; }
        
    }
}
