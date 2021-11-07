using node_script.Lexer;
using node_script.Parser.Steps;
using node_script.Parser.Steps.ControlFlow;
using node_script.PrimaryParsers;
using System;
using System.Collections.Generic;
using System.Text;

namespace node_script.Parser.SecondaryParsers
{
    static class ControlFlow
    {
        public static List<Func<List<Step>, bool>> ControlFlowParsers = new List<Func<List<Step>, bool>>() // "List of functions that take arguments: List<Token>, List<Step> and return a bool
        // List of variable-related syntax parsing functions.
        {
            // This list will grow as I add more syntax variants for parsing anything to do with variables.
            IfStatementParser,
        };
        public static bool TryParseControlFlow(List<Token> tokens, List<Step> steps)
        {
            return ParserTools.TrySecondaryParse(ControlFlowParsers, steps);
        }

        public static bool IfStatementParser(List<Step> steps)
        {
            int i = -1;

            List<Step> reparsedSteps = new List<Step>();

            foreach (Step step in steps)
            {
                i++;
                if (!(step is Keyword) || ((Keyword) step).Value != "if") 
                    // add to replacement list for steps and move to next element if not a keyword or not the 'if' keyword
                {
                    reparsedSteps.Add(step);
                    continue;
                }
                
                if (steps.Count - (i + 1) < 2 || !(steps[i + 1] is Block) || !(steps[i + 2] is Block)) throw new MissingParsingStep("IF_CONDITION, IF_CONTENTS", 0);
                // throw an error if there are not 2 more elements left of the list after the ith element.
                // we do this as after the 'if' keyword we expect a condition block and contents block:
                // if (condition) { contents }

                Block ifCondition = (Block)steps[i + 1];
                Block ifContents = (Block)steps[i + 2];

                if (ifCondition.Delimiters != ("(", ")")) throw new MissingDelimiterError("(", 0); // if the first block doesn't begin with (, throw error
                if (ifContents.Delimiters != ("{", "}")) throw new MissingDelimiterError("{", 0);  // if the second block doesn't begin with {, throw error

                IfStatement ifStatementStep = new IfStatement(0);
                ifStatementStep.IfCondition = ifCondition.Contents;
                ifStatementStep.BlockContents = Parser.Parse(ifContents.Contents); // recursively parse the contents of the code block {}

                // now we add the secondary-parser Step 'ifStatementStep' into the newly formed steps list.
                reparsedSteps.Add(ifStatementStep);
            }

            steps = reparsedSteps;
            return true;
        }
    }
}
