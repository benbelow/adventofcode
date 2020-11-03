using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace AdventOfCode._2019.Common
{
    public static class FileReader
    {
        public static IEnumerable<string> ReadInputLines(int day)
        {
            var subPath = $"Day{day}";
            var path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Inputs", subPath);
            return File.ReadAllLines(path);
        }
    }
}