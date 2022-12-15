using AdventOfCode.Domain;

namespace AdventOfCode.TwentyTwo;

public class DayFourteen : IChallenge<int>
{
    public string ChallengeInputPath => DataProvider<DayFourteen>.GetChallengePath();

    public int RunTaskOne(string[] lines)
    {
        var coords = lines.Select(l => l.Split(" -> ")
            .Select(rawCoords => rawCoords.Split(',').ToArray())
            .Select(a => (a[0], a[1])).ToArray())
            .ToArray();

        var notAtRest = true;


        return 0;
    }

    public int RunTaskTwo(string[] lines)
    {
        return 0;
    }
}