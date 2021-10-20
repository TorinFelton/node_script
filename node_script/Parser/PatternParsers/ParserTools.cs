using node_script.Lexer;
using node_script.Parser.Steps;
using System;
using System.Collections.Generic;
using System.Text;

namespace node_script.PatternParsers
{
    public static class ParserTools
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

            if (index == tokens.Count || !tokens[index].Matches(delimiter.Type, delimiter.Value)) throw new MissingDelimiterError(delimiter.Value, 0);

            return toReturn;
        }

        public static List<Token> GrabNestedPattern(int index, List<Token> tokens, Token Opener, Token Closer)
        {
            // Grab everything inside (including nesting) the opener and closer tokens
            /* E.g. 
             * Opener = new Token("grammar", "{"), Closer = new Token("grammar", "}")
             * This example will grab everything inside the { and }, accounting for nesting:
             * 
             * {
             *   1111
             *   { 
             *      2222
             *      {
             *          3333
             *      }
             *   }
             *   4444
             * }
             * 
             * will be grabbed as "1111 { 2222 { 3333 } } 4444"
             * 
             * This is similar to GrabDelimitedPattern except for that it won't necessarily stop 
             * grabbing when it finds the 'delimiter', and instead will check if it is currently in a nested
             * statement.
             */

            int nestingIndex = 0;
            // For every Opener we find, nestingIndex++
            // For every Closer we find, nestingIndex--

            

            List<Token> toReturn = new List<Token>();

            while (nestingIndex != -1)
            {
                toReturn.Add(tokens[index]);

                if (tokens[index] == Opener) nestingIndex++;
                else if (tokens[index] == Closer) nestingIndex--;

                index++;

                if (index == tokens.Count) throw new MissingDelimiterError(Closer.Value, 0);
                // if we reach the end without enough Closers found, throw error.
                // This will be caused by something like imbalanced brackets.
            }

            return toReturn;
        }

        public static bool TryParse(List<Func<List<Token>, List<Step>, bool>> parsers, List<Token> tokens, List<Step> steps)
        {
            int i = 0;

            while (
                  i < parsers.Count             // while we have not ran out of parsers to try and
               && !parsers[i](tokens, steps)    // we have not found a parser that has succeeded
                  ) i++;
            // when we reach this point, one of two things must have happened:
            // 1. We didn't find a parser that managed to parse the token pattern so we must return false, thus i == parser.Count must be TRUE.
            // XOR:
            // 2. We found a parser that successfully parsed the token pattern and the loop ended because of it.


            return !(i == parsers.Count); // if i == variableParsers.Count is TRUE then we have failed and must return FALSE.
        }
    }
}
