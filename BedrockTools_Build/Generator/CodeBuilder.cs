using System;
using System.Collections.Generic;
using System.Text;

namespace BedrockTools_Build.Generator {
    public class CodeBuilder {
        StringBuilder stringBuilder;
        public int StateTab { get; set; }
        public CodeBuilder(int tabulation=0) {
            stringBuilder = new StringBuilder();
            StateTab = tabulation;
        }
        public CodeBuilder Tab(int tabulation) {
            for (int i=0; i<4*tabulation;i++) {
                stringBuilder.Append(" ");
            }
            return this;
        }
        public CodeBuilder Write(string code) {
            stringBuilder.Append(code);
            return this;
        }
        public CodeBuilder EndLine() {
            stringBuilder.Append("\n");
            return this; 
        }

        public CodeBuilder WriteLine(string code, int tabulation) {
            Tab(tabulation);
            stringBuilder.Append(code);
            stringBuilder.Append("\n");
            return this;
        }
        public CodeBuilder WriteLine(string code) {
            Tab(StateTab);
            stringBuilder.Append(code);
            stringBuilder.Append("\n");
            return this;
        }
        public CodeBuilder Ident() {
            StateTab++;
            return this;
        }
        public CodeBuilder Deident() {
            StateTab--;
            if (StateTab < 0) 
                throw new ArgumentOutOfRangeException();
            return this;
        }
        public CodeBuilder StartLine() {
            Tab(StateTab);
            return this;
        }
        public override string ToString() {
            return stringBuilder.ToString();
        }

        public string GetTabString() {
            StringBuilder s = new StringBuilder();
            for (int i = 0; i < StateTab; i++)
                s.Append("    ");
            return s.ToString();
        }
    }
}
