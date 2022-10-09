
namespace BedrockTools_Build.Generator.CodeCs {
    public static class StringExtension {
        public static string ToCamelCase(string other) {
            string p = other;
            if (p.Length==0) {
                return other;
            }
            return char.ToUpper(p[0]) + p[1..];
        }
        public static string TocamelCase(string other) {
            string p = other;
            if (p.Length==0) {
                return other;
            }
            return char.ToLower(p[0]) + p[1..];
        }
    }
}
