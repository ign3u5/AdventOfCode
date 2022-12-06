using AdventOfCode.Domain;

namespace AdventOfCode.TwentyTwo;

public class DayFive : IChallenge<string>
{
    public string ChallengeInputPath => DataProvider<DayFive>.GetChallengePath();

    private Stack<char>[] stacksOfCrates;

    public string RunTaskOne(string[] lines)
    {
        InitialiseStacksOfCrates(lines);

        for (var i = 10; i < lines.Length; i++)
        {
            Command commandOps = GetOps(lines[i]);
            for (var j = 0; j < commandOps.op; j++)
            {
                var poppedCrate = stacksOfCrates[commandOps.operand[0]].Pop();
                stacksOfCrates[commandOps.operand[1]].Push(poppedCrate);
            }
        }

        return stacksOfCrates.Aggregate("", (acc, cur) => $"{acc}{cur}");
    }

    public string RunTaskTwo(string[] lines)
    {
        InitialiseStacksOfCrates(lines);

        for (var i = 10; i < lines.Length; i++)
        {
            Command commandOps = GetOps(lines[i]);

            var poppedCrates = stacksOfCrates[commandOps.operand[0]].PopMultiple(commandOps.op);
            stacksOfCrates[commandOps.operand[1]].PushMultiple(poppedCrates);
        }

        return stacksOfCrates.Aggregate("", (acc, cur) => $"{acc}{cur}");
    }
    
    private Command GetOps(string command)
    {
        string[] rawOperands = command.Split(' ');
        int[] operands = { int.Parse(rawOperands[1]), int.Parse(rawOperands[3]) - 1, int.Parse(rawOperands[5]) - 1};

        return new Command(operands[0], new[] { operands[1], operands[2] });
    }
    
    private void InitialiseStacksOfCrates(string[] lines)
    {
        stacksOfCrates = new int [9].Select(_ => new Stack<char>()).ToArray();
        
        for (int i = 7; i >= 0; i--)
        {
            for (int j = 0; j < 9; j++)
            {
                var crate = lines[i][j * 4 + 1];
                if (!char.IsWhiteSpace(crate))
                    stacksOfCrates[j].Push(crate);
            }
        }
    }
    
    private record Command(int op, int[] operand);
}

public static class StackExtensions
{
    public static T[] PopMultiple<T>(this Stack<T> stack, int numberToPop)
    {
        var popped = new T[numberToPop];
        for (var i = numberToPop - 1; i >= 0; i--)
        {
            popped[i] = stack.Pop();
        }

        return popped;
    }

    public static void PushMultiple<T>(this Stack<T> stack, IEnumerable<T> elements)
    {
        foreach (var element in elements)
        {
            stack.Push(element);
        }
    }
}