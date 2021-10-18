using node_script.Lexer;
using node_script.Parser;
using node_script.Parser.Steps;
using System;
using System.Collections.Generic;
using System.Linq;

namespace node_script
{
    class Program
    {
        static void Main(string[] args)
        {
            //Lexer.Test.Test1();
            string contents = Console.ReadLine();
            List<Token> tokens = Tokeniser.Tokenise(contents).ToList();
            List<Step> steps = new List<Step>();

            Parser.Parser.Parse(tokens, steps);

            foreach (Step step in steps)
            {
                Console.WriteLine(step.ToString());
            }
        }
    }
}
