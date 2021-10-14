using node_script.Lexer;
using System;
using System.Collections.Generic;
using System.Text;

namespace node_script.Parser.PatternParsers
{
    static class PatternMatching
    {
        public static bool isMatch(List<string> pattern, List<Token> tokens)
        {
            // This function just takes a list of token types and tokens and checks they match up in the right order (return true if they do)
            // NOTE: if pattern is empty this will return true.

            int i = 0; // index for position in tokens list
            foreach (string patternElement in pattern)
            {
                if (tokens.Count >= i) return false; // i is out of range but still pattern elements left, therefore doesn't fit pattern.

                // if the next token does not match the next pattern element, then return false
                if (patternElement[0] == '!'
                    && patternElement.Substring(1) != tokens[i].Type) return false;

                i++;

            }
            return true;

        }

    }
}
