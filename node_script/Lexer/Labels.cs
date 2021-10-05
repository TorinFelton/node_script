using System;
using System.Collections.Generic;
using System.Text;

namespace node_script.Lexer
{
    static class Labels
    {
        public static string Operators = "+-*/^%"; // Operation tokens
        public static string StringDelimiters = " \"' "; // Uses single and double quotes
        public static string Numeric = "0123456789."; // All chars that could be found in a 'number', including decimal point '.'
        public static string IdentifierPattern = "[a-z][A-Z]"; // Char pattern for the identifiers. Only supports alphabet for now.
        public static string Grammar = "(){};=<>!|&,"; // Special grammar characters
    }
}
