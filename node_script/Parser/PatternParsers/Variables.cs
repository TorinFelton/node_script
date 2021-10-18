using node_script.Lexer;
using node_script.Parser.Steps.Variables;
using System;
using System.Collections.Generic;
using System.Text;

namespace node_script.PatternParsers
{
    static class Variables
    {
        public static List<Func<List<Token>, List<string>, bool>> VariableParsers = new List<Func<List<Token>, List<string>, bool>>() 
        // List of variable-related syntax parsing functions.
        {
            // This list will grow as I add more syntax variants for parsing anything to do with variables.
            Variable_Definition, // e.g int a = 2 + 2;
        };
        public static bool TryParseVariables(List<Token> tokens, List<string> steps)
        {
            return false;
        }

        public static bool Variable_Definition(List<Token> tokens, List<string> steps)
        {
            // Pattern we're looking for:
            // IDENTIFIER IDENTIFIER = EXPR;
            // our IsMatch function cannot match expressions as they are of variable length and token types
            // therefore we can only match the "IDENTIFIER INDEITIFER = " part first:
            List<string> pattern = new List<string>() { "!identifier", "!identifier", "$grammar =" };

            if (PatternParsers.PatternTools.IsMatch(pattern, tokens))
            {
                // we know that this *has* to be a variable declaration now
                VariableDefinition varDef = new VariableDefinition(0);

                varDef.Type = tokens[0].Value;
                varDef.Name = tokens[1].Value;
                // tokens[2] == "="
                // so start index starts at 3 for the expression when grabbing:
                varDef.Expression = PatternTools.GrabDelimitedPattern(3, tokens, new Token("grammar", ";"));

                Console.WriteLine(varDef.ToString());

                tokens.RemoveRange(0, varDef.Expression.Count + 4); // remove all tokens we have eaten up to and including the ';'
                return true;
            }
            return false;
        }
    }
}
