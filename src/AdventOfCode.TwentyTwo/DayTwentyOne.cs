using AdventOfCode.Domain;
using System.Xml.Linq;

namespace AdventOfCode.TwentyTwo;
public class DayTwentyOne : IChallenge<double>
{
    public string ChallengeInputPath => DataProvider<DayTwentyOne>.GetChallengePath();

    public double RunTaskOne(string[] lines)
    {
        Dictionary<string, Monkey> monkeys = ParseMonkeys(lines);

        var rootMonkey = monkeys["root"];
        var output = rootMonkey.Val;

        return output;
    }

    public double RunTaskTwo(string[] lines)
    {
        Dictionary<string, Monkey> monkeys = ParseMonkeys(lines);

        var humn = monkeys["humn"];
        var output = humn.GetRequiredValToMeetRootEquality();

        return output;
    }

    private static Dictionary<string, Monkey> ParseMonkeys(string[] lines)
    {
        Dictionary<string, Monkey> monkeys = new();

        foreach (string line in lines)
        {
            var name = line[..4];
            Monkey monkey = monkeys.TryAdd(name, n => new Monkey(n));
            var isValueMonkey = double.TryParse(line[6..], out double val);

            if (isValueMonkey)
            {
                monkey.Val = val;
            }
            else
            {
                monkey.Op = line[11];

                foreach (string miniMonk in GetSubMonkeys(line))
                {
                    var subMonk = monkeys.TryAdd(miniMonk, n => new Monkey(n));

                    subMonk.ParentMonkey = monkey;
                    monkey.SubMonkeys.Add(subMonk);
                }
            }
        }

        return monkeys;

        static string[] GetSubMonkeys(string line) => new[] { line[6..10], line[13..17] };
    }

    private class Monkey
    {
        private double? _val = null;
        
        public string Name { get; set; }
        public IList<Monkey> SubMonkeys { get; set; } = new List<Monkey>();
        public Monkey? ParentMonkey { get; set; }
        public char Op { get; set; }

        public double Val
        {
            get => _val ?? Op switch
            {
                '+' => SubMonkeys[0].Val + SubMonkeys[1].Val,
                '-' => SubMonkeys[0].Val - SubMonkeys[1].Val,
                '*' => SubMonkeys[0].Val * SubMonkeys[1].Val,
                '/' => SubMonkeys[0].Val / SubMonkeys[1].Val,
                _ => throw new InvalidDataException("Unknown operand")
            };
            set => _val = value;
        }

        public double GetRequiredValToMeetRootEquality()
        {
            _val = null;
            return AltVal;
        }

        public double AltVal
        {
            get
            {
                if (_val is not null) return (double)_val;

                if (ParentMonkey is not null)
                {
                    var isFirstOperand = ParentMonkey.SubMonkeys[0].Name == Name;

                    if (ParentMonkey.IsRoot) return isFirstOperand ? ParentMonkey.SubMonkeys[1].Val : ParentMonkey.SubMonkeys[1].Val;

                    return ParentMonkey.Op switch
                    {
                        '+' => isFirstOperand ? ParentMonkey.AltVal - ParentMonkey.SubMonkeys[1].Val : ParentMonkey.AltVal - ParentMonkey.SubMonkeys[0].Val,
                        '-' => isFirstOperand ? ParentMonkey.AltVal + ParentMonkey.SubMonkeys[1].Val : ParentMonkey.SubMonkeys[0].Val - ParentMonkey.AltVal,
                        '*' => isFirstOperand ? ParentMonkey.AltVal / ParentMonkey.SubMonkeys[1].Val : ParentMonkey.AltVal / ParentMonkey.SubMonkeys[0].Val,
                        '/' => isFirstOperand ? ParentMonkey.AltVal * ParentMonkey.SubMonkeys[1].Val : ParentMonkey.SubMonkeys[0].Val / ParentMonkey.AltVal,
                        _ => throw new InvalidDataException("Unknown operator")
                    } ;
                }

                return 0;
            }
        }
        private bool IsRoot => _val == null && ParentMonkey == null;

        public Monkey(string name)
        {
            Name = name;
        }
    }
}
