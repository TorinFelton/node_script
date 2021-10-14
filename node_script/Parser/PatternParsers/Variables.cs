using node_script.Lexer;
using node_script.Parser.Steps.Variables;
using System;
using System.Collections.Generic;
using System.Text;

namespace node_script.PatternParsers
{
    static class Variables
    {
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

            if (PatternParsers.PatternMatching.IsMatch(pattern, tokens))
            {
                // we know that this *has* to be a variable declaration now
                VariableDefinition varDef = new VariableDefinition();

                varDef.Type = tokens[0].Value;
                varDef.Name = tokens[1].Value;
                // tokens[2] == "="
                // so start i at 3
                int i = 3;
                while (i < tokens.Count && !tokens[i].Matches("grammar", ";"))
                {
                    varDef.Expression.Add(tokens[i]);
                    i++;
                }

                if (!tokens[i].Matches("grammar", ";")) throw new MissingSemiColError(0);

                Console.WriteLine(varDef.ToString());
                return true;
            }
            return false;
        }
    }
}
