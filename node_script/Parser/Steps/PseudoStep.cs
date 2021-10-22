using System;
using System.Collections.Generic;
using System.Text;

namespace node_script.Parser.Steps
{
    abstract class PseudoStep : Step
    {
        public PseudoStep(int linePosition) : base(linePosition) { }
    }
}
