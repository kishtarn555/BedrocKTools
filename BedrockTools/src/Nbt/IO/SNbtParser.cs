using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using BedrockTools.Nbt.Elements;

namespace BedrockTools.Nbt.IO {
    public class SNbtParser<T>  where T : NbtCompound, new() {
        struct Token {
            public string value;
            public string tokentype;

            public Token(string value, string tokentype) {
                this.value = value;
                this.tokentype = tokentype;
            }

            public override string ToString() => $"Token[{tokentype}, {value}]";
        }

        string rawCode;
        Token[] tokens;
        int pointer;

        public SNbtParser(string code) {
            rawCode = code;
            List<Token> tokenList = new List<Token>();
            string txt = code;
            Token[] primitives = new Token[] {
                new Token("^\"[^\"]+\"", "string"),
                new Token(@"^(-)?\d+(\.\d+)?[Ff]", "float"),
                new Token(@"^(-)?((\d+(\.\d+)?[Dd])|(\d+(\.\d+)))", "double"),
                new Token(@"^(-)?\d+[Bb]", "byte"),
                new Token(@"^(-)?\d+[Ss]", "short"),
                new Token(@"^(-)?\d+[Ll]", "long"),
                new Token(@"^(-)?\d+", "int"),
                new Token(@"^\w+", "identifier"),
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
                throw new Exception(String.Format("Unexpected token type, got: {0}, expected: {1}\nFull token gotten {2}\nCode: {3}", ctoken.tokentype, tokenType,ctoken,rawCode));
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
            Next(); //Remove last ]
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
                case "byte":
                    return new NbtByte(SByte.Parse(Next().value.Trim('b','B')));
                case "short":
                    return new NbtShort(Int16.Parse(Next().value.Trim('s', 'S')));
                case "int":
                    return new NbtInt(Int32.Parse(Next().value));
                case "long":
                    return new NbtLong(Int64.Parse(Next().value.Trim('l', 'L')));
                case "float":
                    return new NbtFloat(float.Parse(Next().value.Trim('f', 'F')));
                case "double":
                    return new NbtDouble(double.Parse(Next().value.Trim('d', 'D')));
                case "string":
                    return new NbtString(Next().value.Trim('"'));
                case "[":
                    return NextList();
                case "{":
                    return NextCompound();
                default:
                    throw new Exception($"Unexpected token type {ctoken.tokentype}, expecting one that indicates a value\n{ctoken}");
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
