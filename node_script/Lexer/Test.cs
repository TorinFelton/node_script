﻿using System;
using System.Collections.Generic;
using System.Text;

namespace node_script.Lexer
{
    static class Test
    {
        public static void Test1()
        {
            string contents = System.IO.File.ReadAllText("C:\\Users\\torin\\Source\\Repos\\TorinFelton\\node_script\\node_script\\test1.txt");

            foreach (Token tok in Tokeniser.Tokenise(contents)) 
            {
                Console.WriteLine(tok.ToString());
            }
        }
    }
}
