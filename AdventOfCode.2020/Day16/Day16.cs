using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Common;
using AdventOfCode.Common.Utils;

namespace AdventOfCode._2020.Day16
{
    public static class Day16
    {
        private const int Day = 16;

        public static long Part1()
        {
            var lines = FileReader.ReadInputLines(Day).ToList();
            // 3 groups = rules, yours, others
            var groups = lines.Split("").Select(l => l.ToList()).ToList();
            var ticketFactory = new TicketFactory(groups[0]);

            var tickets = ticketFactory.BuildTickets(groups[2]);

            return tickets.Sum(t => t.GetErrorValues().Sum());
            return tickets.Count(t => !t.Validate());
        }

        public class PropertyRule
        {
            public string Name { get; set; }
            public int Min1 { get; set; }
            public int Max1 { get; set; }
            public int Min2 { get; set; }
            public int Max2 { get; set; }

            public PropertyRule(string rawData)
            {
                Name = rawData.Split(":")[0];
                var rest = rawData.Split(":")[1].Trim();
                var range1 = rest.Split(" or ")[0];
                var range2 = rest.Split(" or ")[1];

                Min1 = int.Parse(range1.Split("-")[0]);
                Max1 = int.Parse(range1.Split("-")[1]);
                Min2 = int.Parse(range2.Split("-")[0]);
                Max2 = int.Parse(range2.Split("-")[1]);
            }

            public bool ValidateValue(int value) => (Min1 <= value && Max1 >= value) || (Min2 <= value && Max2 >= value);
        }

        public class TicketFactory
        {
            public List<PropertyRule> Properties { get; set; }

            public TicketFactory(IList<string> properties)
            {
                Properties = properties.Select(p => new PropertyRule(p)).ToList();
            }

            public List<Ticket> BuildTickets(IList<string> inputs)
            {
                if (inputs[0].Equals("nearby tickets:"))
                {
                    inputs = inputs.Skip(1).ToList();
                }

                return inputs.Select(BuildTicket).ToList();
            }

            public Dictionary<int, string> CalculatePropertyLookup(List<Ticket> tickets)
            {
                var indexedValues = Enumerable.Range(0, Properties.Count)
                    .Select(i =>
                        tickets.Select(t => t.Values[i]).ToList()
                    ).ToList();


                var lookup = new Dictionary<int, string>();
                var availableProperties = Properties.Clone().ToHashSet();
                var assignedIndexes = new HashSet<int>();
                
                List<List<PropertyRule>> PossibleProperties() => indexedValues.Select(values =>
                    availableProperties.Where(property => values.All(property.ValidateValue)).ToList()
                ).ToList();

                while (availableProperties.Any())
                {
                    var known = new List<(int, PropertyRule)>();
                    var possibleProperties = PossibleProperties();
                    for (int i = 0; i < possibleProperties.Count; i++)
                    {
                        if (assignedIndexes.Contains(i))
                        {
                            continue;
                        }

                        var possibles = possibleProperties[i];
                        if (possibles.Count() == 1)
                        {
                            known.Add((i, possibles.Single()));
                        }
                    }

                    foreach (var (index, propertyRule) in known)
                    {
                        assignedIndexes.Add(index);
                        availableProperties = availableProperties.Where(p => p.Name != propertyRule.Name).ToHashSet();
                        lookup[index] = propertyRule.Name;
                    }
                }

                return lookup;
            }

            public Ticket BuildTicket(string inputString) => new Ticket(inputString, ValidateTicketValue);

            public bool ValidateTicketValue(int ticketValue) => Properties.Any(p => p.ValidateValue(ticketValue));
        }

        public class Ticket
        {
            private readonly Func<int, bool> valueValidator;
            public List<int> Values { get; set; }

            public Ticket(string valuesString, Func<int, bool> valueValidator)
            {
                this.valueValidator = valueValidator;
                Values = valuesString.Split(",").Select(int.Parse).ToList();
            }

            public List<int> GetErrorValues() => Values.Where(v => !valueValidator(v)).ToList();

            public bool Validate() => !GetErrorValues().Any();
        }

        public static long Part2()
        {
            var lines = FileReader.ReadInputLines(Day).ToList();
            // 3 groups = rules, yours, others
            var groups = lines.Split("").Select(l => l.ToList()).ToList();
            var ticketFactory = new TicketFactory(groups[0]);

            var tickets = ticketFactory.BuildTickets(groups[2]);

            var validTickets = tickets.Where(t => t.Validate()).ToList();

            var lookup = ticketFactory.CalculatePropertyLookup(validTickets);

            var indexesToCheck = lookup
                .Where(l => l.Value.StartsWith("departure"))
                .Select(x => x.Key);

            var myTicket = ticketFactory.BuildTicket(groups[1][1]);
            
            return indexesToCheck.Aggregate(1L, (a,i) => a * myTicket.Values[i]);
        }
    }
}