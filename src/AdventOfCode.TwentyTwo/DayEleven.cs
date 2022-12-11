using System;
using AdventOfCode.Domain;

namespace AdventOfCode.TwentyTwo;

public class DayEleven : IChallenge<double>
{
    public string ChallengeInputPath => DataProvider<DayEleven>.GetChallengePath();

    public double RunTaskOne(string[] lines)
    {
        var numberOfMonkeys = (int)Math.Round(lines.Length / 7d);

        Monkey[] monkeys = new Monkey[numberOfMonkeys];

        for (var i = 0; i < numberOfMonkeys; i++)
        {
            Monkey monkey = ParseMonkey(i, lines);
            monkeys[i] = monkey;
        }

        for (var i = 0; i < 20; i++)
        {
            foreach (var monkey in monkeys)
            {
                foreach (var item in monkey.StartingItems)
                {
                    var worryLevel = monkey.Operation(item);
                    worryLevel /= 3;
                    worryLevel = double.Floor(worryLevel);
                    var isDivisible = monkey.IsDivisible(worryLevel);
                    monkeys[monkey.ThrowTo[isDivisible]].StartingItems.Enqueue(worryLevel);
                    monkey.InspectedItems++;
                }
                monkey.StartingItems.Clear();
            }
        }

        return GetProductOfTopTwoMostActiveMonkeys(monkeys);
    }

    public double RunTaskTwo(string[] lines)
    {
        var numberOfMonkeys = (int)Math.Round(lines.Length / 7d);

        Monkey[] monkeys = new Monkey[numberOfMonkeys];

        for (var i = 0; i < numberOfMonkeys; i++)
        {
            Monkey monkey = ParseMonkey(i, lines);
            monkeys[i] = monkey;
        }

        var superMod = monkeys.Aggregate(1, (acc, cur) => acc * cur.DivisiblyBy);

        for (var i = 0; i < 10000; i++)
        {
            foreach (var monkey in monkeys)
            {
                foreach (var item in monkey.StartingItems)
                {
                    var worryLevel = monkey.Operation(item);
                    worryLevel %= superMod;
                    var isDivisible = monkey.IsDivisible(worryLevel);
                    monkeys[monkey.ThrowTo[isDivisible]].StartingItems.Enqueue(worryLevel);
                    monkey.InspectedItems++;
                }
                monkey.StartingItems.Clear();
            }
        }

        return GetProductOfTopTwoMostActiveMonkeys(monkeys);
    }

    double GetProductOfTopTwoMostActiveMonkeys(Monkey[] monkeys) =>
            monkeys
                .OrderByDescending(m => m.InspectedItems)
                .Take(2)
                .Aggregate(1d, (acc, cur) => acc * cur.InspectedItems);

    private Monkey ParseMonkey(int monkeyId, string[] lines)
    {
        var startingIndex = monkeyId * 7;

        return new Monkey(GetDivisibleBy())
        {
            Id = monkeyId,
            StartingItems = GetStartingItems(),
            Operation = GetOperation(),
            ThrowTo = GetThrowTo()
        };

        Queue<double> GetStartingItems()
        {
            IEnumerable<double> startingItems =
                lines[startingIndex + 1]
                    .Split(':')[1]
                    .Split(',')
                    .Select(si => double.Parse(si.Trim()));

            return new Queue<double>(startingItems);
        }

        Func<double, double> GetOperation()
        {
            string op = lines[startingIndex + 2].Split(':')[1].Split('=')[1].Trim()[4..];

            var (operand, isInt) = op[2..] switch
            {
                "old" => (0, false),
                _ => (double.Parse(op[2..]), true)
            };

            Func<double, double> operation = op[0] switch
            {
                '*' => (double old) => old * (isInt ? operand : old),
                '+' => (double old) => old + (isInt ? operand : old),
                _ => throw new ArgumentException($"Unknown operator {op[0]}")
            };

            return operation;
        }

        int GetDivisibleBy()
        {
            string test = lines[startingIndex + 3].Split("by ")[1];
            return int.Parse(test);
        }

        Dictionary<bool, int> GetThrowTo()
        {
            string ifTrue = lines[startingIndex + 4].Split("monkey ")[1];
            string ifFalse = lines[startingIndex + 5].Split("monkey ")[1];
            return new Dictionary<bool, int>
            {
                [true] = int.Parse(ifTrue),
                [false] = int.Parse(ifFalse)
            };
        }
    }

    private class Monkey
    {
        public int Id { get; init; }
        public Queue<double> StartingItems { get; init; }
        public Func<double, double> Operation { get; init; }
        public Dictionary<bool, int> ThrowTo { get; init; }
        public double InspectedItems { get; set; } = 0;
        public int DivisiblyBy { get; }

        public Monkey(int divisibleBy)
        {
            DivisiblyBy = divisibleBy;
        }

        public bool IsDivisible(double num) => num % DivisiblyBy == 0;
    }
}

