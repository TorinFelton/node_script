using System;
using System.Collections.Generic;
using System.Text;

namespace node_script.Parser.Steps.ControlFlow
{
    class Keyword : PseudoStep
    {
        public string Value;
        public List<string> Expecting = new List<string>();

        public Keyword(int linePosition, string keywordName, List<string> expectingAfter) : base(linePosition) { Value = keywordName; Expecting = expectingAfter; }

        public override string ToString()
        {
            string output = "";
            foreach (string expected in Expecting) output += expected;
            return $"KEYWORD '{Value}' expecting to be followed by: {output}";
        }
    }
}
