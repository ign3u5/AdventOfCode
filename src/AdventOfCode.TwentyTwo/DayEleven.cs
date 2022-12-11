using System;
using AdventOfCode.Domain;

namespace AdventOfCode.TwentyTwo;

public class DayEleven : IChallenge<int>
{
    public string ChallengeInputPath => DataProvider<DayEleven>.GetChallengePath();

    public int RunTaskOne(string[] lines)
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
                    var isDivisible = monkey.IsDivisible(worryLevel);
                    monkeys[monkey.ThrowTo[isDivisible]].StartingItems.Enqueue(worryLevel);
                    monkey.InspectedItems++;
                }
                monkey.StartingItems.Clear();
            }
        }

        return GetProductOfTopTwoMostActiveMonkeys();

        int GetProductOfTopTwoMostActiveMonkeys() =>
            monkeys
                .OrderByDescending(m => m.InspectedItems)
                .Take(2)
                .Aggregate(1, (acc, cur) => acc * cur.InspectedItems);
    }

    public int RunTaskTwo(string[] lines)
    {
        return 0;
    }

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

        Queue<int> GetStartingItems()
        {
            IEnumerable<int> startingItems =
                lines[startingIndex + 1]
                    .Split(':')[1]
                    .Split(',')
                    .Select(si => int.Parse(si.Trim()));

            return new Queue<int>(startingItems);
        }

        Func<int, int> GetOperation()
        {
            string op = lines[startingIndex + 2].Split(':')[1].Split('=')[1].Trim()[4..];

            var (operand, isInt) = op[2..] switch
            {
                "old" => (0, false),
                _ => (int.Parse(op[2..]), true)
            };

            Func<int, int> operation = op[0] switch
            {
                '*' => (int old) => old * (isInt ? operand : old),
                '+' => (int old) => old + (isInt ? operand : old),
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
        public Queue<int> StartingItems { get; init; }
        public Func<int, int> Operation { get; init; }
        public Dictionary<bool, int> ThrowTo { get; init; }
        public int InspectedItems { get; set; } = 0;
        private int _divisiblyBy;

        public Monkey(int divisibleBy)
        {
            _divisiblyBy = divisibleBy;
        }

        public bool IsDivisible(int num) => num % _divisiblyBy == 0;
    }
}

