using node_script.Lexer;
using System;
using System.Collections.Generic;
using System.Text;

namespace node_script.Parser.Steps.Variables
{
    class VariableDefinition : Step
    {
        private int position;
        public string Type;
        public string Name;
        public List<Token> Expression = new List<Token>(); // value of variable but not yet resolved

        public VariableDefinition(int position) : base(position) { }

        public VariableDefinition(int position, string type, string name, List<Token> expr) : base(position)
        {
            Type = type; Name = name; Expression = expr;
        }

        public override string ToString()
        {
            string exprContents = "";
            foreach (Token tok in Expression) exprContents += tok.Value + " ";
            return $"VAR DEFINITION: {Type} {Name} expression: '{exprContents}'";
        }
    }
}
