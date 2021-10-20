using System;
using System.Collections.Generic;
using System.Text;

namespace node_script.Parser.Steps
{
    public abstract class Step
    {
        private int position;

        public Step(int linePosition) { position = linePosition; }
        public override string ToString()
        {
            return base.ToString();
        }

        public int GetPosition() => position;
    }
}
