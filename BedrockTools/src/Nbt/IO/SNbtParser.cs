using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using BedrockTools.Nbt.Elements;

namespace BedrockTools.Nbt.IO {
    public class SNbtParser<T> where T : NbtCompound, new() {

        Stack<string> currentPath;
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


        string DumpStack() {
            List<string> sterr = new List<string>();
            while (currentPath.Count > 0) {
                string path = currentPath.Pop();
                sterr.Add(path);
            }
            sterr.Reverse();
            return string.Join('\n', sterr);

        }
        Token Eat(string tokenType) {
            Token ctoken = Next();
            if (tokenType != ctoken.tokentype)
                throw new Exception(String.Format("Unexpected token type, got: {0}, expected: {1}\nFull token gotten {2}\nTree:{4} \nCode: {3}", ctoken.tokentype, tokenType, ctoken, rawCode, DumpStack()));
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
            NbtElement response = null;
            switch (ctoken.tokentype) {
                case "byte":
                    currentPath.Push("[Nameless byte]");
                    response = new NbtByte(SByte.Parse(Next().value.Trim('b', 'B')));
                    break;
                case "short":
                    currentPath.Push("[Nameless short]");
                    response = new NbtShort(Int16.Parse(Next().value.Trim('s', 'S')));
                    break;
                case "int":
                    currentPath.Push("[Nameless int]");
                    response = new NbtInt(Int32.Parse(Next().value));
                    break;
                case "long":
                    currentPath.Push("[Nameless long]");
                    response = new NbtLong(Int64.Parse(Next().value.Trim('l', 'L')));
                    break;
                case "float":
                    currentPath.Push("[Nameless float]");
                    response = new NbtFloat(float.Parse(Next().value.Trim('f', 'F')));
                    break;
                case "double":
                    currentPath.Push("[Nameless double]");
                    response = new NbtDouble(double.Parse(Next().value.Trim('d', 'D')));
                    break;
                case "string":
                    currentPath.Push("[Nameless string]");
                    response = new NbtString(Next().value.Trim('"'));
                    break;
                case "[":
                    currentPath.Push("[Nameless List]");
                    response = NextList();
                    break;
                case "{":
                    currentPath.Push("[Nameless compound]");
                    response = NextCompound();
                    break;
                default:
                    string sterr = $"Unexpected token type {ctoken.tokentype}, expecting one that indicates a value\n{ctoken} @ {pointer}\n";
                    sterr += DumpStack();
                    throw new Exception(sterr);
            }
            currentPath.Pop();
            return response;

        }
        KeyValuePair<string, NbtElement> NextNamed() {
            Token identifier = Eat("identifier");
            currentPath.Push(identifier.value);
            Eat(":");
            var response = new KeyValuePair<string, NbtElement>(identifier.value, NextNameless());
            currentPath.Pop();
            return response;
        }
        public NbtElement Parse() {
            pointer = 0;
            currentPath = new Stack<string>();
            currentPath.Push("[root]");
            return NextNameless();
        }
    }
}
