using AdventOfCode.Domain;

namespace AdventOfCode.TwentyOne;

public class DayFive : IChallenge<int>
{
    public string ChallengeInputPath { get; }
    public int RunTaskOne(string[] lines)
    {
        for (var i = 0; i < lines.Length; i++)
        {
            var coordPairs = lines[i].Split(" -> ").Select(c => c.Split(',')).ToArray();
            
            if (coordPairs[0][0] != coordPairs[1][0] && coordPairs[0][1] != coordPairs[1][1]) continue;
            
            
        }
    }

    public int RunTaskTwo(string[] lines)
    {
        throw new NotImplementedException();
    }
}