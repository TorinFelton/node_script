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

            

            foreach (Step step in Parser.Parser.Parse(tokens))
            {
                Console.WriteLine(step.ToString());
            }
        }
    }
}
