namespace AdventOfCode.Domain;

public static class DataProvider<T>
{
    public static Func<string> GetChallengePath => () => Path.GetFullPath($"./ChallengeInputData/{typeof(T).Name}.txt");
}