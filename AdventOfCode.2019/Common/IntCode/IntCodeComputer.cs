using System;
using System.Collections.Generic;
using AdventOfCode._2019.Common.IntCode.Models;

namespace AdventOfCode._2019.Common.IntCode
{
    /// <summary>
    /// Responsible for managing input queue, and lazily fetching each output of an int code computer
    /// </summary>
    public class IntCodeComputer
    {
        private readonly IEnumerator<IntCodeOutput> outputs;

        private readonly Queue<long> inputs = new Queue<long>();

        /// <param name="program"></param>
        /// <param name="initialInput"></param>
        /// <param name="getInput">Function for defining inputs when they need to be evaluated only when required, rather than queued upfront</param>
        public IntCodeComputer(string program, int? initialInput = null, Func<long> getInput = null)
        {
            if (initialInput.HasValue)
            {
                inputs.Enqueue(initialInput.Value);
            }

            outputs = IntCodeLogic.ParseAndRunIntCode(program, inputs, getInput: getInput);
        }

        public void QueueInput(long input)
        {
            inputs.Enqueue(input);
        }

        public IntCodeOutput NextOutput(long? newInput = null)
        {
            if (newInput.HasValue)
            {
                inputs.Enqueue(newInput.Value);
            }
            
            outputs.MoveNext();
            return outputs.Current;
        }

        public IEnumerable<IntCodeOutput> RunToCompletion()
        {
            return outputs.ToEnumerable();
        }
    }
}