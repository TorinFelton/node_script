using node_script.Lexer;
using System;
using System.Collections.Generic;
using System.Text;

namespace node_script.Parser.Steps.ControlFlow
{
    public class Block : Step
    {
        public List<Step> Contents = new List<Step>();
        public (string, string) Delimiters;

        public Block(int linePosition) : base(linePosition) { }

        public Block(int linePosition, List<Step> blockContents, (string, string) delimiterTokens) : base(linePosition) { Contents = blockContents; delimiterTokens = Delimiters; }

        public override string ToString()
        {
            string output = "";
            foreach (Step step in Contents) output += "\n" + step.ToString();
            return $"CODE BLOCK: {Delimiters.Item1} {output} {Delimiters.Item2}";
        }
    }
}
