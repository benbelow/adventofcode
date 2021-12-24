using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Common;

namespace AdventOfCode._2021.Day24
{
    public static class Day24
    {
        private const int Day = 24;

        public static List<string> ops;


        public static (long, long, long, long) Check(string inp)
        {
            return isValidInner(inp, ops);
        } 
        
        public static long Part1(bool isExample = false)
        {
            var lines = FileReader.ReadInputLines(Day, isExample).ToList();
            ops = lines;
            
            // return long.Parse(getTarget(0, lines));

            
            for (long i = 66666666666666; i <= 77777777777777; i+= 1)
            {
                var inputs = i.ToString();
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
            return isValidInner(modelNumber, operations).Item4 == 0;
        }
        
        public static (long, long, long, long) isValidInner(string modelNumber, List<string> operations)
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
                            SetX(a, GetX(a) == GetX(b) ? 1 : 0);
                            break;
                    }
                }
            }

            Console.WriteLine(modelNumber);
            foreach (var variable in variables)
            {
                Console.WriteLine($"{variable.Key}: {variable.Value}");
            }

            return (variables["w"], variables["x"], variables["y"], variables["z"]);
        }

        public static string getTarget(int target, List<string> operations)
        {
            var inputIndex = 0;

            var variables = new Dictionary<string, long>();

            

            var targetString = "";

            var buildTargetString = new List<string>();

            Dictionary<string, string> funcs = new Dictionary<string, string>
            {
                {"w", "0"},
                {"x", "0"},
                {"y", "0"},
                {"z", "0"},
            };

            void SetFunc(string t, string newFunc)
            {
                if (!newFunc.Contains("IN"))
                {
                    newFunc = Eval(newFunc);
                }
                
                funcs[t] = newFunc;
            }
            
            string GetFunc(string s)
            {
                if (int.TryParse(s, out var ints))
                {
                    return s;
                }

                return funcs[s];
            }
            
            foreach (var line in operations)
            {
                var op = line.Split(" ").First();
                if (op == "inp")
                {
                    var t = line.Split(" ").Last();
                    funcs[t] = funcs[t] + "IN" + inputIndex;
                    inputIndex++;
                }

                else
                {
                    var a = line.Split(" ")[1];
                    var b = line.Split(" ")[2];

                    switch (op)
                    {
                        case "add":
                            SetFunc(a, GetFunc(a) + " + " + GetFunc(b));
                            break;
                        case "mul":
                            SetFunc(a, GetFunc(a) + " * " + GetFunc(b));
                            break;
                        case "div":
                            SetFunc(a, GetFunc(a) + " / " + GetFunc(b));
                            break;
                        case "mod":
                            SetFunc(a, GetFunc(a) + " % " + GetFunc(b));
                            break;
                        case "eql":
                            SetFunc(a, GetFunc(a) + " == " + GetFunc(b));
                            break;
                    }
                }
            }

            foreach (var variable in variables)
            {
                // Console.WriteLine($"{variable.Key}: {variable.Value}");
            }

            return "";
        }

        private static string Eval(string newFunc)
        {
            System.Data.DataTable table = new System.Data.DataTable();
            return Convert.ToDouble(table.Compute(newFunc, String.Empty)).ToString();
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