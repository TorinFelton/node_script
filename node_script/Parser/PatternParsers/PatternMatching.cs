using node_script.Lexer;
using System;
using System.Collections.Generic;
using System.Text;

namespace node_script.Parser.PatternParsers
{
    static class PatternMatching
    {
        public static bool isMatch(string pattern, List<Token> tokens)
        {
            /* This method is for the parsers to check if the next token(s) match a complex pattern.
             * I've written it so I can avoid having to just write out loads of if statements checking
             * for the next tokens to be certain types or values (I had to do this in the NEA).
             * 
             * Pattern e.g.s: 
             * 1. "%IDENTIFIER%_%GRAMMAR(%" for beginning of function call
             * 2. "%TYPE%_%IDENTIFIER% %GRAMMAR=% " for beginning of var definition
             * 3. "%KEYWORDif%_%GRAMMAR(%" for beginning of if statement
             * 4. "%KEYWORDwhile%_%GRAMMAR(%" for beginning of while statements
             * 
             * Note that these cannot cover everything as there are some token patterns that cannot easily be generalised, such as expressions:
             * "int x = 2*(9-1) + y + z*10;"
             * While this function can check for the first part, "%TYPE% %IDENTIFIER% %GRAMMAR=%", it cannot validate the expression.
             * 
             * Definitions:
             * - %IDENTIFIER% Matches any token with .Type == "identifier"
             * - %TYPE% Matches any token with .Type == "identifier" AND that appears in Labels.Types
             * 
             * - %GRAMMARxxx% Matches any token with .Type == "grammar" AND .Value == xxx, 
             *              e.g. %GRAMMAR=% will only match '=' grammar tokens.
             *              
             * - %KEYWORDxxx% Matches any token with .Type == "identifier" AND .Value == xxx,
             *              e.g. %GRAMMARif% will only match 'if' keywords
             *              e.g. %GRAMMARwhile% will only match 'while' keywords
             *              
             * - The '_' underscore character states there MUST be space in between the two items of the pattern
             *   If it is not present, there can be 0 or more space in between these items.
             *   
             * This is a mini-regex parser just to make my life easier. It simply replaces 
             * writing out if token[0].Type == "identifier" && token[1].Type == "identifier" etc etc etc
             */


        }

        public static List<string> TokenisePattern(string pattern)
        {
            
        }
    }
}
