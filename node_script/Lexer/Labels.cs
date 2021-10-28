using System;
using System.Collections.Generic;
using System.Text;

namespace node_script.Lexer
{
    static class Labels
    {
        public const string Operators = "+-*/^%"; // Operation tokens

        public const string StringDelimiters = " \"' "; // Uses single and double quotes

        public const string Numeric = "0123456789."; // All chars that could be found in a 'number', including decimal point '.'

        public const string IdentifierPattern = "[a-zA-Z]"; // Char pattern for the identifiers. Only supports alphabet for now.

        public const string SingleGrammar = ";<>!|," + BlockOpeners + BlockClosers; // Special grammar chars that are tokenised individually (including brackets, hence the BlockOpeners/Closers)

        public const string FlexGrammar = "=&"; // Special grammar characters that are to be tokenised together

        public const string Keywords = "if else when node func for while loop"; // Keywords for language, so that they are not confused with variable names

        public const string Types = "int float string bool";

        public const string BlockOpeners = "{([";
        public const string BlockClosers = "})]"; // note the indexes match up, so BlockOpeners[0] == { and BlockClosers[0] == }
    }
}
