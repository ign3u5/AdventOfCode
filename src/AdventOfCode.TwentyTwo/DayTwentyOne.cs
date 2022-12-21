using AdventOfCode.Domain;

namespace AdventOfCode.TwentyTwo
{
    public class DayTwentyOne : IChallenge<double>
    {
        public string ChallengeInputPath => DataProvider<DayTwentyOne>.GetChallengePath();

        public double RunTaskOne(string[] lines)
        {
            Dictionary<string, Monkey> monkeys = new();

            foreach(string line in lines)
            {
                Monkey monkey;
                if (double.TryParse(line[6..], out double val))
                {
                    monkey = new()
                    {
                        Name = line[0..4],
                        Val = val
                    };
                } else
                {
                    monkey = new()
                    {
                        Name = line[0..4],
                        SubMonkeys = GetSubMonkeys(line),
                        Operation = GetOperation(line)
                    };
                }

                monkeys[monkey.Name] = monkey;
            }

            var rootMonkey = monkeys["root"];
            var output = rootMonkey.Val;

            Func<string, string, double> GetOperation(string line)
            {
                char oper = line[11];
                return oper switch
                {
                    '+' => (m1, m2) => monkeys[m1].Val + monkeys[m2].Val,
                    '-' => (m1, m2) => monkeys[m1].Val - monkeys[m2].Val,
                    '*' => (m1, m2) => monkeys[m1].Val * monkeys[m2].Val,
                    '/' => (m1, m2) => monkeys[m1].Val / monkeys[m2].Val,
                    _ => throw new InvalidDataException("Unknown operand")
                };
            }

            return output;
        }

        private string[] GetSubMonkeys(string line) => new[] { line[6..10], line[13..17] };


        public double RunTaskTwo(string[] lines)
        {

            return 0;
        }

        private class Monkey
        {
            public string Name { get; set; }
            public string[]? SubMonkeys { get; set; }
            private double? _val = null;
            public double Val 
            {
                get => _val ?? Operation(SubMonkeys[0], SubMonkeys[1]);
                set => _val = value;
            }

            public Func<string, string, double> Operation { get; set; }
        }
    }
}
