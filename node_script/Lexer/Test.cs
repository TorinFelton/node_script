using System;
using System.Collections.Generic;
using System.Text;

namespace node_script.Lexer
{
    static class Test
    {
        public static void Test1()
        {
            //string contents = System.IO.File.ReadAllText("C:\\Users\\torin\\Source\\Repos\\node_script\\node_script\\test1.txt");
            string contents = Console.ReadLine();
            List<Token> tokens = new List<Token>();


            foreach (Token tok in Tokeniser.Tokenise(contents)) 
            {
                tokens.Add(tok);
            }

            while (tokens.Count > 0)
            Console.WriteLine(PatternParsers.Variables.Variable_Definition(tokens, new List<string>() { }));

        }
    }
}
