using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Common;
using MoreLinq;

namespace AdventOfCode._2021.Day12
{
    public static class Day12
    {
        private const int Day = 12;

        public class Node
        {
            private readonly bool allowsOneSmallDoubleVisit;
            public string Id { get; set; }

            public override string ToString()
            {
                return Id;
            }

            public bool isBig { get; set; }

            public HashSet<Node> Connected = new HashSet<Node>();
            
            public Node(string id, bool allowsOneSmallDoubleVisit = false)
            {
                this.allowsOneSmallDoubleVisit = allowsOneSmallDoubleVisit;
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

            public long PathsToTarget(string target, List<Node> visited, bool hasDoubleVisited = false)
            {
                if (Id == target)
                {
                    Console.WriteLine(visited.Aggregate("", (x, y) => x + y + ",") + target);
                    return 1;
                }

                var x = visited.Aggregate("", (x, y) => x + y + ",");
                if (x == "start,DX,fs,DX,he,DX,fs,")
                {
                    var s = 0;
                }

                var isDoubleVisit = !this.isBig && visited.Contains(this);
                
                if (!allowsOneSmallDoubleVisit || hasDoubleVisited)
                {
                    if (isDoubleVisit)
                    {
                        return 0;
                    }
                    
                    return Connected
                        .OrderBy(c => c.Id)
                        .Where(c => c.isBig || (!visited.Contains(c)))
                        .Sum(connectedNode =>
                        {
                            return connectedNode.PathsToTarget(target, visited.Concat(new[] { this }).ToList(), true);
                        });
                }

                var allowedConnected = Connected
                    .OrderBy(c => c.Id)
                    .Where(c =>
                    {
                        var isAllowedBySize = c.isBig;
                        var isForbiddenByRepeatSpecificRules = c.Id is "start" or "end" && visited.Contains(c);
                        return isAllowedBySize || !isForbiddenByRepeatSpecificRules;
                    });
                return allowedConnected
                    .Sum(connectedNode =>
                    {
                        return connectedNode.PathsToTarget(target, visited.Concat(new[] { this }).ToList(), isDoubleVisit);
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

            return nodes["start"].PathsToTarget("end", new List<Node>());
        }

        public static long Part2(bool isExample = false)
        {
            var lines = FileReader.ReadInputLines(Day, isExample).ToList();
            var nodes = new Dictionary<string, Node>();
            
            Node GetNode(string nid) => nodes.ContainsKey(nid) ? nodes[nid] : new Node(nid, true);
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

            return nodes["start"].PathsToTarget("end", new List<Node>());
        }
    }
}