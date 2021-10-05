using node_script.Lexer;
using System;
using System.Collections.Generic;
using System.Text;

namespace node_script.Parser
{
    static class Parser
    {
        /* This 'Parsers' list is a way of separating all the different parsers for syntax features.
         * For example, there is a general parser for variable manipulation syntax, and another for control flow syntax.
         * This differs from the NEA project, which was just a series of if statements for checking each syntax.
         */
        public static List<Func<Queue<Token>, bool>> Parsers = new List<Func<Queue<Token>, bool>>()
        {
            PatternParsers.ControlFlow.TryParseControlFlow,
            PatternParsers.Variables.TryParseVariables
        };
    }
}
