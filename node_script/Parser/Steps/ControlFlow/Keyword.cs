using System;
using System.Collections.Generic;
using System.Text;

namespace node_script.Parser.Steps.ControlFlow
{
    public class Keyword : PseudoStep
    {
        public string Value;

        public Keyword(int linePosition, string keywordName) : base(linePosition) { Value = keywordName; }

        public override string ToString()
        {
            return $"KEYWORD '{Value}'";
        }
    }
}
