using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using MinecraftBedrockStructureBlock.enums;

namespace MinecraftBedrockStructureBlock.types.util {
    public class NbtStringParser {
        struct Token {
            public string value;
            public string tokentype;

            public Token(string value, string tokentype) {
                this.value = value;
                this.tokentype = tokentype;
            }
        }

        string rawCode;
        Token[] tokens;
        int pointer;
        
        public NbtStringParser(string code) {
            rawCode = code;
            List<Token> tokenList = new List<Token>();
            string txt=code;
            Token[] primitives = new Token[] {
                new Token(@"^\w+", "identifier"),
                new Token("^\"[^\"]+\"", "string"),
                new Token(@"^(-)?\d+(\.\d+)?f", "float"),
                new Token(@"^(-)?\d+(\.\d+)?d", "double"),
                new Token(@"^(-)?\d+L", "long"),
                new Token(@"^(-)?\d+s", "short"),
                new Token(@"^(-)?\d+", "int"),
                new Token(@"^,", ","),
                new Token(@"^\{", "{"),
                new Token(@"^\}", "}"),
                new Token(@"^\[", "["),
                new Token(@"^\]", "]"),
                new Token(@"^:", ":")
            };
            while (txt.Length!=0) {
                txt = txt.Trim();
                Token newToken = new Token("NA", "NA");
                for (int i =0; i < primitives.Length; i++) {
                    if (Regex.IsMatch(txt, primitives[i].value)) {
                        newToken = new Token(
                            Regex.Match(txt, primitives[i].value).Value,
                            primitives[i].tokentype
                        );
                        break;
                    }
                }
                if (newToken.tokentype=="NA") {
                    throw new Exception("Error parsing Nbt Type, Unrecognized token");
                }
                txt = txt.Substring(newToken.value.Length);
                tokenList.Add(newToken);
            }
            tokens = tokenList.ToArray();
            pointer = 0;
        }
        bool hasEnded() {
            return pointer == tokens.Length;
        }

        Token Peek() {
            return tokens[pointer];
        }
        Token Next() {
            return tokens[pointer++];
        }
        Token Eat(string tokenType) {
            Token ctoken = Next();
            if (tokenType != ctoken.tokentype)
                throw new Exception(String.Format("Unexpected token type, got: {0}, expected: {1}", ctoken.tokentype, tokenType));
            return ctoken;
        }
        NbtList NextList(string name) {
            List<NbtBase> ls = new List<NbtBase>();
            Next();
            bool first = true;
            while (Peek().tokentype != "]") {
                if (!first)
                    Eat(",");
                first = false;
                ls.Add(NextNameless());
            }
            NbtTypes nbtType = ls.Count > 0 ? ls[0].NType : NbtTypes.TAG_End;
            NbtList response = new NbtList(name, nbtType);
            foreach (NbtBase el in ls) {
                response.Add(el);
            }
            return response;
        }
        NbtCompound NextCompound(string name) {
            NbtCompound nbtCompound = new NbtCompound(name);
            Next();
            bool first = true;
            while (Peek().tokentype != "}") {
                if (!first)
                    Eat(",");
                first = false;
                nbtCompound.Add(NextNamed());
            }
            
            return nbtCompound;
        }
        NbtBase NextNameless(string name ="") {
            Token ctoken = Peek();
            switch (ctoken.tokentype) {
                case "string":
                    return new NbtString(name, Next().value.Trim('"'));
                case "int":
                    return new NbtInt(name, Int32.Parse(Next().value));
                case "[":
                    return NextList(name);
                case "{":
                    return NextCompound(name);
                default:
                    throw new Exception("Expected token, expected Value");
            }

        }
        NbtBase NextNamed() {
            Token identifier = Eat("identifier");
            Eat(":");
            return NextNameless(identifier.value);
        }
        public NbtBase Parse() {
            pointer = 0;
            Token ctoken = Peek();  
            if (ctoken.tokentype == "identifier") {
                return NextNamed();                
            } else {
                return NextNameless();
            }
        }
        public static NbtBase Parse(string code) {
            return new NbtStringParser(code).Parse();
        }

        public static T Get<T>(string code) where T : NbtBase {
            NbtBase res = NbtStringParser.Parse(code);
            if (res is T)
                return (res as T);
            throw new Exception();

        }
    }
}
