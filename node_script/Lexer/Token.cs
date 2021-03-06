using System;
using System.Collections.Generic;
using System.Text;

namespace node_script.Lexer
{
    public class Token
    {
        public string Type;
        public string Value;
        public Token(string type, string value)
        {
            Type = type; Value = value;
        }

        public Token() { }

        public bool Matches(string type, string value) => this.Type == type && this.Value == value;
        public override string ToString()
        {
            return "(" + Type + ", '" + Value + "')";
        }
    }
}
