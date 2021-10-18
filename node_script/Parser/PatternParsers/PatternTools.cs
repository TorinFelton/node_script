using node_script.Lexer;
using System;
using System.Collections.Generic;
using System.Text;

namespace node_script.PatternParsers
{
    public static class PatternTools
    {
        public static bool IsMatch(List<string> pattern, List<Token> tokens)
        {
            // This function just takes a list of token types and tokens and checks they match up in the right order (return true if they do)
            // NOTE: if pattern is empty this will return true.

            int i = 0; // index for position in tokens list
            foreach (string patternElement in pattern)
            {
                if (tokens.Count <= i) return false; // i is out of range but still pattern elements left, therefore doesn't fit pattern.

                // if the next token does not match the next pattern element, then return false
                // ! is for the pattern "<TOK TYPE>"
                if (patternElement[0] == '!'
                    && patternElement.Substring(1) != tokens[i].Type) return false;

                // $ is for the pattern "<TOK TYPE> <TOK VALUE>"
                if (patternElement[0] == '$')
                {
                    string[] split = patternElement.Substring(1).Split(' ');
                    if (split[0] != tokens[i].Type || split[1] != tokens[i].Value) return false;
                }

                i++;

            }
            return true;

        }

        public static List<Token> GrabDelimitedPattern(int index, List<Token> tokens, Token delimiter)
        {
            List<Token> toReturn = new List<Token>();
            while (index < tokens.Count && !tokens[index].Matches(delimiter.Type, delimiter.Value))
            {
                toReturn.Add(tokens[index]);
                index++;
            }

            if (!tokens[index].Matches(delimiter.Type, delimiter.Value)) throw new MissingDelimiterError(delimiter.Value, 0);

            return toReturn;
        }

    }
}
