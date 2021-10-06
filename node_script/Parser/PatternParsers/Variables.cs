using node_script.Lexer;
using node_script.Parser.Steps.Variables;
using System;
using System.Collections.Generic;
using System.Text;

namespace node_script.PatternParserss
{
    static class Variables
    {
        public static bool TryParseVariables(List<Token> tokens, List<string> steps)
        {
            
        }

        public static bool Variable_Definition(List<Token> tokens, List<string> steps)
        {
            // Pattern we're looking for:
            // IDENTIFIER IDENTIFIER = EXPR;
            // "If the first token is an identifier and in the Types list, and the second token is also an identifier"
            if (tokens[0].Type == "identifier" 
                && Labels.Types.Contains(tokens[0].Value.ToLower())
                && tokens[1].Type == "identifier"
                && tokens[2].)
            {
                // We now know we have found the beginning of a variable definition
                int front_pointer = 0; // To use later so we can remove the tokens we've used up from the 'tokens' list

                VariableDefinition varDef = new VariableDefinition();
                // Initialise var def Step object to fill with information about this variable definition
                varDef.Type = tokens[0].Value;
                varDef.Name = tokens[1].Value;

                front_pointer = 2;
            }
        }
    }
}
