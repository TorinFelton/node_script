using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace node_script.Lexer
{
    static class Tokeniser
    {
        /* I've chosen to make this static 
         * which is a change from the original NEA
         * project. I think a initiating a Tokeniser object
         * each time you need to tokenise a string is unnecessary.
         */
        public static IEnumerable<Token> Tokenise(string contents)
        {
            /* I originally planned to make this a more dynamic tokeniser, going through a list of functions to check/grab tokens, 
             * so that I could add / remove token types instead of just hard writing if statements. I have not finalised that design though,
             * so hard-writing checks is what I will use.
             */

            Queue<char> charQ = new Queue<char>(contents.ToCharArray()); // Another change from the NEA project is just using built in Qs
            char popped_char;
            int line_traceback = 1; // Start on line 1 (not 0-indexed..). Will be incremented each time "\n" is found (except in strings)

            while (charQ.Count > 0)
            {
                popped_char = charQ.Dequeue(); // pop next char to look at

                // WHITESPACE & NEW LINES: Skip over them
                if (" \n\t\r".Contains(popped_char))
                {
                    if (popped_char == '\n')
                    {
                        line_traceback++; // increment line count so we can trace back errors
                    }
                    continue;
                }

                // COMMENTS ('//'): Skip line
                else if (popped_char == '/' && MoreChars(charQ) && charQ.Peek() == '/')
                {
                    while (MoreChars(charQ) && charQ.Peek() != '\n') charQ.Dequeue();
                    // Doesn't dequeue the '\n' character when found, will just be skipped next iteration anyway.
                }

                // OPERATORS ('+', '-', etc.): Use generic Eat method to capture operator char(s) into 1 Token obj
                else if (Labels.Operators.Contains(popped_char))
                    yield return new Token("operator", Eat(popped_char, Labels.Operators, charQ));

                // NUMBERS (int, float, etc.): Capture numerical tokens, including decimals (e.g '24.0')
                else if (Labels.Numeric.Contains(popped_char))
                {
                    // We are not fussed with classifying integers or any specific types at the Lexing stage
                    // The only thing we will check is if it conforms to decimal notation or not.
                    string number = Eat(popped_char, Labels.Numeric, charQ);

                    if (number.Contains('.') && !Regex.IsMatch(number, "[0-9].[0-9]"))
                        throw new DecimalSyntaxError(number, line_traceback);
                    // Throw error because token does not conform to "XXXX.XXXX" decimal notation.


                    yield return new Token("number", number);
                }

                // STRINGS: String delimiters found, begin Token capture for string
                else if (Labels.StringDelimiters.Contains(popped_char))
                    yield return new Token("string", Delimiter_Eat(popped_char.ToString(), charQ, line_traceback));

                // IDENTIFIERS: Non-delimited alphabetic
                else if (Regex.IsMatch(popped_char.ToString(), Labels.IdentifierPattern))
                    yield return new Token("identifier", RegEx_Eat(popped_char, Labels.IdentifierPattern, charQ));

                // SINGLE_GRAMMAR: Special characters such as brackets and other 'grammar' for the programs
                else if (Labels.SingleGrammar.Contains(popped_char))
                    // These are grammar chars we want to capture individually, e.g. we want to capture brackets as separate tokens
                    // This: ("grammar", "("), ("grammar", "(")
                    // Instead of: ("grammar", "((")
                    yield return new Token("grammar", popped_char.ToString());

                else if (Labels.FlexGrammar.Contains(popped_char))
                    // These are grammar chars we want to be captured together with any following ones
                    // E.g we want:
                    // This: ("grammar", "==")
                    // Instead of: ("grammar", "="), ("grammar", "=")
                    yield return new Token("grammar", Eat(popped_char, Labels.FlexGrammar, charQ));
            }
        }

        /* TOKEN EATING METHODS
         * General params:
         * first_char: The char found to trigger the method
         * pattern or delimiter: The char or chars that define the condition to stop token consumption
         * charQ: The charQ passed by reference. Allows the method to pop chars out without requiring it to pass them back.
         */

        public static string Eat(char first_char, string pattern, Queue<char> charQ)
        {
            /* Explained:
             * Let first_char = "*", pattern="*+-" and charQ contents = "*++ blah blah"
             * We are using this Eat method to capture the whole '*++' word out of charQ
             */
            string toReturn = first_char.ToString();
            
            // while we still find the next chars in Q are in the pattern, append them to the return string
            while (MoreChars(charQ) && pattern.Contains(charQ.Peek())) 
                toReturn += charQ.Dequeue();

            return toReturn; // return the whole 'word' of chars that matched the pattern.
        }

        public static string Delimiter_Eat(string delimiters, Queue<char> charQ, int line_traceback) // line_traceback required to trace string error
        {
            string toReturn = "";

            // Keep appending chars until one of the delims is found
            while (MoreChars(charQ) && !delimiters.Contains(charQ.Peek())) toReturn += charQ.Dequeue();
            if (!MoreChars(charQ)) throw new UnboundStringSyntaxError(line_traceback);
            charQ.Dequeue(); // Pop delimiter
            return toReturn;
        }
        public static string RegEx_Eat(char first_char, string regex_pattern, Queue<char> charQ)
        {
            /* Duplicate of Eat method just using regex pattern validation instead of just checking if the char is in the pattern string
             * NOTE: This is not using RegEx to match Token types, it is just using it to cover a range of potential chars
             * e.g  [a-zA-Z] could be used if you want to Eat the next chars that are in the alphabet case-insensitive.
             */

            string toReturn = first_char.ToString();

            // Translates to: "While there are more chars in the queue and the next one matches the RegEx pattern, dequeue them and append"
            while (MoreChars(charQ) && Regex.IsMatch(charQ.Peek().ToString(), regex_pattern)) toReturn += charQ.Dequeue();

            return toReturn;
        }
        public static bool MoreChars(Queue<char> cQ) => cQ.Count > 0;
    }
}
