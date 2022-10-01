using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using BedrockTools.Nbt.Elements;

namespace BedrockTools.Nbt.Util {
    public class SNbtParser<T>  where T : NbtCompound, new() {
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

        public SNbtParser(string code) {
            rawCode = code;
            List<Token> tokenList = new List<Token>();
            string txt = code;
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
            while (txt.Length != 0) {
                txt = txt.Trim();
                Token newToken = new Token("NA", "NA");
                for (int i = 0; i < primitives.Length; i++) {
                    if (Regex.IsMatch(txt, primitives[i].value)) {
                        newToken = new Token(
                            Regex.Match(txt, primitives[i].value).Value,
                            primitives[i].tokentype
                        );
                        break;
                    }
                }
                if (newToken.tokentype == "NA") {
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
        NbtList NextList() {
            List<NbtElement> ls = new List<NbtElement>();
            Next();
            bool first = true;
            while (Peek().tokentype != "]") {
                if (!first)
                    Eat(",");
                first = false;
                ls.Add(NextNameless());
            }
            NbtTag nbtType = ls.Count > 0 ? ls[0].Tag : NbtTag.TAG_End;
            NbtList response = new NbtList(nbtType);
            foreach (NbtElement el in ls) {
                response.Add(el);
            }
            return response;
        }
        T NextCompound() {
            T nbtCompound = new T();
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
        NbtElement NextNameless() {
            Token ctoken = Peek();
            switch (ctoken.tokentype) {
                case "string":
                    return new NbtString(Next().value.Trim('"'));
                case "int":
                    return new NbtInt(Int32.Parse(Next().value));
                case "[":
                    return NextList();
                case "{":
                    return NextCompound();
                default:
                    throw new Exception("Expected token, expected Value");
            }

        }
        KeyValuePair<string, NbtElement> NextNamed() {
            Token identifier = Eat("identifier");
            Eat(":");
            return new KeyValuePair<string, NbtElement>(identifier.value, NextNameless());
        }
        public NbtElement Parse() {
            pointer = 0;
            return NextNameless();            
        }
    }
}
