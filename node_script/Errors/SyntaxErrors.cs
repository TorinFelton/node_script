﻿using node_script.Errors;
using System;
using System.Collections.Generic;
using System.Text;

namespace node_script
{
    class DecimalSyntaxError : Exception
    {
        public DecimalSyntaxError(string value, int linePos) : base()
        {
            Error.ShowError("DecimalSyntaxError: Could not tokenise this number correctly: '" + value + "'", linePos);
        }

    }

    class UnrecognisedSyntaxError : Exception
    {
        public UnrecognisedSyntaxError(int linePos) : base()
        {
            Error.ShowError("UnrecognisedSyntaxError: None of the parsers could recognise this syntax. Have you made a typo?", linePos);
        }
    }

    class MissingDelimiterError : Exception
    {
        public MissingDelimiterError(string delimiter, int linePos) : base()
        {
            Error.ShowError("MissingDelimiterError: Could not find the delimiter: '" + delimiter + "' to end token or block. Check that you have closed your strings or code blocks.", linePos);
        }
    }
}
