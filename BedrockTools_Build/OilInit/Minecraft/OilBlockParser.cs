using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace BedrockTools_Build.OilInit.Minecraft {
    public class OilBlockParser : OilParser {
        public OilBlockParser(string code, string objectType, OilSettings settings) : base(code, objectType, settings) {
        }

        public override ObjectInitializerList Parse() {
            string[] codeLines = RawCode.Split("\n");
            int index = 0;
            Dictionary<string, ObjectInitializer> dict = new Dictionary<string, ObjectInitializer>();
            foreach (string iterator in codeLines) {
                string line = iterator.Trim();
                if (line.Length > 0 && !Regex.IsMatch(line,@"^//.*")) {
                    string[] sp = Regex.Split(line, @"\s+");
                    if (sp.Length==2) {
                        line = $"{sp[0]} identifier: \"{sp[1]}\"";
                    } else if (sp.Length==3) {
                        line = $"{sp[0]} identifier: \"{sp[1]}\" blockStates: \"{sp[2]}\"" ;
                    } else {
                        throw new Exception("Error at line "+index+ ": " +iterator);
                    }
                }
                Parse(line, index, dict);
                index++;
            }
            return new ObjectInitializerList(dict);

        }
    }
}
