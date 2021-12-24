using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Common;

namespace AdventOfCode._2021.Day24
{
    public static class Day24
    {
        private const int Day = 24;
        
        public static long Part1(bool isExample = false)
        {
            var lines = FileReader.ReadInputLines(Day, isExample).ToList();

            for (long i = 9999999; i >= 999999; i--)
            {
                var inputs = i.ToString() + "9999999";
                if (inputs.Contains("0"))
                {
                    continue;
                }

                try
                {
                    if (isValid(inputs, lines))
                    {
                        return long.Parse(inputs);
                    }
                }
                catch (Exception e)
                {
                    
                }
            }

            return -1;
        }

        
        
        public static bool isValid(string modelNumber, List<string> operations)
        {
            var inputs = modelNumber;
            var inputIndex = 0;

            var variables = new Dictionary<string, long>();

            void SetX(string x, long val)
            {
                variables[x] = val;
            }

            long GetX(string x)
            {
                if (long.TryParse(x, out var longX))
                {
                    return longX;
                }
                return variables.ContainsKey(x) ? variables[x] : 0;
            }
            
            foreach (var line in operations)
            {
                var op = line.Split(" ").First();
                if (op == "inp")
                {
                    variables[line.Split(" ").Last()] = long.Parse(inputs[inputIndex].ToString());
                    inputIndex++;
                }

                else
                {
                    var a = line.Split(" ")[1];
                    var b = line.Split(" ")[2];

                    switch (op)
                    {
                        case "add":
                            SetX(a, GetX(a) + GetX(b));
                            break;
                        case "mul":
                            SetX(a, GetX(a) * GetX(b));
                            break;
                        case "div":
                            SetX(a, GetX(a) / GetX(b));
                            break;
                        case "mod":
                            SetX(a, GetX(a) % GetX(b));
                            break;
                        case "eql":
                            SetX(a, GetX(a) == GetX(b)  ? 1 : 0);
                            break;
                    }
                }
            }

            foreach (var variable in variables)
            {
                // Console.WriteLine($"{variable.Key}: {variable.Value}");
            }
            return variables["z"] == 0;
        }
        
        public static long Part2(bool isExample = false)
        {
            var lines = FileReader.ReadInputLines(Day, isExample).ToList();
            return -1;
        }

        public static bool IsValidForInput(string s)
        {
            var lines = FileReader.ReadInputLines(Day, false).ToList();
            return isValid(s, lines);
        }
    }
}