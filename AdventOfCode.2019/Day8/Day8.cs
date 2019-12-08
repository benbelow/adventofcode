using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode._2019.Common;

namespace AdventOfCode._2019.Day8
{
    public static class Day8
    {
        public static int Part1()
        {
            var input = FileReader.ReadInputLines(8).First();
            return CheckSum(input, 25, 6);
        }
        
        public static int CheckSum(string pixels, int width, int height)
        {
            return CheckSum(pixels.AsEnumerable().Select(i => int.Parse(i.ToString())).ToList(), width, height);
        }

        /// <summary>
        /// Checksum = the layer that contains the fewest 0 digits. On that layer, what is the number of 1 digits multiplied by the number of 2 digits?
        /// </summary>
        public static int CheckSum(IList<int> pixels, int width, int height)
        {
            var layerSize = width * height;
            if (pixels.Count % layerSize != 0)
            {
                throw new Exception("Expected a whole number of layers");
            }

            var image = new SpaceImage
            {
                Layers = Enumerable.Range(0, pixels.Count / layerSize).Select(i =>
                    new Layer(pixels.Skip(i * layerSize).Take(layerSize), width, height)
                ).ToList()
            };

            var minLayer = image.Layers.OrderBy(l => l.FlatValues().Count(v => v == 0)).First();

            return minLayer.CheckSum();
        }
    }

    public class SpaceImage
    {
        public IList<Layer> Layers { get; set; }
    }

    public class Layer
    {
        public Layer(IEnumerable<int> rawData, int width, int height)
        {
            Rows = Enumerable.Range(0, height).Select(y =>
                new Row {Values = rawData.Skip(y * width).Take(width).ToList()}
            ).ToList();
        }

        public IList<Row> Rows { get; set; }

        public IEnumerable<int> FlatValues()
        {
            return Rows.SelectMany(r => r.Values);
        }

        public int CheckSum()
        {
            var values = FlatValues().ToList();

            return values.Count(v => v == 1) * values.Count(v => v == 2);
        }
    }

    public class Row
    {
        public IList<int> Values { get; set; }
    }
}