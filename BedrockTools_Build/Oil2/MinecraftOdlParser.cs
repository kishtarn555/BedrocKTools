using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using BedrockTools_Build.OilInit;
namespace BedrockTools_Build.Oil2 {
    public class MinecraftOil2Parser: Oil2Parser {
        public MinecraftOil2Parser(string code, OilSettings settings) : base(code, settings) {
        }

        public override ObjectInitializerList Parse() {
            string[] codeLines = RawCode.Split("\n");
            int index = 0;
            Dictionary<string, ObjectInitializer> dict = new Dictionary<string, ObjectInitializer>();
            foreach (string iterator in codeLines) {
                string line = iterator.Trim();
                if (line.Length > 0 && !Regex.IsMatch(line, @"^//.*")) {
                    List<string> sp = new List<string>( Regex.Split(line, @"\s+"));
                    sp.Insert(2,"identifier:");
                    line = string.Join(" ", sp);
                }
                Parse(line, index+1, dict);
                index++;
            }
            return new ObjectInitializerList(dict);

        }
    }
}
