using System;
using System.Collections.Generic;
using System.Text;

namespace node_script.Lexer
{
    class DecimalSyntaxError : Exception
    {
        public DecimalSyntaxError(string value, int charPos, int linePos) : base()
        {
            Error.ShowError("DecimalSyntaxError (Line: " + linePos.ToString() + ", Char: " + charPos.ToString() +
                "): Could not tokenise this number correctly: " + value);
        }
    }

    static class Error // direct copy from NEA
    {
        public static void ShowError(string err) // Pause program (input prompt) then kill it.
        {

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(err);
            Console.ForegroundColor = ConsoleColor.Gray;

            if (!((new System.Diagnostics.StackTrace()).ToString().ToLower().Contains("shell")))
            // If this is NOT running in a Shell instance.
            {
                Console.ReadLine(); // Used to pause it, Enter required to move on
                Environment.Exit(1); // Exits process, killing program after pause from ReadLine()
            }
            // else if it is running in a Shell instance, then let the shell catch the error and restart.
        }
    }
}
