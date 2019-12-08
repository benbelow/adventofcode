using System.Collections.Generic;
using AdventOfCode._2019.Common.IntCode.Models;

namespace AdventOfCode._2019.Common.IntCode
{
    /// <summary>
    /// Responsible for managing input queue, and lazily fetching each output of an int code computer
    /// </summary>
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