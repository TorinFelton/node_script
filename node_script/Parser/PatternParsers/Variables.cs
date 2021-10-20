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
        public static List<Func<List<Token>, List<Step>, bool>> VariableParsers = new List<Func<List<Token>, List<Step>, bool>>() // "List of functions that take arguments: List<Token>, List<Step> and return a bool
        // List of variable-related syntax parsing functions.
        {
            // This list will grow as I add more syntax variants for parsing anything to do with variables.
            VariableDefinitionParser, // e.g int a = 2 + 2;
            VariableChangeParser,     // e.g a = 2;
        };
        public static bool TryParseVariables(List<Token> tokens, List<Step> steps)
        {
            return ParserTools.TryParse(VariableParsers, tokens, steps);
        }

        public static bool VariableDefinitionParser(List<Token> tokens, List<Step> steps)
        {
            // Pattern we're looking for:
            // IDENTIFIER IDENTIFIER = EXPR;
            // our IsMatch function cannot match expressions as they are of variable length and token types
            // therefore we can only match the "IDENTIFIER INDEITIFER = " part first:
            List<string> pattern = new List<string>() { "!identifier", "!identifier", "$grammar =" };

            if (!ParserTools.IsMatch(pattern, tokens)) return false; // if there is no match then return false and don't execute anything after.
            
            // we know that this *has* to be a variable declaration now
            VariableDefinition varDef = new VariableDefinition(0);

            varDef.Type = ParserTools.PopToken(tokens).Value;
            varDef.Name = ParserTools.PopToken(tokens).Value;


            // tokens[2] == "=" so we need to pop it:
            ParserTools.PopToken(tokens);

            varDef.Expression = ParserTools.GrabDelimitedPattern(tokens, new Token("grammar", ";"));

            steps.Add(varDef);

            return true;
        }

        public static bool VariableChangeParser(List<Token> tokens, List<Step> steps)
        {
            // Pattern we're looking for:
            // IDENTIFIER = EXPR;

            List<string> pattern = new List<string>() { "!identifier", "$grammar =" };

            if (!ParserTools.IsMatch(pattern, tokens)) return false; // if no match then return false and exit this block

            string name = ParserTools.PopToken(tokens).Value;
            ParserTools.PopToken(tokens); // remove the "=" token

            VariableChange varChange = new VariableChange(
                0, 
                name, // the first token is the IDENTIFIER type that contains the variable's name
                ParserTools.GrabDelimitedPattern(tokens, new Token("grammar", ";"))); // grab the expression that begins after the '=' token and stops when you find a ';'

            steps.Add(varChange);

            return true;
        }
    }
}
