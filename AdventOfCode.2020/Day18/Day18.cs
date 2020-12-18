using System;
using System.Linq;
using System.Text.RegularExpressions;
using AdventOfCode.Common;
using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.AspNetCore.Server.HttpSys;

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
            var results = lines.Select(EvaluateExpression2);
            return results.Sum();
        }

        public static long EvaluateExpression2(string expression)
        {
            while (GetNextBrackets(expression) != (null, null))
            {
                var (start1, end1) = GetNextBrackets(expression);

                var start = start1.Value;
                var end = end1.Value;
                var diff = end - start;

                var substring = expression.Substring(start + 1, diff - 1);
                var evaluated = EvaluateExpression2(substring);

                expression = expression.Remove(start, diff + 1).Insert(start, evaluated.ToString());
            }

            var plusRegex = new Regex( @"(\d+) \+ (\d+)");
            while (plusRegex.IsMatch(expression))
            {
                var match = plusRegex.Match(expression);
                var p1 = long.Parse(match.Groups[1].Value);
                var p2 = long.Parse(match.Groups[2].Value);
                expression = expression.Replace($"{p1} + {p2}", (p1 + p2).ToString());
            }

            return expression.Split('*').Select(x => x.Trim()).Aggregate(1L, (a, x) => a * long.Parse(x));
        }

        private static (int? innermostOpenBracketIndex, int? matchingCloseBracketIndex) GetNextBrackets(string expression)
        {
            int? innermostOpenBracketIndex = null;
            int? matchingCloseBracketIndex = null;
            int innermostNestingOpenBracketLevel = 0;
            int innermostNestingClosedBracketLevel = 0;
            int nestingBracketLevel = 0;

            for (int i = 0; i < expression.Length; i++)
            {
                var c = expression[i];
                switch (c)
                {
                    case '(':
                        nestingBracketLevel++;
                        if (innermostOpenBracketIndex == null || nestingBracketLevel > innermostNestingOpenBracketLevel)
                        {
                            innermostOpenBracketIndex = i;
                            innermostNestingOpenBracketLevel = nestingBracketLevel;
                        }

                        break;
                    case ')':
                        var firstTimeAtNestingLevel = nestingBracketLevel == innermostNestingClosedBracketLevel && matchingCloseBracketIndex == null;
                        var greaterNestingLevel = nestingBracketLevel > innermostNestingClosedBracketLevel;
                        if (firstTimeAtNestingLevel || greaterNestingLevel)
                        {
                            matchingCloseBracketIndex = i;
                            innermostNestingClosedBracketLevel = nestingBracketLevel;
                        }

                        nestingBracketLevel--;
                        break;
                }
            }

            return (innermostOpenBracketIndex, matchingCloseBracketIndex);
        }
    }
}