using AdventOfCode.Domain;

namespace AdventOfCode.TwentyTwo;

public class DaySix : IChallenge<int>
{
    public string ChallengeInputPath => Path.GetFullPath("./ChallengeInputData/day_6.txt");

    private string input; 
    public int RunTaskOne(string[] lines)
    {
        input = lines[0];
        
        return GetIndexOfDistinctCharacters(4);
    }

    public int RunTaskTwo(string[] lines)
    {
        input = lines[0];

        return GetIndexOfDistinctCharacters(14);
    }

    private int GetIndexOfDistinctCharacters(int numberOfDistinctChars)
    {
        var uniqueContiguousChars = new Dictionary<char, int>();
        var uniqueCharsCount = 0;
        
        for (var i = 0; i < input.Length; i++)
        {
            if (uniqueContiguousChars.TryGetValue(input[i], out var nonUniqueCharIndex))
            {
                uniqueContiguousChars =
                    input.Take((nonUniqueCharIndex + 1)..i).ToDictionary((c, j) => j, (nonUniqueCharIndex + 1));
                uniqueCharsCount = i - (nonUniqueCharIndex + 1);
            }
            
            uniqueContiguousChars.Add(input[i], i);
            if (++uniqueCharsCount == numberOfDistinctChars)
                return i + 1;
        }

        return 0;
    }
}

public static class DictionaryCreationExtensions
{
    public static Dictionary<T, U> ToDictionary<T, U>(this IEnumerable<T> collection,
        Func<T, int, U> method, int startingIndex = 0) => collection.ToDictionary(item => item, item => method(item, startingIndex++));
}