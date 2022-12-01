using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AdventOfCode.Common;

namespace AdventOfCode._2020.Day08
{
    public static class Day08
    {
        private const int Day = 8;

        public static long Part1()
        {
            var lines = FileReader.ReadInputLines(Day).ToList();

            var console = new GameConsole(lines);
            var result = console.RunToCompletion();
            return result.Accumulator;
        }

        public static async Task<long> Part2()
        {
            var lines = FileReader.ReadInputLines(Day).ToList();
            
            var variations = new List<List<string>>();
            for (int i = 0; i < lines.Count(); i++)
            {
                var line = lines[i];
                var modified = line.Contains("jmp") ? line.Replace("jmp", "nop") : line.Replace("nop", "jmp");
                if (line != modified)
                {
                    var clone = lines.Select(x => x).ToList();
                    clone[i] = modified;
                    variations.Add(clone);
                }
            }

            var consoles = variations.Select(v => new GameConsole(v));

            foreach (var console in consoles)
            {
                var end = console.RunToCompletion();
                if (end.Finished)
                {
                    return end.Accumulator;
                }
            }
            throw new Exception("No console completed.");
        }

        public static long RunAmended(int indexToSwap, List<(string, int)> instructions, CancellationToken token)
        {
            var accumulator = 0;
            var i = 0;

            var swapIndex = -1;
            var lineToSwap = -1;
            for (int j = 0; j < instructions.Count(); j++)
            {
                var instruction = instructions[j];
                if (new[] {"nop", "jmp"}.Contains(instruction.Item1))
                {
                    swapIndex++;
                }

                if (swapIndex == indexToSwap)
                {
                    lineToSwap = j;
                    break;
                }
            }

            var visited = new HashSet<int>();

            while (true)
            {
                if (token.IsCancellationRequested)
                {
                    return -1;
                }

                if (i == instructions.Count)
                {
                    return accumulator;
                }

                var oldI = i;
                var op = instructions[i];
                switch (op.Item1)
                {
                    case "nop":
                        if (i == lineToSwap)
                        {
                            i += op.Item2;
                            break;
                        }

                        i++;
                        break;
                    case "acc":
                        accumulator += op.Item2;
                        i++;
                        break;
                    case "jmp":
                        if (i == lineToSwap)
                        {
                            i++;
                            break;
                        }

                        i += op.Item2;
                        break;
                }

                visited.Add(oldI);
            }
        }


        public class GameConsole
        {
            public class GameState
            {
                public bool Finished { get; set; }
                public bool InfiniteLoop { get; set; }
                public int Index { get; set; }
                public long Accumulator { get; set; }
            }

            internal enum OperationType
            {
                Accumulate,
                Jump,
                Noop
            }

            internal class Operation
            {
                public OperationType Type { get; set; }
                public int Value { get; set; }

                public Operation(string input)
                {
                    Type = ParseString(input.Split()[0]);
                    Value = int.Parse(input.Split()[1]);
                }

                private static OperationType ParseString(string op)
                {
                    return op switch
                    {
                        "acc" => OperationType.Accumulate,
                        "nop" => OperationType.Noop,
                        "jmp" => OperationType.Jump,
                    };
                }
            }

            private GameState currentState;

            private List<Operation> instructions;

            private List<long> indexHistory = new List<long>();

            public GameConsole(List<string> instructions)
            {
                this.instructions = instructions.Select(i => new Operation(i)).ToList();
                currentState = new GameState {Accumulator = 0, Finished = false, Index = 0, InfiniteLoop = false};
            }

            public GameState RunToCompletion()
            {
                var states = Run();
                foreach (var state in states)
                {
                    if (state.Finished || state.InfiniteLoop)
                    {
                        return state;
                    }
                }

                throw new Exception("Failed to run to completion");
            }

            public IEnumerable<GameState> Run()
            {
                while (!currentState.Finished && !currentState.InfiniteLoop)
                {
                    RunStep();
                    yield return currentState;
                }
            }

            private void RunStep()
            {
                if (!currentState.InfiniteLoop && indexHistory.Contains(currentState.Index))
                {
                    currentState.InfiniteLoop = true;
                }

                indexHistory.Add(currentState.Index);

                var operation = instructions[currentState.Index];
                switch (operation.Type)
                {
                    case OperationType.Accumulate:
                        currentState.Accumulator += operation.Value;
                        currentState.Index++;
                        break;
                    case OperationType.Jump:
                        currentState.Index += operation.Value;
                        break;
                    case OperationType.Noop:
                        currentState.Index++;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                if (currentState.Index == instructions.Count())
                {
                    currentState.Finished = true;
                }
            }
        }
    }
}