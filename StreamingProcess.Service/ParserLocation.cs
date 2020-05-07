using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamingProcess.Service
{
    public class ParserLocation
    {
        private readonly string _input;
        private readonly int _index;

        public ParserLocation(string input, int index = 0)
        {
            _input = input;
            _index = index;
            if (_index < 0)
            {
                _index = 0;
            }
            if (_index >= input.Length)
            {
                _index = input.Length;
            }
        }

        public ParserLocation MoveToRight()
        {
            return new ParserLocation(_input, _index + 1);
        }

        public bool EndOfStream => _index >= _input.Length;

        public char? Current => !EndOfStream ? _input[_index] : (char?)null;
    }

}
