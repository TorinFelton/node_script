﻿using node_script.Lexer;
using System;
using System.Collections.Generic;
using System.Text;

namespace node_script.Parser
{
    static class Parser
    {
        /* This 'Parsers' list is a way of separating all the different parsers for syntax features.
         * For example, there is a parser for variable manipulation syntax, and another for control flow syntax.
         * 
         * This differs from the NEA project, which was just one large consisting of a series of if statements for checking each syntax expression.
         * 
         * The idea for each of these separate parser methods being put into a list and dynamically called is that they return a boolean:
         * - The boolean is false if all their checks for any syntax they are able to parse returns none.
         * - The boolean is true if they found some syntax they were able to parse and did so.
         *
         * By 'able to parse', it means that the tokens are in the pattern they are looking for. If they are not able to parse it, it does NOT mean
         * that there is an error in the syntax, just instead that another parser should have a go. 
         * If none of the parsers return true ever, there is either no syntax or it is unrecognised.
         */
        public static List<Func<Queue<Token>, bool>> Parsers = new List<Func<Queue<Token>, bool>>()
        {
            // This list will grow longer the more parsers I add.
            // I have chosen this method so that I can dynamically add/remove parsers for specific sets of syntax expressions.
            PatternParsers.ControlFlow.TryParseControlFlow,
            PatternParsers.Variables.TryParseVariables,
            PatternParsers.Functions.TryParseFunctions,
            PatternParsers.Nodes.TryParseNodes
        };

        // TODO: Step type, rm 'void'
        public static void Parse(Queue<Token> tokenQueue)
        {
            bool keepParsing = true;

            while (keepParsing && tokenQueue.Count > 0)
            {
                keepParsing = false; // Assume we should stop trying to parse anything after this unless we can move on

                foreach (Func<Queue<Token>, bool> parser in Parsers)
                {
                    if (parser(tokenQueue)) keepParsing = true;
                    // Try parsing using each parser and if they return true (successful) then keep the parent loop going.
                }
            }

            // If we exited the loop because keepParsing was false BUT there were still tokens in the queue, it means we had syntax that none of our parsers understood.
            if (tokenQueue.Count > 0) throw new UnrecognisedSyntaxError(0); // TODO: Figure out line tracing method.
        }
    }
}
