using System;
using System.Linq;
using AdventOfCode.Common;

namespace AdventOfCode._2020.Day18
{
    public static class Day18
    {
        private const int Day = 18;

        public static long Part1()
        {
            var lines = FileReader.ReadInputLines(Day).ToList();
            var results = lines.Select(EvaluateExpression);
            return results.Sum();
        }

        public enum Operation
        {
            Identity,
            Multiply,
            Add
        }

        public static long EvaluateExpression(string expression)
        {
            var evaluator = new ExpressionEvaluator();

            var state = new ExpressionEvaluator.EvaluationState();
            foreach (var c in expression)
            {
                state = evaluator.FeedCharacter(c);
            }

            state = evaluator.Finalise();

            return state.CurrentState;
        }

        public class ExpressionEvaluator
        {
            private readonly int nestingLevel;
            private long state;
            private Operation nextOperation;
            private string partialNumber;
            private ExpressionEvaluator nestedEvaluator;

            public ExpressionEvaluator(int nestingLevel = 0)
            {
                this.nestingLevel = nestingLevel;
                state = 0;
                nextOperation = Operation.Identity;
                partialNumber = null;
                nestedEvaluator = null;
            }

            public class EvaluationState
            {
                // Only nested evaluators know when they're finished without explicitly calling "Finalise"
                public bool IsFinished { get; set; }
                public long CurrentState { get; set; }
                public int NestingLevel { get; set; }
            }

            private long ApplyOperation(long current, long operand, Operation operation) => operation switch
            {
                Operation.Identity => operand,
                Operation.Multiply => current * operand,
                Operation.Add => current + operand,
                _ => throw new Exception("unexpected")
            };

            private void Apply()
            {
                var operand = long.Parse(partialNumber);

                state = ApplyOperation(state, operand, nextOperation);

                partialNumber = null;
                nextOperation = Operation.Identity;
            }

            public EvaluationState FeedCharacter(char c)
            {
                if (nestedEvaluator != null)
                {
                    var nestedResult = nestedEvaluator.FeedCharacter(c);
                    if (nestedResult.IsFinished)
                    {
                        partialNumber = nestedResult.CurrentState.ToString();
                        nestedEvaluator = null;
                    }
                }
                else
                {
                    switch (c)
                    {
                        case '(':
                            nestedEvaluator = new ExpressionEvaluator(nestingLevel + 1);
                            break;
                        case ')':
                            return Finalise();
                        case '*':
                            nextOperation = Operation.Multiply;
                            break;
                        case '+':
                            nextOperation = Operation.Add;
                            break;
                        case ' ':
                            if (partialNumber != null)
                            {
                                Apply();
                            }

                            break;
                        // must be [0-9] due to no other known characters 
                        default:
                            partialNumber += c;
                            break;
                    }
                }

                return new EvaluationState {NestingLevel = nestingLevel, CurrentState = state};
            }

            public EvaluationState Finalise()
            {
                if (partialNumber != null)
                {
                    Apply();
                }

                return new EvaluationState {NestingLevel = nestingLevel, CurrentState = state, IsFinished = true};
            }
        }


        public static long Part2()
        {
            var lines = FileReader.ReadInputLines(Day).ToList();
            return -1;
        }
    }
}