using node_script.Lexer;
using node_script.Parser.Steps;
using node_script.PrimaryParsers;
using System;
using System.Collections.Generic;
using System.Text;

namespace node_script.Parser.SecondaryParsers
{
    static class ControlFlow
    {
        public static List<Func<List<Token>, List<Step>, bool>> ControlFlowParsers = new List<Func<List<Token>, List<Step>, bool>>() // "List of functions that take arguments: List<Token>, List<Step> and return a bool
        // List of variable-related syntax parsing functions.
        {
            // This list will grow as I add more syntax variants for parsing anything to do with variables.
            WhileLoopParser,
        };
        public static bool TryParseControlFlow(List<Token> tokens, List<Step> steps)
        {
            return ParserTools.TryPrimaryParse(ControlFlowParsers, tokens, steps);
        }

        public static bool WhileLoopParser(List<Token> tokens, List<Step> steps)
        {
            // we don't actually need the List<Token> but for using the dynamic parsing (ParserTools.TryParse) we have defined them all to take it as an argument. 
            foreach ()
        }
    }
}
