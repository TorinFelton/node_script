using node_script.Lexer;
using node_script.Parser.Steps;
using node_script.Parser.Steps.ControlFlow;
using System;
using System.Collections.Generic;
using System.Text;

namespace node_script.PatternParsers
{
    static class ControlFlow
    {
        public static List<Func<List<Token>, List<Step>, bool>> ControlFlowParsers = new List<Func<List<Token>, List<Step>, bool>>() // "List of functions that take arguments: List<Token>, List<Step> and return a bool
        // List of variable-related syntax parsing functions.
        {
            // This list will grow as I add more syntax variants for parsing anything to do with control flow.
            IfStatementParser
        };
        public static bool TryParseControlFlow(List<Token> tokens, List<Step> steps)
        {
            return ParserTools.TryParse(ControlFlowParsers, tokens, steps);
        }

        public static bool IfStatementParser(List<Token> tokens, List<Step> steps)
        {
            // Pattern we're looking for that begins an if statement:
            List<string> pattern = new List<string>() { "$identifier if", "$grammar ("};
            // "if ("

            if (!ParserTools.IsMatch(pattern, tokens)) return false; // if there is no match then exit the code block
            ParserTools.PopToken(tokens);
            ParserTools.PopToken(tokens); // pop off the 'if' and '('

            IfStatement ifStatem = new IfStatement(0);

            ifStatem.IfCondition = ParserTools.GrabNestedPattern(0, tokens, 
                new Token("grammar", "("), new Token("grammar", ")"));
            // Use the GrabNestedPattern tool to grab everything inside the if statement's ( and  )
            // GrabNestedPattern takes into account nested bracket statements.

            if (!ParserTools.PopToken(tokens).Matches("grammar", "{")) throw new MissingTokenError("{", 0);

            List<Token> blockTokens = ParserTools.GrabNestedPattern(0, tokens,
                new Token("grammar", "{"), new Token("grammar", "}")); // grab everything inside { and }


            ifStatem.BlockContents = Parser.Parser.Parse(blockTokens); // parse the contents of the if statement code block

            steps.Add(ifStatem);

            return true;
        }
    }
}
