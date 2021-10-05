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

        public const string SingleGrammar = "(){};<>!|,"; // Special grammar chars that are tokenised individually

        public const string FlexGrammar = "=&"; // Special grammar characters that are to be tokenised together
    }
}
