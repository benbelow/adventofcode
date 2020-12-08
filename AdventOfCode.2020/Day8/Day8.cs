using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AdventOfCode.Common;

namespace AdventOfCode._2020.Day8
{
    public static class Day8
    {
        private const int Day = 8;

        public static long Part1()
        {
            var lines = FileReader.ReadInputLines(Day).ToList();

            var instructions = lines.Select(l =>
            {
                var operation = l.Split()[0];
                var value = int.Parse(l.Split()[1]);
                return (operation, value);
            }).ToList();

            var accumulator = 0;
            var i = 0;

            var visited = new HashSet<int>();

            while (true)
            {
                var oldI = i;
                var op = instructions[i];
                switch (op.Item1)
                {
                    case "nop":
                        i++;
                        break;
                    case "acc":
                        accumulator += op.Item2;
                        i++;
                        break;
                    case "jmp":
                        i += op.Item2;
                        break;
                }

                if (visited.Contains(oldI))
                {
                    return accumulator;
                }

                visited.Add(oldI);
            }
        }

        public static async Task<long> Part2()
        {
            var lines = FileReader.ReadInputLines(Day).ToList();

            var instructions = lines.Select(l =>
            {
                var operation = l.Split()[0];
                var value = int.Parse(l.Split()[1]);
                return (operation, value);
            }).ToList();

            var numberOfPossibleChanges = instructions.Count(i => i.Item1 == "nop" || i.Item1 == "jmp");


            for (int i = numberOfPossibleChanges-1; i >= 0; i--)
            {
                var cts = new CancellationTokenSource();
                Console.WriteLine($"trying iteration {i}");
                try
                {
                    var task = Task.Factory.StartNew(() => RunAmended(i, instructions, cts.Token), cts.Token);
                    var delay = Task.Delay(TimeSpan.FromSeconds(0.1), cts.Token);
                    await Task.WhenAny(task, delay);
                    if (task.IsCompleted && !task.IsCanceled)
                    {
                        return task.Result;
                    }

                    cts.Cancel();
                }
                catch (TaskCanceledException)
                {
                    
                }
            }

            return -1;
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
    }
}