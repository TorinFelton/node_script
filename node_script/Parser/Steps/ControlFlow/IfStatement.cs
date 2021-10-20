using node_script.Lexer;
using System;
using System.Collections.Generic;
using System.Text;

namespace node_script.Parser.Steps.ControlFlow
{
    public class IfStatement : Step
    {
        public List<Token> IfCondition = new List<Token>(); // if (xxxxx) {}
        public List<Step> BlockContents = new List<Step>();    // if () { xxxxx }

        public IfStatement(int position) : base(position) { }

        public IfStatement(int position, List<Token> condition, List<Step> contents) : base(position)
        {
            IfCondition = condition; BlockContents = contents;
        }

        public override string ToString()
        {
            string contents = "";
            foreach (Step step in BlockContents) contents += "\n" + step.ToString();
            return $"IF STATEMENT: if ({IfCondition}) {{ {contents} }}";
        }
    }
}
