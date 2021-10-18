using node_script.Lexer;
using node_script.Parser.Steps;
using node_script.Parser.Steps.Variables;
using System;
using System.Collections.Generic;
using System.Text;

namespace node_script.PatternParsers
{
    static class Variables
    {
        public static List<Func<List<Token>, List<Step>, bool>> variableParsers = new List<Func<List<Token>, List<Step>, bool>>() 
        // List of variable-related syntax parsing functions.
        {
            // This list will grow as I add more syntax variants for parsing anything to do with variables.
            Variable_Definition, // e.g int a = 2 + 2;
            Variable_Change,     // e.g a = 2;
        };
        public static bool TryParseVariables(List<Token> tokens, List<Step> steps)
        {
            int i = 0;
            while (
                  i < variableParsers.Count             // while we have not ran out of parsers to try and
               && !variableParsers[i](tokens, steps)    // we have not found a parser that has succeeded
                  ) i++;
            // when we reach this point, one of two things must have happened:
            // 1. We didn't find a parser that managed to parse the token pattern we must return false, thus i == variableParser.Count must be TRUE.
            // exclusively OR:
            // 2. We found a parser that successfully parsed the token pattern and the loop ended because of it.
            

            return !(i == variableParsers.Count); // if i == variableParsers.Count is TRUE then we have failed and must return FALSE.
        }

        public static bool Variable_Definition(List<Token> tokens, List<Step> steps)
        {
            // Pattern we're looking for:
            // IDENTIFIER IDENTIFIER = EXPR;
            // our IsMatch function cannot match expressions as they are of variable length and token types
            // therefore we can only match the "IDENTIFIER INDEITIFER = " part first:
            List<string> pattern = new List<string>() { "!identifier", "!identifier", "$grammar =" };

            if (!PatternTools.IsMatch(pattern, tokens)) return false; // if there is no match then return false and don't execute anything after.
            
            // we know that this *has* to be a variable declaration now
            VariableDefinition varDef = new VariableDefinition(0);

            varDef.Type = tokens[0].Value;
            varDef.Name = tokens[1].Value;
            // tokens[2] == "="
            // so start index starts at 3 for the expression when grabbing:
            varDef.Expression = PatternTools.GrabDelimitedPattern(3, tokens, new Token("grammar", ";"));

            steps.Add(varDef);

            tokens.RemoveRange(0, 3 + varDef.Expression.Count + 1); // remove all tokens we have eaten up to and including the ';'

            return true;
        }

        public static bool Variable_Change(List<Token> tokens, List<Step> steps)
        {
            // Pattern we're looking for:
            // IDENTIFIER = EXPR;

            List<string> pattern = new List<string>() { "!identifier", "$grammar =" };

            if (!PatternTools.IsMatch(pattern, tokens)) return false; // if no match then return false and exit this block

            VariableChange varChange = new VariableChange(
                0, 
                tokens[0].Value, // the first token is the IDENTIFIER type that contains the variable's name
                PatternTools.GrabDelimitedPattern(2, tokens, new Token("grammar", ";"))); // grab the expression that begins after the '=' token and stops when you find a ';'

            steps.Add(varChange);

            tokens.RemoveRange(0, 2 + varChange.Expression.Count + 1); // Remove all tokens that we have just eaten up to and including the ';'

            return true;
        }
    }
}
