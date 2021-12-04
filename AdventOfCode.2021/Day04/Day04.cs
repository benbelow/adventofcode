using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Common;
using AdventOfCode.Common.Utils;

namespace AdventOfCode._2021.Day04
{
    public class BingoCell
    {
        public int Value { get; }

        public bool HasBeenCalled { get; set; }

        public BingoCell(int value)
        {
            Value = value;
        }

        public void Call(int number)
        {
            if (!HasBeenCalled && number == Value)
            {
                HasBeenCalled = true;
            }
        }
    }

    public class BingoBoard
    {
        private bool won;
        public List<List<BingoCell>> Rows { get; }

        private List<List<BingoCell>> Columns =>
            Enumerable.Range(0, Rows.Count).Select(i => Rows.Select(r => r[i]).ToList()).ToList();

        private List<BingoCell> Cells => Rows.SelectMany(r => r).ToList();

        public BingoBoard()
        {
            Rows = new List<List<BingoCell>>();
        }

        public void AddLine(string line)
        {
            Rows.Add(
                line.Split(" ")
                    .Where(x => x != "")
                    .Select(x => new BingoCell(int.Parse(x))).ToList()
            );
        }

        public void Call(int number)
        {
            foreach (var cell in Cells)
            {
                cell.Call(number);
            }
        }

        public bool HasWon()
        {
            if (won)
            {
                return won;
            }
            
            won = Rows.Any(row => row.All(c => c.HasBeenCalled))
                   || Columns.Any(column => column.All(c => c.HasBeenCalled));

            return won;
        }

        public long Score()
        {
            return Cells.Where(c => !c.HasBeenCalled).Sum(c => c.Value);
        }
    }

    public static class Day04
    {
        private const int Day = 04;

        public static long Part1(bool isExample = false)
        {
            var lines = FileReader.ReadInputLines(Day, isExample).ToList();

            var calledNumbers = lines.First().Split(",").Select(int.Parse).ToList();
            var boards = ParseBoards(lines.Skip(2).ToList());

            foreach (var calledNumber in calledNumbers)
            {
                foreach (var bingoBoard in boards)
                {
                    bingoBoard.Call(calledNumber);
                    if (bingoBoard.HasWon())
                    {
                        return bingoBoard.Score() * calledNumber;
                    }
                }
            }

            return -1;
        }

        private static List<BingoBoard> ParseBoards(List<string> lines)
        {
            var bingoBoards = new List<BingoBoard>();
            var currentBoard = new BingoBoard();
            bingoBoards.Add(currentBoard);

            foreach (var line in lines)
            {
                if (line == "")
                {
                    currentBoard = null;
                    continue;
                }

                if (currentBoard == null)
                {
                    currentBoard = new BingoBoard();
                    bingoBoards.Add(currentBoard);
                }

                currentBoard.AddLine(line);
            }

            return bingoBoards;
        }

        public static long Part2(bool isExample = false)
        {
            var lines = FileReader.ReadInputLines(Day, isExample).ToList();

            var calledNumbers = lines.First().Split(",").Select(int.Parse).ToList();
            var boards = ParseBoards(lines.Skip(2).ToList());

            var winners = new List<BingoBoard>();

            foreach (var calledNumber in calledNumbers)
            {
                foreach (var bingoBoard in boards)
                {
                    bingoBoard.Call(calledNumber);
                }

                var players = boards.Except(winners);
                foreach (var winner in players.Where(b => b.HasWon()))
                {
                    winners.Add(winner);
                }
                if (winners.Count == boards.Count)
                {
                    return winners.Last().Score() * calledNumber;
                }
            }

            return -1;
        }
    }
}