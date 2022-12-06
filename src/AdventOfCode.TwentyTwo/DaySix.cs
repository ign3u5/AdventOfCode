using System.Reflection;
using AdventOfCode.Domain;

namespace AdventOfCode.TwentyTwo;

public class DaySix : IChallenge<int>
{
    public string ChallengeInputPath => Path.GetFullPath("./ChallengeInputData/day_6.txt");
    public int Run(string[] lines)
    {
        return lines.Length;
    }
}