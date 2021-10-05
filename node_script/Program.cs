using node_script.Lexer;
using node_script.Parser;
using System;
using System.Collections.Generic;

namespace node_script
{
    class Program
    {
        static void Main(string[] args)
        {
            //Lexer.Test.Test1();

            List<Func<Queue<Token>, bool>> funcs = new List<Func<Queue<Token>, bool>>() {
                Parser.StepPatterns.Variables.TryParseVariables, 
                Parser.StepPatterns.ControlFlow.TryParseControlFlow
            };

            Queue<Token> q = new Queue<Token>();

            foreach (Func<Queue<Token>, bool> f in funcs)
            {
                Console.WriteLine(f(q));
            }

            
        }
    }
}
