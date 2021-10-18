using node_script.Lexer;
using System;
using System.Collections.Generic;
using System.Text;

namespace node_script.Parser.Steps.Variables
{
    class VariableChange : Step
    {
        private int position;
        public string Name;
        public List<Token> Expression = new List<Token>(); // value of variable but not yet resolved

        public VariableChange(int position) : base(position) { }

        public VariableChange(int position, string name, List<Token> expr) : base(position)
        {
            Name = name; Expression = expr;
        }

        public override string ToString()
        {
            string exprContents = "";
            foreach (Token tok in Expression) exprContents += tok.Value + " ";
            return $"VAR CHANGE: {Name} changes to expression: '{exprContents}'";
        }
    }
}
