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

    class UnboundStringSyntaxError : Exception
    {
        public UnboundStringSyntaxError(int linePos) : base()
        {
            Error.ShowError("UnboundStringSyntaxError: String delimiter never found. Maybe you forgot to close your quotes?", linePos);
        }
    }

    class UnrecognisedSyntaxError : Exception
    {
        public UnrecognisedSyntaxError(int linePos) : base()
        {
            Error.ShowError("UnrecognisedSyntaxError: None of the parsers could recognise this syntax. Have you made a typo?", linePos);
        }
    }

    class MissingSemiColError : Exception
    {
        public MissingSemiColError(int linePos) : base()
        {
            Error.ShowError("MissingSemiColError: Could not find a semi colon to end off statement.", linePos);
        }
    }
}
