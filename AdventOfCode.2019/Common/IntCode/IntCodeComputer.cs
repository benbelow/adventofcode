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

        private readonly Queue<long> _inputs = new Queue<long>();

        public IntCodeComputer(string program, int? initialInput = null)
        {
            if (initialInput.HasValue)
            {
                _inputs.Enqueue(initialInput.Value);
            }

            _outputs = IntCodeLogic.ParseAndRunIntCode(program, _inputs);
        }

        public void QueueInput(long input)
        {
            _inputs.Enqueue(input);
        }

        public IntCodeOutput NextOutput(long? newInput = null)
        {
            if (newInput.HasValue)
            {
                _inputs.Enqueue(newInput.Value);
            }
            _outputs.MoveNext();
            return _outputs.Current;
        }

        public IEnumerable<IntCodeOutput> RunToCompletion()
        {
            return _outputs.ToEnumerable();
        }
    }
}