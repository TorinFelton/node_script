using node_script.Lexer;
using System;
using System.Collections.Generic;
using System.Text;

namespace node_script.Parser.Steps.Variables
{
    class VariableDefinition
    {
        public string Type;
        public string Name;
        public List<Token> Expression = new List<Token>(); // value of variable but not yet resolved

        public VariableDefinition() { }

        public VariableDefinition(string type, string name, List<Token> expr)
        {
            Type = type; Name = name; Expression = expr;
        }

        public override string ToString()
        {
            string exprContents = "";
            foreach (Token tok in Expression) exprContents += tok.Value + " ";
            return Type + " " + Name + " " + exprContents;
        }
    }
}
