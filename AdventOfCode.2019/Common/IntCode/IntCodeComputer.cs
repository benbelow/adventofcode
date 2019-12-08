﻿using System.Collections.Generic;

namespace AdventOfCode._2019.Common.IntCode
{
    public class IntCodeComputer
    {
        private readonly IEnumerator<IntCodeOutput> _outputs;

        private readonly Queue<int> _inputs = new Queue<int>();

        public IntCodeComputer(string program, int initialInput)
        {
            _inputs.Enqueue(initialInput);
            _outputs = IntCodeLogic.ParseAndRunIntCode(program, _inputs);
        }

        public IntCodeOutput NextOutput(int? newInput)
        {
            if (newInput.HasValue)
            {
                _inputs.Enqueue(newInput.Value);
            }
            _outputs.MoveNext();
            return _outputs.Current;
        }
    }
}