using AdventOfCode.Domain;

namespace AdventOfCode.TwentyTwo;

public class DaySix : IChallenge<int>
{
    public string ChallengeInputPath => Path.GetFullPath("./ChallengeInputData/day_6.txt");
    public int Run(string[] lines)
    {
        string line = lines[0];
        HashSet<char> differentContiguousChars = new();
        var numberOfDifferentChars = 0;
        
        for (int i = 0; i < line.Length; i++)
        {
            if (differentContiguousChars.TryGetValue(line[i], out _))
            {
                differentContiguousChars = new() { line[i] };
                numberOfDifferentChars = 1;
                continue;
            }

            differentContiguousChars.Add(line[i]);
            numberOfDifferentChars++;
            if (numberOfDifferentChars == 4)
                return i + 1;
        }

        return 0;
    }
}