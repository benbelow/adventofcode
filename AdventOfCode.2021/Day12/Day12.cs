using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Common;

namespace AdventOfCode._2021.Day12
{
    public static class Day12
    {
        private const int Day = 12;

        public class Node
        {
            public string Id { get; set; }
            
            public bool isBig { get; set; }

            public HashSet<Node> Connected = new HashSet<Node>();
            
            public Node(string id)
            {
                Id = id;
                isBig = id.ToUpper() == id;
            }

            public void ConnectTo(Node node)
            {
                if (node.Id == Id)
                {
                    return;
                }
                Connected.Add(node);
            }

            public long PathsToTarget(string target, HashSet<Node> visited)
            {
                if (Id == target)
                {
                    return 1;
                }

                return Connected
                    .Where(c => c.isBig || !visited.Contains(c))
                    .Sum(connectedNode =>
                {
                    return connectedNode.PathsToTarget(target, visited.Concat(new[] { this }).ToHashSet());
                });
            }
        }

        public static long Part1(bool isExample = false)
        {
            var lines = FileReader.ReadInputLines(Day, isExample).ToList();
            var nodes = new Dictionary<string, Node>();
            
            Node GetNode(string nid) => nodes.ContainsKey(nid) ? nodes[nid] : new Node(nid);
            void SetNode(Node node) => nodes[node.Id] = node;

            foreach (var l in lines.Select(l => l.Split("-")))
            {
                var sourceNode = GetNode(l.First());
                var targetNode = GetNode(l.Last());

                sourceNode.ConnectTo(targetNode);
                targetNode.ConnectTo(sourceNode);
                SetNode(sourceNode);
                SetNode(targetNode);
            }

            return nodes["start"].PathsToTarget("end", new HashSet<Node>());
        }

        public static long Part2(bool isExample = false)
        {
            var lines = FileReader.ReadInputLines(Day, isExample).ToList();
            return -1;
        }
    }
}