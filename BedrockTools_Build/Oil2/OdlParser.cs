using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using BedrockTools_Build.OilInit;
namespace BedrockTools_Build.Oil2 {
    public class Oil2Parser {

        public string RawCode { get; protected set; }
        public OilSettings Settings { get; set; }

        public Oil2Parser(string code, OilSettings settings) {
            RawCode=code;
            Settings=settings;
        }



        public virtual ObjectInitializerList Parse() {
            string[] codeLines = RawCode.Split("\n");
            int index = 0;
            Dictionary<string, ObjectInitializer> dict = new Dictionary<string, ObjectInitializer>();
            foreach (string iterator in codeLines) {
                Parse(iterator.Trim(), index+1, dict);
                index++;
            }

            return new ObjectInitializerList(dict);
        }


        protected void Parse(string line, int lnNumber, Dictionary<string, ObjectInitializer> dict) {
            if (Regex.IsMatch(line, @"^//.*")) {
                return;
            }
            if (line.Length <= 0) {
                return;
            }
            string[] components = Regex.Split(line, @"\s+");
            try {
                int index = 0;
                string key = components[index++];
                string objectType = components[index++];
                ObjectInitializer initializer = new ObjectInitializer(objectType, Settings);
                while (index < components.Length) {
                    string name = components[index++];
                    if (name.Length < 2) {
                        throw new Exception(
                            $"Incorrect param name {name}, at line {lnNumber}, it needs two charcters and the last one be of either '=' ':'"
                            +"depending on the type of parameter"
                            );
                    }
                    char op = name[name.Length-1];
                    string sname = name.Substring(0, name.Length-1);
                    switch (op) {
                        case ':':
                            initializer.AddConstructorParameter(sname, components[index++]);
                            break;
                        case '=':
                            initializer.AddObjectValue(sname, components[index++]);
                            break;
                        default:
                            throw new Exception(
                                $"Incorrect param name '{name}', at line {lnNumber}, it needs two charcters and the last one be of either '=' ':'"
                                +"depending on the type of parameter"
                                );
                    }
                }
                dict[key]=initializer;
            }
            catch (IndexOutOfRangeException) {
                throw new Exception($"While parsing '{line}' at {lnNumber} line, we found unexpectedly the end of line");
            }

        }


    }
}
