using System.Collections.Generic;

namespace AdventOfCode._2019.Common.IntCode
{
    public class IntCodeComputer
    {
        private readonly IEnumerator<IntCodeOutput> _outputs;

        private int? _currentInput = 0;
        private int _outputIndex = 0;
        private List<IntCodeOutput> _returnedOutputs = new List<IntCodeOutput>();

        public IntCodeComputer(string program, int initialInput)
        {
            _outputs = IntCodeLogic.ParseAndRunIntCode(program, initialInput, NextInput);
        }

        public IntCodeOutput NextOutput(int? newInput)
        {
            _currentInput = newInput;
            _outputs.MoveNext();
            var output = _outputs.Current;
            _returnedOutputs.Add(output);
            return output;
        }

        private int NextInput()
        {
            return _currentInput ?? 0;
        }
    }
}