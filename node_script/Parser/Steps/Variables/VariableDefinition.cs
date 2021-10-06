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
        public List<Token> Expression; // value of variable but not yet resolved

        public VariableDefinition() { }

        public VariableDefinition(string type, string name, List<Token> expr)
        {
            Type = type; Name = name; Expression = expr;
        }
    }
}
