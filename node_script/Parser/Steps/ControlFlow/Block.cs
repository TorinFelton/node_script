using node_script.Lexer;
using System;
using System.Collections.Generic;
using System.Text;

namespace node_script.Parser.Steps.ControlFlow
{
    public class Block : Step
    {
        public List<Token> Contents = new List<Token>();
        public (string, string) Delimiters;

        public Block(int linePosition) : base(linePosition) { }

        public Block(int linePosition, List<Token> blockContents, (string, string) delimiterTokens) : base(linePosition) { Contents = blockContents; delimiterTokens = Delimiters; }

        public override string ToString()
        {
            string output = "";
            foreach (Token tok in Contents) output += "\n" + tok.ToString();
            return $"CODE BLOCK: {Delimiters.Item1} {output} {Delimiters.Item2}\nENDBLOCK";
        }
    }
}
