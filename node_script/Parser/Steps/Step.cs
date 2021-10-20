using System;
using System.Collections.Generic;
using System.Text;

namespace node_script.Parser.Steps
{
    public abstract class Step
    {
        private int position;

        public Step(int linePosition) { position = linePosition; }
        public abstract override string ToString();

        public int GetPosition() => position;
    }
}
