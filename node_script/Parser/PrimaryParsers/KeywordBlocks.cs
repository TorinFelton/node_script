using node_script.Lexer;
using node_script.Parser.Steps;
using node_script.Parser.Steps.ControlFlow;
using System;
using System.Collections.Generic;
using System.Text;

namespace node_script.PrimaryParsers
{
    static class KeywordBlocks
        // This is a primary parser to group up block keywords (e.g. 'if', 'while', etc.) and also separately parse any code blocks (anything in between brackets '(', '{', '[')
        // These parsers are parsing the COMPONENTS of if statements, while loops, etc., not the actual loops entirely - that is done by the secondary parsers.
    {
        public static List<Func<List<Token>, List<Step>, bool>> KeywordBlockParsers = new List<Func<List<Token>, List<Step>, bool>>() // "List of functions that take arguments: List<Token>, List<Step> and return a bool
        // List of variable-related syntax parsing functions.
        {
            // This list will grow as I add more syntax variants for parsing anything to do with control flow.
            KeywordParser,
            BlockParser
        };
        public static bool TryParseKeywordBlocks(List<Token> tokens, List<Step> steps)
        {
            return ParserTools.TryPrimaryParse(KeywordBlockParsers, tokens, steps);
        }

        public static bool BlockParser(List<Token> tokens, List<Step> steps)
        {
            if (!Labels.BlockOpeners.Contains(tokens[0].Value)) return false;
            // assembly-style optimisation (inversing conditions to require less branch instructions)
            // essentially exit this branch if the value required is not found

            // if we have got past that initial check then we must be at a block opening.

            int index = Labels.BlockOpeners.IndexOf(tokens[0].Value);
            // get index of the opener char
            // so for example if tokens[0].Value == '{'
            // then in Labels.BlockOpeners "{([" it is at index 0
            // therefore its corresponding closer is at index 0 in Labels.BlockClosers "})]"
            string opener = Labels.BlockOpeners[index].ToString();
            string closer = Labels.BlockClosers[index].ToString();

            ParserTools.PopToken(tokens); // pop the '{' at the beginning because it is no longer needed.

            List<Token> blockTokens = ParserTools.GrabNestedPattern(0, tokens,
                new Token("grammar", opener), new Token("grammar", closer));
            // grabs all tokens in between the opener and closer, accounting for nesting
            // e.g.
            // something {
            //  a = b;
            //  something else {
            //    c = d;
            //  }
            // }
            // it will grab "a = b; something else { c = d; }" (as tokens) in this case

            steps.Add(
                new Block(0, blockTokens, (opener, closer))
            );
            // add a new Block Step object and parse the contents of it
            // this is recursive in that any nested blocks inside the block will be parsed recursively descending until the highest nest level

            return true;
        }

        public static bool KeywordParser(List<Token> tokens, List<Step> steps)
        {
            if (!Labels.Keywords.Contains(tokens[0].Value)) return false; // If the token value is not a keyword then return false

            steps.Add(new Keyword(0, tokens[0].Value)); // If we make it this far, we can guarantee it is a keyword, so just add it.

            return true;
        }
    }
}
