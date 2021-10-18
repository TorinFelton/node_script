using System;
using System.Collections.Generic;
using System.Text;

namespace node_script.Lexer
{
    static class Test
    {
        public static void Test1()
        {
            string contents = Console.ReadLine();


            foreach (Token tok in Tokeniser.Tokenise(contents)) 
                Console.WriteLine(tok.ToString());
            

        }
    }
}
