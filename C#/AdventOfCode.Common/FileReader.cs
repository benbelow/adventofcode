using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace AdventOfCode.Common
{
    public static class FileReader
    {
        public static IEnumerable<string> ReadInputLines(int day, bool isExample = false)
        {
            var dayString = day < 10 ? $"0{day}" : day.ToString(); 
            var dayPath = $"Day{dayString}";
            var fileName = isExample ? "example.txt" : "input.txt";
            var path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), dayPath, fileName);
            return File.ReadAllLines(path);
        }

        public static string ReadSingleLine(int day)
        {
            var lines = ReadInputLines(day);
            return lines.Single();
        }
    }
}